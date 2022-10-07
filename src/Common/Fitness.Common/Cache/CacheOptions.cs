namespace Fitness.Common.Cache;

public class CacheOptions
{
    public bool Enabled { get; set; }
    public int DefaultTime { get; set; }
    public string RedisConnection { get; set; }
}