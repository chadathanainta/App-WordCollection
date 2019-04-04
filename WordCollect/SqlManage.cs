
using System.Collections.Generic;
using Android.Util;
using SQLite;

namespace WordCollect
{
    public class SqlManage
    {
        private string folder = null;

        public SqlManage()
        {
            folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        }

        public void CreateDatabase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Wordfile.db")))
                {
                    connection.CreateTable<Collection>();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
            }
        }

        public void InsertWord(Collection collection)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Wordfile.db")))
                {
                    connection.Insert(collection);
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                throw ex;
            }
        }

        public List<Collection> SelectAllTable()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Wordfile.db")))
                {
                    return connection.Query<Collection>("SELECT * FROM Collection order by KeyWord  limit 20");
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public List<Collection> SelectAllTable(string text)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Wordfile.db")))
                {
                    return connection.Query<Collection>("SELECT * FROM Collection  Where KeyWord LIKE '%" + text + "%' or Detal LIKE '%" + text + "%' order by KeyWord ");
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }

        public bool RemoveWord(string KeyWord)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Wordfile.db")))
                {
                    connection.Query<Collection>("delete from Collection where KeyWord = ? ", KeyWord);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool UpdateWord(Collection collection)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Wordfile.db")))
                {
                    connection.Query<Collection>("UPDATE Collection set Detal=? Where KeyWord=?", collection.Detal, collection.KeyWord);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
    }
}