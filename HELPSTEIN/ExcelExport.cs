using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HELPSTEIN
{
    class ExcelExport
    {
        //https://dejanstojanovic.net/aspnet/2018/february/export-dataset-and-datatable-to-excel-with-c/

        public string  filePath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Inventory.xls");
        DateTime dataTime = DateTime.Now;

        // Метод експорта в ексель из DataTable

        public void ExportToExcel(DataTable dataTable, bool overwiteFile = false)
        {

            try
            {
                if (File.Exists(filePath) && overwiteFile)
                {
                File.Delete(filePath);
                }
               var conn = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={filePath};Extended Properties='Excel 8.0;HDR=Yes;IMEX=0';";
              using (OleDbConnection connection = new OleDbConnection(conn))
              {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand())
                {
                    command.Connection = connection;
                    List<String> columnNames = new List<string>();
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        columnNames.Add(dataColumn.ColumnName);
                    }
                    String tableName = !String.IsNullOrWhiteSpace(dataTable.TableName) ? dataTable.TableName : Guid.NewGuid().ToString();
                    command.CommandText = $"CREATE TABLE [{tableName}] ({String.Join(",", columnNames.Select(c => $"[{c}] VARCHAR").ToArray())});";
                    command.ExecuteNonQuery();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        List<String> rowValues = new List<string>();
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            rowValues.Add((row[column] != null && row[column] != DBNull.Value) ? row[column].ToString() : String.Empty);
                        }
                        command.CommandText = $"INSERT INTO [{tableName}]({String.Join(",", columnNames.Select(c => $"[{c}]"))}) VALUES ({String.Join(",", rowValues.Select(r => $"'{r}'").ToArray())});";
                        command.ExecuteNonQuery();
                    }
                }

                connection.Close();
              }

            }
            catch (Exception ex)
            {
                try
                {
                    string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                    //Console.WriteLine(confFile);
                    using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine("ExelExport_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                        sw.WriteLine("ExelExport_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                    }

                }
                catch
                {

                }

            }

        }


        //ВАЖНО! Метод експорта в !!!ексельX64 из DataTable(Если программа собрана в  X64)
        public void ExportToExcelX64(DataTable dataTable, String filePath, bool overwiteFile = true)
        {
            if (File.Exists(filePath) && overwiteFile)
            {
                File.Delete(filePath);
            }
            var conn = $"Provider= Microsoft.ACE.OLEDB.16.0;Data Source={filePath};Extended Properties='Excel 8.0;HDR=Yes;IMEX=0';";
            using (OleDbConnection connection = new OleDbConnection(conn))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Скачайте и установите:" + "\n" + "https://www.microsoft.com/en-us/download/details.aspx?id=54920" + "\n" + "NOT Found: Microsoft Access Database Engine 2016" + "\n" + "\n" + "\n" + ex);

                }

                using (OleDbCommand command = new OleDbCommand())
                {
                    command.Connection = connection;
                    List<String> columnNames = new List<string>();
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        columnNames.Add(dataColumn.ColumnName);
                    }
                    String tableName = !String.IsNullOrWhiteSpace(dataTable.TableName) ? dataTable.TableName : Guid.NewGuid().ToString();
                    command.CommandText = $"CREATE TABLE [{tableName}] ({String.Join(",", columnNames.Select(c => $"[{c}] VARCHAR").ToArray())});";
                    command.ExecuteNonQuery();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        List<String> rowValues = new List<string>();
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            rowValues.Add((row[column] != null && row[column] != DBNull.Value) ? row[column].ToString() : String.Empty);
                        }
                        command.CommandText = $"INSERT INTO [{tableName}]({String.Join(",", columnNames.Select(c => $"[{c}]"))}) VALUES ({String.Join(",", rowValues.Select(r => $"'{r}'").ToArray())});";
                        command.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }
        }
        //// Метод експорта в ексель из dataSet он ищет  DataTable в dataSet и пременяет метод спорта в ексель из DataTable
        public void ExportToExcelDataSet(DataSet dataSet, bool overwiteFile = true)
        {
            if (File.Exists(filePath) && overwiteFile)
            {
                try
                {
                     File.Delete(filePath);
                }
                catch
                {
                    MessageBox.Show("Не могу создать файл Inventory.xls файл занят другим процесом или недостаточно прав !");
                }
                
            }

            foreach (DataTable dataTable in dataSet.Tables)
            {
                ExportToExcel(dataTable);
            }

        }


    }
}
