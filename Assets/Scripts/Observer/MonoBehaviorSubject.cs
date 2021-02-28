using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourSubject : MonoBehaviour, ISubject
{
    protected List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        observers.Remove(observer);
    }
}
