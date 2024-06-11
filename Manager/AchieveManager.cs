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

    // ���� �޼� ���� �迭
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
        achievement.SetActive(true);  // ���� �˾�â Ȱ��ȭ

        yield return new WaitForSeconds(rate);

        achievement.SetActive(false);  // ���� �˾�â ��Ȱ��ȭ
        notice.SetActive(false);  // notice ������Ʈ ��Ȱ��ȭ

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

    // ���� �޼� ���¸� �ʱ�ȭ
    public void ResetAchievements()
    {
        for (int i = 0; i < achievementsUnlocked.Length; i++)
        {
            achievementsUnlocked[i] = false;
        }
    }

    // Ư�� ������ �޼� ���¸� Ȯ��
    public bool IsAchievementUnlocked(int index)
    {
        return achievementsUnlocked[index];
    }

    // Ư�� ������ �޼� ���·� ����
    public void SetAchievementUnlocked(int index)
    {
        achievementsUnlocked[index] = true;
    }

}
