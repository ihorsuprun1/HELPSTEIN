using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HELPSTEIN_Frida
{
    class userInfo
    {

       // ID ids = new ID();
       // public String IPquery = "";
        public String IPbody = "";
       // public String pcNameQuery = "";
        public String pcNameBody = "";
      //  public String userNameQuery = "";
        public String userNameBody = "";

        //private const string constBigDelim = ",";
        //private const string constSmallDelim = ";";
       // public const string conffile = "c:\\Helpstein\\conf.txt";


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
            //*************************************************
            //text += "***Инвентаризация: " + System.Environment.NewLine;


            // else { return argErrMessage; }

            return result;
        }


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
            return result;
        }



        //IPquery = @"INSERT INTO userInfo.rams VALUES ";
        //        RAMbody = "Информация об Оперативной памяте:";
        //        foreach (ManagementObject RAM in searcher_RAM.Get())
        //        {
        //            Наполняем переменную для отправки по почте
        //            RAMbody += "\n" + "BankLabel: " + RAM["BankLabel"] + "\n" + "Capacity: " + Math.Round(System.Convert.ToDouble(RAM["Capacity"]) / 1024 / 1024 / 1024, 2).ToString() + "Gb" + "\n" + "Speed:" + RAM["Speed"] + "\n";
        //            RAMbody += "\n";
        //            Наполняем переменную для отправки по почте
        //            RAMquery += "('" + RAM["BankLabel"] + "','" + Math.Round(System.Convert.ToDouble(RAM["Capacity"]) / 1024 / 1024 / 1024, 2).ToString() + "Gb','" + RAM["Manufacturer"] + "','" + RAM["MemoryType"] + "','" + RAM["PartNumber"] + "','" + RAM["SerialNumber"] + "','" + RAM["Speed"] + "','" + ids.ids + "')";
        //            RAMquery += ",";


    }
}
