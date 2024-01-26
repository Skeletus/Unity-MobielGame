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

    Vector2 moveInput;
    Vector2 aimInput;

    Camera mainCam;
    CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        moveStick.onStickValueUpdated += MoveInputUpdated;
        aimStick.onStickValueUpdated += AimInputUpdated;
        mainCam = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
    }

    void AimInputUpdated(Vector2 inputValue)
    {
        aimInput = inputValue;
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
        Vector3 moveDirection = StickInputToWorldDirection(moveInput);
        Vector3 aimDirection = moveDirection;

        if(aimInput.magnitude!= 0)
        {
            aimDirection = StickInputToWorldDirection(aimInput);
        }

        if(aimDirection.magnitude!= 0)
        {
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDirection, Vector3.up), 0.5f);
        }

        characterController.Move(moveDirection * Time.deltaTime * moveSpeed);
        if (moveInput.magnitude != 0)
        {
            if (characterController != null)
            {
                cameraController.AddYawInput(moveInput.x);
            }
        }
    }
}
