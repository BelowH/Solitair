using Solitair.Domains;
using Solitair.Domains.Exceptions;

namespace Solitair.Classes;

public class Move
{

    public int PossibleScore;
    
    private List<Card> _cardsToMove;

    private IStack _provider;

    private IStack _receiver;

    private bool _movePossible;
        
    
    public Move(List<Card> cardsToMove, IStack provider, IStack receiver, int score)
    {
        _cardsToMove = cardsToMove;
        _provider = provider;
        _receiver = receiver;
        PossibleScore = score;
    }

    public Move(Card cardToMove, IStack provider, IStack receiver, int score)
    {
        _cardsToMove = new List<Card>{cardToMove};
        _provider = provider;
        _receiver = receiver;
        PossibleScore = score;
    }

    public bool TryMove()
    {
        _movePossible = _cardsToMove.Count == 1 ? _receiver.TryMoveTo(_cardsToMove[0]) : _receiver.TryMoveStackTo(_cardsToMove);

        return _movePossible;
    }

    public void ExecuteMove()
    {
        try
        {
            if (!_movePossible) return;
        
            if (_cardsToMove.Count == 1)
            {
                Card card = _provider.MoveTop();
                _receiver.MoveTo(card);
            }
            else
            {
                List<Card> cards = _provider.MoveStack(_cardsToMove.Count);
                _receiver.MoveStackTo(cards);
            }
        }
        catch (IllegalMoveException e)
        {
            string reason = "";
            if (!string.IsNullOrWhiteSpace(e.Reason))
            {
                reason = "Reason: " + e.Reason;
            }
            Console.WriteLine(e.Message + reason);
        }
    }

    public override string ToString()
    {
        return "Trying to move: " + _cardsToMove + " form " + _provider + " to " + _receiver;
    }
}

