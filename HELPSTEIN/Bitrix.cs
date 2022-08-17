using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HELPSTEIN
{
    class Bitrix
    {
        //Объявляем константы
        private const string BX_ClientID = "local.5c976352347585.65991549"; //Ваш Код приложения (client_id)
        private const string BX_ClientSecret = "34aV3fH2gmhuIN6XV8h0zgJ4SFiWJUn7lsrhtX1ZStYHWNGoJO"; //Ваш Ключ приложения (client_secret)
        private const string BX_Portal = "https://itadvance.bitrix24.ru"; //Адрес вашего портала\сайта в Битрикс24
        private const string BX_OAuthSite = "https://oauth.bitrix.info"; //Этот адрес не изменяйте

        //Объявляем приватные служебные поля
        private string AccessToken;
        private string RefreshToken;
        private DateTime RefreshTime;
        private string Code;
        private string Cookie;

        //Объявляем публичные строковые переменные 
        // public string taskTitle

        //Создаем конструктор с вызовом подключения к Битрикс24 при создании экземпляра данного класса  
        public void Bitrix24()
        {
            Connect();
        }

        //Создаем закрытый метод для начального подключения к Битрикс24
        private void Connect()
        {
            //Создаем HTTP подключение, здесь ничего не меняем
            string BX_URI = BX_Portal + "/oauth/authorize/?client_id=" + BX_ClientID;
            HttpWebRequest requestLogonBitrix24 = (HttpWebRequest)WebRequest.Create(BX_URI);

            //Укажите Ваши логин и пароль пользователя (админа) вашего портала в Битрикс24, под которым будут выполнять REST запросы к Битрикс24.
            string username = "helpstein.helpstein@gmail.com";
            string password = "Password";

            //Настраиваем подключение, ничего не меняем
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            requestLogonBitrix24.Headers.Add("Authorization", "Basic " + svcCredentials);
            requestLogonBitrix24.AllowAutoRedirect = false; // Это обязательное условие, чтобы нас автоматически не переадресовывали на другую страницу
            requestLogonBitrix24.Method = "POST";

            //Подключаемся (отправляем запрос)
            HttpWebResponse responseLogonBitrix24 = (HttpWebResponse)requestLogonBitrix24.GetResponse();

            //Проверяем что статус-код доджен быть 302, нам должны предложить переадресацию, иначе авторизация не требуется, мы и так авторизированы
            if (responseLogonBitrix24.StatusCode == HttpStatusCode.Found)
            {
                //Ничего не меняем, здесь получаем из заголовков ответа Куки и параметры адреса переадресации (из поля "Location") парамер Code
                Uri locationURI = new Uri(responseLogonBitrix24.Headers["Location"]);
                // Ловко парсим URL-адрес с помощью HttpUtility, подключите "System.Web" через пакеты NuGet или добавить ссылку
                var locationParams = System.Web.HttpUtility.ParseQueryString(locationURI.Query);
                Cookie = responseLogonBitrix24.Headers["Set-Cookie"];
                Code = locationParams["Code"];

                //Вызываем исключение, если Код мы не смогли получить, без него далее ни как.
                if (String.IsNullOrEmpty(Code))
                {
                    throw new FormatException("CodeNotFound");
                }

                //Закрываем подключение
                responseLogonBitrix24.Close();

                //Если код успешно получили, то формируем новый HTTP запрос для получения Токенов авторизации
                string BX_OAuth_URI = BX_OAuthSite + "/oauth/token" + "/?" + "grant_type=authorization_code" + "&" +
                "client_id=" + BX_ClientID + "&" +
                "client_secret=" + BX_ClientSecret + "&" +
                "code=" + Code;
                SetToken(BX_OAuth_URI);
            }

        }

        //Закрытый метод для получения и записи Токенов авторизации
        private void SetToken(string BX_OAuth_URI)
        {
            //Формируем новый HTTP запрос для получения Токенов авторизации
            HttpWebRequest requestLogonBitrixOAuth = (HttpWebRequest)WebRequest.Create(BX_OAuth_URI);
            requestLogonBitrixOAuth.Method = "POST";
            requestLogonBitrixOAuth.Headers["Cookie"] = Cookie; //Используем Куки полученный в предыдущем запросе авторизации

            //Подключаемся (отправляем запрос)
            HttpWebResponse responseLogonBitrixOAuth = (HttpWebResponse)requestLogonBitrixOAuth.GetResponse();

            //Если в ответ получаем статус-код отличный от 200, то это ошибка, вызываем исключение
            if (responseLogonBitrixOAuth.StatusCode != HttpStatusCode.OK)
            {
                throw new FormatException("ErrorLogonBitrixOAuth");
            }
            else
            {
                //Читаем тело ответа
                Stream dataStreamLogonBitrixOAuth = responseLogonBitrixOAuth.GetResponseStream();
                var readerLogonBitrixOAuth = new StreamReader(dataStreamLogonBitrixOAuth);
                string stringLogonBitrixOAuth = readerLogonBitrixOAuth.ReadToEnd();

                //Обязательно закрываем подключения и потоки
                readerLogonBitrixOAuth.Close();
                responseLogonBitrixOAuth.Close();

                //Ловко преобразуем тело ответа в формате JSON в .Net объект с помощью Newtonsoft.Json, не забудьте подключить Newtonsoft.Json через NuGet
                var converter = new ExpandoObjectConverter();
                dynamic objLogonBitrixOAuth = JsonConvert.DeserializeObject<ExpandoObject>(stringLogonBitrixOAuth, converter);

                //Записывем Токены авторизации в поля нашего класса из динамического объекта
                AccessToken = objLogonBitrixOAuth.access_token;
                RefreshToken = objLogonBitrixOAuth.refresh_token;
                RefreshTime = DateTime.Now.AddSeconds(objLogonBitrixOAuth.expires_in); //Добавляем к текущей дате количество секунд действия токена, обычно это плюс один час

                //Закрываем поток
                dataStreamLogonBitrixOAuth.Close();
            }
        }

        //Закрытый метод для обновления Токенов авторизации, если истекло время их действия
        private void RefreshTokens()
        {
            if (RefreshTime == DateTime.MinValue) // Если RefreshTime пустая
            {
                //Тогда вызываем авторизацию по полной программе
                Connect();
                return; //Тогда дальше не идём
            }

            //Проверяем, если истекло время действия Токена авторизации, то обновляем его
            if (RefreshTime.AddSeconds(-5) < DateTime.Now)
            {
                //Формируем новый HTTP запрос для обновления Токена авторизации, здесь Code уже не нужен
                string BX_OAuth_URI = BX_OAuthSite + "/oauth/token" + "/?" + "grant_type=refresh_token" + "&" +
                "client_id=" + BX_ClientID + "&" +
                "client_secret=" + BX_ClientSecret + "&" +
                "refresh_token=" + RefreshToken;

                SetToken(BX_OAuth_URI);
            }
        }


        //Открытый метод для отправки REST-запросов в Битрикс24
        public string SendCommand(string Command, string Params = "", string Body = "")
        {
            //Проверяем и обновлем Токены авторизации
            RefreshTokens();

            //Проверяем возможное указание параметров
            string BX_REST_URI = "";
            if (String.IsNullOrEmpty(Params))
                BX_REST_URI = BX_Portal + "/rest/" + Command + "?auth=" + AccessToken;
            else
                // BX_REST_URI = BX_Portal + "/rest/" + Command + "?auth=" + AccessToken + "&" + Params;
                BX_REST_URI = BX_Portal + "/rest/" + Command + "?" + Params + "&auth=" + AccessToken;
            //Создаем новое HTTP подключение для отправки REST-запроса в Битрикс24
            HttpWebRequest requestBitrixREST = (HttpWebRequest)WebRequest.Create(BX_REST_URI);
            requestBitrixREST.Method = "POST";
            requestBitrixREST.Headers["Cookie"] = Cookie; //Используем Куки полученный в запросе авторизации

            //Готовим тело запроса и вставляем его в тело POST-запроса
            byte[] byteArrayBody = Encoding.UTF8.GetBytes(Body);
            requestBitrixREST.ContentType = "application/x-www-form-urlencoded";
            requestBitrixREST.ContentLength = byteArrayBody.Length;
            Stream dataBodyStream = requestBitrixREST.GetRequestStream();
            dataBodyStream.Write(byteArrayBody, 0, byteArrayBody.Length);
            dataBodyStream.Close();
            Console.WriteLine(dataBodyStream);
            Console.WriteLine(requestBitrixREST);
            //Отправляем данные в Битрикс24
            HttpWebResponse responseBitrixREST = (HttpWebResponse)requestBitrixREST.GetResponse();
            Console.WriteLine(responseBitrixREST);
            //Читаем тело ответа от Битрикс24
            Stream dataStreamBitrixREST = responseBitrixREST.GetResponseStream();
            var readerBitrixREST = new StreamReader(dataStreamBitrixREST);
            string stringBitrixREST = readerBitrixREST.ReadToEnd();
            Console.WriteLine(readerBitrixREST);
            //Закрываем все подключения и потоки
            readerBitrixREST.Close();
            dataStreamBitrixREST.Close();
            responseBitrixREST.Close();

            //Возвращаем строку ответа в формате JSON
            return stringBitrixREST;
        }

        public void SendFileAddTasks(string taskid ,string filePath, string fileName)
        {
            RefreshTokens();

            Console.WriteLine(AccessToken);

            string url = "https://itadvance.bitrix24.ru/rest/task.item.addfile.xml?=12&auth=" + AccessToken;


            var client = new HttpClient();
            try
            {
                var content = new MultipartFormDataContent("Upload");

                Byte[] byteArray = File.ReadAllBytes(filePath);
                String file = Convert.ToBase64String(byteArray);

                content.Add(new StringContent(taskid), "TASK_ID");
                content.Add(new StringContent(fileName), "FILE[NAME]");
                content.Add(new StringContent(file), "FILE[CONTENT]");

                var response = client.PostAsync(url, content).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }


        public void sendToBitrix(Boolean isNeed, string TITLE, string DESCRIPTION,string GROUP_ID,string ACCOMPLICES,string RESPONSIBLE_ID, string sendFileScreenShot, string sendFileExel, string ACCOMPLICES_1 = "", string ACCOMPLICES_2 ="", string ACCOMPLICES_3 = "")
        {
            if (isNeed)
            {
                //string textTicked = textBox1.Text + System.Environment.NewLine;
                //string formFields = labelCompany.Text + "  " + textBoxCompanyName.Text + "\n" + labelName.Text + "  " + textBoxName.Text + "\n" + labelPhone.Text + "  " + textBoxPhone.Text + "\n" + labelEmail.Text + "  " + textBoxEmail.Text + "\n";
                DateTime dataTime = DateTime.Now;
                string strdate = dataTime.ToString("yyyy.MM.dd-HH:mm:ss");
                //Добавляет + день к  дате
                DateTime xDay = dataTime.AddDays(1);
                // переменная темы для битрикс
               // string TITLE = m.Subject;
                Console.WriteLine(TITLE);
                // переменная Описания задачи для битрикс
                //string DESCRIPTION = formFields + "Приоритет: " + checkUrgency() + "\n" + "Тип Обращения:  " + trouble + "\n" + "Текст Обращения:\n" + textTicked + "Даные о TeamViewer: " + tv + "\n" + user.pcNameBody + "\n" + user.userNameBody + "\n" + user.IPbody;// m.info;
                Console.WriteLine(DESCRIPTION);
                if (String.IsNullOrEmpty(GROUP_ID))
                {
                    GROUP_ID = "23";
                }
                if (String.IsNullOrEmpty(ACCOMPLICES))
                {
                    ACCOMPLICES = "1";
                }
                if (String.IsNullOrEmpty(RESPONSIBLE_ID))
                {
                    RESPONSIBLE_ID = "1";
                }



                //переменная запроса для создания задачи
                string addTask = "arNewTaskData[TITLE] = " + TITLE + " & arNewTaskData[DESCRIPTION] = " + DESCRIPTION + " &arNewTaskData[DEADLINE]=" + xDay + " & arNewTaskData[GROUP_ID] = " + GROUP_ID + " &arNewTaskData[ACCOMPLICES][] = " + ACCOMPLICES + " & arNewTaskData[ACCOMPLICES][] = " + ACCOMPLICES_1 + " & arNewTaskData[ACCOMPLICES][] = " + ACCOMPLICES_2 + " & arNewTaskData[ACCOMPLICES][] = " + ACCOMPLICES_3 + " & arNewTaskData[AUDITORS][] = 1 & arNewTaskData[RESPONSIBLE_ID] = " + RESPONSIBLE_ID + "";

                string TaskListByJSON = SendCommand("task.item.add", addTask);

                //
                if (TaskListByJSON.Contains("\"result\":"))
                {
                    //переменная айди задачи
                    string taskid = TaskListByJSON.Split(',')[0].Split(':')[1];
                    SendFileAddTasks(taskid, sendFileScreenShot, "scren.png");
                    SendFileAddTasks(taskid, sendFileExel, "Inventory" + strdate + ".xls");
                }


            }
        }


        //Нужно подключить RESTsharp (но пока менот не работает нужно доработать)
        //public string AddFile()
        //{

        //    RefreshTokens();

        //    // string BX_REST_URI = BX_Portal + "rest/task.item.addfile.xml?&auth=" + AccessToken;//+"&TASK_ID="+ taskID+"&FILE[NAME]="+Name+"&FILE[CONTENT]=";
        //    string url = "https://b24-2vbqd6.bitrix24.ru/rest/task.item.addfile.xml?=12&auth=" + AccessToken;
        //    var client = new RestClient(url);
        //    //
        //    //client.Authenticator = new HttpBasicAuthenticator("auth", AccessToken);
        //    var request = new RestRequest(Method.POST);
        //    //request.RequestFormat = DataFormat.Json;

        //    Byte[] bytes = File.ReadAllBytes(@"C:\Users\Igor\Pictures\2016-05-15-19-16-14.jpg");
        //    String file = Convert.ToBase64String(bytes);

        //    var meta = new Dictionary<string, object>();

        //    meta.Add("TASK_ID", "337");
        //    meta.Add("FILE[NAME]", "11.jpg");
        //    meta.Add("FILE[CONTENT]", file);

        //    var json = JsonConvert.SerializeObject(meta);

        //    request.AddHeader("Charset", "utf-8");
        //    request.AddHeader("Content-Type", "multipart/form-data");
        //    // request.AddParameter("auth", AccessToken);
        //    request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
        //    request.AddParameter("multipart/form-data;", json, ParameterType.RequestBody);
        //    //request.AddJsonBody(json);
        //    //request.AddParameter("TASK_ID", 335);
        //    //request.AddParameter("FILE[NAME]", "11.jpg");
        //    //request.AddParameter("FILE[CONTENT]", file);



        //    IRestResponse response = client.Execute(request);

        //    //var response = client.Get(request);
        //    Console.WriteLine("---------------------------------");
        //    Console.WriteLine(response.Content);
        //    Console.WriteLine(response.ResponseStatus);
        //    Console.WriteLine(response.StatusCode);
        //    Console.WriteLine(response.StatusDescription);

        //    Console.WriteLine(AccessToken);
        //    Console.ReadLine();

        //    return response.Content;

        //}


        ////Запросы 
        //DateTime currentTime = DateTime.Now;
        //DateTime xDay = currentTime.AddDays(1);
        //string DESCRIPTION = "текст обращения";
        //string addTask = "arNewTaskData[TITLE] = test! & arNewTaskData[DESCRIPTION] = " + DESCRIPTION + " &arNewTaskData[DEADLINE]=" + xDay + " & arNewTaskData[GROUP_ID] = 21 & &arNewTaskData[ACCOMPLICES][] = 1 & arNewTaskData[ACCOMPLICES][] = 21 & arNewTaskData[AUDITORS][] = 1 & arNewTaskData[RESPONSIBLE_ID] = 1";
        //string TaskListByJSON = bx_logon.SendCommand("task.item.add", addTask);

        //// Получить текущий id задачи
        //if (TaskListByJSON.Contains("\"result\":"))
        //        {
        //        string taskid = TaskListByJSON.Split(',')[0].Split(':')[1];
        ////Console.WriteLine("************");
        ////Console.WriteLine(taskid);








    }
}
