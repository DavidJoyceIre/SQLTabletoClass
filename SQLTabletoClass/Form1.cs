using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
                            "using System.Text;\n\n";

            txtClass.Text += "namespace " + txtDatabase.Text + "\n{\n" +
                                "    public class " + cboTables.Text + "\n    {\n\n" +
                                "        public " + cboTables.Text + "()\n        {}\n\n";

            using (System.Data.DataTable VariableTable = DatabaseConnection.GetDatabaseTable(SelectColumns(cboTables.Text)))
            {
                if (VariableTable != null)
                {
                    for (int iI = 0; iI < VariableTable.Rows.Count; iI++)
                    {
                        privateVariables += "           private " + VariableTable.Rows[iI]["ColumnType"] + " _" + VariableTable.Rows[iI]["ColumnName"] + ";\n";
                        propertyVariables += "        public " + VariableTable.Rows[iI]["ColumnType"] + " " + VariableTable.Rows[iI]["ColumnName"] + 
                                                    " { get { return _" + VariableTable.Rows[iI]["ColumnName"] + "; } set { _" 
                                                    + VariableTable.Rows[iI]["ColumnName"] + " = value; } }\n";
                    }
                }
            }
            txtClass.Text += "        #region Declaration\n" + privateVariables + 
                            "        #endregion\n\n" + propertyVariables;
            txtClass.Text += "\n    }\n}";
        }

        private void btnConvertToVBClass_Click(object sender, EventArgs e)
        {
            IsVBClass = true;
            string privateVariables = "";
            string propertyVariables = "";

            MSSQL DatabaseConnection = new MSSQL(GetConnectionString());

            txtClass.Text += "Namespace " + txtDatabase.Text + "\n" +
                                "    Public Class " + cboTables.Text + "\n\n";

            using (System.Data.DataTable VariableTable = DatabaseConnection.GetDatabaseTable(SelectColumns(cboTables.Text)))
            {
                if (VariableTable != null)
                {
                    for (int iI = 0; iI < VariableTable.Rows.Count; iI++)
                    {
                        privateVariables += "           Private " + " _" + VariableTable.Rows[iI]["ColumnName"] + " As " + VariableTable.Rows[iI]["ColumnType"] + "\n";
                        propertyVariables += "        Public Property " + VariableTable.Rows[iI]["ColumnName"] + "() As " + VariableTable.Rows[iI]["ColumnType"] +
                                                    "\n             Get\n                   Return _" + VariableTable.Rows[iI]["ColumnName"] + "\n             End Get\n" +
                                                    "             Set(ByVal value As " + VariableTable.Rows[iI]["ColumnType"] + ")\n" + 
                                                    "                   _" + VariableTable.Rows[iI]["ColumnName"] +
                                                    " = value\n             End Set\n        End Property\n";
                    }
                }
            }
            txtClass.Text += "#Region \"Declaration\"\n" + privateVariables +
                            "#End Region\n\n" + propertyVariables;
            txtClass.Text += "\n    End Class\nEnd Namespace";

        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
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
                ConnectionString += "Integrated Security=SSPI";
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
            selectString += "END ColumnType, object_definition(col.default_object_id) AS DefaultValue FROM sys.columns col JOIN sys.types typ on col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id " +
                                        "WHERE object_id = object_id('" + tableName + "')";

            return selectString;
        }
    }
}
