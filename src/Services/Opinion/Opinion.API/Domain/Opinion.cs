using System.Net;
using Opinion.API.Application.Exceptions;
using Opinion.API.Constants;
using Opinion.API.Domain.Enums;

namespace Opinion.API.Domain;

public class Opinion
{
    public long Id { get; private set; }
    public string Creator { get; }
    public Guid OpinionFor { get; }
    public List<Reaction>? Reactions { get; }
    public string? Comment { get; }

    public Opinion()
    {
    }

    private Opinion(Guid opinionFor, string? comment, string creator)
    {
        OpinionFor = opinionFor;
        Comment = comment;
        Creator = creator;
        Reactions = new List<Reaction>();
    }

    public static Opinion Create(Guid opinionFor, string? comment, string creator)
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
            throw new OpinionBusinessException(StringMessages.NoPermissionsToDeleteCommentTitle,
                StringMessages.NoPermissionsToDeleteCommentTitle, HttpStatusCode.BadRequest);
        }

        Reactions?.Remove(reaction);
    }
}