using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirController : MonoBehaviour
{
    public Transform[] checkpoints;
    public float speed = 5f;
    public float rotationSpeed = 5f;

    private int currentCheckpointIndex = 0;

    void Update()
    {
        if (checkpoints.Length == 0)
            return;

        Transform targetCheckpoint = checkpoints[currentCheckpointIndex];
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetCheckpoint.position, step);

        // Rotate towards the checkpoint
        Vector3 directionToTarget = targetCheckpoint.position - transform.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToTarget, rotationSpeed * Time.deltaTime);

        if (transform.position == targetCheckpoint.position)
        {
            currentCheckpointIndex++;
            if (currentCheckpointIndex >= checkpoints.Length)
            {
                currentCheckpointIndex = 0;
            }
        }
    }
}
