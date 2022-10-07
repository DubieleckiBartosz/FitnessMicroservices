namespace Fitness.Common.Cache
{
    public interface ICacheRepository
    {
        Task<bool> DeleteAsync(string key);
        Task<T?> GetAsync<T>(string key);
        Task<bool> SetAsync<T>(string key, T data, TimeSpan cacheTime, TimeSpan? slidingTime);
    }
}
