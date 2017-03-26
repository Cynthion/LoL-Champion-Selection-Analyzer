namespace WebApi.Misc.Interfaces
{
    public interface IApiKey
    {
        bool IsProduction { get; }

        string ApiKey { get; }
    }
}
