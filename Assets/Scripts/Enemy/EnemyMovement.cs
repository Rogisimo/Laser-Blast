using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f; // Enemy movement speed
    public Vector2 movementBoundsMin; // Minimum bounds for movement
    public Vector2 movementBoundsMax; // Maximum bounds for movement

    private Vector2 targetPosition;

    void Start()
    {
        GenerateNewTargetPosition();
    }

    void Update()
    {
        MoveTowardsTarget();
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            GenerateNewTargetPosition();
        }
    }

    void GenerateNewTargetPosition()
    {
        float randomX = Random.Range(movementBoundsMin.x, movementBoundsMax.x);
        float randomY = Random.Range(movementBoundsMin.y, movementBoundsMax.y);
        targetPosition = new Vector2(randomX, randomY);
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    // Draw Gizmos to visualize the movement bounds in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // Set the color of the gizmos
        // Draw a wireframe rectangle representing the movement bounds
        Gizmos.DrawLine(new Vector2(movementBoundsMin.x, movementBoundsMin.y), new Vector2(movementBoundsMax.x, movementBoundsMin.y));
        Gizmos.DrawLine(new Vector2(movementBoundsMax.x, movementBoundsMin.y), new Vector2(movementBoundsMax.x, movementBoundsMax.y));
        Gizmos.DrawLine(new Vector2(movementBoundsMax.x, movementBoundsMax.y), new Vector2(movementBoundsMin.x, movementBoundsMax.y));
        Gizmos.DrawLine(new Vector2(movementBoundsMin.x, movementBoundsMax.y), new Vector2(movementBoundsMin.x, movementBoundsMin.y));
    }
}
