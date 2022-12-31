using Opinion.API.Domain;

namespace Opinion.API.Application.Views;

public record OpinionViewModel
{
    public long Id { get; }
    public string Creator { get; }
    public Guid OpinionFor { get; }
    public List<ReactionViewModel>? Reactions { get; }
    public string Comment { get; }

    private OpinionViewModel(long id, string creator, Guid opinionFor, List<ReactionViewModel>? reactions, string comment)
    {
        Id = id;
        Creator = creator;
        OpinionFor = opinionFor;
        Reactions = reactions;
        Comment = comment;
    }

    public static OpinionViewModel Map(Domain.Opinion opinion)
    {
        var reactions = opinion.Reactions?.Select(ReactionViewModel.Map).ToList();
        return new OpinionViewModel(opinion.Id, opinion.Creator, opinion.OpinionFor, reactions, opinion.Comment);
    }
}