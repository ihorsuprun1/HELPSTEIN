using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HELPSTEIN
{
    public partial class Form1 : Form
    {
        //IniFile ini = new IniFile();
        Mail m = new Mail();
        Inventory i = new Inventory();
        TeamViewer teamViewer = new TeamViewer();
        userInfo user = new userInfo();
        Telegram telegram = new Telegram();
        Screenshot screen = new Screenshot();
        Bitrix Bitrix = new Bitrix();
        ID id = new ID();
        
        public string DataTableTiketInfo = "";

        public string trouble = "";
        public string fileNameSend = "";
        
        public bool getRamSettings;
        public bool getCpuSettings;
        public bool getDiskSettings;
        public bool getMonitorSettings;
        public bool getMotheboardSettings;
        public bool getOpSettings;
        public bool getProcessSettings;
        
        public bool getVideoSettings;
        public bool getSoftSettings;
        public bool getServicesSettigs;
        public bool getNetworksSettings;
        public bool getPrinterSettings;
        public bool getUserInfoSettings;
        public bool getTeamViewerSettings;


        public bool BitrixIsNeeds;
        public bool ExelinvtIsNeeds;
        public bool TelegramIsNeeds;
        public bool MailIsNeeds;

        public string telegramToken;
        public string telegramChatId;
        public bool BotTelegramSettings;

        public string GROUP_ID;
        public string ACCOMPLICES;
        public string ACCOMPLICES_1;
        public string ACCOMPLICES_2;
        public string ACCOMPLICES_3;
        public string RESPONSIBLE_ID;






        public Form1()
        {
            InitializeComponent();
            //
           

            //try
            //{
                //string confFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
                //Условие существует ли файл
                //if (File.Exists(confFile))
                //{
                //    если существует то удаляет его
                //    File.Delete(confFile);
                //}
                comboBoxTroubleTypes.SelectedIndex = -1;
                //настройки созданы в свойствах приложения >> Параметры
                textBoxCompanyName.Text = Properties.Settings.Default.textBoxCompanyName;
                comboBoxTroubleTypes.DataSource = Properties.Settings.Default.TroubleTypes;
                labelCompany.Text = Properties.Settings.Default.LabelCompany;
                labelName.Text = Properties.Settings.Default.labelName;
                labelType.Text = Properties.Settings.Default.labelType;
                labelEmail.Text = Properties.Settings.Default.labelEmail;
                labelPhone.Text = Properties.Settings.Default.labelPhone;
                groupBoxUrgency.Text = Properties.Settings.Default.groupBoxUrgency;
                radioButton1.Text = Properties.Settings.Default.radioButton1;
                radioButton2.Text = Properties.Settings.Default.radioButton2;
                radioButton3.Text = Properties.Settings.Default.radioButton3;
                textBoxName.Text = Properties.Settings.Default.textBoxName;
                textBoxPhone.Text = Properties.Settings.Default.textBoxPhone;
                textBoxEmail.Text = Properties.Settings.Default.textBoxEmail;

            textBoxInventoryPC.Text = Properties.Settings.Default.textBOXInvetoryPC;
            textBoxInventoryMonitor.Text = Properties.Settings.Default.textBOXInvetoryMONITOR;
            textBoxInventoryOther.Text = Properties.Settings.Default.textBOXInvetoryOther;

                telegramToken = Properties.Settings.Default.telegramTokenSettings;
                telegramChatId = Properties.Settings.Default.telegramChatIdSettings;
                BotTelegramSettings =Properties.Settings.Default.BotTelegramSettings;

                getCpuSettings = Properties.Settings.Default.getCpuSettings;
                getDiskSettings = Properties.Settings.Default.getDiskSettings;
                getMonitorSettings = Properties.Settings.Default.getMonitorSettings;
                getMotheboardSettings = Properties.Settings.Default.getMotheboardSettings;
                getOpSettings = Properties.Settings.Default.getOpSettings;
                getProcessSettings = Properties.Settings.Default.getProcessSettings;
                getRamSettings = Properties.Settings.Default.getRamSettings;
                getVideoSettings = Properties.Settings.Default.getVideoSettings;
                getSoftSettings = Properties.Settings.Default.getSoftSettings;
                getServicesSettigs = Properties.Settings.Default.getServicesSettigs;
                getNetworksSettings = Properties.Settings.Default.getNetworksSettings;
                getPrinterSettings = Properties.Settings.Default.getPrinterSettings;
                getUserInfoSettings = Properties.Settings.Default.getUserInfoSettings;
                getTeamViewerSettings = Properties.Settings.Default.getTeamViewerSettings;

                GROUP_ID = Properties.Settings.Default.BitrixGROUP_ID;
                ACCOMPLICES = Properties.Settings.Default.BitrixACCOMPLICES;
                ACCOMPLICES_1 = Properties.Settings.Default.BitrixACCOMPLICES_1;
                ACCOMPLICES_2 = Properties.Settings.Default.BitrixACCOMPLICES_2;
                ACCOMPLICES_3 = Properties.Settings.Default.BitrixACCOMPLICES_3;
                RESPONSIBLE_ID = Properties.Settings.Default.BitrixRESPONSIBLE_ID;

                BitrixIsNeeds = Properties.Settings.Default.BitrixToSends;
                MailIsNeeds = Properties.Settings.Default.MailToSends;
                TelegramIsNeeds = Properties.Settings.Default.TelegramToSends;
                ExelinvtIsNeeds = Properties.Settings.Default.ExelinventoryToSends;
            //Properties.Settings.Default.Save();
            try
            {
                IniFile ini = new IniFile();
                WebClient Client = new WebClient();
                string httpAddrees = ini.IniReadValue("ListHttpConfigXML", "URL");
                if (String.IsNullOrEmpty(httpAddrees))
                {
                    httpAddrees = "http://www.helpstein.com.ua/user.config";
                }
                //Строка пути где лежит конфиг приложения(аппДата\локал) 
                string confFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
                //Условие существует ли файл 
                if (File.Exists(confFile))
                {
                    //если существует то удаляет его
                    File.Delete(confFile);

                }

                try
                {
                    //скачивает конфиг к кладет его в указаную папку
                   // Client.DownloadFile(httpAddrees, confFile);
                }
                catch (Exception ex)
                {
                    progressBar1.Value = 0;
                    MessageBox.Show("Сервер конфигурации не доступен:" + httpAddrees + "\n или не верный URL " + "\n\n\n" + ex.ToString());
                }
                //перезаписывает конфиг
                Properties.Settings.Default.Reload();
            }


            catch
            {
            }

            //Строка пути где лежит конфиг приложения(аппДата\локал) 
            //string confFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            ////Условие существует ли файл 
            //if (File.Exists(confFile))
            //{
            //    //если существует то удаляет его
            //    File.Delete(confFile);
            //}
            //System.Diagnostics.Process.Start(Application.ExecutablePath);
            //this.Close();
            // }

            
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            textBoxInventoryPC.Visible = false;
            textBoxInventoryMonitor.Visible = false;
            textBoxInventoryOther.Visible = false;

            //робота с формой
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            showToolStripMenuItem.Click += showToolStripMenuItem_Click;
            screenshotToolStripMenuItem.Click += screenshotToolStripMenuItem_Click;
            toolStripONTOP.Click += ToolStripONTOP_Click;
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            notifyIcon1.MouseDoubleClick += showToolStripMenuItem_Click;
        }


        // запуск только одного экземпляра приложения см. NativeMethods.cs и Program.cs
        // http://sanity-free.org/143/csharp_dotnet_single_instance_application.html

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }
        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            // get our current "TopMost" value (ours will always be false though)
            bool top = TopMost;
            // make our form jump to the top of everything
            TopMost = true;
            // set it back to whatever it was
            TopMost = top;
            allowVisible = true;
            Show();


        }


        
        //метод checked radioButton возращает текс radioButtonа который выбран
        private string checkUrgency()
        {
            string result = "";
            //цыкл ищет контролы в groupBoxUrgency
            foreach (Control c in groupBoxUrgency.Controls)
            {
                //Условие если контрол == RadioButton
                if (c.GetType() == typeof(RadioButton))
                {
                    RadioButton rb = c as RadioButton;
                    // Условие cheked RadioButton
                    if (rb.Checked)
                    {
                        result = rb.Text;
                        Console.WriteLine(rb.Text);
                    }
                }
            }
            return result;
        }


        //метод обновления конфиг файла
        private void updateConfig()
         {
            IniFile ini = new IniFile();

            
                
                //Иницыализация веб клиента
                WebClient Client = new WebClient();
                string httpAddrees = ini.IniReadValue("ListHttpConfigXML", "URL");
                if (String.IsNullOrEmpty(httpAddrees))
                {
                    httpAddrees = "http://www.helpstein.com.ua/user.config";
                }
                //Строка пути где лежит конфиг приложения(аппДата\локал) 
                string confFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
              //Условие существует ли файл 
               if (File.Exists(confFile))
               {
                //если существует то удаляет его
                File.Delete(confFile);
               }

            try
            {
                //скачивает конфиг к кладет его в указаную папку
               // Client.DownloadFile(httpAddrees, confFile);
                //перезаписывает конфиг
                //Properties.Settings.Default.Reload();
                //перезагрузка формы
               //// System.Diagnostics.Process.Start(Application.ExecutablePath);
               // this.Close();
                progressBar1.Value = 0;
            }
            catch (Exception ex)
            {
                progressBar1.Value = 0;
                MessageBox.Show("Сервер конфигурации не доступен:" + httpAddrees + "\n или не верный URL " + "\n\n\n" + ex.ToString());
            }

        }


        public bool senderrors = false;
        //булевая переменная поверх всех окон
        public bool isTOP = false;
        //булевая переменная видимости формы
        private bool allowVisible = true;
        //булевая переменная закрытия формы
        private bool allowClose;

        //Метод! делаем приложение в режиме "поверх всех окон"
        public void switchTop()
        {
            TopMost = isTOP;
            isTOP = !isTOP;
            if (isTOP) { toolStripONTOP.Text = "Поверх всех окон [ ]"; } else { toolStripONTOP.Text = "Поверх всех окон [X]"; }
        }

        //Кнопка! контекстного меню "поверх всех окон"
        private void ToolStripONTOP_Click(object sender, EventArgs e)
        {
            //вызов метода
            switchTop();
        }

        // Кнопка! контекстного меню "показать"
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allowVisible = true;
            Show();
        }

        //// Кнопка! контекстного меню "Снимок екрана"
        private void screenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            screen.makeScreenshot();
        
        }

        //Видимость формы
        protected override void SetVisibleCore(bool value)
        {
            if (!allowVisible)
            {
                value = false;
                if (!this.IsHandleCreated) CreateHandle();
            }
            base.SetVisibleCore(value);
        }

        //Кнопка! контекстного меню "Закрыть"
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allowClose = true;
           
            try
            {
                //Сохранение параметров при закрытии формы
               Properties.Settings.Default.textBoxCompanyName = textBoxCompanyName.Text;
               Properties.Settings.Default.textBoxName = textBoxName.Text;
               Properties.Settings.Default.textBoxPhone = textBoxPhone.Text;
               Properties.Settings.Default.textBoxEmail = textBoxEmail.Text;
                Properties.Settings.Default.textBOXInvetoryPC = textBoxInventoryPC.Text;
                Properties.Settings.Default.textBOXInvetoryMONITOR = textBoxInventoryMonitor.Text;
                Properties.Settings.Default.textBOXInvetoryOther = textBoxInventoryOther.Text; 



                Properties.Settings.Default.Save();
                IniFile ini = new IniFile();
                WebClient Client = new WebClient();
                string httpAddrees = ini.IniReadValue("ListHttpConfigXML", "URL");
                if (String.IsNullOrEmpty(httpAddrees))
                {
                    httpAddrees = "http://www.helpstein.com.ua/user.config";
                }
                    //Строка пути где лежит конфиг приложения(аппДата\локал) 
                    string confFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
                //Условие существует ли файл 
                if (File.Exists(confFile))
                {
                    //если существует то удаляет его
                    File.Delete(confFile);
                }

                try
                {
                    //скачивает конфиг к кладет его в указаную папку
                  // Client.DownloadFile(httpAddrees, confFile);
                }
                catch (Exception ex)
                {
                    progressBar1.Value = 0;
                    MessageBox.Show("Сервер конфигурации не доступен:" + httpAddrees + "\n или не верный URL " + "\n\n\n" + ex.ToString());
                }
                //перезаписывает конфиг
                Properties.Settings.Default.Reload();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            System.Windows.Forms.Application.Exit();
        }

        //Действия при закрытии формы
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!allowClose)
            {
                this.Hide();
                
                e.Cancel = true;
                //updateConfig();
            }
            base.OnFormClosing(e);
        }


        //Действия при закрытии формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Сохранение параметров при закрытии формы
               Properties.Settings.Default.textBoxCompanyName = textBoxCompanyName.Text;
               Properties.Settings.Default.textBoxName = textBoxName.Text;
               Properties.Settings.Default.textBoxPhone = textBoxPhone.Text;
               Properties.Settings.Default.textBoxEmail = textBoxEmail.Text;
                Properties.Settings.Default.textBOXInvetoryPC = textBoxInventoryPC.Text;
                Properties.Settings.Default.textBOXInvetoryMONITOR = textBoxInventoryMonitor.Text;
                Properties.Settings.Default.textBOXInvetoryOther = textBoxInventoryOther.Text;
                // Сохранение параметров при закрытии формы
                Properties.Settings.Default.Save();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
           
        }

        //Проверка сети
        private bool checkNetwork(string ipServer)
        {
            bool isServerLife = false;
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = ping.Send(ipServer, 5000);

                if (pingReply.Status == IPStatus.Success)
                {
                    isServerLife = true;
                }
            }
            catch (Exception e)
            {
                Console.Write("PingToServer: Cannot ping to server, " + e.Message);
            }
            return isServerLife;
        }


        public string  DataSetExport(Boolean isNeeds = true)
        {
            string ExelFilePath = "";
            if (isNeeds)
            {
                DateTime dataTime = DateTime.Now;
                trouble = comboBoxTroubleTypes.SelectedItem.ToString();

                string InventoryNumberPC = textBoxInventoryPC.Text;//.Replace(",", ".").Replace(";", ".") + textBoxInventoryMonitor.Text.Replace(",", ".").Replace(";", ".") + textBoxInventoryOther.Text.Replace(",", ".").Replace(";", ".")
                string InventoryNumberMonitor = textBoxInventoryMonitor.Text;
                string InventoryNumberOther =  textBoxInventoryOther.Text;
                if (InventoryNumberPC == null) { InventoryNumberPC = ""; };
                if (InventoryNumberMonitor == null) { InventoryNumberMonitor = ""; };
                if (InventoryNumberOther == null) { InventoryNumberOther = ""; };

                DataTableTiketInfo = id.ids + "," + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + "," + textBoxCompanyName.Text.Replace(",", ".").Replace(";", ".") + "," + textBoxName.Text.Replace(",", ".").Replace(";", ".") + "," + textBoxPhone.Text.Replace(",", ".").Replace(";", ".") + "," + textBoxEmail.Text.Replace(",", ".").Replace(";", ".") + "," + trouble + "," + textBox1.Text.Replace(",", ".").Replace(";", ".") + "," + InventoryNumberPC.Replace(",", ".").Replace(";", ".") + "," + InventoryNumberMonitor.Replace(",", ".").Replace(";", ".") + "," + InventoryNumberOther.Replace(",", ".").Replace(";", ".") + ";";
                tempTable tempTable = new tempTable();

                teamViewer.get_TeamViewer_id_pass();
                user.userInfoAll(getUserInfoSettings);

                //Вызов методов класов
                i.getRam(getRamSettings);
                progressBar1.Value = 33;
                i.getCPU(getCpuSettings);
                progressBar1.Value = 36;
                i.getDISK(getDiskSettings);
                progressBar1.Value = 39;
                i.getMOTHERBOARD(getMotheboardSettings);
                progressBar1.Value = 41;
                i.getOP(getOpSettings);
                progressBar1.Value = 44;
                i.getVIDEO(getVideoSettings);
                progressBar1.Value = 47;
                i.getNETWORKS(getNetworksSettings);
                progressBar1.Value = 50;
                i.getMONITOR(getMonitorSettings);
                
                
                progressBar1.Value = 53;

                i.getPRINTERS(getPrinterSettings);
                progressBar1.Value = 57;
                i.getSOFT(getSoftSettings);
                progressBar1.Value = 60;
                //i.getSERVICES(getServicesSettigs);
                //progressBar1.Value = 50;

                //i.getPROCESS(getProcessSettings);
                //progressBar1.Value = 69;
                if (DataTableTiketInfo == null) { DataTableTiketInfo = ""; };
                if (user.DataTableUserInfoQuery == null) { user.DataTableUserInfoQuery = ""; };
                if (teamViewer.DataTableTeamViewerQuery == null) { teamViewer.DataTableTeamViewerQuery = ""; };
                if (i.RAMquerDataTable == null) { i.RAMquerDataTable = ""; };
                if (i.CPUquerDataTable == null) { i.CPUquerDataTable = ""; };
                if (i.DISKqueryDataTable == null) { i.DISKqueryDataTable = ""; };
                if (i.OPqueryDataTable == null) { i.OPqueryDataTable = ""; };
                if (i.VIDEOqueryDataTable == null) { i.VIDEOqueryDataTable = ""; };
                if (i.MOTHERBOARDqueryDataTable == null) { i.MOTHERBOARDqueryDataTable = ""; };
                if (i.NETWORKSqueryDataTable == null) { i.NETWORKSqueryDataTable = ""; };
                if (i.MONITORqueryDataTable == null) { i.MONITORqueryDataTable = ""; };
                if (i.PRINTERSqueryDataTable == null) { i.PRINTERSqueryDataTable = ""; };
                if (i.SOFTqueryDataTable == null) { i.SOFTqueryDataTable = ""; };


                //Cоздание временных таблиц 
                    DataTable dataTiketInfo = tempTable.MakeTable("TiketInfo", DataTableTiketInfo, "id", "date", "companyName", "userName", "userPhone", "userEmail", "Trouble", "tiketText","InvetoryNumberPC", "InvetoryNumberMONITOR", "InvetoryNumberOTHER");
                DataTable dataUserInfo = tempTable.MakeTable("userInfo", user.DataTableUserInfoQuery, "id", "date", "pcName", "userName", "IP");
                DataTable dataTeamViewer = tempTable.MakeTable("teamViewer", teamViewer.DataTableTeamViewerQuery, "id", "date", "id_TeamViewer", "password", "fullTextTeamViewer");

                DataTable dataRam = tempTable.MakeTable("rams", i.RAMquerDataTable, "id", "date", "bank_label", "capacity", "manufacturer", "memory_type", "part_number", "serial_number", "speed");
                DataTable dataCpu = tempTable.MakeTable("cpus", i.CPUquerDataTable, "id", "date", "device_id", "name", "number_of_cores", "numberof_logical_processors", "processor_id", "serial_number", "max_clock_speed");
                DataTable dataDisk = tempTable.MakeTable("disks", i.DISKqueryDataTable, "id", "date", "device_id", "interface_type", "model", "size", "serial_number");
                DataTable dataOp = tempTable.MakeTable("op", i.OPqueryDataTable, "id", "date", "caption", "build_number", "version", "serial_number");
                DataTable dataVideo = tempTable.MakeTable("video", i.VIDEOqueryDataTable, "id", "date", "dapter_ram", "caption", "description", "video_processor");
                DataTable dataMotherboard = tempTable.MakeTable("motherboard", i.MOTHERBOARDqueryDataTable, "id", "date", "manufacturer", "product", "serial_number", "version");
                DataTable dataNetworks = tempTable.MakeTable("networks", i.NETWORKSqueryDataTable, "id", "date", "caption", "description", "mac_address", "ip_address");
                DataTable dataMonitor = tempTable.MakeTable("monitor", i.MONITORqueryDataTable, "id", "date", "manufacturer_name", "product_code_id", "serial_number_id", "user_friendly_name");
                DataTable dataPrinter = tempTable.MakeTable("printer", i.PRINTERSqueryDataTable, "id", "date", "name", "network", "port_name");
                DataTable dataSofts = tempTable.MakeTable("softs", i.SOFTqueryDataTable, "id", "date", "caption", "install_date");
                //DataTable dataProcess = tempTable.MakeTable("process", i.PROCESSqueryDataTable, "id", "date", "name", "command_line");
                //DataTable dataServicese = tempTable.MakeTable("servicese", i.SERVICESqueryDataTable, "id", "date", "caption", "description", "display_name", "name", "path_name", "started");


                DataSet ds = new DataSet("inventory");
                ds.Tables.Add(dataTiketInfo);
                ds.Tables.Add(dataUserInfo);
                ds.Tables.Add(dataTeamViewer);

                ds.Tables.Add(dataRam);
                ds.Tables.Add(dataCpu);
                ds.Tables.Add(dataDisk);
                ds.Tables.Add(dataOp);
                ds.Tables.Add(dataVideo);
                ds.Tables.Add(dataMotherboard);
                ds.Tables.Add(dataNetworks);
                ds.Tables.Add(dataMonitor);
                ds.Tables.Add(dataPrinter);
                ds.Tables.Add(dataSofts);
                //ds.Tables.Add(dataProcess);
                //ds.Tables.Add(dataServicese);


                ExcelExport excel = new ExcelExport();
                excel.ExportToExcelDataSet(ds);
                //освобождаем переменные чтобы не дублировалась информация 


                user.DataTableUserInfoQuery = null;
                teamViewer.DataTableTeamViewerQuery = null;
                i.RAMquerDataTable = null;
                i.CPUquerDataTable = null;
                i.DISKqueryDataTable = null;
                i.OPqueryDataTable = null;
                i.VIDEOqueryDataTable = null;
                i.MOTHERBOARDqueryDataTable = null;
                i.NETWORKSqueryDataTable = null;
                i.MONITORqueryDataTable = null;
                i.PRINTERSqueryDataTable = null;
                i.SOFTqueryDataTable = null;
                //i.PROCESSqueryDataTable = null;
                //i.SERVICESqueryDataTable = null;
                ExelFilePath = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Inventory.xls");
            }
            return ExelFilePath;
        }









        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(ExelinvtIsNeeds.ToString() + BitrixIsNeeds.ToString()+TelegramIsNeeds.ToString() + MailIsNeeds.ToString());
            DateTime dataTime = DateTime.Now;
            //Переменная Тимвювера
            string tv = "";
            string tvFullText = "";
            string sendFileScreenShot = "";
            string sendFileExel =""; 
            string Subject = "";
            string info = "";
            string textTicked = textBox1.Text + System.Environment.NewLine;
            //string numberTiked = dataTime.ToString("yyyyMMddHHmmss");
            string strdate = dataTime.ToString("yyyy.MM.dd-HH:mm:ss");
            try
            {
                trouble = comboBoxTroubleTypes.SelectedItem.ToString();
            }
            catch
            {
                trouble = "";
            }
          

            string formFields = labelCompany.Text + "  " + textBoxCompanyName.Text + "\n" + labelName.Text + "  " + textBoxName.Text + "\n" + labelPhone.Text + "  " + textBoxPhone.Text + "\n" + labelEmail.Text + "  " + textBoxEmail.Text + "\n";
            //Условия Проверкы сети
            if (checkNetwork("8.8.8.8"))
            {
                // if  (comboBoxTroubleTypes.SelectedItem.ToString() == "" || comboBoxTroubleTypes.SelectedItem.ToString() == null)
                if (trouble == "" || trouble == null)
                {
                    MessageBox.Show("Выберите тип обращения!!!\nИначе вы не сможете отправить запрос" + "\n" + "Нажмите ОК ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressBar1.Value = 0;
                    senderrors = false;
                }
                else if (String.IsNullOrEmpty(textBoxCompanyName.Text))
                {
                    MessageBox.Show("Ведите название компании!!!\nИначе вы не сможете отправить запрос" + "\n" + "Нажмите ОК ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressBar1.Value = 0;
                    senderrors = false;
                }
                else if (String.IsNullOrEmpty(textBoxName.Text))
                {
                    MessageBox.Show("Ведите свое Ф.И.О!!!\nИначе вы не сможете отправить запрос" + "\n" + "Нажмите ОК ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressBar1.Value = 0;
                    senderrors = false;
                }
                else if (String.IsNullOrEmpty(textBoxPhone.Text))
                {
                    MessageBox.Show("Ведите свой контактный номер !!!\nИначе вы не сможете отправить запрос" + "\n" + "Нажмите ОК ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressBar1.Value = 0;
                    senderrors = false;
                }
                else if (String.IsNullOrEmpty(textBoxEmail.Text))
                {
                    MessageBox.Show("Ведите свой контактный email !!!\nИначе вы не сможете отправить запрос" + "\n" + "Нажмите ОК ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressBar1.Value = 0;
                    senderrors = false;
                }

                else
                {
                   

                    if (checkBox_TVinfo.Checked)
                    {
                        progressBar1.Value = 10;
                        TeamViewer teamViewer = new TeamViewer();
                        tv += teamViewer.get_TeamViewer_id_pass();
                        tvFullText += teamViewer.tvFullText;
                            progressBar1.Value = 20;
                        //Условия если переменная  Тимвювера начинается с текста "Не найден процесс TV!"
                        if (tv.StartsWith("Не найден процесс TV!"))
                        {
                            MessageBox.Show("Не найден процесс TeamViewer! Запустите TeamViewer и  повторите Ваш запрос" + "\n" + "В протинном случаае Адинимтратор не сможет к Вам удаленно подключится!" + "\n" + "Нажмите ОК чтобы продолжыть отправку запроса", "TeamViewer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                           
                        }

                    }

                   
                    //Условия если чекбокс Cheked(То мы наполняем переменную sendFile класа mail 
                    if (checkBox_makeScreenshot.Checked)
                    {
                        progressBar1.Value = 30;
                        sendFileScreenShot = screen.makeScreenshot();

                    }

                    sendFileExel = DataSetExport(ExelinvtIsNeeds);
                    progressBar1.Value = 60;
                    user.addInfo_getPCName();
                    progressBar1.Value = 63;
                    user.addInfo_getUserName();
                    progressBar1.Value =67;
                    user.addInfo_getIP();
                    progressBar1.Value = 70;
                    

                    Subject = strdate + "  " + labelCompany.Text + "  " + textBoxCompanyName.Text + " Тип Обращения:  " + trouble; 
                    info =  formFields + "Приоритет: " + checkUrgency() + "\n" + "Тип Обращения:  " + trouble + "\n" + "Текст Обращения:\n" + textTicked + "Даные о TeamViewer: " + tv + "\n" + user.pcNameBody + "\n" + user.userNameBody + "\n" + user.IPbody;

                   

                    //Bitrix.sendToBitrix(BitrixIsNeeds ,Subject, info, GROUP_ID, ACCOMPLICES, RESPONSIBLE_ID, sendFileScreenShot, sendFileExel, ACCOMPLICES_1, ACCOMPLICES_2, ACCOMPLICES_3);
                    Console.WriteLine("Bitrix.ok");
                    progressBar1.Value = 90;
                    m.Send(true, Subject, info, sendFileScreenShot, sendFileExel);
                    Console.WriteLine("mail.ok");






                    progressBar1.Value = 100;




                    if (!senderrors)
                    {
                        MessageBox.Show("Сообщение специалисту отправлено", "Прекрасно!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        comboBoxTroubleTypes.SelectedIndex = -1;
                        //comboBoxTroubleTypes.Items.Add("");
                        string telegramSendMessange = labelCompany.Text + "  " + textBoxCompanyName.Text + "\nТип Обращения:  " + trouble + "\nТекст Обращения:  " + textBox1.Text + "\nФ.И.О: " + textBoxName.Text + "\nКонтакты:" + textBoxPhone.Text +  user.IPbody + user.pcNameBody + user.userNameBody;
                       // telegram.telegramAsync(telegramToken, telegramChatId, telegramSendMessange,TelegramIsNeeds);
                        textBox1.Clear();
                    }
                   
                    Properties.Settings.Default.Save();
                    updateConfig();
                    //m.sendFileExel = null;
                }




            }
            //сеть не доступна
            else { MessageBox.Show("Похоже, у Вас нет интернета." + "\n" + "Я не смогу отправить Ваш запрос специалисту :(" + "\n" + "Попробуйте альтернативный метод связи с ним (телефон, например)", "ОЙйой !!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }


            //tv = null;
            //tvFullText = null;
            //sendFileScreenShot = null;
            //sendFileExel = null;
            //Subject = null;
            //info = null;
            //strdate = null;


        }




        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://download.teamviewer.com/download/TeamViewerQS.exe");
            Process.Start(sInfo);
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            
        }

        //Метод прикрепить файл из диалогового окна с ограничениям по размеру
       public void attachedFile()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            return;
           
            FileInfo file = new FileInfo(openFileDialog1.FileName);
            long size = file.Length; //file.Length.ToString();
            long maxSize = 10485760;
            Console.WriteLine(size);

            if (size < maxSize)
            {
                fileNameSend += openFileDialog1.FileName;
                Console.WriteLine($"Число {size} больше числа {maxSize}");
            }
            else
            {
                MessageBox.Show("Прикрепленный файл не должен привышать 10 мегабайт!!");
              
            }

        }

        private void comboBoxTroubleTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var myLines = new List<string>();

                myLines.Add("Номер кадриджа");
                myLines.Add("brwn");
                myLines.Add("brn");
                myLines.Add("brow");
                myLines.Add("br");
                myLines.Add("brw");



                if (comboBoxTroubleTypes.SelectedItem.ToString().Contains("Инвентаризация"))
                {
                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    textBoxInventoryPC.Visible = true;
                    textBoxInventoryMonitor.Visible = true;
                    textBoxInventoryOther.Visible = true;
                }
                //else if (comboBoxTroubleTypes.SelectedItem.ToString().Contains("Замена катриджа"))
                //{
                //    textBox1.AppendText("Название принтера = " + Environment.NewLine);
                //    textBox1.AppendText(Environment.NewLine);
                //    textBox1.AppendText("Название катриджа =" + Environment.NewLine);
                //    textBox1.AppendText(Environment.NewLine);
                //    textBox1.Lines = myLines.ToArray();
                //    label1.Visible = false;
                //    label2.Visible = false;
                //    label3.Visible = false;
                //    textBoxInventoryPC.Visible = false;
                //    textBoxInventoryMonitor.Visible = false;
                //    textBoxInventoryOther.Visible = false;
                //}
                //else if (!comboBoxTroubleTypes.SelectedItem.ToString().Contains("Замена катриджа"))
                //{
                //    textBox1.Clear();
                //    label1.Visible = false;
                //    label2.Visible = false;
                //    label3.Visible = false;
                //    textBoxInventoryPC.Visible = false;
                //    textBoxInventoryMonitor.Visible = false;
                //    textBoxInventoryOther.Visible = false;
                //}
                else if (!comboBoxTroubleTypes.SelectedItem.ToString().Contains("Инвентаризация"))
                {

                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    textBoxInventoryPC.Visible = false;
                    textBoxInventoryMonitor.Visible = false;
                    textBoxInventoryOther.Visible = false;
                }
            }
            catch { }

        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    string Path = Environment.CurrentDirectory;
        //    MessageBox.Show(Path);
        //    IniFile ini = new IniFile();
        //   MessageBox.Show(ini.path);
        //    MessageBox.Show(ini.IniReadValue("ListHttpConfigXML", "URL"));
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    string Path = Environment.CurrentDirectory;
        //    MessageBox.Show(Path);
        //    IniFile ini = new IniFile();

        //    MessageBox.Show(ini.IniReadValue("ListHttpConfigXML", "http"));
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    updateConfig();
        //    //    informations inf = new informations
        //    //    {
        //    //        userdata = labelCompany.Text + "  " + textBoxCompanyName.Text + "\n" + labelName.Text + "  " + textBoxName.Text + "\n" + labelPhone.Text + "  " + textBoxPhone.Text + "\n" + labelEmail.Text + "  " + textBoxEmail.Text + "\n" + "Приоритет: " + checkUrgency() + "\n" + "Тип Обращения:  " + trouble + "\n" + "Текст Обращения:\n" + textBox1.Text + System.Environment.NewLine + "Даные о TeamViewer: " + tv + "\n" + user.pcNameBody + "\n" + user.userNameBody + "\n" + user.IPbody, 

        //    //};
        //}



        //class informations
        //{

        //    public string TeamViewer { get; set; }
        //    public string userdata { get; set; }
        //    public string screnshot { get; set; }
        //    //public string TeamViewer { get; set; }
        //}
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    DateTime dataTime = DateTime.Now;
        //    //DataSetExport();
        //    Bitrix bx_logon = new Bitrix();

        //    DateTime xDay = dataTime.AddDays(1);
        //    string TITLE = "Заявка  " + labelCompany.Text + "  " + textBoxCompanyName.Text;
        //    string DESCRIPTION = labelCompany.Text + "  " + textBoxCompanyName.Text + "\n" + labelName.Text + "  " + textBoxName.Text + "\n" + labelPhone.Text + "  " + textBoxPhone.Text + "\n" + labelEmail.Text + "  " + textBoxEmail.Text + "\n";
        //    string addTask = "arNewTaskData[TITLE] = " + TITLE + " & arNewTaskData[DESCRIPTION] = " + DESCRIPTION + " &arNewTaskData[DEADLINE]=" + xDay + " & arNewTaskData[GROUP_ID] = 21 & &arNewTaskData[ACCOMPLICES][] = 1 & arNewTaskData[ACCOMPLICES][] = 1 & arNewTaskData[AUDITORS][] = 1 & arNewTaskData[RESPONSIBLE_ID] = 1";

        //    string TaskListByJSON = bx_logon.SendCommand("task.item.add", addTask);

        //    if (TaskListByJSON.Contains("\"result\":"))
        //    {
        //        string taskid = TaskListByJSON.Split(',')[0].Split(':')[1];
        //        //bx_logon.SendFileAddTasks(taskid,);
        //    }
        //}



        //private async void button4_Click_1Async(object sender, EventArgs e)
        //{
        //  //  string telegramToken = "718077915:AAHc6eaS8GGZaSOYeQQ66Vu7Z-FMTBKCbJ0";
        //    string telegramtext = textBoxPhone.Text;
        //    Console.WriteLine(telegramtext);
        //    string telegramSendMessange = "Заяка №1";

        //    Telegram telegram = new Telegram();
        //    await telegram.telegramBotAsync(BotTelegramSettings,telegramToken, telegramtext);

        //}


    }
}
