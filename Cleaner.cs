using UnityEngine;

public class Cleaner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.SetActive(false);
    }
}