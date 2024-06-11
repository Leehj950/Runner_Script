using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoad : MonoBehaviour
{
    public GameObject road;
    public Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawn = trans.position + new Vector3(0, 0, 86.88f);

        Instantiate(road, spawn, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
