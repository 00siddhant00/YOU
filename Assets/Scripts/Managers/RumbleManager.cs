using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class RumbleManager : MonoBehaviour
{
    public static RumbleManager instance;

    private Gamepad pad;

    public Coroutine stopRumbleAfterTimeCoroutine;


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

    public void RumblePulse(float lowFrequency, float highFrequency, float duration)
    {
        pad = Gamepad.current;

        if (pad != null)
        {
            //start Rumble 

            pad.SetMotorSpeeds(lowFrequency, highFrequency);


            //Stop Rumble
            stopRumbleAfterTimeCoroutine = StartCoroutine(StopRumble(duration, pad));
        }

    }

    private IEnumerator StopRumble(float duration, Gamepad pad)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        //StoppingRumble
        pad.SetMotorSpeeds(0, 0);

    }
}
