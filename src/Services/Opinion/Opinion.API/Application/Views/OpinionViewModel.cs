using Opinion.API.Domain;

namespace Opinion.API.Application.Views;

public class OpinionViewModel
{
    public long Id { get; private set; }
    public int Creator { get; }
    public Guid OpinionFor { get; }
    public List<ReactionViewModel>? Reactions { get; }
    public string? Comment { get; }
}