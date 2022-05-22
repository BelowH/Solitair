namespace Solitair.Classes;

public interface IStack
{
    public Card? GetCard();

    public int GetVisibleStackSize();
    
    public bool TryMoveTo(Card card);

    public bool TryMoveStackTo(IList<Card> cards);

    public Card MoveTop();

    public List<Card> MoveStack(int count);

    public void MoveTo(Card card);

    public void MoveStackTo(IList<Card> cards);

    public string ToString();


}