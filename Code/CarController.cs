using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject[] checkpoints;
    public float movementSpeed = 5f;
    public float rotationSpeed = 0.5f;
    public float pauseDuration = 5f; // Duration to pause at even checkpoints
    private int currentCheckpointIndex = 0;
    private bool isPaused = false;
    private float pauseTimer = 0f;
    private bool rotateCamera = false;
    public GameObject carCam;    

    private float rotateTimer = 0f;

    void Start()
    {

    }

    void Update()
    {
        // Check if the checkpoint array is not empty
        if (checkpoints.Length > 0)
        {
            // Get the current checkpoint
            GameObject currentCheckpoint = checkpoints[currentCheckpointIndex];

            // Calculate the direction towards the current checkpoint
            Vector3 targetPosition = currentCheckpoint.transform.position;
            Vector3 moveDirection = targetPosition - transform.position;
            moveDirection.Normalize();

            // Rotate the car towards the target direction
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Slow down movement speed during rotation
            float currentSpeed = moveDirection == Vector3.zero ? 0f : movementSpeed;

            if (!isPaused)
            {
                // Move the car towards the current checkpoint
                Vector3 newPosition = transform.position + moveDirection * currentSpeed * Time.deltaTime;
                transform.position = newPosition;
            }
            else
            {
                // Increment pause timer
                pauseTimer += Time.deltaTime;

                // Check if pause duration has elapsed
                if (pauseTimer >= pauseDuration)
                {
                    // Resume movement and reset pause timer
                    isPaused = false;
                    pauseTimer = 0f;
                }
            }

            // Check if the car has reached the current checkpoint
            float distanceToCheckpoint = Vector3.Distance(transform.position, targetPosition);
            if (distanceToCheckpoint < 0.1f) // Adjust this threshold as needed
            {
                // Rotate camera to 90 degrees at checkpoint 4
                if (currentCheckpointIndex == 1)
                {
                    
                    rotateCamera = true;
                    rotateTimer = 0f;
                }

                // Pause at even checkpoints
                if (currentCheckpointIndex % 2 != 0)
                {

                    isPaused = true;
                }

                // Move to the next checkpoint
                currentCheckpointIndex = (currentCheckpointIndex + 1) % checkpoints.Length;
            }
        }
        else
        {
            Debug.LogError("No checkpoints assigned!");
        }
// Rotate the camera if needed

}
}