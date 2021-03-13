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
    private Trophy trophy = null;

    private PersistenceManager persistenceManager;

    private readonly Stopwatch stopwatch = new Stopwatch();

    private void Awake()
    {
        persistenceManager = PersistenceManager.Instance;

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

    private void Start()
    {
        InvokeRepeating(nameof(Step), 1.0f, 1.0f);
    }

    private void Step() {
        if (!stopwatch.IsRunning) return;

        stopwatchText.text = string.Format("{0:00}\n{1:00}", stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds);

        EvaluateTrophy();
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

    private void EvaluateTrophy() {
        if(persistenceManager.BestTime.Equals(TimeSpan.Zero)) {
            return;
        }

        if (stopwatch.Elapsed > persistenceManager.BestTime && trophy.IsVisible)
        {
            trophy.Show(false);
        }
        else if (stopwatch.Elapsed < persistenceManager.BestTime && !trophy.IsVisible) {
            trophy.Show(true);
        }
    }
}