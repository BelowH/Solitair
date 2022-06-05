using Solitair.Classes.Enums;
using Solitair.Domains.Enums;
using Solitair.Domains.Exceptions;

namespace Solitair.Classes;

public class SuitStack : IStack
{
    public Suit Suit;

    public List<Card?> Cards;

    public bool IsFull;

    public SuitStack(Suit suit)
    {
        Cards = new List<Card?>();
        Suit = suit;
    }

    public bool AddCard(Card? card)
    {
        if (card != null && card.Suit != Suit)
        {
            return false;
        }

        if (card != null && Cards.Count == 0 && card.Rank == Rank.Ace)
        {
            Cards.Add(card);
            return true;
        }

        if (card != null && card.Rank != Cards.Last()!.Rank + 1) return false;
        
        
        Cards.Add(card);
        if (card != null && card.Rank == Rank.K)
        {
            IsFull = true;
        }
        return true;

    }

    

    public Card? GetCard()
    {
        return Cards.Count > 0 ? Cards.Last() : null;
    }

    public int GetVisibleStackSize()
    {
        return 1;
    }

    public bool TryMoveTo(Card? card)
    {
        if (card != null && card.Suit != Suit)
        {
            return false;
        }

        return card != null && Cards.Count switch
        {
            0 when card.Rank == Rank.Ace => true,
            0 => false,
            _ => card.Rank == Cards.Last()!.Rank + 1
        };
    }

    public bool TryMoveStackTo(IList<Card?> cards)
    {
        return false;
    }

    public Card? MoveTop()
    {
        Card? card = Cards.Last();
        Cards.Remove(Cards.Last());
        return card;
    }

    public List<Card?> MoveStack(int count)
    {
        throw new IllegalMoveException("you can only move one card from a the suit stack.");
    }

    public void MoveTo(Card? card)
    {
        Cards.Add(card);
        if (card is {Rank: Rank.K})
        {
            IsFull = true;
        }
    }

    public void MoveStackTo(IList<Card?> cards)
    {
        throw new IllegalMoveException();
    }

    public override string ToString()
    {
        return Suit + " Stack";
    }
    
}