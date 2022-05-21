using Solitair.Domains;

namespace Solitair.Classes;

public class CardStack : IStack
{

    public List<Card> Cards { get; set; }

    private int _initialSize;

    private readonly int maxSize;
    
    public CardStack( List<Card> initCards)
    {
        _initialSize = initCards.Count;
        Cards = initCards;
        Cards.First().IsVisible = true;
        maxSize = _initialSize + 12;
    }
    
    public bool AddCards(List<Card> cards)
    {
        if (!Cards.First().IsCompatible(cards.First()) || (cards.Count + Cards.Count) >= maxSize) return false;
        Cards.AddRange(cards);
        return true;
    }
    
    public Card GetCard()
    {
        return Cards.First();
    }

    public int GetMoveableStackSize()
    {
        IList<Card> visibleCards = Cards.Where(card => card.IsVisible).ToList();
        return visibleCards.Count;
    }

    public bool TryMoveTo(Card card)
    {
        return Cards.First().IsCompatible(card) && (1 + Cards.Count) < maxSize;
    }

    public bool TryMoveStackTo(IList<Card> cards)
    {
        return Cards.First().IsCompatible(cards.First()) && (cards.Count + Cards.Count) < maxSize;
    }

    public Card MoveTop()
    {
        Card card = Cards.First();
        Cards.Remove(card);
        return card;
    }

    public List<Card> MoveStack(int count)
    {
        List<Card> cards = Cards.GetRange(0, count);
        Cards.RemoveRange(0,count);
        return cards;
    }

    public void MoveTo(Card card)
    {
        Cards.Add(card);
    }

    public void MoveStackTo(IList<Card> cards)
    {
        Cards.AddRange(cards);
    }

    public List<Card> PickUpCards(int pos)
    {
        List<Card> pickedCards = Cards.GetRange(pos, Cards.Count - pos);
        return pickedCards;
    }

    public void RemoveCards(int pos)
    {
        Cards.RemoveRange(pos,Cards.Count - pos);
    }

}