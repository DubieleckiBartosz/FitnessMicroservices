using Newtonsoft.Json;
using Opinion.API.Domain.Enums;

namespace Opinion.API.Application.Parameters;

public class AddReactionParameters
{ 
    public Guid ReactionFor { get; init; }
    public ReactionType ReactionType { get; init; }

    public AddReactionParameters()
    {
    }
    [JsonConstructor]
    public AddReactionParameters(Guid reactionFor, ReactionType reactionType)
    {
        ReactionFor = reactionFor;
        ReactionType = reactionType;
    }
}