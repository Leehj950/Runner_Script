using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public interface IAchievementCheck
{
    void CheckAchievement();
}

public class AchieveCheck : MonoBehaviour, IAchievementCheck
{
    public void CheckAchievement()
    {
        if (!AchieveManager.instance.IsAchievementUnlocked(0) && PlayerManager.Instance.Player.status.score >= 1)
        {
            AchieveManager.instance.SetAchievementUnlocked(0);
            AchieveManager.instance.AchievementUnlocked(AchieveManager.instance.achievements[0]);
        }
        else if(!AchieveManager.instance.IsAchievementUnlocked(1) && PlayerManager.Instance.Player.status.coin >= 1) 
        {
            AchieveManager.instance.SetAchievementUnlocked(1);
            AchieveManager.instance.AchievementUnlocked(AchieveManager.instance.achievements[1]);
        }
    }

}

