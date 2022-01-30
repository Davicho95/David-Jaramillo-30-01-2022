using ListaTareas.Models.Tablas;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListaTareas.Data
{
    public class DatabaseContext
    {
        private readonly SQLiteAsyncConnection connection;

        public DatabaseContext(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            #region Tablas
            connection.CreateTableAsync<Tareas>().Wait();
            connection.CreateTableAsync<TareasEliminadas>().Wait();
            #endregion

        }

        public async Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            return await connection.Table<T>().ToListAsync();
        }

        public async Task<List<T>> FilterItemsAsync<T>(string table, string condition) where T : new()
        {
            return await connection.QueryAsync<T>($"SELECT * FROM {table} WHERE {condition}");
        }

        public async Task<T> GetItemAsync<T>(object id) where T : new()
        {
            return await connection.FindAsync<T>(id);
        }

        public async Task<int> SaveItemAsync<T>(T item, bool isInsert) where T : new()
        {
            return (isInsert)
                ? await connection.InsertAsync(item)
                : await connection.UpdateAsync(item);
        }

        public async Task<int> SaveAllItemAsync<T>(List<T> item, bool isInsert) where T : new()
        {
            return (isInsert)
                ? await connection.InsertAllAsync(item)
                : await connection.UpdateAllAsync(item);
        }

        public async Task<int> DeleteItemAsync<T>(T item) where T : new()
        {
            return await connection.DeleteAsync(item);
        }

        public async Task<int> DeleteAllItemAsync<T>() where T : new()
        {
            return await connection.DeleteAllAsync<T>();
        }

        public async Task DeleteItemsQueryAsync<T>(string table, string condition) where T : new()
        {
            var resultado = await connection.QueryAsync<T>($"DELETE FROM {table} WHERE {condition}");
        }
    }
}
