using UnityEngine;

public class P_Plate : PuzzleBase
{
    private Transform GFX;
    public P_Door pDoor;
    public VisualTriggerLight[] vl;


    public bool allowOpen = true;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.childCount > 0)
            GFX = transform.GetChild(0);
        else Debug.LogWarning("GFX dosnt exist");

        AddDoorToPlatesCheck();
    }

    public void AddDoorToPlatesCheck()
    {
        OnKeyEnabled += pDoor.OpenDoor;
    }

    private void OnDestroy()
    {
        OnKeyEnabled -= pDoor.OpenDoor;
    }
    // Update is called once per frame
    void Update()
    {
        CheckIfKeyEnabled();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<AudioManager>().Play("Plate");
        if (collision.gameObject.CompareTag("Player"))
        {
            PressPlate(true);
            if (vl != null)
            {
                foreach (var v in vl)
                {
                    v.FadeTriggered(true);
                }
            }
            if (allowOpen)
            {
                keyEnabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FindObjectOfType<AudioManager>().Play("Plate");
        if (collision.gameObject.CompareTag("Player"))
        {
            PressPlate(false);
            if (vl != null)
                foreach (var v in vl)
                {
                    v.FadeTriggered(false);
                }
            if (!allowOpen) return;

            keyEnabled = false;
            pDoor.CloseDoor();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PressPlate(true);
            if (allowOpen)
            {
                keyEnabled = true;
                if (vl != null)
                    foreach (var v in vl)
                    {
                        v.FadeTriggered(true);
                    }
            }
        }
    }

    void PressPlate(bool pressed)
    {
        if (pressed)
        {
            GFX.localPosition = new Vector3(0f, -0.33f, 0f);
        }
        else GFX.localPosition = new Vector3(0f, 0f, 0f);
    }
}
