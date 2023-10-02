using Amazon.S3;
using Amazon.S3.Model;

namespace ApiImagenes.Services
{
    public class ServiceS3
    {
        private string bucketName;
        private IAmazonS3 awsCLient;

        public ServiceS3(IAmazonS3 cLient,IConfiguration configuration)
        {
            this.awsCLient = cLient;
            this.bucketName = configuration.GetValue<string>("AWS:BucketName");
        }
        public async Task<List<string>> GetFilesAsync()
        {
            ListVersionsResponse response = await this.awsCLient.ListVersionsAsync(this.bucketName);
            return response.Versions.Select(x => x.Key).ToList();
        }

        public async Task<bool> UploadFileAsync(Stream stream, string fileName)
        {
            PutObjectRequest request = new PutObjectRequest
            {
                InputStream = stream,
                Key = fileName,
                BucketName = this.bucketName
            };
            PutObjectResponse response = await this.awsCLient.PutObjectAsync(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
