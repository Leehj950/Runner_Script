using UnityEngine;

public class SandwichItem : BaseItem, IUsable
{
    public Material invincibleMat;

    public override void DoItem()
    {
        UseItem();
    }

    public void UseItem()
    {
        playerStatus.EquipItem(data.itemType);
        playerStatus.MakeInvincible(invincibleMat, data.duration);
        gameObject.SetActive(false);
    }
}