namespace Fitness.Common.Outbox.MongoOutbox;

public interface IOutboxStore
{
    Task AddAsync(OutboxMessage message);
    Task<IEnumerable<string>> GetUnprocessedMessageIdsAsync();
    Task SetMessageToProcessedAsync(string id);
    Task DeleteAsync(IEnumerable<string> ids);
    Task<OutboxMessage?> GetMessageAsync(string id);
}