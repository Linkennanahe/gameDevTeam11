using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the game object you want to follow
    public Vector3 offset = new Vector3(0f, 0f, -0f); // Offset between camera and target
    void Start()
    {
        offset = transform.position - player.transform.position;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    

    void LateUpdate()
    {
        // Check if the target is assigned
        if (target != null)
        {
            // Calculate the desired camera position
            Vector3 desiredPosition = target.position + offset;

            // Smoothly move the camera towards the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);

            // Look at the target
            transform.LookAt(target);
            transform.position = player.transform.position + offset;
        }
    }
    public GameObject player;        //Public variable to store a reference to the player game object

}
