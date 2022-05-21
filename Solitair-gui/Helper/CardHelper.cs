using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Solitair.Classes;
using Solitair.Domains.Enums;

namespace Solitair_gui.Helper;

public class CardHelper
{
    private const string BackCard = "🂠";
    public const string EmptyField = " ";

    private static string[] _spades = new[] {"🂡","🂢","🂣","🂤","🂥","🂦","🂧","🂨","🂩","🂪","🂫","🂬","🂭","🂮"};
    private static string[] _hearts = new[] {"🂱","🂲","🂳","🂴","🂵","🂶","🂷","🂸","🂹","🂺","🂻","🂼","🂽","🂾"};
    private static string[] _diamonds = new[] {"🃁","🃂","🃃","🃄","🃅","🃆","🃇","🃈","🃉","🃊","🃋","🃌","🃍","🃎"};
    private static string[] _clubs = new[] {"🃑","🃒","🃓","🃔","🃕","🃖","🃗","🃘","🃙","🃚","🃛","🃜","🃝","🃞"};


    private static string CardToString(Card card)
    {
        if (card.IsVisible)
        {
            switch (card.Suit)
            {
                case Suit.Clubs:
                    return _clubs[(int) card.Rank - 1];
                case Suit.Diamonds:
                    return _diamonds[(int) card.Rank - 1];
                case Suit.Hearts:
                    return _hearts[(int) card.Rank - 1];
                case Suit.Spades:
                    return _spades[(int) card.Rank - 1];
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return BackCard;
    }

    public static Label GetCardLabel(Card card)
    {
        Label label = new Label
        {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            FontSize = 70
        };
        if (card.Suit is Suit.Hearts or Suit.Diamonds)
        {
            if (card.IsVisible)
            {
                label.Foreground = Brushes.Red; 
            }
            
        }

        label.Content = CardToString(card); //+ " " + card;
        return label;
    }
    
}