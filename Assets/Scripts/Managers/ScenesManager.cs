using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public UnityEngine.UI.Image fadeImg;
    public float fadeDuration;
    public Color fadeColor;
    public float waitForFadeOut;

    public void Fade()
    {
        fadeImg.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f);
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // Fade In
        float elapsedTime = 0f;
        Color startColor = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, 0f);
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < fadeDuration)
        {
            fadeImg.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeImg.color = targetColor;

        // Wait for a specified duration
        yield return new WaitForSeconds(waitForFadeOut);

        // Fade Out
        elapsedTime = 0f;
        startColor = fadeImg.color;
        targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < fadeDuration)
        {
            fadeImg.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeImg.color = targetColor;
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel.Instance.gameOverRestart();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);

        }
    }

    public void DeleteSavedData()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "player-stats.json");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Existing data file deleted successfully.");
        }
        else
        {
            Debug.LogWarning("Data file not found. No file deleted.");
        }
    }

    /// <summary>
    /// Sets the confinier boundry to current active section
    /// </summary>
    public void ChangeSectionConfiner(GameObject levelConfiner)
    {
        GameManager.Instance.CameraShake.Confiner.m_BoundingShape2D = levelConfiner.GetComponent<PolygonCollider2D>();
    }


    #region Scene System Functions

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    #endregion
}