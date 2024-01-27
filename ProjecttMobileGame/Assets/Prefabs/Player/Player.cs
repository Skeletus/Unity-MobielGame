using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] JoyStick aimStick;
    [SerializeField] JoyStick moveStick;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float turnSpeed = 30f;
    [SerializeField] float animTurnSpeed = 11f;

    [Header("Invetory")]
    [SerializeField] InventoryComponent inventoryComponent;

    Vector2 moveInput;
    Vector2 aimInput;

    Camera mainCam;
    CameraController cameraController;
    Animator animator;

    float animatorTurnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        moveStick.onStickValueUpdated += MoveInputUpdated;
        aimStick.onStickValueUpdated += AimInputUpdated;
        aimStick.onStickTapped += StartSwitchWeapon;
        mainCam = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
        animator = GetComponent<Animator>();
    }

    void StartSwitchWeapon()
    {
        animator.SetTrigger("SwitchWeapon");
    }

    public void SwitchWeapon()
    {
        inventoryComponent.NextWeapon();
    }

    void AimInputUpdated(Vector2 inputValue)
    {
        aimInput = inputValue;
        if(aimInput.magnitude > 0 )
        {
            animator.SetBool("Attacking", true);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
    }

    void MoveInputUpdated(Vector2 inputValue)
    {
        moveInput = inputValue;
    }

    Vector3 StickInputToWorldDirection(Vector2 inputValue)
    {
        Vector3 rightDirection = mainCam.transform.right;
        Vector3 upDirection = Vector3.Cross(rightDirection, Vector3.up);

        return rightDirection * inputValue.x + upDirection * inputValue.y;
    }

    // Update is called once per frame
    void Update()
    {
        PerformMoveAndAim();
        UpdateCamera();
    }

    private void PerformMoveAndAim()
    {
        Vector3 moveDirection = StickInputToWorldDirection(moveInput);
        characterController.Move(moveDirection * Time.deltaTime * moveSpeed);

        UpdateAim(moveDirection);

        float forward = Vector3.Dot(moveDirection, transform.forward);
        float right = Vector3.Dot(moveDirection, transform.right);

        animator.SetFloat("ForwardSpeed", forward);
        animator.SetFloat("RightSpeed", forward);
    }

    private void UpdateAim(Vector3 curretMoveDirection)
    {
        Vector3 aimDirection = curretMoveDirection;
        if (aimInput.magnitude != 0)
        {
            aimDirection = StickInputToWorldDirection(aimInput);
        }
        RotateTowards(aimDirection);
    }

    private void UpdateCamera()
    {
        // if player it's not moving but not aiming, and cameraController exits
        if (moveInput.magnitude != 0 && aimInput.magnitude == 0 && cameraController != null)
        {
            cameraController.AddYawInput(moveInput.x);
        }
    }

    private void RotateTowards(Vector3 aimDirection)
    {
        float currentTurnSpeed = 0;
        if (aimDirection.magnitude != 0)
        {
            Quaternion previousRotation = transform.rotation;

            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDirection, Vector3.up), 0.5f);

            Quaternion currentRotation = transform.rotation;
            float direction = Vector3.Dot(aimDirection, transform.right) > 0 ? 1: -1;
            float rotationDelta = Quaternion.Angle(previousRotation, currentRotation) * direction;
            currentTurnSpeed = rotationDelta / Time.deltaTime;

        }
        animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed, Time.deltaTime * animTurnSpeed);
        animator.SetFloat("TurningSpeed", animatorTurnSpeed);
    }
}
