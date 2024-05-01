//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class P_Door : MonoBehaviour, IDoor
//{
//    public bool OpenJustOnce = false;
//    public float openSpeed;
//    public enum MoveDirection
//    {
//        up,
//        down,
//        left,
//        right
//    }

//    public enum OpenType
//    {
//        Smooth,
//        Snap
//    }

//    public OpenType openType;

//    public MoveDirection moveDirection;
//    public float maxMoveDistance = 1f;

//    private Vector3 originalPosition;
//    private Vector3 targetPosition;
//    public bool isOpening = false;

//    public bool opened;

//    // Start is called before the first frame update
//    void Start()
//    {
//        originalPosition = transform.position;
//        targetPosition = originalPosition;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (!isOpening)
//        {
//            if (opened) return;
//            // If not opening, smoothly move back to the original position
//            transform.position = Vector3.MoveTowards(transform.position, originalPosition, (openType == OpenType.Smooth ? openSpeed : 1000) * Time.deltaTime);
//        }

//        if (isOpening && OpenJustOnce)
//        {
//            OpenDoor();
//        }
//    }

//    public void OpenDoor()
//    {
//        isOpening = true;

//        // Set the target position based on the selected move direction
//        switch (moveDirection)
//        {
//            case MoveDirection.up:
//                targetPosition = originalPosition + Vector3.up * maxMoveDistance;
//                break;
//            case MoveDirection.down:
//                targetPosition = originalPosition + Vector3.down * maxMoveDistance;
//                break;
//            case MoveDirection.left:
//                targetPosition = originalPosition + Vector3.left * maxMoveDistance;
//                break;
//            case MoveDirection.right:
//                targetPosition = originalPosition + Vector3.right * maxMoveDistance;
//                break;
//        }

//        transform.position = Vector3.MoveTowards(transform.position, targetPosition, (openType == OpenType.Smooth ? openSpeed : 1000) * Time.deltaTime);

//        if (OpenJustOnce)
//        {
//            opened = true;
//        }
//    }

//    public void CloseDoor()
//    {
//        isOpening = false;
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Door : MonoBehaviour, IDoor
{
    public bool OpenJustOnce = false;
    public float openSpeed;
    public float closeSpeed;

    private float currentSpeed;

    bool once = true;

    public enum MovementType
    {
        Linear,
        Exponential
    }

    public enum MoveDirection
    {
        up,
        down,
        left,
        right
    }

    public enum OpenType
    {
        Smooth,
        Snap
    }
    public MovementType movementType;

    public OpenType openType;

    public MoveDirection moveDirection;
    public float maxMoveDistance = 1f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    public bool isOpening = false;

    public float closeDelay;

    public bool opened;
    private GameObject lastChild;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        targetPosition = originalPosition;

        if (transform.childCount > 0)
        {
            lastChild = transform.GetChild(transform.childCount - 1).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = isOpening ? openSpeed : closeSpeed;

        if (!isOpening)
        {
            if (opened) return;
            // If not opening, smoothly move back to the original position
            StartClosingDoor();
        }

        if (isOpening && OpenJustOnce)
        {
            OpenDoor();
        }
    }

    public void StartClosingDoor()
    {
        if (Vector3.Distance(transform.position, originalPosition) < 0.1f)
        {
            if (!once)
            {
                GameManager.Instance.CameraShake.ShakeCamera(10f, 7f, 0.15f);
                RumbleManager.instance.RumblePulse(0.5f, 0.9f, 0.12f);
                once = true;
            }

            return;
        }

        // Calculate the remaining distance
        float remainingDistance = Vector3.Distance(transform.position, originalPosition);

        // Calculate the target speed based on the remaining distance
        float targetSpeed = Mathf.Lerp(closeSpeed, 0f, remainingDistance / maxMoveDistance);

        // Interpolate the current speed towards the target speed
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 100);

        // Move the door towards the original position with the current speed
        transform.position = Vector3.MoveTowards(transform.position, originalPosition, (openType == OpenType.Smooth ? (movementType == MovementType.Exponential ? currentSpeed : closeSpeed) : 1000) * Time.deltaTime);
        //RumbleManager.instance.RumblePulse(0.002f, 0.005f, 0.01f);
    }

    public void OpenDoor()
    {
        isOpening = true;
        once = false;

        // Set the target position based on the selected move direction
        switch (moveDirection)
        {
            case MoveDirection.up:
                targetPosition = originalPosition + Vector3.up * maxMoveDistance;
                break;
            case MoveDirection.down:
                targetPosition = originalPosition + Vector3.down * maxMoveDistance;
                break;
            case MoveDirection.left:
                targetPosition = originalPosition + Vector3.left * maxMoveDistance;
                break;
            case MoveDirection.right:
                targetPosition = originalPosition + Vector3.right * maxMoveDistance;
                break;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, (openType == OpenType.Smooth ? currentSpeed : 1000) * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) > 1)
        {
            GameManager.Instance.CameraShake.ShakeCamera(2f, 3f, 0.1f);
            if (RestartLevel.Instance.currentLevel != 7)
                RumbleManager.instance.RumblePulse(0.002f, 0.005f, 0.01f);
        }

        if (OpenJustOnce)
        {
            opened = true;
        }



        if (lastChild != null)
        {
            lastChild.SetActive(false);
        }
    }

    public void CloseDoor()
    {
        StartCoroutine(CloseDoorAfterDelay());
    }

    IEnumerator CloseDoorAfterDelay()
    {
        this.gameObject.SetActive(true);
        yield return new WaitForSeconds(closeDelay);

        isOpening = false;

        if (lastChild != null)
        {
            lastChild.SetActive(true);
        }
    }
}