using UnityEngine;

public class GameEvaluator : MonoBehaviour, IObserver
{
    [SerializeField]
    private GameManager gameManager = null;

    private int NumOfStonesOnBoard;

    private void Start()
    {
        NumOfStonesOnBoard = GameManager.GameStonesPairCount * 2;
    }

    public void OnNotify(ISubject subject, ActionType actionType)
    {
        Stone stone = subject as Stone;
        if (stone)
        {
            NumOfStonesOnBoard--;
            Evaluate();
        }
    }

    private void Evaluate() {
        if(NumOfStonesOnBoard <= 0) {
            gameManager.EndGame();
        }
    }
}
