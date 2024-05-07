using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class InputRecorder : MonoBehaviour
{
    [Header("Mutual Time Travel var")]
    public GameObject PlayerOfSacredTimeLine;
    public GameObject PlayerTimeTravledInstancePrefab;
    private Vector2 PlayerInitalTimeTravelPos;
    private bool wallColliding;
    private float checkRadius = 0.7f;
    public LayerMask wallCollideLayer;
    public Transform sideCheck;

    public GameObject FxF;
    public GameObject FxP;
    public UnityEngine.UI.Image futureBar;
    public UnityEngine.UI.Image presentBar;

    [Header("Future time variables")]
    public int noOfFutureTravel;

    public bool inFuture;
    public bool goingInMultipleFutures;
    public bool multipleFuturesPlayerInstancePosChanged;

    public bool calledFutureInstance;
    public float maxTimeInFuture;
    public float goToSecsInFuture;

    public List<(Vector2, float)> PlayerMultipleFutureInstancePos = new List<(Vector2, float)>();
    public int PlayerMultipleFutureInstanceChangeIndex;

    //Hidden var
    public static InputRecorder i;
    public List<(Vector2, float)> moveInputHistory = new();
    public List<(int, float)> jumpInputHistory = new();
    public List<(int, float)> meleInputHistory = new();
    [HideInInspector]
    public Vector2 PlayerPosRec;

    //Timers
    [Header("Time Line variables")]
    public TextMeshProUGUI godsTimeLineText;
    public TextMeshProUGUI sacredTimeLineText;
    public TextMeshProUGUI startTimeInFutureText;
    public TextMeshProUGUI elapsedTimeInFutureText;

    public TextMeshProUGUI startTimeInPresentText;
    public TextMeshProUGUI elapsedTimeInPresentText;
    public TextMeshProUGUI noOfFutureTravelText;

    [HideInInspector]
    public float sacredTimeLine;
    private float godsTimeLine;
    private float startTimeInFuture;
    private float elapsedTimeInFuture;

    [HideInInspector]
    public float startTimeInPresent;
    private float elapsedTimeInPresent;

    private Vector2 presentPlayerPos;

    //Instances
    GameObject playerFutureInstanceInFuture;
    GameObject playerFutureInstanceInPresent;
    //List<Tuple<Dictionary<KeyCode, Action>, float>> inputTimeData = new();

    //Dictionary<KeyCode, Action> keyActionPairs = new Dictionary<KeyCode, Action> {
    //    { KeyCode.Space, () => GameManager.Instance.playerController.playerMovement.OnJumpInput() },
    //    { KeyCode.D, () => {float right = 1f; } }
    //};

    //public event EventHandler<OnStayingInFutureEventArgs> OnStayingInFuture;

    //public class OnStayingInFutureEventArgs : EventArgs
    //{
    //    public List<(Vector2, float)> _moveInputHistory = new List<(Vector2, float)>();
    //}

    // Start is called before the first frame update
    void Awake()
    {
        i = this;
        inFuture = false;
        goingInMultipleFutures = false;
        startTimeInPresent = 340282346638528859811704183484516925440.000000f;
        //PlayerMultipleFutureInstance.Item2 = startTimeInPresent;
        //PlayerMultipleFutureInstancePos[0] = (PlayerMultipleFutureInstancePos[0].Item1, startTimeInPresent);
        PlayerMultipleFutureInstanceChangeIndex = 0;
        StartCoroutine(UpdateSacreadTime());
    }

    public void PrintTimeCheck()
    {

    }

    #region Sacread TimeLine

    private bool timePaused = false;
    // Property to get the formatted time
    public string FormattedSacredTime
    {
        get { return sacredTimeLine.ToString("0.00000"); }
    }

    IEnumerator UpdateSacreadTime()
    {
        while (true)
        {
            if (!timePaused)
            {
                // Use Time.deltaTime for a more precise update and convert to milliseconds
                sacredTimeLine += Time.deltaTime;
            }

            yield return null; // Use null to wait for the next frame
        }
    }

    // External method to add or subtract time in seconds
    public void ModifySacredTime(float timeToAddInSeconds)
    {
        sacredTimeLine += timeToAddInSeconds; // Convert seconds to milliseconds
    }

    // External method to toggle pause
    public void TogglePause()
    {
        timePaused = !timePaused;
    }

    public void ResetFutureInstanceConfig()
    {
        // print("second time in future");

        moveInputHistory.Clear();
        jumpInputHistory.Clear();
        inFuture = false;
        goingInMultipleFutures = false;
        startTimeInPresent = 340282346638528859811704183484516925440.000000f;
        PlayerMultipleFutureInstanceChangeIndex = 0;
        PlayerMultipleFutureInstancePos.Clear();

        if (playerFutureInstanceInFuture != null) Destroy(playerFutureInstanceInFuture);

        StartCoroutine(UpdateSacreadTime());
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        godsTimeLine = Time.time;
        // Update the elapsed time each frame
        UpdateFutureTimer();
        UpdatePresentTimer();
        UpdateUITimersList();

        if (inFuture)
        {
            futureBar.gameObject.SetActive(true);
            FxF.SetActive(true);
            FxP.SetActive(false);
        }
        else
        {
            futureBar.gameObject.SetActive(false);
            FxF.SetActive(false);
            FxP.SetActive(true);
        }

        //if (elapsedTimeInPresent >= goToSecsInFuture)
        //{
        //    presentBar.gameObject.SetActive(false);
        //}

        //if (elapsedTimeInFuture >= maxTimeInFuture)
        //{
        //    presentBar.gameObject.SetActive(true);
        //}
        if (elapsedTimeInPresent < goToSecsInFuture && elapsedTimeInPresent > 0)
        {
            presentBar.gameObject.SetActive(true);
            presentBar.fillAmount = (elapsedTimeInPresent / goToSecsInFuture);
            print("true");
        }
        else presentBar.gameObject.SetActive(false);

        if (elapsedTimeInPresent >= maxTimeInFuture + goToSecsInFuture + 0.7)
        {
            ResetFutureInstanceConfig();
        }

        //if (Input.GetKeyDown(KeyCode.F))
        if (PlayerInputHandler.Instance.AbilityActivate == 1 && RestartLevel.Instance.currentLevel >= 3)
        {
            PlayerInputHandler.Instance.AbilityActivate = 0;

            if (inFuture)
            {
                return;
                //goingInMultipleFutures = true;
            }


            //check if going back in future before pre recoded actions are completed if not then override new actions in future by reseting future configirations
            if (elapsedTimeInPresent < maxTimeInFuture + goToSecsInFuture && elapsedTimeInPresent > -1)
            {
                return;
                //ResetFutureInstanceConfig();
            }


            TogglePause();

            presentPlayerPos = PlayerOfSacredTimeLine.transform.position;

            //PlayerOfSacredTimeLine.GetComponent<PlayerController>().TimeFreezPanal.SetActive(true);

            if (noOfFutureTravel == 0)
            {
                playerFutureInstanceInFuture = Instantiate(PlayerTimeTravledInstancePrefab, presentPlayerPos, Quaternion.identity, transform);

                // Remove the PlayerMovement component
                Destroy(playerFutureInstanceInFuture.GetComponent<PlayerMovement>());
                Destroy(playerFutureInstanceInFuture.transform.GetChild(1).gameObject);
                //Destroy(playerFutureInstanceInFuture.GetComponent<PlayerCombat>());
                Destroy(playerFutureInstanceInFuture.GetComponent<PlayerAnimator>());

                playerFutureInstanceInFuture.GetComponent<Collider2D>().isTrigger = true;
                playerFutureInstanceInFuture.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }

            playerFutureInstanceInFuture.transform.position = presentPlayerPos;
            //PlayerOfSacredTimeLine.transform.position = PlayerOfSacredTimeLine.GetComponent<PlayerMovement>().lookingRight ? PlayerOfSacredTimeLine.transform.position + new Vector3(4, 0, 0) : PlayerOfSacredTimeLine.transform.position + new Vector3(-4, 0, 0);

            wallColliding = Physics2D.OverlapCircle(sideCheck.position, checkRadius, wallCollideLayer);

            if (!wallColliding)
            {
                PlayerOfSacredTimeLine.transform.position = PlayerOfSacredTimeLine.GetComponent<PlayerMovement>().lookingRight ? PlayerOfSacredTimeLine.transform.position + new Vector3(3, 0.5f, 0) : PlayerOfSacredTimeLine.transform.position + new Vector3(-3, 0.5f, 0);
            }
            else
            {
                PlayerOfSacredTimeLine.transform.position = PlayerOfSacredTimeLine.GetComponent<PlayerMovement>().lookingRight ? PlayerOfSacredTimeLine.transform.position + new Vector3(-3, 0.5f, 0) : PlayerOfSacredTimeLine.transform.position + new Vector3(3, 0.5f, 0);
            }   //}
                //else if (Input.GetKeyUp(KeyCode.F))
                //else if (PlayerInputHandler.Instance.AbilityActivate == 2)
                //{
            PlayerInputHandler.Instance.AbilityActivate = 0;
            TogglePause();
            ModifySacredTime(goToSecsInFuture);
            if (goingInMultipleFutures)
            {
                var pPos = PlayerOfSacredTimeLine.transform.position;
                var pTime = elapsedTimeInFuture + goToSecsInFuture;
                PlayerMultipleFutureInstancePos.Add((pPos, pTime));

                //PlayerMultipleFutureInstance.Item1 = PlayerOfSacredTimeLine.transform.position;
                //PlayerMultipleFutureInstance.Item2 = elapsedTimeInFuture + goToSecsInFuture;
            }
            goingInMultipleFutures = false;
            noOfFutureTravel++;
            if (noOfFutureTravel == 1) PlayerInitalTimeTravelPos = playerFutureInstanceInFuture.transform.position;
            RecordInput();
        }

        //

        if (PlayerMultipleFutureInstancePos.Count > 0 && PlayerMultipleFutureInstanceChangeIndex < PlayerMultipleFutureInstancePos.Count)
            //Teleport future varient to its future
            if (elapsedTimeInPresent >= PlayerMultipleFutureInstancePos[PlayerMultipleFutureInstanceChangeIndex].Item2)
            {
                playerFutureInstanceInPresent.transform.position = PlayerMultipleFutureInstancePos[PlayerMultipleFutureInstanceChangeIndex].Item1;
                //print($"Time : {PlayerMultipleFutureInstancePos[PlayerMultipleFutureInstanceChangeIndex].Item2 - goToSecsInFuture}, Future no. {noOfFutureTravel}");
                //multipleFuturesPlayerInstancePosChanged = true;
                PlayerMultipleFutureInstanceChangeIndex++;
                //print($"{PlayerMultipleFutureInstanceChangeIndex} : {PlayerMultipleFutureInstancePos.Count}");
            }

        // Letting the player spawned time passed when to the time he will spawn as the time where he has gone in future
        if (elapsedTimeInPresent >= goToSecsInFuture && !calledFutureInstance)
        {
            playerFutureInstanceInPresent = Instantiate(PlayerTimeTravledInstancePrefab, PlayerPosRec, Quaternion.identity, transform);
            playerFutureInstanceInPresent.GetComponent<PlayerMovement>().isReplayingFuture = true;
            var petInstancePos = playerFutureInstanceInPresent.transform.GetChild(playerFutureInstanceInPresent.transform.childCount - 1);
            petInstancePos.gameObject.SetActive(true);
            calledFutureInstance = true;
        }

        // Check if a certain amount of time has passed (e.g., 5 seconds)
        if (elapsedTimeInFuture >= maxTimeInFuture && inFuture)
        {
            inFuture = false;
            ModifySacredTime(-(goToSecsInFuture + maxTimeInFuture));
            elapsedTimeInFuture = 0;

            Destroy(playerFutureInstanceInFuture);

            StartPresentTimer();
            //print($"Time Spent in future : {elapsedTimeInFuture}");

            PlayerOfSacredTimeLine.transform.position = noOfFutureTravel > 1 ? PlayerInitalTimeTravelPos : presentPlayerPos;

            noOfFutureTravel = 0;
        }
    }


    public void UpdateUITimersList()
    {
        noOfFutureTravelText.text = "No of multiple future traveled : " + noOfFutureTravel.ToString();
        godsTimeLineText.text = "God's Time Line : " + godsTimeLine.ToString();
        sacredTimeLineText.text = "Sacred Time Line            : " + FormattedSacredTime;
        startTimeInFutureText.text = "start Time In Future         : " + startTimeInFuture.ToString();
        elapsedTimeInFutureText.text = "elapsed Time In Future          : " + elapsedTimeInFuture.ToString();
        startTimeInPresentText.text = "start Time In Present       : " + startTimeInPresent.ToString();
        elapsedTimeInPresentText.text = "elapsed Time In Present  : " + elapsedTimeInPresent.ToString();
    }

    public void RecordInput()
    {
        inFuture = true;
        StartFutureTimer();
    }

    void StartFutureTimer()
    {
        if (noOfFutureTravel > 1)
        {
            //Reset strat time so that elapsed time in future can remain constant and start from where it left off after time freez is tured off insted of starting from zero(0) again
            startTimeInFuture = (Time.time - elapsedTimeInFuture);

            //Reset player record time start input if traveling in multiple future
            PlayerOfSacredTimeLine.GetComponent<PlayerMovement>().startTimeInFutureWhenInFutureForMove = startTimeInFuture;
            PlayerOfSacredTimeLine.GetComponent<PlayerMovement>().startTimeInFutureWhenInFutureForJump = startTimeInFuture;
            PlayerOfSacredTimeLine.GetComponent<PlayerMovement>().startTimeInFutureWhenInFutureForMele = startTimeInFuture;
        }
        else
        {
            // Set the start time to the current time
            startTimeInFuture = Time.time;
        }
    }

    void UpdateFutureTimer()
    {
        if (!inFuture) return;

        if (goingInMultipleFutures) return;

        // Calculate the elapsed time
        elapsedTimeInFuture = Time.time - startTimeInFuture;
    }

    public void StartPresentTimer()
    {
        // Set the start time to the current time
        startTimeInPresent = Time.time;
    }

    void UpdatePresentTimer()
    {
        // Calculate the elapsed time
        elapsedTimeInPresent = Time.time - startTimeInPresent;
        futureBar.fillAmount = 1 - (elapsedTimeInFuture / maxTimeInFuture);
    }
}

