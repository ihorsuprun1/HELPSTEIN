using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
//using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HELPSTEIN
{
    class Mail
    {
        DateTime dataTime = DateTime.Now;
        public string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
       
        public void Send(Boolean isNeed, string Subject, string info,string sendFileScreenShot = "", string sendFileExel = "", string sendFileAttached = "")
        {
            if (isNeed)
            {

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Helpstein", "helpstein.helpstein@meta.ua"));
                message.To.Add(new MailboxAddress("Helpstein", "helpstein.helpstein@meta.ua"));
                message.Subject = Subject;
                var builder = new BodyBuilder();
                builder.TextBody = info;
                if(sendFileExel != "")
                {
                    builder.Attachments.Add(sendFileExel);
                }
                if(sendFileScreenShot != "")
                {
                    builder.Attachments.Add(sendFileScreenShot);
                }
                if(sendFileAttached != "")
                {
                    builder.Attachments.Add(sendFileAttached);
                }

                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.meta.ua", 465, true);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("helpstein.helpstein@meta.ua", "Pasword1@123");

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            
            //  System.Net.NetworkCredential crdSupport = new System.Net.NetworkCredential("helpstein@i.ua", "Pasword1@123");

            //  SmtpClient smtpConfirmation = new SmtpClient("smtp.i.ua", 25);// тут можно указать адрес сервер, логин и пароль доступа к которому казан выше

            // // smtpConfirmation.UseDefaultCredentials = true;
            ////  smtpConfirmation.EnableSsl = true; //Включить ssl шифрование
            //  smtpConfirmation.Credentials = crdSupport;

            //  MailMessage mmConfirmation = new MailMessage();

            //  mmConfirmation.From = new MailAddress("helpstein@i.ua"); // от кого отправлять

            //  mmConfirmation.To.Add("igorsuprun1@gmail.com");// кому отправлять

            //  mmConfirmation.To.Add("helpstein.helpstein@gmail.com");
            //  mmConfirmation.Subject = Subject; //+ i.RAMbody;
            //  mmConfirmation.Body = info; //i.RA
            //  try
            //  {
            //      mmConfirmation.Attachments.Add(new Attachment(sendFileExel));

            //  }
            //  catch (Exception ex)
            //  {
            //      try
            //      {
            //          // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
            //          //Console.WriteLine(confFile);
            //          using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
            //          {
            //              sw.WriteLine("sendFileExel_INVENTORY_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
            //              sw.WriteLine("sendFileExel_INVENTORY_Error  - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

            //          }

            //      }
            //      catch
            //      {

            //      }

            //  }
            //  try
            //  {
            //      mmConfirmation.Attachments.Add(new Attachment(sendFileScreenShot));

            //  }
            //  catch (Exception ex)
            //  {
            //      try
            //      {

            //          using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
            //          {
            //              sw.WriteLine("sendFileScreenShot_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
            //              sw.WriteLine("sendFileScreenShot_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

            //          }

            //      }
            //      catch
            //      {

            //      }

            //  }
            //  //try
            //  //{
            //  //    mmConfirmation.Attachments.Add(new Attachment(sendFileAttached));

            //  //}


            //  //catch (Exception ex)
            //  //{
            //  //    try
            //  //    {
            //  //        // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
            //  //        //Console.WriteLine(confFile);
            //  //        using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
            //  //        {
            //  //            sw.WriteLine("sendFileAttached_Прикрепленный файл_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
            //  //            sw.WriteLine("sendFileAttached_Прикрепленный файл_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

            //  //        }

            //  //    }
            //  //    catch
            //  //    {

            //  //    }

            //  //}
            //  try
            //  {
            //      smtpConfirmation.Send(mmConfirmation);
            //      //Освобождаем переменные sendFileAttached от файлов
            //      mmConfirmation.Dispose();
            //      mmConfirmation = null;
            //  }

            //  catch (Exception ex)
            //  {
            //      MessageBox.Show(ex.Message);
            //      MessageBox.Show(ex.ToString());
            //      try
            //      {
            //          // string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
            //          //Console.WriteLine(confFile);
            //          using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
            //          {
            //              sw.WriteLine("sendFileAttached_Прикрепленный файл_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
            //              sw.WriteLine("sendFileAttached_Прикрепленный файл_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

            //          }

            //      }
            //      catch
            //      {

            //      }
            //  }

            // }




        }
    
    
    
    }
}
