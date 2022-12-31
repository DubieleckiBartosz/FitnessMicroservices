using Newtonsoft.Json;
using Opinion.API.Domain.Enums;

namespace Opinion.API.Application.Parameters;

public class AddReactionToOpinionParameters
{
    public long OpinionId { get; init; }
    public ReactionType ReactionType { get; init; }

    public AddReactionToOpinionParameters()
    {
    }

    [JsonConstructor]
    public AddReactionToOpinionParameters(long opinionId, ReactionType reactionType)
    {
        OpinionId = opinionId;
        ReactionType = reactionType;
    }
}