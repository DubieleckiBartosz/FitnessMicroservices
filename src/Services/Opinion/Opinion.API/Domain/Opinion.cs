using System.Net;
using Opinion.API.Application.Exceptions;
using Opinion.API.Constants;
using Opinion.API.Domain.Common;
using Opinion.API.Domain.Enums;

namespace Opinion.API.Domain;

public class Opinion : BaseEntity
{
    public long Id { get; private set; }
    public string Creator { get; private set; }
    public Guid OpinionFor { get; private set; }
    public List<Reaction>? Reactions { get; private set; }
    public string Comment { get; private set; }

    public Opinion()
    {
    }

    private Opinion(Guid opinionFor, string comment, string creator)
    {
        OpinionFor = opinionFor;
        Comment = comment;
        Creator = creator;
        Reactions = new List<Reaction>();
    }

    public static Opinion Create(Guid opinionFor, string comment, string creator)
    {
        return new Opinion(opinionFor, comment, creator);
    }

    public Reaction NewReaction(string user, ReactionType reactionType)
    {
        var reaction = Reaction.Create(Id, user, reactionType);

        return reaction;
    }

    public void RemoveReaction(string user, long reactionId, bool isAdmin = false)
    {
        var reaction = Reactions?.FirstOrDefault(_ => _.Id == reactionId);

        if (reaction == null)
        {
            throw new OpinionBusinessException(StringMessages.ReactionNotFoundMessage,
                StringMessages.ReactionNotFoundTitle, HttpStatusCode.NotFound);
        }

        if (user != Creator && user != reaction.User && !isAdmin)
        {
            throw new OpinionBusinessException(StringMessages.NoPermissionsToDeleteCommentMessage,
                StringMessages.NoPermissionsTitle, HttpStatusCode.BadRequest);
        }

        Reactions?.Remove(reaction);
    }
}