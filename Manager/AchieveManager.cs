using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class AchieveManager : MonoBehaviour
{
    public static AchieveManager instance;
    private float score;
    private List<IAchievementCheck> achievementCheckers = new List<IAchievementCheck>();

    public event Action<GameObject> OnAchievementUnlockedAction;

    public GameObject notice;
    public GameObject[] achievements;

    // 업적 달성 상태 배열
    private bool[] achievementsUnlocked; 
    private bool isShowingAchievement = false;

    private void Awake()
    {
        if(instance == null)
        {
           instance = this;
           achievementsUnlocked = new bool[achievements.Length];
        }
        else
        {
            if(instance !=  this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        achievementCheckers.AddRange(GetComponents<IAchievementCheck>());
        Debug.Log(achievementCheckers.Count);
    }

    public void AchievementUnlocked(GameObject image)
    {
        OnAchievementUnlockedAction?.Invoke(image);
    }

    private void OnEnable()
    {
        OnAchievementUnlockedAction += ShowAchievement;
    }

    private void OnDisable()
    {
        OnAchievementUnlockedAction -= ShowAchievement;
    }

    private void ShowAchievement(GameObject achievement)
    {
            StartCoroutine(NoticeRoutine(achievement, 5f));
    }

    private IEnumerator NoticeRoutine(GameObject achievement,float rate)
    {

        if (isOpen())
        {
            notice.SetActive(false);
        }

        foreach (Transform child in notice.transform)
        {
            child.gameObject.SetActive(false);
        }

        notice.SetActive(true);
        achievement.SetActive(true);  // 업적 팝업창 활성화

        yield return new WaitForSeconds(rate);

        achievement.SetActive(false);  // 업적 팝업창 비활성화
        notice.SetActive(false);  // notice 오브젝트 비활성화

    }


    public bool isOpen()
    {
        return notice.activeInHierarchy;
    }


    public void AddScore(float points)
    {
        score += points;
        Debug.Log("Current Score: " + score);
        CheckForAchievements();
    }

    private void CheckForAchievements()
    {
        foreach (var checker in achievementCheckers)
        {
            checker.CheckAchievement();
        }
    }

    // 업적 달성 상태를 초기화
    public void ResetAchievements()
    {
        for (int i = 0; i < achievementsUnlocked.Length; i++)
        {
            achievementsUnlocked[i] = false;
        }
    }

    // 특정 업적의 달성 상태를 확인
    public bool IsAchievementUnlocked(int index)
    {
        return achievementsUnlocked[index];
    }

    // 특정 업적을 달성 상태로 설정
    public void SetAchievementUnlocked(int index)
    {
        achievementsUnlocked[index] = true;
    }

}
