using UnityEngine;

public class Gate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt("CurrentLevel", RestartLevel.Instance.currentLevel + 1);
            RestartLevel.Instance.currentLevel += 1;
            RestartLevel.Instance.gameOverRestart();
        }
    }
}