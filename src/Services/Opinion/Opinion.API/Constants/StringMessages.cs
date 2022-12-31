namespace Opinion.API.Constants;

public class StringMessages
{
    //Info messages
    public const string NewOpinionCreatedMessage = "Opinion has been created."; 
    public const string ReactionAddedMessage = "Reaction has been added."; 

    //Exception messages
    public const string ReactionNotFoundMessage = "Reaction cannot be null.";
    public const string OpinionNotFoundMessage = "Opinion cannot be null.";
    public const string NoPermissionsToDeleteCommentMessage = "You do not have permission to delete a comment.";

    //Exception titles
    public const string ReactionNotFoundTitle = "Reaction not found";
    public const string OpinionNotFoundTitle = "Opinion not found";
    public const string NoPermissionsToDeleteCommentTitle = "No permissions";
}