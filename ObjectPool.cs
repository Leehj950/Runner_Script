using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private List<Pool> pools = new List<Pool>();
    private Dictionary<string, Queue<GameObject>> PoolDictionary;

    private GameObject obj = null;

    private void Awake()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        for (int i = 0; i < pools.Count; i++)
        {
            // ���� �����ϴ� ť ���·� ���� ����� ���� Ȥ�� ���ؼ� ��������
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int j = 0; j < pools[i].size; j++)
            {
                // ������Ʈ Ǯ�ȿ� �������⸸ �մϴ�. 
                GameObject obj = Instantiate(pools[i].prefab, transform);
                // ��Ȱ���Ѵ�.
                obj.SetActive(false);
                // �׸��� �ٽ� ť�� �����ֽ��ϴ�.
                queue.Enqueue(obj);
            }
            // ��ųʸ� �ȿ� �ٽ� �� ť�� �±׸� ������ ���� �԰� �ֽ��ϴ�.
            PoolDictionary.Add(pools[i].tag, queue);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        // ���� ������Ʈ�� ���ü������� ����ó���� �մϴ�.
        if (!PoolDictionary.ContainsKey(tag))
        {
            return null;
        }

        // ���� ������ ������Ʈ ���
        GameObject obj = PoolDictionary[tag].Dequeue();

        PoolDictionary[tag].Enqueue(obj);
        obj.SetActive(true);

        return obj;
    }


}

