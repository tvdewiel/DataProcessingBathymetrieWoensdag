using DataSetManager;
using MongoDBManager;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleAppMongoDBrepo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string source = @"C:\data\belgica\belgica.txt";
            string campaign = "Juli 2023";
            string dataSeries = "ModelTest ID 1";

            string conn = "mongodb://localhost:27017";
            MongoDBDataSetRepository repo=new MongoDBDataSetRepository(conn);


            var data = FileDataSetManager.ReadData(@"C:\data\belgica\belgica.txt");
            var sets = FileDataSetManager.MakeDataSetsWithTestSet(data.data, new List<int> { 50000, 200000, 25000, 50000, 10000, 15000, 20000, 2500 });

            DataSetMetaInfo meta = new DataSetMetaInfo(sets[0].data.Count, source, campaign, dataSeries, DataSetType.TestSet);
            List<MongoDBDataSet> dataSets = new List<MongoDBDataSet>();

            MongoDBDataSet mdDS = new MongoDBDataSet(sets[0], meta); //test set
            dataSets.Add(mdDS);

            for(int i=1;i<sets.Count;i++)
            {
                meta=new DataSetMetaInfo(sets[i].data.Count, source, campaign, dataSeries, DataSetType.DataSet);
                mdDS=new MongoDBDataSet(sets[i], meta);
                dataSets.Add(mdDS);
            }
            repo.WriteDataSets(dataSets);
            Console.WriteLine("end");
        }
    }
}