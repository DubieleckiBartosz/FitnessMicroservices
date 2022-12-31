using Opinion.API.Domain.Enums;

namespace Opinion.API.Domain;

public class Reaction
{
    public long Id { get; private set; }
    public long? OpinionId { get; private set; }
    public Guid? ReactionFor { get; private set; }
    public string User { get; private set; } 
    public ReactionType ReactionType { get; private set; }

    public Reaction()
    {
    }

    private Reaction(long? opinionId, Guid? reactionFor, string user, ReactionType reactionType)
    {
        OpinionId = opinionId;
        ReactionFor = reactionFor;
        User = user;
        ReactionType = reactionType;
    }

    /// <summary>
    /// Reaction for opinion
    /// </summary>
    /// <param name="opinionId"></param>
    /// <param name="user"></param>
    /// <param name="reactionType"></param>
    /// <returns></returns>
    public static Reaction Create(long? opinionId, string user, ReactionType reactionType)
    {
        return new Reaction(opinionId, null, user, reactionType);
    }

    /// <summary>
    /// Reaction for external data
    /// </summary>
    /// <param name="reactionFor"></param>
    /// <param name="user"></param>
    /// <param name="reactionType"></param>
    /// <returns></returns>
    public static Reaction Create(Guid? reactionFor, string user, ReactionType reactionType)
    {
        return new Reaction(null, reactionFor, user, reactionType);
    }
}