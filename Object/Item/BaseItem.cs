using UnityEngine;

public interface IUsable
{
    public void UseItem();
}

public abstract class BaseItem : MonoBehaviour
{
    public ItemSO data; //SO 데이터
    public PlayerStatus playerStatus;
    public AudioClip audioClip;
    public AudioSource audioSource;

    public float turnSpeed = 200.0f;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }
    private void Start()
    {
        playerStatus = PlayerManager.Instance.Player.status;
    }

    private void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * turnSpeed);
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ranrdjajn");
        if (other.CompareTag("Player"))
        {
            Debug.Log("deeeeeeee");
            if(playerStatus.equippedItem == data.itemType) return;
            Debug.Log("???");
            //audioSource.PlayOneShot(audioClip); 
            SoundManager.Instance.PlayOneShot(audioClip);
            DoItem();
        }
    }

    public abstract void DoItem();
}

