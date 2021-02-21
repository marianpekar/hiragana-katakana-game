public enum ActionType {
    Unspeficied = 0,
    GameEnds = 1,
}

public interface IObserver
{
    void OnNotify(ISubject subject, ActionType actionType = 0); 
}
