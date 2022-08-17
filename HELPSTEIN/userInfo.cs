using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HELPSTEIN
{
    class userInfo
    {

         ID ids = new ID();
        
        public String DataTableUserInfoQuery = "";
       

        public String pcNameBody = "";
        public String IPbody = "";
        public String userNameBody = "";

        DateTime dataTime = DateTime.Now;
        // public String pcNameQuery = "";
        // public String IPquery = "";
        // public String userNameQuery = "";

        
        public string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
        //Console.WriteLine(confFile);

        //private const string constBigDelim = ",";
        //private const string constSmallDelim = ";";
        // public const string conffile = "c:\\Helpstein\\conf.txt";

        public string userInfoAll(Boolean isNeeds)
        {
            if (isNeeds)
            {
                try
                {
                    string pcName = "";
                    pcName = Dns.GetHostName();
                    DataTableUserInfoQuery += ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "," + pcName.ToString().Replace(",", ".") + ",";

                    string userName = "";
                    userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    DataTableUserInfoQuery += userName.ToString().Replace(",", ".") + ",";


                    string IPresult = "";
                    int numberIPaddresses = 0;
                    string currentline = "";
                    string pattern = "([0-9]{1,3}\\.){3}[0-9]{1,3}";

                    String strHostName = Dns.GetHostName();
                    string tempIPtext = "";


                    IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                    IPAddress[] addr = ipEntry.AddressList;
                    //text += "***Данные о сети: " + System.Environment.NewLine;
                    for (int i = 0; i < addr.Length; i++)
                    {
                        currentline = addr[i].ToString();
                        Regex r = new Regex(pattern);
                        Match m = r.Match(currentline);
                        // tempIPtext = "0.0.0.0";
                        if (m.Success)
                        {
                            if (numberIPaddresses > 0)
                            {
                                tempIPtext += "   ";
                            }
                            numberIPaddresses++;
                            tempIPtext += currentline;
                            //result += tempIPtext;
                        }
                        //text +=  + System.Environment.NewLine;                   
                    }
                    IPresult += tempIPtext;

                    DataTableUserInfoQuery += IPresult + ";";

                    return DataTableUserInfoQuery;

                }
                catch (Exception ex)
                {
                    try
                    {
                        // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                        //Console.WriteLine(confFile);
                        using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine("UserINFO_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                            sw.WriteLine("UserINFO_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                        }

                    }
                    catch
                    {

                    }

                }


            }
            return null;
        }

        //public List<string> fillTroublesList(string argUNCPath = conffile)
        //{
        //    //строка с путём к файлу со списком проблем
        //    string TroublesListFileShare = argUNCPath;
        //    string path1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Helpstein\";
        //    Directory.CreateDirectory(path1);
        //    string TroublesListFile = Path.Combine(path1, "conf.txt");
        //    //string TroublesListFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "conf.txt");
        //    //если этот файл существует, заполняем комбобох списком из этого файла
        //    if (File.Exists(TroublesListFileShare))
        //    {
        //        //создали ЛИСТ(список) заполненный данными из файла
        //        List<string> listProblemTypes = new List<string>(File.ReadAllLines(TroublesListFileShare));
        //        //кобмобоксу дали в качестве источника данных - наш список

        //        return listProblemTypes;
        //    }
        //    else
        //    {
        //        List<string> listProblemTypes = new List<string>(File.ReadAllLines(TroublesListFile));
        //        //кобмобоксу дали в качестве источника данных - наш список
        //        return listProblemTypes;
        //    }

        //}

        //Считывание в файл информации о контактных данных и ФИО в файл
        //public List<string> loadUsernameInfo()
        //{
        //    string path1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Helpstein\";
        //    Directory.CreateDirectory(path1);
        //    string info_user_ListFile = Path.Combine(path1, "usr.txt");
        //    //строка с путём к файлу с контактной информацией
        //    // string info_user_ListFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "usr.txt");
        //    //если этот файл существует, заполняем комбобох списком из этого файла
        //    if (File.Exists(info_user_ListFile))
        //    {
        //        //создали ЛИСТ(список) заполненный данными из файла
        //        return new List<string>(File.ReadAllLines(info_user_ListFile));
        //    }
        //    else { return new List<string>() { "", "", "" }; }

        //}



        //Запись в файл информации о контактных данных и ФИО в файл
        //public void saveUsernameInfo(IEnumerable<string> m_oEnum)
        //{
        //    //string info_user_ListFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "usr.txt");
        //    string path1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Helpstein\";
        //    Directory.CreateDirectory(path1);
        //    string info_user_ListFile = Path.Combine(path1, "usr.txt");

        //    File.WriteAllLines(info_user_ListFile, m_oEnum);
        //}
        public string addInfo_getPCName()
        {
            string result = "";
            result = Dns.GetHostName();
            // pcNameQuery = @"INSERT INTO public.userinfo (pcname) VALUES ";
           // pcNameQuery += result;
           // pcNameQuery += "','";
            // pcNameQuery = pcNameQuery.TrimEnd(pcNameQuery[pcNameQuery.Length - 1]);
            //pcNameQuery += ";";
            pcNameBody = "Информация о Имени ПК:  ";
            pcNameBody += result + "\n";
            pcNameBody += "\n";
           // DataTableUserInfoQuery +=ids.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "," + result + ",";
            return result;

        }
        public string addInfo_getUserName()
        {
            string result = "";
            result = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            //userNameQuery = @"INSERT INTO public.userinfo (logonname) VALUES ";
           // userNameQuery += result;
          //  userNameQuery += "','";
            //userNameQuery = userNameQuery.TrimEnd(userNameQuery[userNameQuery.Length - 1]);
            //userNameQuery += ";";
            userNameBody = "Информация о Имени пользователя, который вошел в систему:  ";
            userNameBody += result + "\n"; ;
            userNameBody += "\n";
          //  DataTableUserInfoQuery += result + ",";
            return result;
        }



        public string addInfo_getIP()
        {
            //string argInternalDelim = "')";
            string result = "";
            int numberIPaddresses = 0;
            string currentline = "";
            string pattern = "([0-9]{1,3}\\.){3}[0-9]{1,3}";

            String strHostName = Dns.GetHostName();
            string tempIPtext = "";


            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            //text += "***Данные о сети: " + System.Environment.NewLine;
            for (int i = 0; i < addr.Length; i++)
            {
                currentline = addr[i].ToString();
                Regex r = new Regex(pattern);
                Match m = r.Match(currentline);
                // tempIPtext = "0.0.0.0";
                if (m.Success)
                {
                    if (numberIPaddresses > 0)
                    {
                        tempIPtext += "   ";
                    }
                    numberIPaddresses++;
                    tempIPtext += currentline;
                    //result += tempIPtext;
                }
                //text +=  + System.Environment.NewLine;                   
            }
            result += tempIPtext;
            // IPquery = @"INSERT INTO public.userInfo (ipaddress) VALUES ";
           // IPquery += result;
            //IPquery += "','";
            //IPquery = IPquery.TrimEnd(IPquery[IPquery.Length - 1]);
            //IPquery += ";";
            IPbody = "\n" + "Информация о IP-Адрес:   ";
            IPbody += result + "\n";
            IPbody += "\n";
            //DataTableUserInfoQuery += result + ";";


            // else { return argErrMessage; }

            return result;
        }



    }
}
