using UnityEngine;

public class SpawnRoad : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MapManager.Instance.SpawnRoadFromPool(transform.parent.gameObject);
        }
    }
}