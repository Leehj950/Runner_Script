using UnityEngine;

public class ShoesItem : BaseItem, IUsable
{
    public override void DoItem()
    {
        UseItem();
    }

    public void UseItem()
    {
        playerStatus.EquipItem(data.itemType);
        playerStatus.JumpBoost(data.multiplier, data.duration);
        gameObject.SetActive(false);
    }
}
