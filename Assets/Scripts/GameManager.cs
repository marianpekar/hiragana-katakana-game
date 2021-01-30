using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private StonesPool stonesPool;

    [SerializeField]
    private int gameStonesPairCount = 9;

    void Start()
    {
        stonesPool.Initialize();
        CreateGame();
    }

    void CreateGame()
    {
        Array signs = Enum.GetValues(typeof(Sign));

        for(int i = 0; i < gameStonesPairCount; i++) {
            Sign randomSign = (Sign)signs.GetValue(UnityEngine.Random.Range(0, signs.Length));

            stonesPool.GetHiraganaStone(randomSign);
            stonesPool.GetKatakanaStone(randomSign);
        }
    }

}

