using Solitair.Classes.Enums;
using Solitair.Domains;

namespace Solitair.Classes;

public class CardStack : IStack
{

    public List<Card?> Cards { get; set; }

    private readonly int _maxSize;

    private readonly string _name;
    
    public CardStack( string name,List<Card?> initCards)
    {
        _name = name;
        int initialSize = initCards.Count;
        Cards = initCards;
        Cards.First().IsVisible = true;
        _maxSize = initialSize + 12;
    }

    public int GetCardCount()
    {
        return Cards.Count;
    }

    public Card? GetCard()
    {
        return Cards.Count > 0 ? Cards.First() : null;
    }

    public int GetVisibleStackSize()
    {
        IList<Card?> visibleCards = Cards.Where(card => card.IsVisible).ToList();
        return visibleCards.Count;
    }

    public bool TryMoveTo(Card? card)
    {
        if (Cards.Count == 0 && card.Rank == Rank.K)
        {
            return true;
        }
        return Cards.First().IsCompatible(card) && (1 + Cards.Count) < _maxSize;
    }

    public bool TryMoveStackTo(IList<Card?> cards)
    {
        if (Cards.Count == 0 && cards.Last().Rank == Rank.K)
        {
            return true;
        }
        return Cards.First().IsCompatible(cards.Last()) && (cards.Count + Cards.Count) < _maxSize;
    }

    public Card? MoveTop()
    {
        Card? card = Cards.First();
        Cards.Remove(card);
        if (GetVisibleStackSize() == 0 && Cards.Count > 0)
        {
            Cards.First().IsVisible = true;
        }
        return card;
    }

    public List<Card?> MoveStack(int count)
    {
        List<Card?> cards = Cards.GetRange(0, count);
        Cards.RemoveRange(0,count);
        if (GetVisibleStackSize() == 0 && Cards.Count > 0)
        {
            Cards.First().IsVisible = true;
        }
        return cards;
    }

    public void MoveTo(Card? card)
    {
        Cards.Insert(0,card);
    }

    public void MoveStackTo(IList<Card?> cards)
    {
        Cards.InsertRange(0,cards);
    }

    public List<Card?> PickUpCards(int pos)
    {
        
        List<Card?> pickedCards = Cards.GetRange(0, pos);
        return pickedCards;
    }
    
    public override string ToString()
    {
        return _name;
    }
}