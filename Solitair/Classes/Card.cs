using Solitair.Domains.Enums;

namespace Solitair.Classes;

public class Card
{
    public Suit Suit { get; set; }
    
    public Rank Rank { get; set; }

    public bool IsVisible { get; set; } = false;
    
    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }



    public bool IsCompatible(Card cardToAdd)
    {
        if (this.Suit is not (Suit.Diamonds or Suit.Hearts) || cardToAdd.Suit is not (Suit.Clubs or Suit.Spades)) return false;
        return this.Rank.Equals(cardToAdd.Rank-1);
    }

    public override string ToString()
    {
        return $"Card:[{Suit},{Rank}]\r\n";
    }
}