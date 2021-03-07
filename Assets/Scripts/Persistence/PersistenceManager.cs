using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

using Volume = AudioManager.Volume;

public class PersistenceManager :  MonoBehaviour
{
    private readonly BinaryFormatter binaryFormatter = new BinaryFormatter();

    [System.Serializable]
    private class GameData
    {
        public Volume volume = Volume.Max;
        public TimeSpan bestTime = TimeSpan.Zero;
    }

    private GameData gameData;
    private string gameDataPath;
    private readonly string gameDataFileName = "GameData.dat";

    public Volume Volume
    {
        get => gameData.volume;
        set => gameData.volume = value;
    }

    public TimeSpan BestTime {
        get => gameData.bestTime;
        set => gameData.bestTime = value;
    }

    private void Awake()
    {
        gameDataPath = Path.Combine(Application.persistentDataPath, gameDataFileName);

        LoadGameData();

        Debug.Log(gameData.bestTime);
    }

    private void LoadGameData() {
        if (File.Exists(gameDataPath))
        {
            FileStream gameDataFile = File.Open(gameDataPath, FileMode.Open);
            GameData gameData = (GameData)binaryFormatter.Deserialize(gameDataFile);
            gameDataFile.Close();

            this.gameData = gameData;
        } 
        else {
            this.gameData = new GameData();
        }
    }

    private void SaveGameData() {
        FileStream gameDataFile = File.Create(gameDataPath);
        binaryFormatter.Serialize(gameDataFile, this.gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}
