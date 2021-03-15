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
        public string IsOpened()
        {
            return m_dbConnection.State.ToString();
        }
        public void AddUserToDb(long chatId, string userName)
        {
            if (m_dbConnection.State.ToString() != "Open")
                m_dbConnection.Open();
            string sql = String.Format("insert into users(chatid, name) values({0}, '{1}')",chatId, userName);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.CommandType = System.Data.CommandType.Text;
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        public void AddWeatherConf(long chatId, string userName, string city, string measurementSystem)
        {
            m_dbConnection.Open();
            string sql = String.Format("select * from users where exists(select * from users where chatid={0})",chatId);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.CommandType = System.Data.CommandType.Text;
            SQLiteDataReader r = command.ExecuteReader();
            if (!r.HasRows)
            {
                AddUserToDb(chatId, userName);
            }
            if (m_dbConnection.State.ToString() != "Open")
                m_dbConnection.Open();
            //temp refactor this! using using()
            sql = String.Format("select * from weather where exists(select * from weather where chatid={0})", chatId);
            command = new SQLiteCommand(sql, m_dbConnection);
            command.CommandType = System.Data.CommandType.Text;
            r = command.ExecuteReader();
            if (r.HasRows)
            {
                UpdateWeatherConf(chatId, city, measurementSystem);
                return;
            }
            sql = String.Format("insert into weather (chatid, city, measurementsys) values ({0}, '{1}', '{2}')",chatId, city,measurementSystem);
            command = new SQLiteCommand(sql, m_dbConnection); 
            command.CommandType = System.Data.CommandType.Text;
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        public string[] GetWeatherConf(long chatId)
        {
            m_dbConnection.Open();
            string sql = String.Format("select * from weather where chatid = {0}", chatId);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.CommandType = System.Data.CommandType.Text;
            SQLiteDataReader r = command.ExecuteReader();
            string[] conf = new string[2];
            while (r.Read())
            {
                conf[0] = r.GetString(2);
                conf[1] = r.GetString(3);
            }
            m_dbConnection.Close();
            return conf;
        }
        public void UpdateWeatherConf(long chatId, string city, string measurementSystem)
        {
            if (m_dbConnection.State.ToString() != "Open")
                m_dbConnection.Open();
            string sql = String.Format("update weather set city = '{0}', measurementsys = '{1}' where chatid == {2}", city, measurementSystem, chatId);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.CommandType = System.Data.CommandType.Text;
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
    }
}
