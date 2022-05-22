using Solitair.Domains.Enums;

namespace Solitair.Classes;

public class GuiPlayer : IPlayer
{
    public readonly PlayingField PlayingField;
    public event EventHandler<PlayingField>? CurrentField;

    public event EventHandler<string>? NewStatus; 

    private int _round;
    public GuiPlayer()
    {
        _round = 1;
        PlayingField = new PlayingField();
    }
    
    public void PlayRound()
    {
        try
        {
            if (PlayingField.GetGameState() == GameState.PLAYING)
            {
                List<Move> moves = PlayingField.GetLegalMoves();
                moves = moves.OrderByDescending(m => m.PossibleScore).ToList();
                Console.WriteLine($"-----Round {_round}-----");
                foreach (Move move in moves)
                {
                    Console.WriteLine(move);
                }
                if (moves.Count == 0 || moves.First().PossibleScore == -15)
                {
                    /*
                    if (PlayingField._pile.Pile.Count <= _cycleCount && moves.Count == 0)
                    {
                        OnNewStatus("Game end! Status: Stuck.");
                        return;
                    }
                    */

                    if(moves.Count > 0)
                    {
                        moves.First().ExecuteMove();
                        OnNewStatus(moves[0].ToString());
                        OnRoundPlayed();
                        return;
                    }
                    OnNewStatus("Cycling Pile.");
                    PlayingField.CyclePile();
                   
                }
                else
                {
                    Random rng = new Random();
                    int index = rng.Next(moves.Count);
                    OnNewStatus(moves[index].ToString());
                    PlayingField.ExecuteMove(moves[index]);
                }
                
            }

            _round++;
            OnRoundPlayed();
        }
        catch (Exception e)
        {
            OnNewStatus("Error: " + e);
        }
    }

    protected virtual void OnRoundPlayed()
    {
        CurrentField?.Invoke(this, PlayingField);
    }

    protected virtual void OnNewStatus(string e)
    {
        NewStatus?.Invoke(this, e);
    }
}