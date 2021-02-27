public enum ActionType {
    Unspeficied,
    GameStarts,
    GameEnds
}

public interface IObserver
{
    void OnNotify(ISubject subject, ActionType actionType = ActionType.Unspeficied); 
}
