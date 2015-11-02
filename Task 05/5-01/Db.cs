namespace _5_01
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;

    internal class Db : IDataSource
    {
        private readonly string sqlCreate =
            $"CREATE TABLE IF NOT EXISTS {Event.TableName}(" +
            $"{Event.FId} INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
            $"{Event.FGuid} TEXT NOT NULL," +
            $"{Event.FVersion} INTEGER NOT NULL," +
            $"{Event.FName} TEXT NOT NULL," +
            $"{Event.FOldName} TEXT NULL," +
            $"{Event.FDate} INTEGER NOT NULL," +
            $"{Event.FChange} INTEGER NOT NULL);";

        private readonly string sqlFileName;

        private IDbConnection sqlConnection;

        public Db(string sqlFileName)
        {
            this.sqlFileName = sqlFileName;

            if (this.IsDbEmpty())
            {
                this.CreateDb();
            }

            if (this.sqlConnection == null || this.sqlConnection.State == ConnectionState.Closed)
            {
                this.sqlConnection = new SQLiteConnection($"Data Source={this.sqlFileName};Version=3;");
                this.sqlConnection.Open();
            }
        }

        public void Add(string guid, int version, string fileName, string oldName, int date, WatcherChangeTypes changeType)
        {
            string sqlInsert =
                $"INSERT INTO {Event.TableName} " +
                $"({Event.FGuid},{Event.FVersion},{Event.FName},{Event.FOldName},{Event.FDate},{Event.FChange})" +
                $"VALUES('{guid}', {version}, '{fileName.ToLower()}', '{oldName}', '{date}', '{(int)changeType}');";

            IDbCommand command = new SQLiteCommand(sqlInsert, (SQLiteConnection)sqlConnection);
            command.ExecuteNonQuery();
        }

        public Event GetLastEventByName(string name)
        {
            string sqlFind =
                $"SELECT * FROM {Event.TableName} " +
                $"WHERE {Event.FName} = '{name.ToLower()}' " +
                $"ORDER BY {Event.FDate} DESC " +
                "LIMIT 1";

            IDbCommand command = new SQLiteCommand(sqlFind, (SQLiteConnection)sqlConnection);

            IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return this.OneRowToEvent(reader);
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Event> ListToRestore(int date)
        {
            string sqlList =
                $"SELECT * FROM {Event.TableName} " +
                $"WHERE {Event.FDate} <= {date} " +
                $"GROUP BY {Event.FGuid};";

            IDbCommand command = new SQLiteCommand(sqlList, (SQLiteConnection)sqlConnection);
            IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return this.OneRowToEvent(reader);
            }
        }

        public IEnumerable<Event> ListAll()
        {
            string sqlListAll =
                $"SELECT * FROM {Event.TableName};";

            IDbCommand command = new SQLiteCommand(sqlListAll, (SQLiteConnection)sqlConnection);
            IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return this.OneRowToEvent(reader);
            }
        }

        private void CreateDb()
        {
            if (!File.Exists(this.sqlFileName))
            {
                SQLiteConnection.CreateFile(this.sqlFileName);
            }

            if (this.sqlConnection == null || this.sqlConnection.State == ConnectionState.Closed)
            {
                this.sqlConnection = new SQLiteConnection($"Data Source={this.sqlFileName};Version=3;");
                this.sqlConnection.Open();
            }

            IDbCommand command = new SQLiteCommand(this.sqlCreate, (SQLiteConnection)sqlConnection);
            command.ExecuteNonQuery();
        }

        private Event OneRowToEvent(IDataReader reader)
        {
            int id = Convert.ToInt32(reader[Event.FId]);
            string guid = reader[Event.FGuid].ToString();
            int version = Convert.ToInt32(reader[Event.FVersion]);
            string name = reader[Event.FName].ToString();
            string oldName = reader[Event.FOldName].ToString();
            int date = Convert.ToInt32(reader[Event.FDate]);
            int change = Convert.ToInt32(reader[Event.FChange]);

            return new Event(id, guid, version, name, oldName, date, change);
        }

        private bool IsDbEmpty()
        {
            if (!File.Exists(this.sqlFileName))
            {
                return true;
            }

            if (this.sqlConnection == null || this.sqlConnection.State == ConnectionState.Closed)
            {
                this.sqlConnection = new SQLiteConnection($"Data Source={this.sqlFileName};Version=3;");
                this.sqlConnection.Open();
            }

            string sqlExistTable =
                $"SELECT name FROM sqlite_master WHERE type='table' AND name='{Event.TableName}';";

            IDbCommand command = new SQLiteCommand(sqlExistTable, (SQLiteConnection)sqlConnection);
            IDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return true;
            }

            return false;
        }
    }
}