using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerMovement playerMovement;
    [HideInInspector] public PlayerAnimator playerAnimator;

    public void DamagePlayer()
    {
        print("Damaging");
    }

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimator = GetComponent<PlayerAnimator>();

        playerMovement.allowPlayerMovement = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Gate gate))
        {
            StartCoroutine(ChangeSection(gate));
        }
    }

    private IEnumerator ChangeSection(Gate gate)
    {
        yield break;
        //ScenesManager scenesManager = GameManager.Instance.sceneManager;

        //gate.loadNextLevel.gameObject.SetActive(true);

        //this.transform.parent = gate.loadNextLevel.transform;
        //this.transform.SetAsFirstSibling();

        //scenesManager.Fade();
        //StartCoroutine(TogglePlayerMovement(false, 0.27f));
        //yield return new WaitForSeconds((scenesManager.fadeDuration));
        //StartCoroutine(TogglePlayerMovement(true, (scenesManager.fadeDuration + (scenesManager.waitForFadeOut / 2)) - 0.12f));

        ////Spawns Player to next gate position
        //this.transform.position = gate.loadNextLevel.transform.Find("Spawn").position;

        //yield return new WaitForSeconds(0.1f);

        ////Sets last section exited as Disabled
        //gate.transform.parent.gameObject.SetActive(false);
        //scenesManager.ChangeSectionConfiner(gate.loadNextLevel.transform.Find("Confiner").gameObject);
    }

    IEnumerator TogglePlayerMovement(bool allow, float duration)
    {
        yield return new WaitForSeconds(duration);
        playerMovement.allowPlayerMovement = allow;
    }
}
