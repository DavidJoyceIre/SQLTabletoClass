using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SQLTabletoClass
{
    public partial class Form1 : Form
    {
        #region Declaration
            bool IsVBClass = false;
        #endregion

        public Form1()
        {
            InitializeComponent();
        }
        private void ApplySyntax()
        {
            //General
            string tokens = "(auto|double|int|struct|break|else|long|switch|case|enum|register|typedef|char|extern|return|union|const|float|short|unsigned|continue|for" +
                            "|signed|void|default|goto|sizeof|volatile|do|if|static|while|private|void|using|namespace|public|class|#region|#endregion|string|get|set|value" +
                            "|decimal)";
            Regex rex = new Regex(tokens);
            MatchCollection mc = rex.Matches(txtClass.Text);
            int StartCursorPosition = txtClass.SelectionStart;
            foreach (Match m in mc)
            {
                int startIndex = m.Index;
                int StopIndex = m.Length;
                txtClass.Select(startIndex, StopIndex);
                txtClass.SelectionColor = Color.Blue;
                txtClass.SelectionStart = StartCursorPosition;
                txtClass.SelectionColor = Color.Black;
            }
            tokens = "(DateTime)";
            rex = new Regex(tokens);
            mc = rex.Matches(txtClass.Text);
            StartCursorPosition = txtClass.SelectionStart;
            foreach (Match m in mc)
            {
                int startIndex = m.Index;
                int StopIndex = m.Length;
                txtClass.Select(startIndex, StopIndex);
                txtClass.SelectionColor = Color.FromArgb(43,145, 175);
                txtClass.SelectionStart = StartCursorPosition;
                txtClass.SelectionColor = Color.Black;
            }
            // Parameters
            tokens = "(/// <summary>|/// </summary>|/// <para>|</para>)";
            rex = new Regex(tokens);
            mc = rex.Matches(txtClass.Text);
            StartCursorPosition = txtClass.SelectionStart;
            foreach (Match m in mc)
            {
                int startIndex = m.Index;
                int StopIndex = m.Length;
                txtClass.Select(startIndex, StopIndex);
                txtClass.SelectionColor = Color.FromArgb(128, 128, 128);
                txtClass.SelectionStart = StartCursorPosition;
                txtClass.SelectionColor = Color.Black;
            }
        }
        private void btnGetTables_Click(object sender, EventArgs e)
        {
            try
            {
                MSSQL DatabaseConnection = new MSSQL(GetConnectionString());
                cboTables.DataSource = DatabaseConnection.GetDatabaseTable("SELECT TABLE_NAME FROM " + txtDatabase.Text + ".INFORMATION_SCHEMA.TABLES " +
                                                                            "WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME");
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConvertToClass_Click(object sender, EventArgs e)
        {
            IsVBClass = false;
            string privateVariables = "";
            string propertyVariables = "";

            MSSQL DatabaseConnection = new MSSQL(GetConnectionString());
            
            txtClass.Text = "using System;\n" +
                            "using System.Collections.Generic;\n" +
                            "using System.Text;\n" +
                            "using System.Diagnostics;\n\n";

            txtClass.Text += "namespace " + txtDatabase.Text + "\n{\n" +
                                "    public class " + cboTables.Text + "\n    {\n\n" +
                                "        public " + cboTables.Text + "()\n        {}\n\n";

            using (System.Data.DataTable VariableTable = DatabaseConnection.GetDatabaseTable(SelectColumns(cboTables.Text)))
            {
                if (VariableTable != null)
                {
                    for (int iI = 0; iI < VariableTable.Rows.Count; iI++)
                    {
                        string PropertyComment = "";

                        PropertyComment = "        /// <summary>\n";
                        if (VariableTable.Rows[iI]["Comment"].ToString() != "")
                        {
                            PropertyComment += "        /// " + VariableTable.Rows[iI]["Comment"] + "\n";                                             
                        }
                        PropertyComment += "        /// <para>SQL Column Type: " + VariableTable.Rows[iI]["sqlColumnType"] + "</para>\n";
                        PropertyComment += GetColumnIndex(cboTables.Text, VariableTable.Rows[iI]["ColumnName"].ToString());
                        PropertyComment += "        /// </summary>\n";

                        privateVariables += "           [DebuggerBrowsable(DebuggerBrowsableState.Never)]\n           private " + VariableTable.Rows[iI]["ColumnType"] + " _" + VariableTable.Rows[iI]["ColumnName"] + ";\n";
                        propertyVariables += PropertyComment + "        public " + VariableTable.Rows[iI]["ColumnType"] + " " + VariableTable.Rows[iI]["ColumnName"] + 
                                                    " { get { return _" + VariableTable.Rows[iI]["ColumnName"] + "; } set { _" 
                                                    + VariableTable.Rows[iI]["ColumnName"] + " = value; } }\n";
                    }
                }
            }
            txtClass.Text += "        #region Declaration\n" + privateVariables + 
                            "        #endregion\n\n" + propertyVariables;
            txtClass.Text += "\n" +  GenerateIndexComments(cboTables.Text, false) + "\n    }\n}\n\n";


            //Mark the class as been created by this program
            txtClass.Text += "//=======================================================\n";
            txtClass.Text += "//Class created using SQLTabletoClass\n";
            txtClass.Text += "//Convert your SQL Database into C# VB.NET Classes\n";
            txtClass.Text += "//for faster Development\n";
            txtClass.Text += "//www.cahersoftware.com\n";
            txtClass.Text += "//=======================================================\n";

            ApplySyntax();
        }

        private void btnConvertToVBClass_Click(object sender, EventArgs e)
        {
            IsVBClass = true;
            string privateVariables = "";
            string propertyVariables = "";

            MSSQL DatabaseConnection = new MSSQL(GetConnectionString());

            txtClass.Text = "Namespace " + txtDatabase.Text + "\n" +
                                "    Public Class " + cboTables.Text + "\n\n";

            using (System.Data.DataTable VariableTable = DatabaseConnection.GetDatabaseTable(SelectColumns(cboTables.Text)))
            {
                if (VariableTable != null)
                {
                    for (int iI = 0; iI < VariableTable.Rows.Count; iI++)
                    {
                        string PropertyComment = "";

                        PropertyComment = "        ''' <summary>\n";
                        if (VariableTable.Rows[iI]["Comment"].ToString() != "")
                        {
                            PropertyComment += "        ''' " + VariableTable.Rows[iI]["Comment"] + "\n";
                        }
                        PropertyComment += "        ''' <para>SQL Column Type: " + VariableTable.Rows[iI]["sqlColumnType"] + "</para>\n";
                        PropertyComment += "        ''' </summary>\n";

                        privateVariables += "           Private " + " _" + VariableTable.Rows[iI]["ColumnName"] + " As " + VariableTable.Rows[iI]["ColumnType"] + "\n";
                        propertyVariables += PropertyComment +
                            "        Public Property " + VariableTable.Rows[iI]["ColumnName"] + "() As " + VariableTable.Rows[iI]["ColumnType"] +
                                                    "\n             Get\n                   Return _" + VariableTable.Rows[iI]["ColumnName"] + "\n             End Get\n" +
                                                    "             Set(ByVal value As " + VariableTable.Rows[iI]["ColumnType"] + ")\n" + 
                                                    "                   _" + VariableTable.Rows[iI]["ColumnName"] +
                                                    " = value\n             End Set\n        End Property\n";
                    }
                }
            }
            txtClass.Text += "#Region \"Declaration\"\n" + privateVariables +
                            "#End Region\n\n" + propertyVariables;

            txtClass.Text += GenerateIndexComments(cboTables.Text, true);

            
            txtClass.Text += "\n    End Class\nEnd Namespace\n\n";
            //Mark the class as been created by this program
            txtClass.Text += "'=======================================================\n";
            txtClass.Text += "'Class created using SQLTabletoClass\n";            
            txtClass.Text += "'Convert your SQL Database into C# VB.NET Classes\n";
            txtClass.Text += "'for faster Development\n";
            txtClass.Text += "'www.cahersoftware.com\n";
            txtClass.Text += "'=======================================================\n";
            ApplySyntax();

        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            string SelectedPath = "";
            string fileExtension = ".cs";
            if (IsVBClass)
            {
                fileExtension = ".vb";
            }

            if (!txtClass.Text.Contains("www.cahersoftware.com"))
            {
                if (IsVBClass)
                {
                    txtClass.Text += "'=======================================================\n";
                    txtClass.Text += "'Class created using SQLTabletoClass\n";
                    txtClass.Text += "'Convert your SQL Database into C# VB.NET Classes\n";
                    txtClass.Text += "'for faster Development\n";
                    txtClass.Text += "'www.cahersoftware.com\n";
                    txtClass.Text += "'=======================================================\n";
                } else
                {
                    txtClass.Text += "//=======================================================\n";
                    txtClass.Text += "//Class created using SQLTabletoClass\n";
                    txtClass.Text += "//Convert your SQL Database into C# VB.NET Classes\n";
                    txtClass.Text += "//for faster Development\n";
                    txtClass.Text += "//www.cahersoftware.com\n";
                    txtClass.Text += "//=======================================================\n";
                }
            }
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SelectedPath = folderBrowserDialog1.SelectedPath;
                if (SelectedPath != "")
                {
                    System.IO.StreamWriter writetext = new System.IO.StreamWriter(SelectedPath + "\\" + cboTables.Text + fileExtension);
                    writetext.WriteLine(txtClass.Text);
                    writetext.Close();


                    MessageBox.Show("File has been created", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private string GetConnectionString()
        {
            string ConnectionString = "Server=" + txtServer.Text + ";Database=" + txtDatabase.Text + ";";
            
            if (txtUserID.Text != "")
            {
                ConnectionString += "User Id=" + txtUserID.Text + ";";
            }
            else
            {
                ConnectionString += "Integrated Security=" + cboIS.Text;
            }
            if (txtPassword.Text != "")
            {
                ConnectionString += "Password=" + txtPassword.Text + ";";
            }
            return ConnectionString;
        }

        private string SelectColumns(string tableName)
        {
            string selectString = "SELECT REPLACE(col.name, ' ', '_') ColumnName, column_id, CASE typ.name ";

            if (IsVBClass)
            {
                selectString +=  "WHEN 'bigint' then 'long'               " +
                "WHEN 'binary' then 'byte[]'                 " +
                "WHEN 'bit' then 'bool'                      " +
                "WHEN 'char' then 'string'                   " +
                "WHEN 'date' then 'DateTime'                 " +
                "WHEN 'datetime' then 'DateTime'             " +
                "WHEN 'datetime2' then 'DateTime'            " +
                "WHEN 'datetimeoffset' then 'DateTimeOffset'" +
                "WHEN 'decimal' then 'decimal'              " +
                "WHEN 'float' then 'float'                  " +
                "WHEN 'image' then 'byte[]'                 " +
                "WHEN 'int' then 'integer'                      " +
                "WHEN 'money' then 'decimal'                " +
                "WHEN 'nchar' then 'char'                   " +
                "WHEN 'ntext' then 'string'                 " +
                "WHEN 'numeric' then 'decimal'              " +
                "WHEN 'nvarchar' then 'string'              " +
                "WHEN 'real' then 'double'                  " +
                "WHEN 'smalldatetime' then 'DateTime'       " +
                "WHEN 'smallint' then 'short'               " +
                "WHEN 'smallmoney' then 'decimal'           " +
                "WHEN 'text' then 'string'                  " +
                "WHEN 'time' then 'TimeSpan'                " +
                "WHEN 'timestamp' then 'DateTime'           " +
                "WHEN 'tinyint' then 'byte'                 " +
                "WHEN 'uniqueidentifier' then 'Guid'        " +
                "WHEN 'varbinary' then 'byte[]'             " +
                "WHEN 'varchar' then 'string'               " +
                "ELSE 'UNKNOWN_' + typ.name                ";
            } else
            {
                selectString += "WHEN 'bigint' then 'long'               " +
                "WHEN 'binary' then 'byte[]'                 " +
                "WHEN 'bit' then 'bool'                      " +
                "WHEN 'char' then 'string'                   " +
                "WHEN 'date' then 'DateTime'                 " +
                "WHEN 'datetime' then 'DateTime'             " +
                "WHEN 'datetime2' then 'DateTime'            " +
                "WHEN 'datetimeoffset' then 'DateTimeOffset'" +
                "WHEN 'decimal' then 'decimal'              " +
                "WHEN 'float' then 'float'                  " +
                "WHEN 'image' then 'byte[]'                 " +
                "WHEN 'int' then 'int'                      " +
                "WHEN 'money' then 'decimal'                " +
                "WHEN 'nchar' then 'char'                   " +
                "WHEN 'ntext' then 'string'                 " +
                "WHEN 'numeric' then 'decimal'              " +
                "WHEN 'nvarchar' then 'string'              " +
                "WHEN 'real' then 'double'                  " +
                "WHEN 'smalldatetime' then 'DateTime'       " +
                "WHEN 'smallint' then 'short'               " +
                "WHEN 'smallmoney' then 'decimal'           " +
                "WHEN 'text' then 'string'                  " +
                "WHEN 'time' then 'TimeSpan'                " +
                "WHEN 'timestamp' then 'DateTime'           " +
                "WHEN 'tinyint' then 'byte'                 " +
                "WHEN 'uniqueidentifier' then 'Guid'        " +
                "WHEN 'varbinary' then 'byte[]'             " +
                "WHEN 'varchar' then 'string'               " +
                "ELSE 'UNKNOWN_' + typ.name                ";
            }
            selectString += "END ColumnType, object_definition(col.default_object_id) AS DefaultValue, sep.value AS Comment, CASE typ.name " +
                "WHEN 'int' THEN  UPPER(typ.name ) + '(' + CAST(col.[precision] AS VARCHAR(50)) + ')' " +
                "WHEN 'decimal' THEN  UPPER(typ.name ) + '(' + CAST(col.[precision] AS VARCHAR(50)) + ',' + CAST(col.scale AS VARCHAR(50)) + ')' " +
                "WHEN 'varchar' THEN  UPPER(typ.name ) + '(' + CAST(col.max_length AS VARCHAR(50)) + ')' " + 
                "WHEN 'nvarchar' THEN  UPPER(typ.name ) + '(' + CAST(col.max_length AS VARCHAR(50)) + ')' " +
                "WHEN 'nchar' THEN  UPPER(typ.name ) + '(' + CAST(col.max_length AS VARCHAR(50)) + ')' " +
                "WHEN 'char' THEN  UPPER(typ.name ) + '(' + CAST(col.max_length AS VARCHAR(50)) + ')' " +
                "ELSE UPPER(typ.name ) END AS sqlColumnType FROM sys.columns col " +
                "JOIN sys.types typ on col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id " +
                "left join sys.extended_properties sep on object_id = sep.major_id AND col.column_id = sep.minor_id and sep.name = 'MS_Description' " +
                "WHERE object_id = object_id('" + tableName + "')";

            return selectString;
        }

        private string GenerateIndexComments(string tableName, Boolean isVB)
        {
            string indexComment = "";
            MSSQL DatabaseConnection = new MSSQL(GetConnectionString());
            string sqlStatement = GetIndexSQL(tableName);
            using (System.Data.DataTable VariableTable = DatabaseConnection.GetDatabaseTable(sqlStatement))
            {
                if (VariableTable != null)
                {
                    if (!isVB) { indexComment += "        /***\n"; }
                    for (int iI = 0; iI < VariableTable.Rows.Count; iI++)
                    {
                        if (VariableTable.Rows[iI]["name"].ToString() != "")
                        {
                            if (!isVB)
                            {
                                indexComment += "         ===============================\n" +
                                                "         Index: " + VariableTable.Rows[iI]["name"].ToString() + ", Primary Key: " + VariableTable.Rows[iI]["is_primary_key"].ToString() + "\n" +
                                                "         ===============================\n" +
                                                "         " + VariableTable.Rows[iI]["Column1"].ToString() + "\n" +
                                                "         " + VariableTable.Rows[iI]["Column2"].ToString() + "\n" +
                                                "         " + VariableTable.Rows[iI]["Column3"].ToString() + "\n" +
                                                "         " + VariableTable.Rows[iI]["Column4"].ToString() + "\n" +
                                                "         " + VariableTable.Rows[iI]["Column5"].ToString() + "\n" +
                                                "         " + VariableTable.Rows[iI]["Column6"].ToString() + "\n";
                            }
                            else
                            {
                                indexComment += "        '===============================\n" +
                                                "        'Index: " + VariableTable.Rows[iI]["name"].ToString() + ", Primary Key: " + VariableTable.Rows[iI]["is_primary_key"].ToString() + "\n" +
                                                "        '===============================\n" +
                                                "        '" + VariableTable.Rows[iI]["Column1"].ToString() + "\n" +
                                                "        '" + VariableTable.Rows[iI]["Column2"].ToString() + "\n" +
                                                "        '" + VariableTable.Rows[iI]["Column3"].ToString() + "\n" +
                                                "        '" + VariableTable.Rows[iI]["Column4"].ToString() + "\n" +
                                                "        '" + VariableTable.Rows[iI]["Column5"].ToString() + "\n" +
                                                "        '" + VariableTable.Rows[iI]["Column6"].ToString() + "\n";
                            }
                        }
                    }
                    if (!isVB) { indexComment += "         ***/"; }
                }
            }
            if (indexComment.Length == 26)
            {
                indexComment = "";
            }
            return indexComment;
        }

        private string GetIndexSQL(string tableName)
        {
            return "SELECT si.name, is_primary_key, " +
	                    "INDEX_COL('" + tableName + "', index_id, 1) AS Column1, " +
	                    "INDEX_COL('" + tableName + "', index_id, 2) AS Column2, " +
	                    "INDEX_COL('" + tableName + "', index_id, 3) AS Column3, " +
	                    "INDEX_COL('" + tableName + "', index_id, 4) AS Column4, " +
	                    "INDEX_COL('" + tableName + "', index_id, 5) AS Column5, " +
	                    "INDEX_COL('" + tableName + "', index_id, 6) AS Column6 FROM sys.indexes AS si " +
                        "LEFT JOIN sys.objects as so on so.object_id=si.object_id " +
                        "WHERE OBJECT_NAME(si.object_id) = '" + tableName +"'";
        }

        public string GetColumnIndex(string tableName, string columnName)
        {
            string XMLDoc = "";
            MSSQL DatabaseConnection = new MSSQL(GetConnectionString());
            string SQL = "SELECT si.name, is_primary_key FROM sys.indexes AS si LEFT JOIN sys.objects as so on so.object_id=si.object_id WHERE OBJECT_NAME(si.object_id) = '" + tableName + "' " +
                            "AND (INDEX_COL(schema_name(schema_id)+'.'+OBJECT_NAME(si.object_id),index_id,1) = '" + columnName + "' OR " +
                            "INDEX_COL(schema_name(schema_id)+'.'+OBJECT_NAME(si.object_id),index_id,2) = '" + columnName + "' OR " +
                            "INDEX_COL(schema_name(schema_id)+'.'+OBJECT_NAME(si.object_id),index_id,3) = '" + columnName + "' OR " +
                            "INDEX_COL(schema_name(schema_id)+'.'+OBJECT_NAME(si.object_id),index_id,4) = '" + columnName + "' OR " +
                            "INDEX_COL(schema_name(schema_id)+'.'+OBJECT_NAME(si.object_id),index_id,5) = '" + columnName + "' OR " +
                            "INDEX_COL(schema_name(schema_id)+'.'+OBJECT_NAME(si.object_id),index_id,6) = '" + columnName + "')";

            using (System.Data.DataTable VariableTable = DatabaseConnection.GetDatabaseTable(SQL))
            {
                if (VariableTable != null)
                {
                    for (int iI = 0; iI < VariableTable.Rows.Count; iI++)
                    {
                        XMLDoc += "        /// <para>Index Name: " + VariableTable.Rows[iI]["name"].ToString();
                        if (VariableTable.Rows[iI]["is_primary_key"].ToString().ToLower() == "true")
                        {
                            XMLDoc += ", Primary Key: Yes";
                        }
                        else
                        {
                            XMLDoc += ", Primary Key: No";
                        }
                        XMLDoc += "</para>\n";
                    }
                }
            }

            return XMLDoc;
        }

        private void btnDatabaseToC_Click(object sender, EventArgs e)
        {
            IsVBClass = false;
            ExportDatabase();
        }

        private void btnDatabasetoVB_Click(object sender, EventArgs e)
        {
            IsVBClass = true;
            ExportDatabase();
        }

        private void ExportDatabase()
        {
            string SelectedPath = "";
            string fileExtension = ".cs";
            if (IsVBClass)
            {
                fileExtension = ".vb";
            }
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SelectedPath = folderBrowserDialog1.SelectedPath;
                if (SelectedPath != "")
                {
                    for (int iI = 0; iI <= cboTables.Items.Count - 1; iI++)
                    {
                        cboTables.SelectedIndex = iI;
                        if (!IsVBClass)
                        {
                            btnConvertToClass_Click(null, null);
                        }
                        else
                        {
                            btnConvertToVBClass_Click(null, null);
                        }
                        Application.DoEvents();
                        System.IO.StreamWriter writetext = new System.IO.StreamWriter(SelectedPath + "\\" + cboTables.Text + fileExtension);
                        writetext.WriteLine(txtClass.Text);
                        writetext.Close();
                    }



                    MessageBox.Show("Database " + txtDatabase.Text + " has been converted", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboIS.SelectedIndex = 0;
        }
    }
}
