using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] JoyStick moveStick;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 20f;
    Vector2 moveInput;
    Camera mainCam;
    CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        moveStick.onStickValueUpdated += MoveInputUpdated;
        mainCam = Camera.main;
        cameraController = FindObjectOfType<CameraController>();
    }

    void MoveInputUpdated(Vector2 inputValue)
    {
        moveInput = inputValue;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rightDirection = mainCam .transform.right;
        Vector3 upDirection = Vector3.Cross(rightDirection, Vector3.up);
        characterController.Move( (rightDirection * moveInput.x + upDirection * moveInput.y) * Time.deltaTime * moveSpeed);
        if (moveInput.magnitude != 0)
        {
            cameraController.AddYawInput(moveInput.x);
        }
    }
}
