using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private StonesPool stonesPool;

    private const int gameStonesPairCount = 16;
    private const int stonesPerRow = 4;

    private GameObject[] stonesGO = new GameObject[gameStonesPairCount * 2];

    private List<Sign> currentSigns = new List<Sign>();

    private void Start()
    {
        stonesPool.Initialize();
        CreateGame();
    }

    private void CreateGame()
    {
        TakeStonesFromPool();
        PlaceStones();
    }

    private void TakeStonesFromPool()
    {
        Array signs = Enum.GetValues(typeof(Sign));

        for (int i = 0; i < gameStonesPairCount; i++)
        {
            Sign randomSign = (Sign)signs.GetValue(UnityEngine.Random.Range(0, signs.Length));

            if (currentSigns.Contains(randomSign))
            {
                i--;
                continue;
            }

            currentSigns.Add(randomSign);
            stonesGO[i] = stonesPool.GetHiraganaStone(randomSign);
            stonesGO[gameStonesPairCount + i] = stonesPool.GetKatakanaStone(randomSign);
        }
    }

    private void PlaceStones()
    {
        float x = 0;
        float z = 0;
        float j = 0;
        float c = 0;
        foreach (GameObject stone in stonesGO)
        {
            if (z % stonesPerRow == 0)
            {
                z = 0;
                x++;
            }

            if (j >= gameStonesPairCount) c = 2;

            stone.transform.position = new Vector3(x * 2 + c, 0, z * 2);
            z++;
            j++;
        }
    }

}

