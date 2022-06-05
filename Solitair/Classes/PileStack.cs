using Solitair.Domains.Exceptions;

namespace Solitair.Classes;

public class PileStack : IStack
{
    public readonly List<Card?> Pile;

    private Card? _talon;
    
    public PileStack(List<Card?> cards)
    {
        foreach (Card? card in cards.Where(card => !card.IsVisible))
        {
            card.IsVisible = true;
        }
        
        Pile = cards;
        _talon = null;
    }

    public void MoveCardToTalon()
    {
        
        if (_talon != null)
        {
            Pile.Add(_talon);
        }
        if (Pile.Count == 0 )
        {
            return;
        }
        _talon = Pile.First();
        Pile.Remove(_talon);

    }
    
    public Card? GetCard()
    {
        
        return _talon;
    }

    public bool TryMoveTo(Card? card)
    {
        return false;
    }

    public bool TryMoveStackTo(IList<Card?> cards)
    {
        return false;
    }

    public Card? MoveTop()
    {
        Card? card = GetCard()!;
        _talon = null;
        return card;
    }

    public List<Card?> MoveStack(int count)
    {
        List<Card?> movedTalon = new List<Card?> {GetCard()!};
        _talon = null;
        return movedTalon;
    }

    public void MoveTo(Card? card)
    {
        throw new IllegalMoveException("You can't move a card to the pile.");
    }

    public void MoveStackTo(IList<Card?> cards)
    {
        throw new IllegalMoveException("You can't move a card's to the pile.");
    }

    public override string ToString()
    {
        return "Pile";
    }
}