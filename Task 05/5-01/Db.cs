namespace _5_01
{
    using System;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;

    internal class Db
    {
        public event EventHandler<EventArgs> InitEvent;

        private readonly string sqlCreate =
            $"CREATE TABLE IF NOT EXISTS \"{Event.TableName}\"(" +
            $"\"{Event.FId}\" INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
            $"\"{Event.FGuid}\" TEXT NOT NULL," +
            $"\"{Event.FVersion}\" INTEGER NOT NULL," +
            $"\"{Event.FName}\" TEXT NOT NULL," +
            $"\"{Event.FOldName}\" TEXT NULL," +
            $"\"{Event.FDate}\" INTEGER NOT NULL," +
            $"\"{Event.FChange}\" INTEGER NOT NULL);";

        private readonly string sqlFileName;

        private SQLiteConnection sqlConnection;

        public Db(string sqlFileName)
        {
            this.sqlFileName = sqlFileName;

            if (!File.Exists(this.sqlFileName))
            {
                SQLiteConnection.CreateFile(this.sqlFileName);
            }

            if (this.sqlConnection == null || this.sqlConnection.State == ConnectionState.Closed)
            {
                this.sqlConnection = new SQLiteConnection($"Data Source={this.sqlFileName};Version=3;");
                this.sqlConnection.Open();
            }
        }

        public void Init()
        {
            if (IsDbEmpty())
            {
                CreateDb();

                if (InitEvent != null)
                {
                    InitEvent(this, EventArgs.Empty);
                }
            }
        }

        public void Add(string guid, int version, string fileName, string oldName, int date, WatcherChangeTypes changeType)
        {
            string sqlInsert =
                $"INSERT INTO {Event.TableName} " +
                $"({Event.FGuid},{Event.FVersion},{Event.FName},{Event.FOldName},{Event.FDate},{Event.FChange})" +
                $"VALUES('{guid}', {version}, '{fileName}', '{oldName}', '{date}', '{(int)changeType}');";

            SQLiteCommand command = new SQLiteCommand(sqlInsert, this.sqlConnection);
            command.ExecuteNonQuery();
        }

        public Event GetLastEventByName(string name)
        {
            string sqlFind =
                $"SELECT *, MAX({Event.FDate}) " + 
                $"FROM {Event.TableName} " +
                $"WHERE {Event.FName} = '{name}' " +
                "LIMIT 1";

            SQLiteCommand command = new SQLiteCommand(sqlFind, sqlConnection);

            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return OneRowToEvent(reader);
            }
            else
            {
                return null;
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
            
            SQLiteCommand command = new SQLiteCommand(this.sqlCreate, this.sqlConnection);
            command.ExecuteNonQuery();
        }

        private Event OneRowToEvent(SQLiteDataReader reader)
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

            SQLiteCommand command = new SQLiteCommand(sqlExistTable, sqlConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return true;
            }

            string sqlConsistsRows =
                $"SELECT * FROM '{Event.TableName}';";

            command = new SQLiteCommand(sqlConsistsRows, sqlConnection);
            reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return true;
            }

            return false;
        }
    }
}