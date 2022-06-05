using Solitair.Classes.Enums;
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



    public bool IsCompatible(Card? cardToAdd)
    {
        return cardToAdd.Suit switch
        {
            Suit.Clubs or Suit.Spades when Suit is Suit.Diamonds or Suit.Hearts => Rank.Equals(cardToAdd.Rank + 1),
            Suit.Hearts or Suit.Diamonds when Suit is Suit.Clubs or Suit.Spades => Rank.Equals(cardToAdd.Rank + 1),
            _ => false
        };
    }

    public override string ToString()
    {
        string rank = Rank.ToString();

        return Suit + " " + Rank;
    }
}