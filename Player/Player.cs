using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData data; //나중에 프로퍼티로 변경
    public PlayerStatus status;
    public PlayerMoveController controller;

    private void Awake()
    {
        PlayerManager.Instance.Player = this;
        status = GetComponent<PlayerStatus>();
        controller = GetComponent<PlayerMoveController>();

        data = DataUtility.LoadData<PlayerData>(DataUtility.playerDataPath) ?? new PlayerData();
        data.SetListAndQueue();
    }
}

[System.Serializable]
public class PlayerData
{
    public int life = 1;
    public float moveSpeed = 3f;
    public float jumpPower = 40f;
    public float bestScore;
    public string bestScorePlayDate;
    public float entireCoin;
    public int maxRecordNum = 10; 
    public List<InGameData> recordList;
    [NonSerialized]
    public Queue<InGameData> recordQueue;

    public void SetListAndQueue()
    {
        if (recordList == null)
        {
            recordList = new List<InGameData>();
            recordQueue = new Queue<InGameData>();
        }
        else
        {
            recordQueue = new Queue<InGameData>(recordList);
        }
    }

    public void QueueToList()
    {
        recordList = recordQueue.ToList();
    }

    public void AddNewRecord(InGameData inGameData)
    {
        if (recordQueue.Count >= maxRecordNum)
        {
            recordQueue.Dequeue();
        }

        recordQueue.Enqueue(inGameData);
        QueueToList();
    }
    
}

[System.Serializable]
public class InGameData
{
    public float score;
    public float coin;
    public string playDate;

    public InGameData(float score, float coin, string playDate)
    {
        this.score = score;
        this.coin = coin;
        this.playDate = playDate;
    }
}