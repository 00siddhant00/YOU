using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
    public GameObject InfoEnable;
    public GameObject Info;
    bool infoToggle;


    [Header("Controller")]
    public GameObject KeyboardInfo;
    public GameObject ControllerInfo;
    public bool controllerConnected = false;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        playerController = FindObjectOfType<PlayerController>();
        ControllerInfo.SetActive(true);
        KeyboardInfo.SetActive(false);
    }

    private void OnDisable()
    {
        RumbleManager.instance.RumblePulse(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            infoToggle = !infoToggle;
            InfoEnable.SetActive(!infoToggle);
            Info.SetActive(infoToggle);
        }

        ControllerCheck();
    }

    private void ControllerCheck()
    {
        // Check for any connected controllers
        if (Gamepad.all.Count > 0)
        {
            controllerConnected = true;
        }
        else
        {
            controllerConnected = false;
        }

        // Use the 'controllerConnected' bool as needed
        if (controllerConnected)
        {
            if (RestartLevel.Instance.currentLevel == 3)
            {
                ControllerInfo.SetActive(true);
                KeyboardInfo.SetActive(false);
            }
            Debug.Log("A controller is connected.");
        }
        else
        {
            ControllerInfo.SetActive(false);
            KeyboardInfo.SetActive(true);
            Debug.Log("No controller is connected.");
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
