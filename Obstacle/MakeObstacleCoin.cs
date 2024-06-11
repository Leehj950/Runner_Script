using UnityEngine;

public class MakeObstacleCoin : MonoBehaviour
{
    public float yOffset;

    public void MakeCoin()
    {
        GameObject obj = MapManager.Instance.SpawnFromPool("coin");
        obj.transform.position = transform.position + new Vector3(0, yOffset, 0);
    }
}
