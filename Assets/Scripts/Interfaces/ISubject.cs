public interface ISubject
{
    void NotifyObservers();
    void RegisterObserver(IObserver observer);
    void UnregisterObserver(IObserver observer);
}
