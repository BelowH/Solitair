using Solitair.Domains.Enums;

namespace Solitair.Classes;

public class GuiPlayer : IPlayer
{
    public readonly PlayingField PlayingField;
    public event EventHandler<PlayingField>? CurrentField;

    public event EventHandler<string>? NewStatus; 

    private int cycleCount;
    public GuiPlayer()
    {
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
                if (moves.Count == 0 || moves.First().PossibleScore == -15)
                {
                    if (PlayingField._pile.Pile.Count <= cycleCount && moves.Count == 0)
                    {
                        OnNewStatus("Game end! Status: Stuck.");
                        return;
                    }

                    if(PlayingField._pile.Pile.Count <= cycleCount)
                    {
                        moves.First().ExecuteMove();
                        OnNewStatus(moves[0].ToString());
                        OnRoundPlayed();
                        return;
                    }

                    OnNewStatus("Cycling Pile.");
                    PlayingField.CyclePile();
                    cycleCount++;
                }
                else
                {
                    Random rng = new Random();
                    int index = rng.Next(moves.Count);
                    OnNewStatus(moves[index].ToString());
                    PlayingField.ExecuteMove(moves[index]);
                }
                
            }
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