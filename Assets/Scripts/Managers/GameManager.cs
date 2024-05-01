using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player")]
    public PlayerController playerController;

    [Header("Utilitis")]
    public ScenesManager sceneManager;
    public CameraShake CameraShake;
    public PuzzleBase PuzzleBase;

    [Header("Level")]
    public float gateSpawnDistance;

    [Header("System")]
    public GameObject Info;
    bool infoToggle;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            infoToggle = !infoToggle;
            Info.SetActive(infoToggle);
        }
    }

    public void TimeSlow(int slowAmount = 100, float forSec = 1)
    {
        slowAmount = slowAmount < 0 ? 0 : slowAmount > 100 ? 100 : slowAmount;
        Time.timeScale = (100 - slowAmount) / 100.0f;
        StartCoroutine(TimeBackToNormal(forSec));
    }

    IEnumerator TimeBackToNormal(float sec)
    {
        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup - startTime < sec)
        {
            yield return null;
        }
        Time.timeScale = 1.0f;
    }
}
