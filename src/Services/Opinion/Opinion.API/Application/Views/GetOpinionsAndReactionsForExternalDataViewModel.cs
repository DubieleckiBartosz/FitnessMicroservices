namespace Opinion.API.Application.Views;

public record GetOpinionsAndReactionsForExternalDataViewModel 
{
    public List<OpinionViewModel>? Opinions { get; }
    public List<ReactionViewModel>? ReactionsWithoutOpinions { get; }

    private GetOpinionsAndReactionsForExternalDataViewModel(List<OpinionViewModel>? opinions,
        List<ReactionViewModel>? reactionsWithoutOpinions)
    {
        Opinions = opinions;
        ReactionsWithoutOpinions = reactionsWithoutOpinions;
    }

    public static GetOpinionsAndReactionsForExternalDataViewModel Create(List<OpinionViewModel>? opinions,
        List<ReactionViewModel>? reactionsWithoutOpinions)
    {
        return new GetOpinionsAndReactionsForExternalDataViewModel(opinions, reactionsWithoutOpinions);
    }
}