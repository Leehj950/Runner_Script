using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerMoveController : MonoBehaviour
{
    [Header("Movement")]
    public PlayerStatus playerStatus;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;

    private bool isGrounded;
    private int jumpCount = 0;
    private int maxJumpCount = 2;
    public float doubleJumpPower = 30f;

    private Rigidbody rigidbody;
    private BoxCollider collider;
    public PlayerAnimtionContorller playerAnimationContorller;
    private Transform childenTransform;

    IEnumerator enumerator;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerAnimationContorller = GetComponent<PlayerAnimtionContorller>();  
        childenTransform = GetComponentInChildren<Transform>();
        collider = GetComponent<BoxCollider>(); 
        playerStatus = GetComponent<PlayerStatus>();
    }

    public List<Transform> LinePosition;
    private int positionIndex = 1;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            positionIndex--;
            positionIndex = Mathf.Clamp(positionIndex, 0, 2);
            UpdatePosition();
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            positionIndex++;
            positionIndex = Mathf.Clamp(positionIndex, 0, 2);
            UpdatePosition();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if(SceneManager.GetActiveScene().name == "Intro")
        {
            Time.timeScale = 0f;
        }
        else
        {
            Vector3 movePos = transform.position;
            movePos.z += playerStatus.MoveSpeed * Time.deltaTime;
            transform.position = movePos;
        }
     
    }


    void UpdatePosition()
    {
        // ���� Transform�� ��ġ�� LinePosition[positionIndex]�� ������Ʈ
        if (LinePosition != null && LinePosition.Count > 0)
        {
            Vector3 movePos = transform.position;
            movePos.x = LinePosition[positionIndex].position.x;
            transform.position = movePos;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }



    public void OnSlideInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            playerAnimationContorller.SildeStart();

            if (playerAnimationContorller.ItemBoard())
            {
                Quaternion startRot = childenTransform.rotation;
                Quaternion targetRot = Quaternion.Euler(0f, 90f, 0f);

                if (enumerator != null)
                {
                    StopCoroutine(enumerator);
                }

                //childenTransform.rotation = targetRot;
                enumerator = Cor_Rotate(startRot, targetRot, 0.3f);
                StartCoroutine(enumerator);
            }

            collider.size = new Vector3(1f,0.5f,1f);;
        }
        else if (context.phase == InputActionPhase.Performed)
        {
            playerAnimationContorller.SildingStart();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            if (playerAnimationContorller.ItemBoard())
            {
                Quaternion startRot = childenTransform.rotation;
                Quaternion targetRot = Quaternion.Euler(0f, 0f, 0f);

                if (enumerator != null)
                {
                    StopCoroutine(enumerator);
                }

                enumerator = Cor_Rotate(startRot, targetRot, 0.3f);
                StartCoroutine(enumerator);
            }

            playerAnimationContorller.SlideEnd();
            playerAnimationContorller.SlidingEnd();
            collider.size = new Vector3(1, 1, 1);
        }
    }
    private IEnumerator Cor_Rotate(Quaternion StartRot, Quaternion TargetRot, float time)
    {
        // asdasdasda
        //dadasdas/d
        //�Ѱ� Ÿ��
        float totalTime = time;
        //���� Ÿ��
        float curTime = 0f;
        float t = 0f;

        while (true)
        {
            curTime += Time.deltaTime;
            t = curTime / totalTime;

            childenTransform.rotation = Quaternion.Lerp(StartRot, TargetRot, t);

            if (curTime >= totalTime)
            {
                break;
            }
            yield return null;
        }

        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
            
            playerAnimationContorller.jumpend();
            playerAnimationContorller.DoubleJumpend();

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void OnNewJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (isGrounded || jumpCount < maxJumpCount)
        {
            if(jumpCount == 0)
            {
                rigidbody.AddForce(Vector2.up * playerStatus.JumpForce, ForceMode.Impulse);
                playerAnimationContorller.JumpStart();
                jumpCount++;
            }
            else
            {
                rigidbody.AddForce(Vector2.up * doubleJumpPower, ForceMode.Impulse);
                playerAnimationContorller.DoubleJumpStart();
                jumpCount++;
            }
        }
    }
} 
