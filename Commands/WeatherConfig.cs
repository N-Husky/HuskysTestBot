using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Data.SQLite;


namespace Husky_sTestBot.Commands
{
    class WeatherConfig : Command
    {
        public override string Name => "wconfig";
        static readonly string ConnectionString = "Data Source=botPesonComfig.db;";
        static bool result;
        public override async void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            await client.SendTextMessageAsync(chatId, "Specify weather configuration like so:\nyour_city, default(which is Fahrenheit) or metric (whis is Celsius)", replyToMessageId: messageId);
        }
        public static void AddUserToDb(long chatId, string userName)
        {
            string sql = String.Format("insert into users(chatid, name) values({0}, '{1}')", chatId, userName);
            using(SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                using(SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void AddWeatherConf(long chatId, string userName, string city, string measurementSystem)
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
                sql = String.Format("select * from weather where exists(select * from weather where chatid={0})", chatId);
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
                UpdateWeatherConf(chatId, city, measurementSystem);
            }
            using (SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                sql = String.Format("insert into weather (chatid, city, measurementsys) values ({0}, '{1}', '{2}')", chatId, city, measurementSystem);
                using (SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static string[] GetWeatherConf(long chatId)
        {
            string sql = String.Format("select * from weather where chatid = {0}", chatId);
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
                            conf[0] = rdr.GetString(2);
                            conf[1] = rdr.GetString(3);
                        }
                    }
                }
            }
            return conf;
        }
        public static void UpdateWeatherConf(long chatId, string city, string measurementSystem)
        {
            string sql = String.Format("update weather set city = '{0}', measurementsys = '{1}' where chatid == {2}", city, measurementSystem, chatId);
            using(SQLiteConnection c = new SQLiteConnection(ConnectionString))
            {
                c.Open();
                using(SQLiteCommand cmd = new SQLiteCommand(sql, c))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}


//prev weather conf
//m_dbConnection.Open();
//SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
//command.CommandType = System.Data.CommandType.Text;
//SQLiteDataReader r = command.ExecuteReader();
//if (!r.HasRows)
//{
//    AddUserToDb(chatId, userName);
//}
//if (m_dbConnection.State.ToString() != "Open")
//    m_dbConnection.Open();
////temp refactor this! using using()
//sql = String.Format("select * from weather where exists(select * from weather where chatid={0})", chatId);
//command = new SQLiteCommand(sql, m_dbConnection);
//command.CommandType = System.Data.CommandType.Text;
//r = command.ExecuteReader();
//if (r.HasRows)
//{
//    UpdateWeatherConf(chatId, city, measurementSystem);
//    return;
//}
//sql = String.Format("insert into weather (chatid, city, measurementsys) values ({0}, '{1}', '{2}')", chatId, city, measurementSystem);
//command = new SQLiteCommand(sql, m_dbConnection);
//command.CommandType = System.Data.CommandType.Text;
//command.ExecuteNonQuery();
//m_dbConnection.Close();





//return conf;
//m_dbConnection.Open();
//SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
//command.CommandType = System.Data.CommandType.Text;
//SQLiteDataReader r = command.ExecuteReader();
//string[] conf = new string[2];
//while (r.Read())
//{
//    conf[0] = r.GetString(2);
//    conf[1] = r.GetString(3);
//}
//m_dbConnection.Close();
//return conf;




//if (m_dbConnection.State.ToString() != "Open")
//    m_dbConnection.Open();
//SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
//command.CommandType = System.Data.CommandType.Text;
//command.ExecuteNonQuery();
//m_dbConnection.Close();