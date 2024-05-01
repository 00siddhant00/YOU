using UnityEngine;
using System.Collections;

public class EnableDisable : State
{
    private float delay = 4f;

    public EnableDisable(StateMachine stateMachine, References rf) : base(stateMachine, rf)
    {

    }

    public override void EnterState()
    {
        if (RestartLevel.Instance.currentLevel == 0)
            rf.StartCoroutine(EnableDisablePlayerGraphics(delay));
        else
            rf.StartCoroutine(EnableDisablePlayerGraphics(0));
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }

    private IEnumerator EnableDisablePlayerGraphics(float delay)
    {
        yield return new WaitForSeconds(delay);

        rf.virtualCamera.enabled = true;
        //originalPLayerGfx = originalPLayerGfx.Where(g => g != L.Item1).ToArray();

        foreach (GameObject obj in rf.fakePLayerGfx)
        {
            obj.SetActive(false);
        }
        // Enable originalPlayerGfx and disable fakePlayerGfx
        foreach (GameObject obj in rf.originalPLayerGfx)
        {
            obj.SetActive(true);
        }

        rf.bsr.color = rf.originalColor;


        rf.StartCoroutine(PlatformLVLMinusOneSpawn());
        //rf.L.Item2.SetActive(false);
        //yield return new WaitForSeconds(0.001f);
        //rf.L.Item1.SetActive(true);
    }

    IEnumerator PlatformLVLMinusOneSpawn()
    {
        yield return new WaitForSeconds(1f);
        rf.PlatformZero.SetActive(true);
    }
}
