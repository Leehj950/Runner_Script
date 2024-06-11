using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("MapManager").AddComponent<MapManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            PoolDictionary = new Dictionary<string, Queue<GameObject>>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int poolSize;
        public Transform parentTransform;
    }

    [SerializeField] private List<Pool> pools = new List<Pool>();
    private Dictionary<string, Queue<GameObject>> PoolDictionary;

    private GameObject obj = null;

    public GameObject prevRoadObj;
    public Vector3 firstRoadPos = new Vector3(-1.46f, 1.56f, 1.8f);
    public float roadoffset = 86.88f;
    //public int poolSize = 3;

    private void Start()
    {
        for (int i = 0; i < pools.Count; i++)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int j = 0; j < pools[i].poolSize; j++)
            {
                obj = Instantiate(pools[i].prefab, pools[i].parentTransform);
                //roadObj.hideFlags = HideFlags.HideInHierarchy;
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            PoolDictionary.Add(pools[i].tag, queue);
        }

        InitRoad();
    }

    public void InitPool()
    {
        for (int i = 0; i < PoolDictionary["road"].Count; i++)
        {
            GameObject roadObj = PoolDictionary["road"].Dequeue();
            if (roadObj.activeSelf) roadObj.SetActive(false);
            PoolDictionary["road"].Enqueue(roadObj);
        }
    }

    public void InitRoad()
    {
        GameObject roadObj = PoolDictionary["road"].Dequeue();
        PoolDictionary["road"].Enqueue(roadObj);
        roadObj.transform.position = firstRoadPos;
        roadObj.GetComponent<SpawnItemObstacle>().Spawn();
        roadObj.SetActive(true);
    }

    // public void ReturnRoadToPool()
    // {
    //     if (prevRoadObj != null)
    //     {
    //         prevRoadObj.SetActive(false);
    //         //PoolDictionary["road"].Enqueue(prevRoadObj);
    //     }
    // }

    public void SpawnRoadFromPool(GameObject curObj)
    {
        //ReturnRoadToPool();
        if (prevRoadObj != null) prevRoadObj.SetActive(false);
        GameObject roadObj = PoolDictionary["road"].Dequeue();
        PoolDictionary["road"].Enqueue(roadObj);
        roadObj.transform.position = curObj.transform.position + new Vector3(0, 0, roadoffset);
        roadObj.GetComponent<SpawnItemObstacle>().Spawn();
        roadObj.SetActive(true);
        prevRoadObj = curObj;
    }

    public GameObject SpawnFromPool(string tag)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            return null;
        }

        GameObject obj = PoolDictionary[tag].Dequeue();

        PoolDictionary[tag].Enqueue(obj);
        obj.SetActive(true);

        return obj;
    }

    public bool IsEmptyPool(string tag)
    {
        if (!PoolDictionary.ContainsKey(tag)) return true;

        if (PoolDictionary[tag].Count == 0) return true;
        return false;
    }
}

