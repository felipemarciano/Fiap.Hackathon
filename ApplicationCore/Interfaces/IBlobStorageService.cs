namespace ApplicationCore.Interfaces
{
    public interface IBlobStorageService
    {
        string Upload(byte[] base64Video);
    }
}
