using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HELPSTEIN
{
    class IniFile
    {
        public string path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "http.ini");
        // public string path1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Helpstein\";
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public IniFile()//string INIPath
        {

            try
            {
                //string confFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
                ////path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "http.ini");
                ////Console.WriteLine(imageFolder);
                ////string screenshotFolder = imageFolder + "config";
                ////string path1 = System.IO.Path.Combine(Environment.CurrentDirectory) + @"\config\";
                ////    //string path1 = System.IO.Path.Combine(Environment.CurrentDirectory) + @"\config\";
                ////    string path2 = "ini.ini";
                //if (!System.IO.File.Exists(confFile))
                //{
                //   File.Create(confFile);
                //}



                //    path = Path.Combine(path1, path2);
                if (!System.IO.File.Exists(path))
                {

                    using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                    {
                        sw.WriteLine("[ListHttpConfigXML]");
                        sw.WriteLine("URL=http://www.helpstein.com.ua/user.config");
                        //sw.WriteLine("URL=http://192.168.60.33/user.config");

                        //  sw.WriteLine("http=http://91.196.103.220:8888/user.config");

                    }
                }
                else
                {


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Возможно у вас не достаточно прав для записи в файл  конфигарации/n Запустите программу первый раз от имени адинимтратора чтобы создать конфигурацию /n" + ex.ToString());


            }

        }


        //}
        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                                            255, this.path);
            return temp.ToString();

        }

    }
}
