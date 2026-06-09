using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [Header("角色操作资产")]
    public Animator animator;

    [Header("MoveSetting")]
    public float run_Front = 1f;
    public float run_Back = 0.6f;
    public float cem = 0.8f;
    public float walkSpeed = 1.5f;
    public float backSpeed = 1f;
    public float turnSpeed = 10f;

    [Header("Camera")]
    public Transform cameraTransform;

    [Tooltip("摄像机方向平滑速度，值越小越不容易被 Cinemachine 跟随偏移影响")]
    [Range(1f, 30f)]
    public float camSmoothSpeed = 5f;

    CharacterController characterController;
    Vector3 playerMovement;
    float threshold = 0.1f;

    float rawInputY;
    float rawInputX;
    float currentSpeed;
    float currentSpeedX;

    float verticalVelocity;
    float gravity = -9.81f;

    Vector3 smoothedCamForward;
    bool hasMovementInput;
    bool camForwardInitialized;
    Quaternion lastCamRotation;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (cameraTransform == null)
        {
            if (Camera.main != null)
                cameraTransform = Camera.main.transform;
            else
                Debug.LogWarning("[PlayerInput] 未找到 Main Camera，请在 Inspector 中拖入 cameraTransform");
        }
    }

    private void Update()
    {
        UpdateSmoothedCameraForward();
        if (hasMovementInput)
            RotateToSmoothedForward();
        CalculateMovement();
        MovePlayer();
    }

    public void GetPlayerMoveInput(InputAction.CallbackContext context)
    {
        Vector2 inputTarget = context.ReadValue<Vector2>();
        if (inputTarget.y > threshold) { rawInputY = run_Front; hasMovementInput = true; }
        else if (inputTarget.y < -threshold) { rawInputY = -run_Back; hasMovementInput = true; }
        else { rawInputY = 0f; }
        if (inputTarget.x > threshold) { rawInputX = cem; hasMovementInput = true; }
        else if (inputTarget.x < -threshold) { rawInputX = -cem; hasMovementInput = true; }
        else { rawInputX = 0f; }
        if (rawInputY == 0f && rawInputX == 0f) hasMovementInput = false;
    }

    void UpdateSmoothedCameraForward()
    {
        if (cameraTransform == null) return;

        Vector3 currentCamForward = cameraTransform.forward;
        currentCamForward.y = 0f;
        if (currentCamForward.sqrMagnitude < 0.001f) return;
        currentCamForward.Normalize();

        if (!camForwardInitialized)
        {
            smoothedCamForward = currentCamForward;
            lastCamRotation = cameraTransform.rotation;
            camForwardInitialized = true;
            return;
        }

        // 只有相机旋转发生了实质变化才更新（过滤 Cinemachine 微小偏移）
        if (Quaternion.Angle(cameraTransform.rotation, lastCamRotation) < 0.1f)
            return;

        lastCamRotation = cameraTransform.rotation;
        smoothedCamForward = Vector3.Slerp(smoothedCamForward, currentCamForward, camSmoothSpeed * Time.deltaTime);
        smoothedCamForward.y = 0f;
        smoothedCamForward.Normalize();
    }

    void RotateToSmoothedForward()
    {
        if (smoothedCamForward == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(smoothedCamForward, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void CalculateMovement()
    {
        Vector3 moveDir = Vector3.zero;

        if (cameraTransform == null)
        {
            playerMovement = Vector3.zero;
            return;
        }

        // W 用摄像机前方
        Vector3 moveForward = smoothedCamForward != Vector3.zero
            ? smoothedCamForward
            : transform.forward;

        // S/A/D 用角色当前朝向
        Vector3 charForward = transform.forward;
        charForward.y = 0f; charForward.Normalize();
        Vector3 charRight = transform.right;
        charRight.y = 0f; charRight.Normalize();

        if (rawInputY > 0f)
        {
            moveDir += moveForward * rawInputY;
        }
        else if (rawInputY < 0f)
        {
            moveDir += -charForward * Mathf.Abs(rawInputY);
        }

        if (rawInputX > 0f)
        {
            moveDir += charRight * rawInputX;
        }
        else if (rawInputX < 0f)
        {
            moveDir += -charRight * Mathf.Abs(rawInputX);
        }

        if (moveDir.sqrMagnitude > 0.001f)
        {
            float speed;
            if (rawInputY < 0f)
                speed = backSpeed;
            else
                speed = walkSpeed;

            playerMovement = moveDir.normalized * speed;
        }
        else
            playerMovement = Vector3.zero;

        playerMovement.y = 0f;
    }

    void MovePlayer()
    {
        currentSpeedX = Mathf.Lerp(currentSpeedX, rawInputX, 0.1f);
        currentSpeed = Mathf.Lerp(currentSpeed, rawInputY, 0.1f);
        animator.SetFloat("Speed", currentSpeed);
        animator.SetFloat("Speedcem", currentSpeedX);
    }

    private void OnAnimatorMove()
    {
        if (characterController.isGrounded)
            verticalVelocity = -1f;
        else
            verticalVelocity += gravity * Time.deltaTime;
        Vector3 finalMove = playerMovement;
        finalMove.y = verticalVelocity;
        characterController.Move(finalMove * Time.deltaTime);
    }
}
