using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class AIChase2 : MonoBehaviour
{
    public GameObject player;
    public float speed = 20f;
    public float rotationSpeed = 100f;
    public float distanceBetween;

    private Transform enemyTransform;
    private Transform playerTransform;
    private float sqrDistanceBetween;

    private Rigidbody2D rb; // Add a reference to the Rigidbody2D component

    private void Start()
    {
        enemyTransform = transform;
        playerTransform = player.transform;
        sqrDistanceBetween = distanceBetween * distanceBetween;

        rb = GetComponent<Rigidbody2D>(); // Initialize the Rigidbody2D reference
    }

    private void FixedUpdate()
    {
        //missile movement
        Vector2 offset = (playerTransform.position - enemyTransform.position); //direction enemyTransform needs to face
        Vector2 direction = offset.normalized; //normalized direction
        rb.velocity = speed * Time.fixedDeltaTime * direction; //enemy velocity
        float rotationSteer = Vector3.Cross(transform.up, direction).z; //Cross product of player and enemy vector
        rb.angularVelocity = rotationSteer * rotationSpeed; //enemy rotation of Y-axis to face player


        //distance barrier between enemy and player
        if(offset.sqrMagnitude < sqrDistanceBetween)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
        }
        

        //float sqrDistance = direction.sqrMagnitude;

        /* if (sqrDistance < sqrDistanceBetween)
         {
             direction.Normalize();
             float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

             // Move enemy based on the forward vector
             rb.MovePosition(rb.position * (Vector2)enemyTransform.up * speed * Time.deltaTime * -1);

             *//*if (direction != Vector2.zero)
             {
                 RotateTowards(playerTransform.position, 10f); // Rotate towards the player with a specified rotation speed
             }*//*


         }*/

    }





    // Rotate the enemy towards a target
    /*void RotateTowards(Vector3 targetPosition, float rotationSpeed)
    {
        Vector2 direction = targetPosition - enemyTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(Vector3.forward * angle);
        enemyTransform.rotation = Quaternion.RotateTowards(enemyTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }*/
}
    