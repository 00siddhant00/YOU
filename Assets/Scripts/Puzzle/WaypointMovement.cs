using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WaypointMovement : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints
    public float speed = 5f; // Speed of movement
    private int currentWaypointIndex = 0; // Index of the current waypoint
    public Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.isKinematic = true; // Ensure the Rigidbody doesn't respond to physics forces
    }

    void FixedUpdate()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            // Calculate the direction towards the current waypoint
            Vector2 direction = (Vector2)waypoints[currentWaypointIndex].position - rb2D.position;

            // Normalize the direction vector
            direction.Normalize();

            // Move the Rigidbody towards the current waypoint
            rb2D.MovePosition(rb2D.position + direction * speed * Time.fixedDeltaTime);

            // Check if the GameObject is close enough to the current waypoint
            if (Vector2.Distance(rb2D.position, waypoints[currentWaypointIndex].position) < 0.5f)
            {
                // Move to the next waypoint
                currentWaypointIndex++;
            }
        }
        else
        {
            // Reset to the first waypoint if we have reached the last one
            currentWaypointIndex = 0;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //collision.gameObject.transform.parent = this.transform;
        }

    }

}
