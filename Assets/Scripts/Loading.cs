using UnityEngine;

public class Loading : MonoBehaviour
{
    public Transform hole; // Drag and drop the hole GameObject into this field in the Inspector
    public GameObject ballPrefab; // Drag and drop the ball prefab into this field in the Inspector
    public int numberOfBalls = 10;
    public float ballSpeed = 2f;

    private GameObject[] balls;

    void Start()
    {
        SpawnBalls();
    }

    void SpawnBalls()
    {
        balls = new GameObject[numberOfBalls];
        for (int i = 0; i < numberOfBalls; i++)
        {
            GameObject ball = Instantiate(ballPrefab, GetRandomStartPosition(), Quaternion.identity);
            balls[i] = ball;
            MoveBallTowardsHole(ball);
        }
    }

    Vector3 GetRandomStartPosition()
    {
        float radius = 2f; // Adjust this value to determine how far away the balls spawn from the hole
        Vector2 randomCircle = Random.insideUnitCircle.normalized * radius;
        return new Vector3(randomCircle.x, 0f, randomCircle.y);
    }

    void MoveBallTowardsHole(GameObject ball)
    {
        Vector3 directionToHole = (hole.position - ball.transform.position).normalized;
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        ballRigidbody.velocity = directionToHole * ballSpeed;
    }

    void Update()
    {
        // Check if any ball has passed through the hole
        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i] != null && Vector3.Distance(balls[i].transform.position, hole.position) < 0.5f)
            {
                Destroy(balls[i]); // Ball went through the hole, destroy it
                balls[i] = null;
                SpawnBallAgain(i); // Spawn a new ball to continue the loop
            }
        }
    }

    void SpawnBallAgain(int index)
    {
        // Delay the spawning of the new ball to simulate the loop
        StartCoroutine(SpawnBallDelayed(index));
    }

    System.Collections.IEnumerator SpawnBallDelayed(int index)
    {
        yield return new WaitForSeconds(2f); // Adjust this value to control the delay between ball spawns
        GameObject ball = Instantiate(ballPrefab, GetRandomStartPosition(), Quaternion.identity);
        balls[index] = ball;
        MoveBallTowardsHole(ball);
    }
}
