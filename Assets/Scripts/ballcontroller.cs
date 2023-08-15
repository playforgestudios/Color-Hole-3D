using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballcontroller : MonoBehaviour
{
    public Transform hole;
    private Vector3 startingPosition;
    private bool isMovingToHole = true;
    private float ballSpeed = 2f; // Speed of the ball moving towards the hole

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (isMovingToHole)
        {
            MoveToHole();
        }
        else
        {
            MoveToStartingPosition();
        }
    }

    void MoveToHole()
    {
        // Move the ball downward towards the hole smoothly
        float step = ballSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, hole.position, step);

        // Check if the ball has reached the hole
        if (transform.position == hole.position)
        {
            isMovingToHole = false;
        }
    }

    void MoveToStartingPosition()
    {
        // Move the ball upward towards its starting position smoothly
        float step = ballSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, startingPosition, step);

        // Check if the ball has reached its starting position
        if (transform.position == startingPosition)
        {
            isMovingToHole = true;
        }
    }

}
