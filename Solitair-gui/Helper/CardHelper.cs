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

    private static string[] _spades =   new[] {"🂡","🂢","🂣","🂤","🂥","🂦","🂧","🂨","🂩","🂪","🂫","🂭","🂮"};
    private static string[] _hearts =   new[] {"🂱","🂲","🂳","🂴","🂵","🂶","🂷","🂸","🂹","🂺","🂻","🂽","🂾"};
    private static string[] _diamonds = new[] {"🃁","🃂","🃃","🃄","🃅","🃆","🃇","🃈","🃉","🃊","🃋","🃍","🃎"};
    private static string[] _clubs =    new[] {"🃑","🃒","🃓","🃔","🃕","🃖","🃗","🃘","🃙","🃚","🃛","🃝","🃞"};


    private static string CardToString(Card card)
    {
        //if (!card.IsVisible) return BackCard;
        return card.Suit switch
        {
            Suit.Clubs => _clubs[(int) card.Rank - 1],
            Suit.Diamonds => _diamonds[(int) card.Rank - 1],
            Suit.Hearts => _hearts[(int) card.Rank - 1],
            Suit.Spades => _spades[(int) card.Rank - 1],
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static Label GetCardLabel(Card card)
    {
        Label label = new Label
        {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            FontSize = 30
        };
        if (!card.IsVisible)
        {
            label.Foreground = Brushes.Yellow; 
        }
        else
        {
            if (card.Suit is Suit.Hearts or Suit.Diamonds)
            {
            
                label.Foreground = Brushes.Red; 
            }
        }
        

        label.Content = CardToString(card)+ " " + card;
        return label;
    }
    
}