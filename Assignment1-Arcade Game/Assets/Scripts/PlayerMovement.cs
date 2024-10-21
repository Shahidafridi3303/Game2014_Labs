using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f; // Speed of character movement
    [SerializeField] Boundaries horizontalLimits, verticalLimits;
    [SerializeField] bool simulateMobileInput;

    Camera mainCamera;
    Vector2 targetPosition;
    bool isMobileDevice = true;

    void Awake()
    {
        mainCamera = Camera.main;

        if (!simulateMobileInput)
        {
            isMobileDevice = Application.platform == RuntimePlatform.Android ||
                             Application.platform == RuntimePlatform.IPhonePlayer;
        }
    }

    void Update()
    {
        // Handle input based on the platform
        if (isMobileDevice)
        {
            HandleTouchInput();
        }
        else
        {
            HandleKeyboardInput();
        }

        EnforceMovementBoundaries();
    }

    // Rotate the player toward a given target
    void FaceTowards(Vector2 destination)
    {
        Vector2 direction = destination - (Vector2)transform.position;
        transform.up = direction;
    }

    // Move player to the new destination
    void ExecuteMovement()
    {
        transform.position = targetPosition;
    }

    // Get input from keyboard or mouse
    void HandleKeyboardInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
        float moveY = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;

        // Calculate new position based on input
        targetPosition = new Vector3(moveX + transform.position.x, moveY + transform.position.y, 0);

        // Move player and rotate toward mouse pointer
        ExecuteMovement();
        FaceTowards(mainCamera.ScreenToWorldPoint(Input.mousePosition));
    }

    // Get input from touch screen
    void HandleTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            targetPosition = mainCamera.ScreenToWorldPoint(touch.position);
            Vector2 touchPosition = targetPosition;

            // Move player towards the touch position with smooth interpolation
            targetPosition = Vector2.Lerp(transform.position, targetPosition, movementSpeed * Time.deltaTime);

            ExecuteMovement();
            FaceTowards(-touchPosition); // Rotate in opposite direction for effect
        }
    }

    // Ensure the player stays within defined boundaries
    void EnforceMovementBoundaries()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, horizontalLimits.min, horizontalLimits.max);
        currentPosition.y = Mathf.Clamp(currentPosition.y, verticalLimits.min, verticalLimits.max);
        transform.position = currentPosition;
    }
}
