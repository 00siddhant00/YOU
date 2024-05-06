using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float speed;
    public GameObject bulletFx;

    private void Start()
    {
        //gameObject.SetActive(false);
    }
    private void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;
        //Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    print(collision.name);
    //    //if (collision.gameObject.name != "Confiner")
    //    Destroy(gameObject);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Confiner")
        {
            Instantiate(bulletFx, transform.position, bulletFx.transform.rotation);
            GameManager.Instance.CameraShake.ShakeCamera(7f, 5f, 0.15f);
            RumbleManager.instance.RumblePulse(0.3f, 0.7f, 0.11f);
            Destroy(gameObject);
        }
    }
}
