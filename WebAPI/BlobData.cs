namespace WebAPI
{

    class DB
    {
        public TextReader txtReader;
        public DB() { }
    }

    [Microsoft.EntityFrameworkCore.Keyless]
    class BlobData : Microsoft.EntityFrameworkCore.DbContext
    {
        
        public BlobData(Microsoft.EntityFrameworkCore.DbContextOptions<BlobData> options)
            : base(options) { }

        public string connectionString = "DefaultEndpointsProtocol=https;AccountName=veritasblobstorage;AccountKey=aXicA5SBx0x5GX9edEs+Czm5uOhn/CfhXnxJhEoOgdVSOjlkIJjFEubbJSlSpjPGnKJ/fXY47ojn+AStJYiQvg==;EndpointSuffix=core.windows.net";
        public string containerName = "sample-container";
        public string blobName = "sample-blob";
        public string filePath = "sample-file";

        public DB blobData = new DB();
    }
}