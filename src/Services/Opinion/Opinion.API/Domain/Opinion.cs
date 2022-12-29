namespace Opinion.API.Domain;

public class Opinion
{
    public int Id { get; set; }
    public Guid OpinionFor { get; set; }
    public List<Like> Likes { get; set; }
    public string Description { get; set; }
    //[TODO]
}