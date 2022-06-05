using Solitair.Domains.Enums;

namespace Solitair.Classes;

public class GuiPlayer : IPlayer
{
    public readonly PlayingField PlayingField;
    public event EventHandler<PlayingField>? CurrentField;

    public event EventHandler<string>? NewStatus; 

    private int _round;

    private int _pileCycles;
    
    public GuiPlayer()
    {
        _round = 1;
        PlayingField = new PlayingField();
    }
    
    public void PlayRound()
    {
        try
        {
            if (_round >= 1000)
            {
                OnNewStatus("Stuck! :O, too many moves");
                return;
            }
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
                    if(moves.Count > 0 && _pileCycles >= PlayingField._pile.Pile.Count)
                    {
                        _pileCycles = 0;
                        PlayingField.ExecuteMove(moves.First());
                        OnNewStatus(moves.First().ToString());
                        OnRoundPlayed();
                        return;
                    }

                    if (_pileCycles >= PlayingField._pile.Pile.Count)
                    {
                        OnNewStatus("Stuck! :O");
                        return;
                    }
                    _pileCycles++;
                    OnNewStatus("Cycling Pile: " + _pileCycles + " out of" + PlayingField._pile.Pile.Count);
                    PlayingField.CyclePile();
                    

                }
                else
                {
                    _pileCycles = 0;
                    OnNewStatus( "Round: " + _round + " " + moves.First());
                    PlayingField.ExecuteMove(moves.First());
                }
                
            }
            else if (PlayingField.GetGameState() == GameState.FINISHED)
            {
                OnNewStatus("Finished! :D");
                return;
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