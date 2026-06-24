namespace MapperTeachingDemo.WebAPI.Cashing;

public interface ICacheService
{
    Task<T?> Get<T>(string key);
    Task Set <T>(string key, T value , TimeSpan expiry);
}
