namespace MongoExample.Data.Config
{
    public interface IDatabaseSettings
    {
        string Books { get; set; }
        string Branch { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
    public class DatabaseSettings : IDatabaseSettings
    {
        public string Books { get; set; }
        public string Branch { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
