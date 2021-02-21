using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StoneEffectsController))]
public class Stone : MonoBehaviour, ISubject
{
    private StoneProperties stoneProperties = new StoneProperties();

    private StoneEffectsController stoneEffectsController;

    [SerializeField]
    float DelayBeforeDisableAfterDissolve = 10.0f;

    public bool IsDisolving { get; set; }

    public void SetSign(Sign sign) => stoneProperties.Sign = sign;
    public Sign GetSign() => stoneProperties.Sign;

    public void SetAlphabet(Alphabet type) => stoneProperties.Alphabet = type;
    public Alphabet GetAlphabet() => stoneProperties.Alphabet;

    private void Awake()
    {
        stoneEffectsController = GetComponent<StoneEffectsController>();
    }

    private void OnEnable()
    {
        IsDisolving = false;
    }

    public void Dissolve() {
        IsDisolving = true;

        NotifyObservers();

        stoneEffectsController.Dissolve();
        Invoke(nameof(Disable), DelayBeforeDisableAfterDissolve);
    }

    private void Disable() {
        this.gameObject.SetActive(false);
    }

    private List<IObserver> observers = new List<IObserver>();

    public void NotifyObservers()
    {
        foreach(var observer in observers) {
            observer.OnNotify(this);
        }
    }

    public void RegisterObserver(IObserver observer)
    {
        if(!observers.Contains(observer))
            observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        observers.Remove(observer);
    }
}
