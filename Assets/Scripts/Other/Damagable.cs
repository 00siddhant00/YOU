using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //PlayerController.OnPlayerDeathAction.Invoke();
            RestartLevel.Instance.gameOverRestart();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //PlayerController.OnPlayerDeathAction.Invoke();
            RestartLevel.Instance.gameOverRestart();
        }
    }


}
