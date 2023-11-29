using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBManager
{
    public class MongoDBDataSetRepository
    {
        private string connectionString;
        private IMongoClient dbClient;
        private IMongoDatabase database;

        public MongoDBDataSetRepository(string connectionString)
        {
            this.connectionString = connectionString;
            dbClient = new MongoClient(connectionString);
            database = dbClient.GetDatabase("BelgicaWoensdag");
        }
        public void WriteDataSets(List<MongoDBDataSet> dataSets)
        {
            var collection = database.GetCollection<MongoDBDataSet>("datasets");
            collection.InsertMany(dataSets);
        }
    }
}
