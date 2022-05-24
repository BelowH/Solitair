using Solitair.Domains.Enums;
using Solitair.Helper;

namespace Solitair.Classes;
/// <summary>
///  scoring:
///      10 per card to suit stack
///       5 per card form pile to build
///       5 per new visible card
///       3 move between build stacks
///       2 cycle pile
///     -15 per card form suit to 
/// </summary>
public class PlayingField
{

    public PileStack _pile;
    
    public SuitStack _spadesDeck;

    public SuitStack _clubsDeck;

    public SuitStack _diamondsDeck;

    public SuitStack _heartsDeck;

    public CardStack _buildStack1;

    public CardStack _buildStack2;

    public CardStack _buildStack3;
    
    public CardStack _buildStack4;
    
    public CardStack _buildStack5;
    
    public CardStack _buildStack6;
    
    public CardStack _buildStack7;

    private List<Move> _moves;

    public PlayingField()
    {
        _spadesDeck = new SuitStack(Suit.Spades);
        _clubsDeck = new SuitStack(Suit.Clubs);
        _diamondsDeck = new SuitStack(Suit.Diamonds);
        _heartsDeck = new SuitStack(Suit.Hearts);
        _moves = new List<Move>();
        
        List<Card> deck = DeckHelper.GetFullDeck();
        _buildStack1 = new CardStack("buildStack1",deck.GetRange(0, 1));
        deck.RemoveRange(0,1);
        _buildStack2 = new CardStack("buildStack2",deck.GetRange(0, 2));
        deck.RemoveRange(0,2);
        _buildStack3 = new CardStack("buildStack3",deck.GetRange(0, 3));
        deck.RemoveRange(0,3);
        _buildStack4 = new CardStack("buildStack4",deck.GetRange(0, 4));
        deck.RemoveRange(0,4);
        _buildStack5 = new CardStack("buildStack5",deck.GetRange(0, 5));
        deck.RemoveRange(0,5);
        _buildStack6 = new CardStack("buildStack6",deck.GetRange(0, 6));
        deck.RemoveRange(0,6);
        _buildStack7 = new CardStack("buildStack7",deck.GetRange(0, 7));
        deck.RemoveRange(0,7);

        _pile = new PileStack(deck);
        
    }

    public List<Move> GetLegalMoves()
    {
        List<Move> allMoves = new List<Move>();

        List<CardStack> buildStacks = new List<CardStack>()
        {
            _buildStack1,
            _buildStack2,
            _buildStack3,
            _buildStack4,
            _buildStack5,
            _buildStack6,
            _buildStack7,
        };

        List<SuitStack> suitStacks = new List<SuitStack>()
        {
            _clubsDeck,
            _heartsDeck,
            _spadesDeck,
            _diamondsDeck
        };
        
        #region SCORE: 10 try move form buildStack or pile to suit stack
        foreach (CardStack buildStack in buildStacks)
        {
            Card topCard = buildStack.GetCard();
            foreach (SuitStack suitStack in suitStacks)
            {
                Move move = new Move(topCard, buildStack, suitStack, 10);
                if (DetectLoop(move))
                {
                    continue;
                }
                try
                {
                    if (move.TryMove())
                    {
                        allMoves.Add(move);
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        if (allMoves.Count > 0)
        {
            return allMoves;
        }
        

        #endregion

        #region  SCORE: 5 try move form pile to build stack or suit stack

        //This should only apply for the first search 
        Card topPile;
        if (_pile.GetCard() != null)
        {
            topPile = _pile.GetCard()!;
        }
        else
        {
            _pile.MoveCardToTalon(); 
            topPile = _pile.GetCard()!;
        }
        
        

        foreach (CardStack buildStack in buildStacks)
        {
            Move move = new Move(topPile, _pile, buildStack, 5);
            if (DetectLoop(move))
            {
                continue;
            }
            try
            {
                if (move.TryMove())
                {
                    allMoves.Add(move);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        foreach (SuitStack suitStack in suitStacks)
        {
            Move move = new Move(topPile, _pile, suitStack, 5);
            if (DetectLoop(move))
            {
                continue;
            }
            try
            {
                if (move.TryMove())
                {
                    allMoves.Add(move);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        if (allMoves.Count > 0)
        {
            return allMoves;
        }
        

        #endregion

        #region SCORE:  3 try move form build stack to build stack (every combination)
        for (int i = 0; i < 7; i++ ) //provider deck counter
        {
            int nums = buildStacks[i].GetVisibleStackSize();
            for (int j = 1; j <= nums; j++)
            {
                for (int k = 0; k < 7; k++) // receiver deck counter
                {
                    if (k == i) continue;
                    List<Card> cards = buildStacks[i].PickUpCards(j);
                    Move move = new Move(cards, buildStacks[i], buildStacks[k], 3);
                    if (DetectLoop(move))
                    {
                        continue;
                    }
                    try
                    {
                        if (move.TryMove())
                        {
                            allMoves.Add(move);
                        }
                    }
                    catch (Exception)
                    {
                        //ignored
                    }
                }
            }
        }

        if (allMoves.Count > 0)
        {
            return allMoves;
        }
        return allMoves;
        #endregion

        #region SCORE:-15 try move card form suit stack to build stack
        /*
        foreach (SuitStack suitStack in suitStacks)
        {
            Card card = suitStack.GetCard()!;
            if (card == null)
            {
                continue;
            }
            foreach (CardStack buildStack in buildStacks)
            {
                Move move = new Move(card, suitStack, buildStack, -15);
                if (DetectLoop(move))
                {
                    continue;
                }
                try
                {
                    if (move.TryMove())
                    {
                        allMoves.Add(move);
                    }
                }
                catch (Exception)
                {
                    //ignored
                }
            }
        }

        return allMoves;
        */
        #endregion
        
    }

    public void CyclePile()
    {
        _pile.MoveCardToTalon();
    }

    public void ExecuteMove(Move move)
    {
        move.ExecuteMove();
        _moves.Add(move);
    }

    private bool DetectLoop(Move moveToTry)
    {
        return Enumerable.Contains(_moves, moveToTry);
    }
    
    public GameState GetGameState()
    {
        if (_heartsDeck.IsFull && _spadesDeck.IsFull && _clubsDeck.IsFull && _diamondsDeck.IsFull)
        {
            return GameState.FINISHED;
        }

        return GameState.PLAYING;
    }

}