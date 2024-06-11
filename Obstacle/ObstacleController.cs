using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public GameObject obstacle;
    public float distance = 3f;
    public float time = 2f;
    private Transform playerPos;

    void RandomObstacle()
    {
        float xPos = playerPos.position.x + Random.Range(-distance, distance);
        float yPos = playerPos.position.y;
        float zPos = playerPos.position.z + Random.Range(-distance, distance);
        
        Vector3 randPos = new Vector3 (xPos, yPos, zPos);
        Instantiate(obstacle, randPos, Quaternion.identity);
    }

    void Update()
    {
        
    }

    void Start()
    {
        playerPos = PlayerManager.Instance.Player.transform;
        InvokeRepeating("RandomObstacle", 0f, time);
    }
}
