using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerMovement mov;

    [Header("Movement Tilt")]
    [SerializeField] private float maxTilt;
    [SerializeField][Range(0, 1)] private float tiltSpeed;

    [Header("Particle FX")]
    [SerializeField] public Color fxColor;
    [SerializeField] private GameObject jumpFX;
    [SerializeField] private GameObject landFX;
    [SerializeField] private GameObject HitFX;
    private ParticleSystem _jumpParticle;
    private ParticleSystem _landParticle;
    private Animator anim;
    public bool isMoving;

    private Rigidbody2D rb;

    public bool startedJumping { private get; set; }
    public bool justLanded { private get; set; }

    public bool isGrounded = true;

    private void Start()
    {
        mov = GetComponent<PlayerMovement>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //GFX = transform.GetChild(0);
        //legsAnim = GFX.GetChild(2).GetComponent<Animator>();

        _jumpParticle = jumpFX.GetComponent<ParticleSystem>();
        _landParticle = landFX.GetComponent<ParticleSystem>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            yield return new WaitForSeconds(Random.Range(0, 3));

            anim.SetTrigger("Blink");
        }
        yield return new WaitForSeconds(Random.Range(0, 4));
        StartCoroutine(Blink());
    }

    private void LateUpdate()
    {
        #region Tilt

        isMoving = mov._moveInput.x != 0;
        //isMoving = Mathf.Abs(rb.velocity.x) > 0f && mov._moveInput.x != 0;
        MoveAnim();
        JumpAnim();

        if (startedJumping) isGrounded = false;
        if (justLanded) isGrounded = true;

        if (isMoving)
        {
            anim.speed = Mathf.Abs(mov._moveInput.x);
        }
        else
        {
            anim.speed = 1;
        }

        #endregion

        CheckAnimationState();

        ParticleSystem.MainModule jumpPSettings = _jumpParticle.main;
        jumpPSettings.startColor = new ParticleSystem.MinMaxGradient(mov.GetGroundColor());
        ParticleSystem.MainModule landPSettings = _landParticle.main;
        landPSettings.startColor = new ParticleSystem.MinMaxGradient(mov.GetGroundColor());
    }

    float lerpValue;

    public void MoveAnim()
    {
        anim.SetBool("IsRunning", isMoving);
        if (isMoving)
        {
            // Increment the lerp value from 0 to 1 over 1 second
            lerpValue += Time.deltaTime * 2; // Increment lerpValue by Time.deltaTime

            // Clamp lerpValue to ensure it stays between 0 and 1
            lerpValue = Mathf.Clamp01(lerpValue);

            // Set the "moveInput" parameter using lerpValue
            anim.SetFloat("moveInput", lerpValue);
        }
        else
        {
            // If isMoving is false, reset lerpValue back to 0
            lerpValue = 0f;
        }
    }

    float maxVel;

    public void JumpAnim()
    {
        anim.SetBool("IsJumping", !isGrounded);

        //print(rb.velocity.y);

        //if (maxVel < rb.velocity.y)
        //{
        //    maxVel = rb.velocity.y;
        //}

        if (isGrounded)
        {
            return;
        }
        anim.SetFloat("jumpVal", rb.velocity.y);
    }

    public void MoveAnim(bool move)
    {
        anim.SetBool("IsRunning", move);
    }

    public void WallHit(Transform pos)
    {
        GameObject obj = Instantiate(HitFX, pos.position, Quaternion.identity);
        Destroy(obj, 1);
    }

    private void CheckAnimationState()
    {
        if (startedJumping)
        {
            //anim.SetTrigger("Jump");
            GameObject obj = Instantiate(jumpFX, transform.position - (Vector3.up * 1.25f), Quaternion.Euler(-90, 0, 0));
            Destroy(obj, 1);
            startedJumping = false;
            return;
        }

        if (justLanded)
        {
            GameManager.Instance.CameraShake.ShakeCamera(0.7f, 0.29f, 0.15f);
            //anim.SetTrigger("Land");
            GameObject obj = Instantiate(landFX, transform.position - (Vector3.up * 1.25f), Quaternion.Euler(-90, 0, 0));
            Destroy(obj, 1);
            justLanded = false;
            return;
        }

        //anim.SetFloat("Vel Y", mov.RB.velocity.y);
    }
}
