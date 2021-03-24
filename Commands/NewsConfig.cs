using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Husky_sTestBot.Commands
{
    class NewsConfig : Command
    {
        public override string Name => "nconfig";
        static readonly string ConnectionString = "Data Source=botPesonComfig.db;";

        static bool result;
        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            await client.SendTextMessageAsync(chatId, "Specify news configuration like so:\nmax_amount_of_news_to_be_displayed", replyToMessageId: messageId);
        }
        public static void AddUserToDb(long chatId, string userName)
        {
            string sql = String.Format("insert into users(chatid, name) values({0}, '{1}')", chatId, userName);
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        internal static void WriteLastMessage(string pubDate, long chatId)
        {
            string sql = String.Format("update news set lastnewsdate = '{0}' where chatid == {1}", pubDate,chatId);
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void AddNewsConf(long chatId, int maxamount, string userName) 
        {
            string sql = String.Format("select * from users where exists(select * from users where chatid={0})", chatId);
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        result = rdr.HasRows;
                    }
                }
            }
            if (!result)
            {
                AddUserToDb(chatId, userName);
            }
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                sql = String.Format("select * from news where exists(select * from news where chatid={0})", chatId);
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        result = rdr.HasRows;
                    }
                }
            }
            if (result)
            {
                UpdateNewsConf(chatId, maxamount);
            }
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                sql = String.Format("insert into news (maxamount,chatid) values ({0}, {1})", maxamount, chatId);
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static string[] GetNewsConf(long chatId)
        {
            string sql = String.Format("select * from news where chatid = {0}", chatId);
            string[] conf = new string[2];
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            //Gettin maxamount and last sendet news
                            conf[0] = rdr.GetInt32(1).ToString();
                            conf[1] = rdr.GetString(3);
                        }
                    }
                }
            }
            return conf;
        }
        public static void UpdateNewsConf(long chatId, int maxamount)
        {
            string sql = String.Format("update news set maxamount = {0} where chatid == {1}", maxamount, chatId);
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateLastNewsDate(long chatId,string date)
        {
            string sql = String.Format("update news set lastnewsdate = {0} where chatid == {1}", date, chatId);
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
