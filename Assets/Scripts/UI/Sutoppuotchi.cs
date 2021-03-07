using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Sutoppuotchi : MonoBehaviour, IObserver
{

    [SerializeField]
    private GameManager gameManager = null;

    [SerializeField]
    private Text stopwatchText = null;

    [SerializeField]
    private PersistenceManager persistenceManager = null;

    private readonly Stopwatch stopwatch = new Stopwatch();

    private void Awake()
    {
        gameManager.RegisterObserver(this);
    }

    public void Run()
    {
        stopwatch.Start();
    }

    public void Reset()
    {
        stopwatch.Reset();
    }

    public void Stop()
    {
        stopwatch.Stop();
    }

    private void FixedUpdate()
    {
        if (!stopwatch.IsRunning) return;

        stopwatchText.text = string.Format("{0:00}\n{1:00}", stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds);
    }

    public void OnNotify(ISubject subject, ActionType actionType = ActionType.Unspeficied)
    {
        switch (actionType)
        {
            case ActionType.GameStarts:
                Reset();
                Run();
                break;
            case ActionType.GameEnds:
                EvaluateTime();
                Stop();
                break;
            default:
                break;
        }
    }

    private void EvaluateTime() {
        if(stopwatch.Elapsed < persistenceManager.BestTime || persistenceManager.BestTime.Equals(TimeSpan.Zero)) {
            persistenceManager.BestTime = stopwatch.Elapsed;
        }
    }
}