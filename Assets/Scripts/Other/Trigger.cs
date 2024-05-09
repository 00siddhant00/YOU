using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [System.Serializable]
    public struct Obsticals
    {
        public GameObject obstical;
        public float delay;
    }
    public VisualTriggerLight[] vl;

    public Transform cannonSpawn;

    public Obsticals[] obsticals;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            foreach (var ob in obsticals)
            {
                StartCoroutine(SpawnSpike(ob));
            }
        }
        FindObjectOfType<AudioManager>().Play("Trig");
        foreach (var v in vl)
        {
            v.FadeTriggered(true);
        }
    }

    IEnumerator SpawnSpike(Obsticals obs)
    {
        yield return new WaitForSeconds(obs.delay);
        if (obs.obstical.TryGetComponent<Spikes>(out Spikes spikes))
        {
            obs.obstical.SetActive(true);
            spikes.TriggerSpike();
        }
        else if (obs.obstical.TryGetComponent<Bullet>(out Bullet bullet))
        {
            //obs.obstical.SetActive(true);
            GameObject bulletPrefab = Instantiate(obs.obstical, cannonSpawn.position, Quaternion.identity);            //bullet.TriggerBullet();
            FindObjectOfType<AudioManager>().Play("Shoot");
            bulletPrefab.SetActive(true);
        }

        //Destroy(gameObject);
    }

}
