using Opinion.API.Domain.Enums;

namespace Opinion.API.Domain;

public class Reaction
{
    public long Id { get; private set; }
    public long? OpinionId { get; private set; }
    public Guid? ReactionFor { get; private set; }
    public int UserId { get; private set; } 
    public ReactionType ReactionType { get; private set; }

    public Reaction()
    {
    }

    private Reaction(long? opinionId, Guid? reactionFor, int userId, ReactionType reactionType)
    {
        OpinionId = opinionId;
        ReactionFor = reactionFor;
        UserId = userId;
        ReactionType = reactionType;
    }

    public static Reaction Create(long? opinionId, int userId, ReactionType reactionType)
    {
        return new Reaction(opinionId, null, userId, reactionType);
    }

    public static Reaction Create(Guid? reactionFor, int userId, ReactionType reactionType)
    {
        return new Reaction(null, reactionFor, userId, reactionType);
    }
}