using Newtonsoft.Json;

namespace Opinion.API.Application.Parameters;

public class RemoveReactionParameters
{
    public long ReactionId { get; init; }

    public RemoveReactionParameters()
    {
    }

    [JsonConstructor]
    public RemoveReactionParameters(long reactionId)
    {
        ReactionId = reactionId;
    }
}