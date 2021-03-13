using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Husky_sTestBot
{
    public class DbConnector
    {
        public static string DbName { get; } = "botPesonComfig.db";
        private SQLiteConnection m_dbConnection;
        public DbConnector()
        {
            m_dbConnection = new SQLiteConnection("Data Source=botPesonComfig.db;");
            //m_dbConnection.Open();
        }
        public void Close()
        {
            m_dbConnection.Close();
        }
        public void AddUserToDb(long chatId, string userName)
        {
            m_dbConnection.Open();
            string sql = String.Format("insert into user(chatid, name) values({0}, '{1}')",chatId, userName);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.CommandType = System.Data.CommandType.Text;
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        public void AddWeatherConf(int chatId, string userName, string city, string measurementSystem)
        {
            string sql = String.Format("select * from user where exists(select * from user where chatid={0})",chatId);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.CommandType = System.Data.CommandType.Text;
            SQLiteDataReader r = command.ExecuteReader();
            if (!r.HasRows)
            {
                AddUserToDb(chatId, userName);
            }
            sql = $"insert into weather (chatid, city, measurementsys) values ({chatId}, {city}, {measurementSystem})";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.CommandType = System.Data.CommandType.Text;
            command.ExecuteNonQuery();
        }
        public void UpdateWeatherConf(int chatId, string city, string measurementSystem)
        {

        }
    }
}
