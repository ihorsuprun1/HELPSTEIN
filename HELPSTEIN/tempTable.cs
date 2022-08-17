using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HELPSTEIN
{
    class tempTable
    {
        DateTime dataTime = DateTime.Now;
        public string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
        //Console.WriteLine(confFile);
        public DataTable MakeTable(string NameTable, string DataTableQuery, string ColumnName1, string ColumnName2, string ColumnName3, string ColumnName4, string ColumnName5, string ColumnName6, string ColumnName7, string ColumnName8, string ColumnName9, string ColumnName10, string ColumnName11)
        {
            try
            {
                DataTable table = new DataTable(NameTable);
                DataColumn column;
                DataRow row;
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName1;
                column.AutoIncrement = false;
                column.Caption = ColumnName1;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName2;
                column.AutoIncrement = false;
                column.Caption = ColumnName2;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName3;
                column.AutoIncrement = false;
                column.Caption = ColumnName3;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName4;
                column.AutoIncrement = false;
                column.Caption = ColumnName4;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName5;
                column.AutoIncrement = false;
                column.Caption = ColumnName5;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName6;
                column.AutoIncrement = false;
                column.Caption = ColumnName6;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName7;
                column.AutoIncrement = false;
                column.Caption = ColumnName7;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName8;
                column.AutoIncrement = false;
                column.Caption = ColumnName8;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName9;
                column.AutoIncrement = false;
                column.Caption = ColumnName9;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName10;
                column.AutoIncrement = false;
                column.Caption = ColumnName10;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName11;
                column.AutoIncrement = false;
                column.Caption = ColumnName11;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);


                string message = DataTableQuery;
                //разделитель ; строки 
                string[] str = message.Split(';');

                for (int i = 0; i < str.GetLength(0) - 1; i++)
                {
                    //разделитель , между столбцами строки 
                    string[] st = str[i].Trim().Split(',');
                    row = table.NewRow();

                    row[ColumnName1] = st[0];
                    row[ColumnName2] = st[1];// - ошибка "Индекс находится вне границ массива"
                    row[ColumnName3] = st[2];
                    row[ColumnName4] = st[3];
                    row[ColumnName5] = st[4];
                    row[ColumnName6] = st[5];
                    row[ColumnName7] = st[6];
                    row[ColumnName8] = st[7];
                    row[ColumnName9] = st[8];
                    row[ColumnName10] = st[9];
                    row[ColumnName11] = st[10];
                
                table.Rows.Add(row);
                }
                table.AcceptChanges();

                return table;
            }
            catch (Exception ex)
            {
                try
                {
                    // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                    //Console.WriteLine(confFile);
                    using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine("TEMP_MakeTable_9_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                        sw.WriteLine("TEMP_MakeTable_9_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                    }

                }
                catch
                {

                }

            }



            return null;
        }
        public DataTable MakeTable(string NameTable, string DataTableQuery, string ColumnName1, string ColumnName2, string ColumnName3, string ColumnName4, string ColumnName5, string ColumnName6, string ColumnName7, string ColumnName8, string ColumnName9)
        {
            try
            {
                DataTable table = new DataTable(NameTable);
                DataColumn column;
                DataRow row;
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName1;
                column.AutoIncrement = false;
                column.Caption = ColumnName1;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName2;
                column.AutoIncrement = false;
                column.Caption = ColumnName2;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName3;
                column.AutoIncrement = false;
                column.Caption = ColumnName3;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName4;
                column.AutoIncrement = false;
                column.Caption = ColumnName4;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName5;
                column.AutoIncrement = false;
                column.Caption = ColumnName5;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName6;
                column.AutoIncrement = false;
                column.Caption = ColumnName6;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName7;
                column.AutoIncrement = false;
                column.Caption = ColumnName7;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName8;
                column.AutoIncrement = false;
                column.Caption = ColumnName8;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName9;
                column.AutoIncrement = false;
                column.Caption = ColumnName9;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);




                string message = DataTableQuery;
                //разделитель ; строки 
                string[] str = message.Split(';');

                for (int i = 0; i < str.GetLength(0) - 1; i++)
                {
                    //разделитель , между столбцами строки 
                    string[] st = str[i].Trim().Split(',');
                    row = table.NewRow();

                    row[ColumnName1] = st[0];
                    row[ColumnName2] = st[1];// - ошибка "Индекс находится вне границ массива"
                    row[ColumnName3] = st[2];
                    row[ColumnName4] = st[3];
                    row[ColumnName5] = st[4];
                    row[ColumnName6] = st[5];
                    row[ColumnName7] = st[6];
                    row[ColumnName8] = st[7];
                    row[ColumnName9] = st[8];
                    table.Rows.Add(row);
                }
                table.AcceptChanges();

                return table;
            }
            catch (Exception ex)
            {
                try
                {
                   // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                    //Console.WriteLine(confFile);
                    using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine("TEMP_MakeTable_9_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                        sw.WriteLine("TEMP_MakeTable_9_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                    }

                }
                catch
                {

                }

            }



            return null;
        }

        //Перезагрузка метода
        public DataTable MakeTable(string NameTable, string DataTableQuery, string ColumnName1, string ColumnName2, string ColumnName3, string ColumnName4, string ColumnName5, string ColumnName6, string ColumnName7, string ColumnName8)
        {
            try
            {
                DataTable table = new DataTable(NameTable);
                DataColumn column;
                DataRow row;
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName1;
                column.AutoIncrement = false;
                column.Caption = ColumnName1;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName2;
                column.AutoIncrement = false;
                column.Caption = ColumnName2;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName3;
                column.AutoIncrement = false;
                column.Caption = ColumnName3;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName4;
                column.AutoIncrement = false;
                column.Caption = ColumnName4;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName5;
                column.AutoIncrement = false;
                column.Caption = ColumnName5;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName6;
                column.AutoIncrement = false;
                column.Caption = ColumnName6;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName7;
                column.AutoIncrement = false;
                column.Caption = ColumnName7;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName8;
                column.AutoIncrement = false;
                column.Caption = ColumnName8;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);


                string message = DataTableQuery;
                //разделитель ; строки 
                string[] str = message.Split(';');

                for (int i = 0; i < str.GetLength(0) - 1; i++)

                {
                    //разделитель , между столбцами строки 
                    string[] st = str[i].Trim().Split(',');
                    row = table.NewRow();

                    row[ColumnName1] = st[0];
                    row[ColumnName2] = st[1];// - ошибка "Индекс находится вне границ массива"
                    row[ColumnName3] = st[2];
                    row[ColumnName4] = st[3];
                    row[ColumnName5] = st[4];
                    row[ColumnName6] = st[5];
                    row[ColumnName7] = st[6];
                    row[ColumnName8] = st[7];

                    table.Rows.Add(row);
                }
                table.AcceptChanges();


                return table;

            }
            catch (Exception ex)
            {
                try
                {
                   // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                    //Console.WriteLine(confFile);
                    using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine("TEMP_MakeTable _8_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                        sw.WriteLine("TEMP_MakeTable_8_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                    }

                }
                catch
                {

                }

            }



            return null;

        }




        //Перезагрузка метода
        public DataTable MakeTable(string NameTable, string DataTableQuery, string ColumnName1, string ColumnName2, string ColumnName3, string ColumnName4, string ColumnName5, string ColumnName6, string ColumnName7)
        {
            try
            {
                DataTable table = new DataTable(NameTable);
                DataColumn column;
                DataRow row;
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName1;
                column.AutoIncrement = false;
                column.Caption = ColumnName1;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName2;
                column.AutoIncrement = false;
                column.Caption = ColumnName2;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName3;
                column.AutoIncrement = false;
                column.Caption = ColumnName3;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName4;
                column.AutoIncrement = false;
                column.Caption = ColumnName4;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName5;
                column.AutoIncrement = false;
                column.Caption = ColumnName5;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName6;
                column.AutoIncrement = false;
                column.Caption = ColumnName6;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName7;
                column.AutoIncrement = false;
                column.Caption = ColumnName7;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);






                string message = DataTableQuery;
                //разделитель ; строки 
                string[] str = message.Split(';');

                for (int i = 0; i < str.GetLength(0) - 1; i++)
                {
                    //разделитель , между столбцами строки 
                    string[] st = str[i].Trim().Split(',');
                    row = table.NewRow();

                    row[ColumnName1] = st[0];
                    row[ColumnName2] = st[1];// - ошибка "Индекс находится вне границ массива"
                    row[ColumnName3] = st[2];
                    row[ColumnName4] = st[3];
                    row[ColumnName5] = st[4];
                    row[ColumnName6] = st[5];
                    row[ColumnName7] = st[6];

                    table.Rows.Add(row);
                }
                table.AcceptChanges();


                return table;

            }
            catch (Exception ex)
            {
                try
                {
                   // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                    //Console.WriteLine(confFile);
                    using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine("TEMP_MakeTable_7_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                        sw.WriteLine("TEMP_MakeTable_7_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                    }

                }
                catch
                {

                }

            }



            return null;


        }

        //Перезагрузка метода
        public DataTable MakeTable(string NameTable, string DataTableQuery, string ColumnName1, string ColumnName2, string ColumnName3, string ColumnName4, string ColumnName5, string ColumnName6)
        {
            try
            {
                DataTable table = new DataTable(NameTable);
                DataColumn column;
                DataRow row;
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName1;
                column.AutoIncrement = false;
                column.Caption = ColumnName1;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName2;
                column.AutoIncrement = false;
                column.Caption = ColumnName2;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName3;
                column.AutoIncrement = false;
                column.Caption = ColumnName3;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName4;
                column.AutoIncrement = false;
                column.Caption = ColumnName4;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName5;
                column.AutoIncrement = false;
                column.Caption = ColumnName5;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName6;
                column.AutoIncrement = false;
                column.Caption = ColumnName6;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);


                string message = DataTableQuery;
                 // разделитель; строки
                string[] str = message.Split(';');

                for (int i = 0; i < str.GetLength(0) - 1; i++)
                {
                    //  разделитель , между столбцами строки
                    string[] st = str[i].Trim().Split(',');
                    row = table.NewRow();

                    row[ColumnName1] = st[0];
                    row[ColumnName2] = st[1];// - ошибка "Индекс находится вне границ массива"
                    row[ColumnName3] = st[2];
                    row[ColumnName4] = st[3];
                    row[ColumnName5] = st[4];
                    row[ColumnName6] = st[5];


                    table.Rows.Add(row);
                }
                table.AcceptChanges();


                return table;

            }
            catch (Exception ex)
            {
                try
                {
                   // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                    //Console.WriteLine(confFile);
                    using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine("TEMP_MakeTable_6_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                        sw.WriteLine("TEMP_MakeTable_6_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                    }

                }
                catch
                {

                }

            }



            return null;



        }

        //Перезагрузка метода
        public DataTable MakeTable(string NameTable, string DataTableQuery, string ColumnName1, string ColumnName2, string ColumnName3, string ColumnName4, string ColumnName5)
        {
            try
            {
                DataTable table = new DataTable(NameTable);
                DataColumn column;
                DataRow row;
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName1;
                column.AutoIncrement = false;
                column.Caption = ColumnName1;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName2;
                column.AutoIncrement = false;
                column.Caption = ColumnName2;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName3;
                column.AutoIncrement = false;
                column.Caption = ColumnName3;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName4;
                column.AutoIncrement = false;
                column.Caption = ColumnName4;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName5;
                column.AutoIncrement = false;
                column.Caption = ColumnName5;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);



                string message = DataTableQuery;
                //разделитель ; строки 
                string[] str = message.Split(';');

                for (int i = 0; i < str.GetLength(0) - 1; i++)
                {
                    //разделитель , между столбцами строки 
                    string[] st = str[i].Trim().Split(',');
                    row = table.NewRow();

                    row[ColumnName1] = st[0];
                    row[ColumnName2] = st[1];// - ошибка "Индекс находится вне границ массива"
                    row[ColumnName3] = st[2];
                    row[ColumnName4] = st[3];
                    row[ColumnName5] = st[4];

                    table.Rows.Add(row);
                }
                table.AcceptChanges();


                return table;

            }
            catch (Exception ex)
            {
                try
                {
                    //string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                    //Console.WriteLine(confFile);
                    using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine("TEMP_MakeTable_5_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                        sw.WriteLine("TEMP_MakeTable_5_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                    }

                }
                catch
                {

                }

            }
            return null;
        }




        //Перезагрузка метода
        public DataTable MakeTable(string NameTable, string DataTableQuery, string ColumnName1, string ColumnName2, string ColumnName3, string ColumnName4)
        {
            try
            {
                DataTable table = new DataTable(NameTable);
                DataColumn column;
                DataRow row;
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName1;
                column.AutoIncrement = false;
                column.Caption = ColumnName1;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName2;
                column.AutoIncrement = false;
                column.Caption = ColumnName2;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName3;
                column.AutoIncrement = false;
                column.Caption = ColumnName3;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = ColumnName4;
                column.AutoIncrement = false;
                column.Caption = ColumnName4;
                column.ReadOnly = false;
                column.Unique = false;
                table.Columns.Add(column);



                string message = DataTableQuery;
                //разделитель ; строки 
                string[] str = message.Split(';');

                for (int i = 0; i < str.GetLength(0) - 1; i++)
                {
                    //разделитель , между столбцами строки 
                    string[] st = str[i].Trim().Split(',');
                    row = table.NewRow();

                    row[ColumnName1] = st[0];
                    row[ColumnName2] = st[1];// - ошибка "Индекс находится вне границ массива"
                    row[ColumnName3] = st[2];
                    row[ColumnName4] = st[3];

                    table.Rows.Add(row);
                }
                table.AcceptChanges();


                return table;

            }
            catch (Exception ex)
            {
                try
                {
                    //string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                    //Console.WriteLine(confFile);
                    using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine("TEMP_MakeTable_4_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                        sw.WriteLine("TEMP_MakeTable_4_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                    }

                }
                catch
                {

                }

            }
            return null;

        }

       


      
    }
}
