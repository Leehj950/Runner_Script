using UnityEngine;

public class CoinItem : BaseItem, IUsable
{
    public override void DoItem()
    {
        UseItem();
    }

    public void UseItem()
    {
        playerStatus.coin++;
        gameObject.SetActive(false);
    }
}
