namespace ApplicationCore.Settings
{
    public class Configuration
    {
        public const string SECTION = "Configuration";
        public BlobStorageConfiguration BlobStorageSettings { get; set; }
    }

    public class BlobStorageConfiguration
    {
        public string Container { get; set; }
    }
}
