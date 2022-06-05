using Solitair.Classes;
using Solitair.Classes.Enums;
using Solitair.Domains;
using Solitair.Domains.Enums;

namespace Solitair.Helper;

public static class DeckHelper
{


    public static List<Card?> GetFullDeck()
    {

        IEnumerable<Suit> siutList = Enum.GetValues(typeof(Suit)).Cast<Suit>();
        IEnumerable<Rank> rankList = Enum.GetValues(typeof(Rank)).Cast<Rank>();
        List<Card?> cards = (from suit in siutList from rank in rankList select new Card(suit, rank)).ToList();
        Random rng = new Random();
        
        return cards.OrderBy(c => rng.Next()).ToList();
    }

}