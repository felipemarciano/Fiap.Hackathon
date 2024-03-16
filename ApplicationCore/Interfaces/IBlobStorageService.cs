namespace ApplicationCore.Interfaces
{
    public interface IBlobStorageService
    {
        Task<string> Upload(byte[] base64Video);
    }
}
