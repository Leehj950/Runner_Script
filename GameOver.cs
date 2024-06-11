using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject restartBtn;
    public GameObject exitBtn;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            restartBtn.SetActive(true);
            exitBtn.SetActive(true);
            Time.timeScale = 0f;
            DataUtility.UpdatePlayerData(PlayerManager.Instance.Player.status, PlayerManager.Instance.Player.data);

        }
    }

    public void ReStart()
    {
        Time.timeScale = 1f;
        Debug.Log("hhhhhh");
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Debug.Log("dasda");
        SceneManager.LoadScene("Intro");
    }
}
