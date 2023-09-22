using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{

    Animator animator;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] Vector2 playerCheckSize;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform playerCheck;

    public float speed;
     bool isGrounded;
     bool isPlayer;
     bool isAgainstWall;
     bool flipPosition;
    bool canMove;
    public float animationTime;
    public float preparationTime;
    bool flipped;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (flipPosition)
        {
            Flip();
        }
    }

    void Update()
    {

        if (isPlayer)
        {

            canMove = true;
            StartCoroutine(PerformAttack());
            animator.SetBool("Move", false);
        }
        if (!canMove)
        {
            if (isAgainstWall || !isGrounded)
            {
                animator.SetBool("Move", false);
                if (!flipped)
                {
                    StartCoroutine(SwapPosition());
                    flipped = true;
                }
            }
            else
            {
                StopCoroutine(SwapPosition());
                animator.SetBool("Move", true);
                if(transform.localScale.x >0)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * speed);
                }
                else
                {
                    transform.Translate(Vector3.left* Time.deltaTime * speed);
                }

            }

        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        isAgainstWall = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, wallLayer);

        isPlayer = Physics2D.OverlapBox(playerCheck.position, playerCheckSize, 0, playerLayer);

    }


    public void Flip()
    {
        flipped = !flipped;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }

    IEnumerator SwapPosition()
    {
        animator.SetBool("Move", false);
        yield return new WaitForSeconds(4f);
        if(isAgainstWall || !isGrounded)
        {
            Flip();
        }
        else { flipped = false;  yield break; }
    
    }

    IEnumerator PerformAttack()
    {

        yield return new WaitForSeconds(preparationTime);
        animator.Play("Atack", 0);
        yield return new WaitForSeconds(animationTime);
        canMove = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(playerCheck.position, playerCheckSize);
    }
}
