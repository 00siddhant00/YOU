using UnityEngine;
using System.Collections;
using UnityEditor;

public class LedgeAssist : MonoBehaviour
{
    public LayerMask vaultLayer;
    public Transform vaultCheck;
    public float vaultCheckLength = 1f;
    public float maxWallHeight = 0.5f;
    public float vaultSpeed = 0.1f;
    private float playerHeight = 2.5f;


    void Update()
    {
        Vault();
    }

    private void Vault()
    {
        CheckDirection(GameManager.Instance.playerController.playerMovement.lookingRight ? vaultCheck.right : -vaultCheck.right);
    }

    private void CheckDirection(Vector2 direction)
    {
        RaycastHit2D firstHit = Physics2D.Raycast(vaultCheck.position, direction, vaultCheckLength, vaultLayer);
        if (firstHit.collider != null)
        {
            Debug.Log("Vaultable in " + (direction == Vector2.right ? "right" : "left") + " direction");

            Vector2 endPoint = (firstHit.point + (direction * 1)) + (Vector2.up * maxWallHeight);
            DrawCircle((Vector3)endPoint, 0.1f, Color.red, 100);

            if (Physics2D.OverlapCircle(endPoint, 0.1f, vaultLayer))
            {
                Debug.Log("Wall is too high");
                return;
            }

            RaycastHit2D secondHit = Physics2D.Raycast(endPoint, Vector2.down, maxWallHeight, vaultLayer);
            if (secondHit.collider != null)
            {
                Vector2 endPos = new Vector2(secondHit.point.x, secondHit.point.y + (playerHeight * 0.55f));
                DrawCircle((Vector3)endPos, 0.1f, Color.green, 100);
                Debug.Log("Found place to land");
                StartCoroutine(LerpVault(endPos, vaultSpeed));
            }
        }
    }

    IEnumerator LerpVault(Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    private void OnDrawGizmos()
    {
        //Debug.DrawRay(vaultCheck.position, vaultCheck.right * vaultCheckLength, Color.blue);
        //Debug.DrawRay(vaultCheck.position, vaultCheck.right * -vaultCheckLength, Color.blue);

        Debug.DrawRay(vaultCheck.position, vaultCheck.right * (GameManager.Instance.playerController.playerMovement.lookingRight ? vaultCheckLength : -vaultCheckLength), Color.blue);
    }

    private void DrawCircle(Vector3 center, float radius, Color color, int segments)
    {
        float angleStep = 360f / segments;
        Vector3 prevPoint = center + new Vector3(radius, 0, 0);

        for (int i = 0; i < segments + 1; i++)
        {
            float angle = angleStep * i;
            Vector3 newPoint = center + Quaternion.Euler(0, 0, angle) * new Vector3(radius, 0, 0);
            Debug.DrawLine(prevPoint, newPoint, color);
            prevPoint = newPoint;
        }
    }
}
