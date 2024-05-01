using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;

    //TaskManager taskManager;
    public TMP_Text dialogueTxtDisp;
    public bool dialoguePlaying;

    public string[] allDialogue;
    public int dialogIndex;
    public float dialogueSpeed;
    public string writtenTextDial;
    public GameObject ObjectToEnableAfterDialodue;


    public AudioSource dialogueAudioSource;
    public List<AudioClip> dialogueClip;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    private void OnEnable()
    {
        writtenTextDial = "";
        writtenTextDial = null;
        dialogueTxtDisp.text = writtenTextDial;

        dialogIndex = 0;
        dialoguePlaying = false;
        // taskManager = FindAnyObjectByType<TaskManager>();
        StartCoroutine(WriteDialogue());
    }

    void Start()
    {

    }


    void Update()
    {
        //if (dialogIndex < allDialogue.Length)
        //{
        //    StartCoroutine(WriteDialogue());

        //}
        //else
        //{
        //    //taskManager.GameStarted = true;
        //    writtenTextDial = "";
        //    gameObject.SetActive(false);
        //    dialogueAudioSource.Stop();
        //    dialogueAudioSource.clip = null;
        //    if (ObjectToEnableAfterDialodue != null)
        //        ObjectToEnableAfterDialodue.SetActive(true);
        //}
    }

    IEnumerator WriteDialogue()
    {
        FindObjectOfType<AudioManager>().Play("Jump");

        dialogueTxtDisp.text = writtenTextDial;
        if (dialogIndex < allDialogue.Length)
        {
            dialogueAudioSource.clip = dialogueClip[dialogIndex];
            dialogueAudioSource.Play();

        }

        foreach (char ch in allDialogue[dialogIndex].ToCharArray())
        {
            dialoguePlaying = true;
            writtenTextDial += ch;
            dialogueTxtDisp.text = writtenTextDial;
            if (ch == '.')
            {
                yield return new WaitForSeconds(1f);

            }
            Debug.Log("Called");
            yield return new WaitForSeconds(dialogueSpeed);

        }
        dialoguePlaying = false;
        dialogueAudioSource.Pause();
        if (dialogIndex < allDialogue.Length)
        {
            yield return new WaitForSeconds(1f);
            PlayNExtDialogue();
        }
    }

    public void ActivateDialogue()
    {
        writtenTextDial = null;
        writtenTextDial = "Click to continue ------";
        dialogueTxtDisp.text = writtenTextDial;
        gameObject.SetActive(true);
    }

    public void PlayNExtDialogue()
    {

        writtenTextDial = "";
        writtenTextDial = null;
        dialogIndex++;
        StartCoroutine(WriteDialogue());
        //if (dialogIndex < allDialogue.Length)
        //{
        //    dialogueAudioSource.clip = dialogueClip[dialogIndex];
        //    dialogueAudioSource.Play();

        //}

    }

}
