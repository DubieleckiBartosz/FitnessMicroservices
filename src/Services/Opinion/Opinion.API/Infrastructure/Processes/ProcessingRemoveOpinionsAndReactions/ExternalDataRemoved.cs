using Fitness.Common.EventStore;
using Fitness.Common.EventStore.Events;
using Opinion.API.Constants;

namespace Opinion.API.Infrastructure.Processes.ProcessingRemoveOpinionsAndReactions;
[EventQueue(routingKey: Keys.DataRemovedKey)]

public record ExternalDataRemoved(Guid TrainingId) : IEvent;