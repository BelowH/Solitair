namespace Solitair.Classes;

public interface IPlayer
{

    public event EventHandler<PlayingField>? CurrentField; 
    
    public void PlayRound();

}