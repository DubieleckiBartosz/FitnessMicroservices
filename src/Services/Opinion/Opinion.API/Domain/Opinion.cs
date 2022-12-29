using System.Net;
using Opinion.API.Application.Exceptions;
using Opinion.API.Constants;
using Opinion.API.Domain.Enums;

namespace Opinion.API.Domain;

public class Opinion
{
    public long Id { get; private set; }
    public int Creator { get; }
    public Guid OpinionFor { get; }
    public List<Reaction>? Reactions { get; }
    public string? Comment { get; }

    public Opinion()
    {
    }

    private Opinion(Guid opinionFor, string? comment, int creator)
    {
        OpinionFor = opinionFor;
        Comment = comment;
        Creator = creator;
        Reactions = new List<Reaction>();
    }

    public static Opinion Create(Guid opinionFor, string? comment, int creator)
    {
        return new Opinion(opinionFor, comment, creator);
    }

    public Reaction NewReaction(int userId, ReactionType reactionType)
    {
        var reaction = Reaction.Create(Id, userId, reactionType);

        return reaction;
    }

    public void RemoveReaction(int userId, long reactionId, bool isAdmin = false)
    {
        var reaction = Reactions?.FirstOrDefault(_ => _.Id == reactionId);

        if (reaction == null)
        {
            throw new OpinionBusinessException(StringMessages.ReactionNotFoundMessage,
                StringMessages.ReactionNotFoundTitle, HttpStatusCode.NotFound);
        }

        if (userId != Creator && userId != reaction.UserId && !isAdmin)
        {
            throw new OpinionBusinessException(StringMessages.NoPermissionsToDeleteCommentTitle,
                StringMessages.NoPermissionsToDeleteCommentTitle, HttpStatusCode.BadRequest);
        }

        Reactions?.Remove(reaction);
    }
}