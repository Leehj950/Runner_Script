using TMPro;
using UnityEngine;

public class PlayerScoreController : MonoBehaviour
{
    private PlayerStatus status;
    private PlayerData data;

    public TextMeshProUGUI scoreText; //임시 UI 코드로 옮겨야할듯
    public TextMeshProUGUI bestScoreText; //임시

    private void Awake()
    {
        status = GetComponent<PlayerStatus>();
        data = status.data;
    }

    private void Start()
    {
        //bestScoreText.text = "best score : " + status.bestScore.ToString();
    }

    private void Update()
    {
        status.score = transform.position.z;

        //scoreText.text = "cur score : " + status.score.ToString();
        //if (status.IsBestScore) bestScoreText.text = "new best score : " + status.score.ToString();

        if (status.score > data.bestScore && !status.IsBestScore)
        {
            status.IsBestScore = true;
        }
    }
}
