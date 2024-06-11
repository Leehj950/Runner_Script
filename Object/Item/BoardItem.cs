public class BoardItem : BaseItem, IUsable
{
    public override void DoItem()
    {
        UseItem();
    }

    public void UseItem()
    {
        playerStatus.EquipItem(data.itemType);
        playerStatus.SpeedBoost(data.multiplier, data.duration);
        gameObject.SetActive(false);
    }
}