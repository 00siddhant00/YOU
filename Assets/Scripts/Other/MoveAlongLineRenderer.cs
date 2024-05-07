using UnityEngine;

public class MoveAlongLineRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float speed = 1.0f;

    private int currentPointIndex;

    void Start()
    {
        currentPointIndex = 0;
    }

    void Update()
    {
        // Check if there are enough points in the line renderer
        if (lineRenderer.positionCount < 2)
        {
            Debug.LogError("The line renderer needs at least 2 points to move along.");
            return;
        }

        // Get the positions of the current and next points
        Vector3 currentPoint = lineRenderer.GetPosition(currentPointIndex);
        Vector3 nextPoint = lineRenderer.GetPosition((currentPointIndex + 1) % lineRenderer.positionCount);

        // Move towards the next point

        transform.position = currentPointIndex == lineRenderer.positionCount - 1 ? transform.position = nextPoint : transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);

        // Check if the object has reached the next point
        if (Vector3.Distance(transform.position, nextPoint) < 0.1f)
        {
            // Update current point index
            currentPointIndex = (currentPointIndex + 1) % lineRenderer.positionCount;
        }
    }
}
