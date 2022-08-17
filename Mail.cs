using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HELPSTEIN_Frida
{
    class Mail
    {
        public string info = "";
        public string sendFile = "";
        public string Subject = "";
        // public void Send();
        //public mail()

        // {
        //Inventory i = new Inventory();

        // i.Get_Inventory();

        public void Send()
        {



            System.Net.NetworkCredential crdSupport = new System.Net.NetworkCredential("admin@checkpoint.ua", "31011991");

            SmtpClient smtpConfirmation = new SmtpClient("smtp.gmail.com", 587);// тут можно указать адрес сервер, логин и пароль доступа к которому казан выше

            smtpConfirmation.UseDefaultCredentials = true;
            smtpConfirmation.EnableSsl = true; //Включить ssl шифрование
            smtpConfirmation.Credentials = crdSupport;

            MailMessage mmConfirmation = new MailMessage();

            mmConfirmation.From = new MailAddress("admin@checkpoint.ua"); // от кого отправлять

            mmConfirmation.To.Add("admin@checkpoint.ua");// кому отправлять
            
            try
            {
                mmConfirmation.Subject = Subject; //+ i.RAMbody;
                mmConfirmation.Body = info; //i.RA
               mmConfirmation.Attachments.Add(new Attachment(sendFile));
            }
            catch
            {

            }
            
            smtpConfirmation.Send(mmConfirmation);
        }
    }
}
