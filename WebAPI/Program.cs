using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Text;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddConnections();
builder.Services.AddControllers();

builder.Services.AddDbContext<WebAPI.BlobData>(opt => opt.UseInMemoryDatabase("VeritasDB"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/GetSQLDatabase", () =>
    {
        string connectionString = "Server=tcp:mydatabaseforveritas.database.windows.net,1433;Initial Catalog=VeritasDatabase;Persist Security Info=False;User ID=reshma;Password=admin12345!@#$%;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        string dbname = conn.Database;
        SqlCommand command;
        SqlDataReader reader;
        string sql, output = "";

        sql = "select * from [SalesLT].[Customer]";
        command = new SqlCommand(sql, conn);
        reader = command.ExecuteReader();
        FileStream f = new FileStream("DownloadedData", FileMode.CreateNew);

        while (reader.Read())
        {
            int cnt = 0;
            output = "";
            while( cnt < reader.FieldCount)
            {
                output = output + reader.GetValue(cnt++).ToString() + ", ";  
            }
            byte[] info = new UTF8Encoding(true).GetBytes(output);
            f.Write(info, 0, info.Length);
        }
        f.Flush();
        reader.Close();
        command.Dispose();
        conn.Close();
        f.Close();
        
        return ("Connection Open to \n");
    }
);


app.MapPut("/UploadToBlob", (String dataToupload, BlobData blobdata ) =>
{
    dataToupload = "DownloadedData";
    BlobContainerClient container = new BlobContainerClient(blobdata.connectionString, blobdata.containerName);
    container.Create();
    BlobClient blob = container.GetBlobClient(blobdata.blobName);
    blob.Upload( dataToupload);
}
);

app.Run();

