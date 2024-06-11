using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public PlayerData data;

    public int life;
    public float score;
    public float bestScore;
    public int coin;

    private Renderer childRenderer;
    private Material orginMat;

    private float startMoveSpeed;
    public float MoveSpeed { get; private set; }
    private float startJumpForce;
    public float JumpForce { get; private set; }

    public bool IsBestScore { get; set; }

    public ITEM_TYPE equippedItem;

    private void Start()
    {
        data = PlayerManager.Instance.Player.data;
        life = data.life;
        Debug.Log(data.moveSpeed);
        bestScore = data.bestScore;
        startMoveSpeed = data.moveSpeed;
        MoveSpeed = startMoveSpeed;
        startJumpForce = data.jumpPower;
        JumpForce = startJumpForce;

        Transform ajTransform = transform.GetChild(1);
        childRenderer = ajTransform.GetChild(0).GetComponent<Renderer>();
        orginMat = childRenderer.material;
    }

    public void MakeInvincible(Material newMat, float duration)
    {
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        childRenderer.material = newMat;
        Invoke("EndInnvincible", duration);
    }

    public void EndInnvincible()
    {
        UnEquipItem();
        childRenderer.material = orginMat;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public void LifeBonus()
    {
        life++;
    }

    public void EndLifeBonus()
    {
        life--;
    }

    public void EquipItem(ITEM_TYPE itemType)
    {
        equippedItem = itemType;
    }

    public void UnEquipItem()
    {
        equippedItem = ITEM_TYPE.NONE;
    }

    public void SpeedBoost(float boostMultiplier, float duration)
    {
        MoveSpeed *= boostMultiplier;
        Invoke("EndSpeedBoost", duration);
    }

    public void EndSpeedBoost()
    {
        UnEquipItem();
        MoveSpeed = startMoveSpeed;
    }

    public void JumpBoost(float boostMultiplier, float duration)
    {
        JumpForce *= boostMultiplier;
        Invoke("EndJumpBoost", duration);
    }

    public void EndJumpBoost()
    {
        UnEquipItem();
        JumpForce = startJumpForce;
    }
}