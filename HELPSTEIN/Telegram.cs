using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;


namespace HELPSTEIN
{
    class Telegram
    {
        DateTime dataTime = DateTime.Now;
        //public string telegramToken = "718077915:AAHc6eaS8GGZaSOYeQQ66Vu7Z-FMTBKCbJ0";
        //public string telegramChatId = "-328915940";
        //public string telegramSendMessange = "";
        //1. Нужно добавить библиотеку Telegram.Bot
        //2. Создать бота для этого добавляем себе в контакты @BotFather и пишем ему сначала /start, потом /newbot.
        //3. Мы создали бота для оповещений и получили для него token, который нам понадобится далее. Теперь нужно добавить в свой список контактов созданного бота.Для этого найдите его по имени. В моем случае имя @serveradmin_zabbix_bot.
        //4. Чтобы узнать id моего аккаунта или созданой нами группы нужно сначала добавить бота в группу и что то написать в этой группе написать (Чтобы его узнать, добавьте бота @my_id_bot и напишите ему /start)
        //5. Вводим в браузере https://api.telegram.org/bot<YourBOTToken>/getUpdates и ищем там ChatId
        //
        //http://qaru.site/questions/120100/telegram-bot-how-to-get-a-group-chat-id
        //https://serveradmin.ru/nastroyka-opoveshheniy-zabbix-v-telegram/ 
        //http://qaru.site/questions/499239/how-to-use-telegram-api-in-c-to-send-a-message
        //http://qaru.site/questions/551612/telegram-c-example-send-message
        //https://github.com/alex-erygin/hulio/blob/master/source/huliobot/HulioBot.cs



        public async void telegramAsync(string telegramToken, string telegramChatId, string telegramSendMessange,Boolean isNeeds = true)
        {
            if (isNeeds)
            {
                try
                {
                    TelegramBotClient botClient = new TelegramBotClient(telegramToken);
                    var me = botClient.GetMeAsync().Result;
                    Console.WriteLine(me.Username);


                    var chatId = telegramChatId;
                    var t = botClient.SendTextMessageAsync(chatId, telegramSendMessange);



                }
                catch (Exception ex)
                {
                    try
                    {
                        string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                        //Console.WriteLine(confFile);
                        using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine("Telegram_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                            sw.WriteLine("Telegram_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                        }

                    }
                    catch
                    {

                    }

                }
            }
           

        }












        //ответы от бота
        // http://aftamat4ik.ru/pishem-bota-telegram-na-c/
        // http://qaru.site/questions/551612/telegram-c-example-send-message
        public async Task telegramBotAsync(Boolean isNeed, string telegramToken, string text)
            {
              if(isNeed)
              {
                try
                {
                     var Bot = new TelegramBotClient(telegramToken); //("718077915:AAHc6eaS8GGZaSOYeQQ66Vu7Z-FMTBKCbJ0");
                     await Bot.SetWebhookAsync("");
                     int offset = 0;
                     // test your api configured correctly
                     var me = await Bot.GetMeAsync();
                      Console.WriteLine($"{me.Username} started");

                    while (true)
                    {
                        var updates = await Bot.GetUpdatesAsync(offset); // получаем массив обновлений

                        foreach (var update in updates) // Перебираем все обновления
                        {
                            Console.WriteLine(update.Type);
                            offset = update.Id + 1;
                            var message = update.Message;

                            if (message.Text == text)
                            {
                        

                                DialogResult dialog = MessageBox.Show("Последняя ваша заявка в Helpsein Закрыта ? ", "Статус заявки", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                if (dialog == DialogResult.Yes)
                                {
                                   //отправка сообщения
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "Позьзуватель поттвердил закрытие задачи");
                                }
                                else if (dialog == DialogResult.No)
                                {
                                    await Bot.SendTextMessageAsync(message.Chat.Id, "Свяжитесь с пользувателем задача не закрыта");
                                }
                                if (dialog == DialogResult.Cancel)
                                {
                                    await Bot.SendTextMessageAsync(message.Chat.Id, " Пользуватель Проигнорировал ");
                                }

                            }
                            if (message.Text == "/sceen")
                            {
                                Screenshot screenshot = new Screenshot();
                                string sendScreen =  screenshot.makeScreenshot();
                                Thread.Sleep(2000);
                                using (var stream = new FileStream(sendScreen, FileMode.Open))
                                {
                                   
                                    await Bot.SendPhotoAsync(message.Chat.Id,sendScreen, "Revolution!");
                                }
                                // в ответ на команду /getimage выводим картинку
                                await Bot.SendPhotoAsync(message.Chat.Id, sendScreen, "Revolution!");
                            }


                        }

                    }


                }
                catch(Exception ex)
                {
                    try
                    {
                        string savePathFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath.Replace("user.config", "Errors.txt");
                        //Console.WriteLine(confFile);
                        using (StreamWriter sw = new StreamWriter(savePathFile, true, System.Text.Encoding.Default))
                        {
                            sw.WriteLine("Telegram_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex.Message);
                            sw.WriteLine("Telegram_Error - " + dataTime.ToString("HH:mm:ss dd.MM.yyyy") + " : " + ex);

                        }

                    }
                    catch
                    {

                    }

                }

              }

            }


        
    }
}