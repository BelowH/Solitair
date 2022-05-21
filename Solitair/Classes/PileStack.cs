using Solitair.Domains;

namespace Solitair.Classes;

public class PileStack : IStack
{
    private List<Card> Pile;

    private List<Card> Talon;
    
    private event EventHandler PileEmpty;

    public PileStack(List<Card> cards)
    {
        Pile = cards;
        Talon = new List<Card>();
    }

    public void MoveCardToTalon()
    {
        if (Pile.Count + Talon.Count  == 0)
        {
            return;
        }
        
        if (Pile.Count < 1)
        {
            Pile = Talon;
            Talon.Clear();
        }
        Card card = Pile.First();
        card.IsVisible = true;
        Pile.RemoveAt(0);
        Talon.Add(card);
    }

    public Card GetCard()
    {
        return Talon.Count > 0 ? Talon.First() : null!;
    }

    public int GetMoveableStackSize()
    {
        return 1;
    }

    public bool TryMoveTo(Card card)
    {
        return false;
    }

    public bool TryMoveStackTo(IList<Card> cards)
    {
        return false;
    }

    public Card MoveTop()
    {
        return GetCard();
    }

    public List<Card> MoveStack(int count)
    {
        return new List<Card> { GetCard() };
    }

    public void MoveTo(Card card)
    {
        throw new NotImplementedException();
    }

    public void MoveStackTo(IList<Card> cards)
    {
        throw new NotImplementedException();
    }

    public Card RemoveFromTalon()
    {
        Card card = GetCard();
        Talon.Remove(card);
        return card;
    }
    
}