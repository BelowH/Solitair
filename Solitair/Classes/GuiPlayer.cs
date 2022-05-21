using Solitair.Domains.Enums;

namespace Solitair.Classes;

public class GuiPlayer : IPlayer
{
    public readonly PlayingField PlayingField;
    public event EventHandler<PlayingField>? CurrentField; 

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
                Random rng = new Random();
                int index = rng.Next(moves.Count);
                PlayingField.ExecuteMove(moves[index]);
            }
            OnRoundPlayed();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected virtual void OnRoundPlayed()
    {
        CurrentField?.Invoke(this, PlayingField);
    }
}