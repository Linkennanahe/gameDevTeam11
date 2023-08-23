using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float moveDistance = 5f; // Distance the object will move
    public float moveSpeed = 2f;    // Speed of the movement

    private Vector3 startPos;
    private bool movingRight = true;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        // Calculate new position
        Vector3 targetPos = movingRight ? startPos + Vector3.right * moveDistance : startPos;

        // Move the object
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        // Check if the object reached the target position
        if (transform.position == targetPos)
        {
            movingRight = !movingRight; // Change direction
        }
    }
}
    