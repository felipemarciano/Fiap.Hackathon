namespace Fiap.Hackaton.API.Models.Request
{
    public class UploadProcessRequest
    {
        public string Name { get; set; }
        public byte[] Base64Video { get; set; }
    }
}
