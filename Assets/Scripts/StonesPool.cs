using System;
using System.Collections.Generic;
using UnityEngine;

public class StonesPool : Singleton<StonesPool>
{
    [SerializeField]
    StoneFactory stoneFactory = null;

    private readonly Dictionary<Sign, GameObject> hiraganaStones = new Dictionary<Sign, GameObject>();
    private readonly Dictionary<Sign, GameObject> katakanaStones = new Dictionary<Sign, GameObject>();

    public void Initialize()
    {
        Array alphabets = Enum.GetValues(typeof(Alphabet));
        Array signs = Enum.GetValues(typeof(Sign));

        foreach(Alphabet alphabet in alphabets)
            foreach(Sign sign in signs) {
                var stoneGO = stoneFactory.CreateStone(alphabet, sign);
                stoneGO.transform.parent = this.transform;

                if (alphabet.Equals(Alphabet.Hiragana))
                    hiraganaStones.Add(sign, stoneGO);
                else if (alphabet.Equals(Alphabet.Katakana))
                    katakanaStones.Add(sign, stoneGO);
            }
    }

    public GameObject GetHiraganaStone(Sign sign) => GetFromPool(hiraganaStones[sign]);
    public GameObject GetKatakanaStone(Sign sign) => GetFromPool(katakanaStones[sign]);

    private GameObject GetFromPool(GameObject go) {        
        go.SetActive(true);
        return go;
    }
}
