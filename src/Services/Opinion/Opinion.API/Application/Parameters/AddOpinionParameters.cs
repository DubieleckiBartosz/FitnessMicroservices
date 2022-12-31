using Newtonsoft.Json;

namespace Opinion.API.Application.Parameters;

public class AddOpinionParameters
{ 
    public Guid OpinionFor { get; init; }
    public string Comment { get; init; }

    public AddOpinionParameters()
    {
    }

    [JsonConstructor]
    public AddOpinionParameters(Guid opinionFor, string comment)
    {
        OpinionFor = opinionFor;
        Comment = comment;
    }
}