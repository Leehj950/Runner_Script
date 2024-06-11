using System;
using System.IO;
using UnityEngine;

public class DataUtility : MonoBehaviour
{
    public static string playerDataPath = Application.persistentDataPath + "/playerData.txt";

    public static void UpdatePlayerData(PlayerStatus status, PlayerData data)
    {
        string curDateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

        if(status.IsBestScore)
        {
            data.bestScore = status.score;
            data.bestScorePlayDate = curDateTime;
        }

        data.entireCoin += status.coin;
        InGameData inGameData = new InGameData(status.score, status.score, curDateTime);

        data.AddNewRecord(inGameData);

        SaveData<PlayerData>(data, playerDataPath);
    }

    public static void SaveData<T>(T data, string path)
    {
        Debug.Log(playerDataPath);
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    public static void PrintInfo()
    {
        Debug.Log(playerDataPath);
    }

    public static T LoadData<T>(string path)
    {
        if (File.Exists(path))
        {
            var jsonData = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(jsonData);
        }
        else return default;
    }
}