using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSubject
{
    [SerializeField]
    private StonesPool stonesPool;

    public const int GameStonesPairCount = 16;
    private const int StonesPerRow = 4;

    private readonly GameObject[] currentStones = new GameObject[GameStonesPairCount * 2];
    private readonly List<Sign> currentSigns = new List<Sign>();

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

    public void RestartGame() {
        for (int i = 0; i < currentStones.Length; i++)
        {
            currentStones[i].SetActive(false);
            currentStones[i] = null;
        }

        CreateGame();
    }

    public void EndGame() {
        Debug.Log("Game ends");

        foreach (var observer in observers)
        {
            observer.OnNotify(this, ActionType.GameEnds);
        }
    }

    private void TakeStonesFromPool()
    {
        Array signs = Enum.GetValues(typeof(Sign));

        GameObject[] hiraganaStones = new GameObject[GameStonesPairCount];
        GameObject[] katakanaStones = new GameObject[GameStonesPairCount];

        for (int i = 0; i < GameStonesPairCount; i++)
        {
            Sign randomSign = (Sign)signs.GetValue(UnityEngine.Random.Range(0, signs.Length));

            if (currentSigns.Contains(randomSign))
            {
                i--;
                continue;
            }

            currentSigns.Add(randomSign);
            hiraganaStones[i] = stonesPool.GetHiraganaStone(randomSign);
            katakanaStones[i] = stonesPool.GetKatakanaStone(randomSign);
        }

        hiraganaStones.Shuffle();
        katakanaStones.Shuffle();

        for (int i = 0; i < GameStonesPairCount; i++) {
            currentStones[i] = hiraganaStones[i];
            currentStones[GameStonesPairCount + i] = katakanaStones[i];
        }
    }

    private void PlaceStones()
    {
        float x = 0;
        float z = 0;
        float j = 0;
        float c = 0;
        foreach (GameObject stone in currentStones)
        {
            if (z % StonesPerRow == 0)
            {
                z = 0;
                x++;
            }

            if (j >= GameStonesPairCount) c = 2;

            stone.transform.position = new Vector3(x * 2 + c, 0, z * 2);
            z++;
            j++;
        }
    }
}

