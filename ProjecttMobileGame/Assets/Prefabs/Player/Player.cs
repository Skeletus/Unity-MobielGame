using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, TeamInterface
{
    [SerializeField] JoyStick aimStick;
    [SerializeField] JoyStick moveStick;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float maxMoveSpeed = 80f;
    [SerializeField] float minMoveSpeed = 5f;
    [SerializeField] float animTurnSpeed = 11f;
    [SerializeField] MovementComponent movementComponent;
    [SerializeField] int TeamID = 1;

    internal void AddMoveSpeed(float boostAmt)
    {
        moveSpeed += boostAmt;
        moveSpeed = Mathf.Clamp(moveSpeed, minMoveSpeed, maxMoveSpeed);
    }

    [Header("Invetory")]
    [SerializeField] InventoryComponent inventoryComponent;

    [Header("HeathAndDamage")]
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] PlayerValueGauge healthBar;

    [Header("AbilityAndStamina")]
    [SerializeField] AbilityComponent abilityComponent;
    [SerializeField] PlayerValueGauge staminaBar;

    [Header("UI")]
    [SerializeField] UIManager uiManager;

    Vector2 moveInput;
    Vector2 aimInput;

    Camera mainCam;
    CameraController cameraController;
    Animator animator;

    float animatorTurnSpeed;

    public int GetTeamID()
    {
        return TeamID;
    }

    // Start is called before the first frame update
    void Start()
    {
        moveStick.onStickValueUpdated += MoveInputUpdated;
        aimStick.onStickValueUpdated += AimInputUpdated;
        aimStick.onStickTapped += StartSwitchWeapon;
        mainCam = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
        animator = GetComponent<Animator>();
        healthComponent.onHealthChange += HealthChanged;
        healthComponent.onHealthEmpty += StartDeathSequence;
        healthComponent.BroadcastHealthValueImmeidately();

        abilityComponent.onStaminaChange += StaminaChanged;
        abilityComponent.BroadcastStaminaChangeImmedietely();
        GameplayStatics.GameStarted();
    }

    private void StaminaChanged(float newAmount, float maxAmount)
    {
        staminaBar.UpdateValue(newAmount, 0, maxAmount);
    }

    private void StartDeathSequence(GameObject Killer)
    {
        animator.SetLayerWeight(2, 1);
        animator.SetTrigger("Death");
        uiManager.SetGameplayControlEnabled(false);
    }

    private void HealthChanged(float health, float delta, float maxHealth)
    {
        healthBar.UpdateValue(health, delta, maxHealth);
    }

    public void AttackPoint()
    {
        if (inventoryComponent.HasWeapon())
        {
            inventoryComponent.GetActiveWeapon().Attack();
        }
    }

    void StartSwitchWeapon()
    {
        if (inventoryComponent.HasWeapon())
        {
            animator.SetTrigger("SwitchWeapon");
        }
    }

    public void SwitchWeapon()
    {
        inventoryComponent.NextWeapon();
    }

    void AimInputUpdated(Vector2 inputValue)
    {
        aimInput = inputValue;
        if(inventoryComponent.HasWeapon())
        {
            if (aimInput.magnitude > 0)
            {
                animator.SetBool("Attacking", true);
            }
            else
            {
                animator.SetBool("Attacking", false);
            }
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
        characterController.Move(Vector3.down * Time.deltaTime * 10f);
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
        float currentTurnSpeed = movementComponent.RotateTowards(aimDirection);
        animatorTurnSpeed = Mathf.Lerp(animatorTurnSpeed, currentTurnSpeed, Time.deltaTime * animTurnSpeed);
        animator.SetFloat("TurningSpeed", animatorTurnSpeed);
    }

    public void DeathFinished()
    {
        uiManager.SwithToDeathMenu();
    }
}
