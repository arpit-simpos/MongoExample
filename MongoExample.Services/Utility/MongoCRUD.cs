using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoExample.Services.Utility
{
    public interface IMongoCRUD
    {
        Task InsertRecord<T>(string table, T record);
        Task<List<T>> LoadRecords<T>(string table);
        Task<T> LoadRecordsById<T>(string table, string id);
        Task UpdateRecord<T>(string table, string id, T record);
        Task DeleteRecord<T>(string table, string id);
    }
    public class MongoCRUD : IMongoCRUD
    {
        private IMongoDatabase db;
        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public async Task InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            await collection.InsertOneAsync(record);

        }
        public async Task<List<T>> LoadRecords<T>(string table)
        {
            var collections = db.GetCollection<T>(table);
            return await collections.Find(new BsonDocument())
                .ToListAsync();
        }
        public async Task<T> LoadRecordsById<T>(string table, string id)
        {
            var collections = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("id", id);

            return await collections.Find(filter).FirstAsync();
        }
        public async Task UpdateRecord<T>(string table, string id, T record)
        {
            var collections = db.GetCollection<T>(table);
            var result = await collections.ReplaceOneAsync(new BsonDocument("_id", id), record, new ReplaceOptions { IsUpsert = true });
        }

        public async Task DeleteRecord<T>(string table, string id)
        {
            var collections = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("id", id);
            await collections.DeleteOneAsync(filter);
        }
    }
}
