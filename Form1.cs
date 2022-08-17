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

namespace HELPSTEIN_Frida
{
    public partial class Form1 : Form
    {
        Mail m = new Mail();
        Inventory i = new Inventory();
        TeamViewer tv = new TeamViewer();
        userInfo user = new userInfo();
        Screenshot screen = new Screenshot();


        public Form1()
        {
            InitializeComponent();
            //
            try
            {
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

            }
            catch
            {
                //Строка пути где лежит конфиг приложения(аппДата\локал) 
                string confFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
                //Условие существует ли файл 
                if (File.Exists(confFile))
                {
                    //если существует то удаляет его
                    File.Delete(confFile);
                }
                System.Diagnostics.Process.Start(Application.ExecutablePath);
                this.Close();
            }
           
            //робота с формой
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            showToolStripMenuItem.Click += showToolStripMenuItem_Click;
            screenshotToolStripMenuItem.Click += screenshotToolStripMenuItem_Click;
            toolStripONTOP.Click += ToolStripONTOP_Click;
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            notifyIcon1.MouseDoubleClick += showToolStripMenuItem_Click;

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
            try
            {
               //Иницыализация веб клиента
               WebClient Client = new WebClient();
               //Строка пути где лежит конфиг приложения(аппДата\локал) 
               string confFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
              //Условие существует ли файл 
               if (File.Exists(confFile))
               {
                //если существует то удаляет его
                File.Delete(confFile);
               }
                //скачивает конфиг к кладет его в указаную папку
                Client.DownloadFile("http://127.0.0.1/user.config", confFile);
                 //перезаписывает конфиг
                 Properties.Settings.Default.Reload();
                //перезагрузка формы
                System.Diagnostics.Process.Start(Application.ExecutablePath);
                this.Close();
            }
            catch
            {
                progressBar1.Value = 0;

            }
            
         }


        public bool senderrors = false;
        //булевая переменная поверх всех окон
        public bool isTOP = false;
        //булевая переменная видимости формы
        private bool allowVisible;
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
            //Сохранение параметров при закрытии формы
            Properties.Settings.Default.textBoxCompanyName = textBoxCompanyName.Text;
            Properties.Settings.Default.textBoxName = textBoxName.Text;
            Properties.Settings.Default.textBoxPhone = textBoxPhone.Text;
            Properties.Settings.Default.textBoxEmail = textBoxEmail.Text;
            Properties.Settings.Default.Save();

            System.Windows.Forms.Application.Exit();
        }

        //Действия при закрытии формы
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!allowClose)
            {
                this.Hide();
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }


        //Действия при закрытии формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
           // Сохранение параметров при закрытии формы
            Properties.Settings.Default.textBoxCompanyName = textBoxCompanyName.Text;
            Properties.Settings.Default.textBoxName = textBoxName.Text;
            Properties.Settings.Default.textBoxPhone = textBoxPhone.Text;
            Properties.Settings.Default.textBoxEmail = textBoxEmail.Text;
            Properties.Settings.Default.Save();

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


        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            //Текущая дата
            DateTime dat1 = DateTime.Now;
            string strdate = dat1.ToString("yyyy.MM.dd-HH:mm:ss");
            //Переменная Текста Обращения
            string textTicked = textBox1.Text + System.Environment.NewLine;
            //Переменная Типа Обращения
            string trouble = comboBoxTroubleTypes.SelectedItem.ToString();
            progressBar1.Value = 5;
            //Переменная Инфомации о контактах пользувателя
            string formFields = "";
            //Переменная Тимвювера
            string tv = "";
            progressBar1.Value = 10;
            // Наполнение Переменная Инфомации о контактах пользувателя
            formFields += labelCompany.Text + "  " + textBoxCompanyName.Text + "\n" + labelName.Text + "  " + textBoxName.Text + "\n" + labelPhone.Text + "  " + textBoxPhone.Text + "\n" + labelEmail.Text + "  " + textBoxEmail.Text + "\n" ;
            progressBar1.Value = 15;
            //Вызов методов класов
            i.getRam(true);
            progressBar1.Value = 20;
            i.getCPU(true);
            progressBar1.Value = 25;
            i.getDISK(true);
            progressBar1.Value = 30;
            i.getOP(true);
            progressBar1.Value = 35;
            i.getVIDEO(true);
            progressBar1.Value = 40;
            i.getMOTHERBOARD(true);
            progressBar1.Value = 45;
            i.getMONITOR(true);
            progressBar1.Value = 50;
            i.getPRINTERS(true);
            progressBar1.Value = 55;
            user.addInfo_getUserName();
            progressBar1.Value = 58;
            user.addInfo_getIP();
            progressBar1.Value = 61;
            user.addInfo_getPCName();
            progressBar1.Value = 65;
            //Условия если чекбокс Cheked
            if (checkBox_TVinfo.Checked)
            {
                TeamViewer teamViewer = new TeamViewer();
                tv += teamViewer.get_TeamViewer_id_pass();
                //Условия если переменная  Тимвювера начинается с текста "Не найден процесс TV!"
                if (tv.StartsWith("Не найден процесс TV!"))
                {
                    MessageBox.Show("Не найден процесс TeamViewer! Запустите TeamViewer и  повторите Ваш запрос" + "\n" + "В протинном случаае Адинимтратор не сможет к Вам удаленно подключится!" + "\n" + "Нажмите ОК чтобы продолжыть отправку запроса", "TeamViewer" ,MessageBoxButtons.OK, MessageBoxIcon.Information);
                    senderrors = false;
                } 
               
                
            }
            progressBar1.Value = 75;
            //Условия если чекбокс Cheked(То мы наполняем переменную sendFile класа mail 
            if (checkBox_makeScreenshot.Checked)
            {
                
                m.sendFile = screen.makeScreenshot();
               
            }
            progressBar1.Value = 90;
            //Условия Проверкы сети
            if (checkNetwork("8.8.8.8"))
            {
               progressBar1.Value = 96;
                //Условия если не выбран тип обращения то программа не отправляет запрос
                if (comboBoxTroubleTypes.SelectedItem.ToString() == "")
                {
                   MessageBox.Show("Выберите тип обращения!!!\nИначе вы не сможете отправить запрос" + "\n" + "Нажмите ОК ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressBar1.Value = 0;
                    senderrors = true;
                }
                
                else
                {
                   m.Subject = strdate+" Заявка от: " + labelCompany.Text + "  " + textBoxCompanyName.Text + "  Тип Обращения:  " + trouble;
                   m.info = formFields + "Приоритет: " + checkUrgency() + "\n" + "Тип Обращения:  " + trouble + "\n"+ "Текст Обращения:\n" + textTicked + "Даные о TeamViewer: " + tv + "\n" + user.pcNameBody + user.userNameBody + user.IPbody+ i.RAMbody + i.CPUbody + i.DISKbody + i.OPbody + i.MOTHERBOARDbody + i.VIDEObody  + i.MONITORbody + i.PRINTERSbody ; //+ i.NETWORKSbody
                   m.Send();
                    progressBar1.Value = 100;
                    if (!senderrors) { MessageBox.Show("Сообщение специалисту отправлено", "Прекрасно!", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                   updateConfig();
                }


            }
            //сеть не доступна
            else { MessageBox.Show("Похоже, у Вас нет интернета." + "\n" + "Я не смогу отправить Ваш запрос специалисту :(" + "\n" + "Попробуйте альтернативный метод связи с ним (телефон, например)", "ОЙйой !!", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
          
            
        }

       

       


       

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://download.teamviewer.com/download/TeamViewerQS.exe");
            Process.Start(sInfo);
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
