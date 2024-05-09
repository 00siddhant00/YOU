using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualTriggerLight : MonoBehaviour
{
    public SpriteRenderer[] spritesR;
    public GameObject light;
    public Color low;
    public Color high;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void FadeTriggered(bool onFade)
    {
        if (onFade)
        {
            if (light != null)
                light.SetActive(true);
            foreach (var spriteR in spritesR)
            {
                spriteR.color = high;
            }
        }
        else
        {
            if (light != null)
                light.SetActive(false);
            foreach (var spriteR in spritesR)
            {
                spriteR.color = low;
            }
        }
    }
}
