using UnityEngine;

public class PlayerAnimtionContorller : AnimationController
{
    private readonly int IsSilding = Animator.StringToHash("IsSliding");
    private readonly int IsJump = Animator.StringToHash("IsJump");
    private readonly int IsDoubleJump = Animator.StringToHash("IsdoubleJump");
    private readonly int IsBtnSliding = Animator.StringToHash("IsBtnSliding");
    private readonly int IsSprint = Animator.StringToHash("IsSpint");
    private readonly int IsBoard = Animator.StringToHash("IsBorad");

    protected override void Awake()
    {
        base.Awake();
    }

    // 이걸 연 이유는 키값에 따라 상황에 맞게 바꿔야하는데.
    // 일단 키 입력 받는게 옵저버 패턴이 아니고 키값에따라 누르고 잇을때 나눠야하기때문에 
    // public을 열어서 넣게 해야할거 같습니다.

    // slide을 시작하는 애니메이션에 대한 함수
    public void SildeStart()
    {
        animator.SetBool(IsBtnSliding, true);
    }
    public void SlideEnd()
    {
        animator.SetBool(IsBtnSliding, false);
    }

    // Slide을 진행중이는 애니메이션에 대한 함수.
    public void SildingStart()
    {
        animator.SetBool(IsSilding, true);
    }
    public void SlidingEnd()
    {
        animator.SetBool(IsSilding, false);
    }

    // jump에 대한 함수
    public void JumpStart()
    {
        animator.SetBool(IsJump, true);
    }
    public void jumpend()
    {
        animator.SetBool(IsJump, false);
    }

    // dobleJump에 대한 함수
    public void DoubleJumpStart()
    {
        animator.SetBool(IsDoubleJump, true);
    }

    public void DoubleJumpend()
    {
        animator.SetBool(IsDoubleJump, false);
    }

    public void SprintStart()
    {
        animator.SetBool(IsSprint, true);
    }
    public void Sprintend()
    {
        animator.SetBool(IsSprint, false);
    }
    public void PickItem()
    {
        animator.SetTrigger("IsItem");
    }

    public void Crash()
    {
        animator.SetTrigger("IsCrash");
    }

    public void ItemBoardStart()
    {
        animator.SetBool(IsBoard, true);
    }

    public void ItemBoardEnd()
    {
        animator.SetBool(IsBoard, false);
    }
    public bool ItemBoard()
    {
       return animator.GetBool(Animator.StringToHash("IsBorad"));
    }
}
