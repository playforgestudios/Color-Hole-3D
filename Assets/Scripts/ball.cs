using UnityEngine;

public class Ball : MonoBehaviour
{
    public Transform hole;
    private Vector3 startingPosition;
    private bool isResetting = false;
    private float resetTime = 2f; // Time it takes to reset the ball (in seconds)
    private float ballSpeed = 2f; // Speed of the ball moving towards the hole

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        // Check if the ball has passed through the hole
        if (transform.position.y < hole.position.y)
        {
            if (!isResetting)
            {
                isResetting = true;
                StartCoroutine(MoveBallSmoothly(hole.position, resetTime, true));
            }
        }
    }

    System.Collections.IEnumerator MoveBallSmoothly(Vector3 targetPosition, float duration, bool isResetting)
    {
        float elapsedTime = 0f;
        Vector3 startingPos = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the ball is precisely at the target position
        transform.position = targetPosition;

        if (isResetting)
        {
            StartCoroutine(MoveBallSmoothly(startingPosition, resetTime, false));
        }
        else
        {
            isResetting = false;
        }
    }

    void FixedUpdate()
    {
        // Move the ball downward towards the hole smoothly
        if (!isResetting)
        {
            float step = ballSpeed * Time.fixedDeltaTime;
            transform.position = Vector3.MoveTowards(transform.position, hole.position, step);
        }
    }
}
