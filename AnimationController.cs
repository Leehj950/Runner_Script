using UnityEngine;

public class AnimationController : MonoBehaviour
{
    protected PlayerMoveController moveController;
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        moveController = GetComponent<PlayerMoveController>();
    }
}