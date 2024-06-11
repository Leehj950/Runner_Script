using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    void Start()
    {
        MapManager.Instance.InitPool();
        MapManager.Instance.InitRoad();
    }
}
