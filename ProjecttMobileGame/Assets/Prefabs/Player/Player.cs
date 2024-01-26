using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] JoyStick moveStick;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float turnSpeed = 30f;
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
        Vector3 moveDirection = rightDirection * moveInput.x  + upDirection * moveInput.y;
        characterController.Move(moveDirection * Time.deltaTime * moveSpeed);
        if (moveInput.magnitude != 0)
        {
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), 0.5f); 
            if (characterController != null)
            {
                cameraController.AddYawInput(moveInput.x);
            }
        }
    }
}
