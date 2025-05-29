using DWShop.Client.Mobile.Models;
using SQLite;

namespace DWShop.Client.Mobile.Context
{
    public class ProductRepo
    {
        SQLiteAsyncConnection db;

        async Task Init()
        {
            if (db is not null)
                return;

            db = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "DWShop.db3"),
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

            _ = await db.CreateTableAsync<ProductModel>();
        }

        public async Task<ProductModel> GetProduct(int id)
        {
            await Init();
            return await db.Table<ProductModel>().FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<List<ProductModel>> GetProducts()
        {
            await Init();
            return await db.Table<ProductModel>().ToListAsync();
        }
    }

}

