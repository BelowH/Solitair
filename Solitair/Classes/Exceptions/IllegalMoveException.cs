namespace Solitair.Domains.Exceptions;

public class IllegalMoveException : Exception
{
    private const string IllegalMoveMessage = "This move was illegal";

    public string Reason;
    
    public IllegalMoveException(string reason = "") : base(IllegalMoveMessage)
    {
        Reason = reason;
    }

}