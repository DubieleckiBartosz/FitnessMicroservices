using Opinion.API.Domain;
using Opinion.API.Domain.Enums;

namespace Opinion.API.Application.Views;

public class ReactionViewModel
{
    public long Id { get; }
    public long? OpinionId { get; }
    public Guid? ReactionFor { get;  }
    public string User { get;  }
    public ReactionType ReactionType { get;  }

    private ReactionViewModel(long id, long? opinionId, Guid? reactionFor, string user, ReactionType reactionType)
    {
        Id = id;
        OpinionId = opinionId;
        ReactionFor = reactionFor;
        User = user;
        ReactionType = reactionType;
    }

    public static ReactionViewModel Map(Reaction reaction)
    {
        return new ReactionViewModel(reaction.Id, reaction.OpinionId, reaction.ReactionFor, reaction.User,
            reaction.ReactionType);
    }
}