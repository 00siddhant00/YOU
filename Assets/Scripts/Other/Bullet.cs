using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float speed;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * speed;
        //Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
        if (collision.gameObject.name != "Confiner")
            Destroy(gameObject);
    }
}
