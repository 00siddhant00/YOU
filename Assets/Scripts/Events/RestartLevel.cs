//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class RestartLevel : MonoBehaviour
//{
//    public static RestartLevel Instance;

//    public GameObject[] allLevelRef;
//    public int currentLevel;

//    public GameObject mainPlayer;

//    public bool test;

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//        }
//        else
//        {
//            Destroy(gameObject);
//        }

//        if (!test)
//            currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
//    }


//    private void Start()
//    {
//        ScenesManager scenesManager = GameManager.Instance.sceneManager;
//        //scenesManager.Fade();

//        mainPlayer = GameObject.FindGameObjectWithTag("Player");
//        //Deactivating all the levels
//        foreach (GameObject go in allLevelRef)
//        {
//            go.SetActive(false);
//        }

//        allLevelRef[currentLevel].SetActive(true);
//        mainPlayer.SetActive(true);
//        mainPlayer.transform.parent = allLevelRef[currentLevel].transform;

//        //Spawns Player to next gate position
//        mainPlayer.transform.position = allLevelRef[currentLevel].transform.Find("Spawn").position;

//        scenesManager.ChangeSectionConfiner(allLevelRef[currentLevel].transform.Find("Confiner").gameObject);


//    }


//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.R))
//        {
//            PlayerPrefs.SetInt("CurrentLevel", 0);

//        }
//    }


//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        //if (collision.gameObject.CompareTag("Player"))
//        //{
//        //    ScenesManager scenesManager = GameManager.Instance.sceneManager;
//        //    scenesManager.Fade();

//        //    mainPlayer.transform.parent = null;
//        //    foreach(GameObject go in allLevelRef)
//        //    {
//        //        go.SetActive(false);
//        //    }

//        //    allLevelRef[currentLevel].SetActive(true);
//        //    mainPlayer.SetActive(true);
//        //    mainPlayer.transform.parent = allLevelRef[currentLevel].transform;


//        //    //Spawns Player to next gate position
//        //    this.transform.position = allLevelRef[currentLevel].transform.Find("Spawn").position;
//        //    StartCoroutine(restartingLevel());

//        //}
//    }

//    public void gameOverRestart()
//    {
//        ScenesManager scenesManager = GameManager.Instance.sceneManager;
//        scenesManager.Fade();

//        mainPlayer.transform.parent = null;
//        foreach (GameObject go in allLevelRef)
//        {
//            go.SetActive(false);
//        }

//        allLevelRef[currentLevel].SetActive(true);
//        mainPlayer.SetActive(true);
//        mainPlayer.transform.parent = allLevelRef[currentLevel].transform;


//        //Spawns Player to next gate position
//        .transform.position = allLevelRef[currentLevel].transform.Find("Spawn").position;
//        StartCoroutine(restartingLevel());
//    }

//    IEnumerator restartingLevel()
//    {
//        yield return new WaitForSeconds(1f);
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

//    }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public static RestartLevel Instance;

    public GameObject[] allLevelRef;
    public int currentLevel;


    public GameObject mainPlayer;

    public bool test;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (!test)
            currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
    }


    private void Start()
    {
        ScenesManager scenesManager = GameManager.Instance.sceneManager;
        //scenesManager.Fade();

        mainPlayer = GameObject.FindGameObjectWithTag("Player");
        //Deactivating all the levels
        foreach (GameObject go in allLevelRef)
        {
            go.SetActive(false);
        }

        allLevelRef[currentLevel].SetActive(true);
        mainPlayer.SetActive(true);
        mainPlayer.transform.parent = allLevelRef[currentLevel].transform;

        //Spawns Player to next gate position
        mainPlayer.transform.position = allLevelRef[currentLevel].transform.Find("Spawn").position;

        scenesManager.ChangeSectionConfiner(allLevelRef[currentLevel].transform.Find("Confiner").gameObject);

        /*  if(currentLevel == 1 && DeathManager.instance.currentLevelDeath==0)
          {
              gameOverRestart();
          }*/


    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("CurrentLevel", 0);
            SceneManager.LoadScene(1);
            //StartCoroutine(restartingLevel()
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(1);
            //StartCoroutine(restartingLevel()
        }
        if (Input.GetKeyDown(KeyCode.N) && PlayerPrefs.GetInt("CurrentLevel") < allLevelRef.Length - 1)
        {
            PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
            SceneManager.LoadScene(1);
            //StartCoroutine(restartingLevel()
        }
        if (Input.GetKeyDown(KeyCode.B) && PlayerPrefs.GetInt("CurrentLevel") > 0)
        {
            PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") - 1);
            SceneManager.LoadScene(1);
            //StartCoroutine(restartingLevel()
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    ScenesManager scenesManager = GameManager.Instance.sceneManager;
        //    scenesManager.Fade();

        //    mainPlayer.transform.parent = null;
        //    foreach(GameObject go in allLevelRef)
        //    {
        //        go.SetActive(false);
        //    }

        //    allLevelRef[currentLevel].SetActive(true);
        //    mainPlayer.SetActive(true);
        //    mainPlayer.transform.parent = allLevelRef[currentLevel].transform;


        //    //Spawns Player to next gate position
        //    this.transform.position = allLevelRef[currentLevel].transform.Find("Spawn").position;
        //    StartCoroutine(restartingLevel());

        //}
    }

    public IEnumerator g()
    {
        ScenesManager scenesManager = GameManager.Instance.sceneManager;
        scenesManager.Fade();

        yield return new WaitForSeconds(1f);


        mainPlayer.transform.parent = null;
        foreach (GameObject go in allLevelRef)
        {
            go.SetActive(false);
        }

        allLevelRef[currentLevel].SetActive(true);
        mainPlayer.SetActive(true);
        mainPlayer.transform.parent = allLevelRef[currentLevel].transform;


        //Spawns Player to next gate position
        mainPlayer.transform.position = allLevelRef[currentLevel].transform.Find("Spawn").position;
        //StartCoroutine(restartingLevel());

        scenesManager.ChangeSectionConfiner(allLevelRef[currentLevel].transform.Find("Confiner").gameObject);
        //LoadNExtLevel();
        SceneManager.LoadScene(1);
    }

    IEnumerator restartingLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void gameOverRestart()
    {
        StartCoroutine(g());
    }

    //IEnumerator LoadNExtLevel()
    //{
    //    ScenesManager scenesManager = GameManager.Instance.sceneManager;
    //    scenesManager.Fade();


    //    yield return new WaitForSeconds(1f);
    //    //print()

    //    mainPlayer.transform.parent = null;
    //    foreach (GameObject go in allLevelRef)
    //    {
    //        go.SetActive(false);
    //    }

    //    allLevelRef[currentLevel].SetActive(true);
    //    mainPlayer.SetActive(true);
    //    mainPlayer.transform.parent = allLevelRef[currentLevel].transform;


    //    //Spawns Player to next gate position
    //    mainPlayer.transform.position = allLevelRef[currentLevel].transform.Find("Spawn").position;
    //    //StartCoroutine(restartingLevel());
    //    scenesManager.ChangeSectionConfiner(allLevelRef[currentLevel]..transform.Find("Confiner").gameObject);
    //}

    //IEnumerator restartingLevel()
    //{
    //    yield return new WaitForSeconds(1f);
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    //}
}






//}


