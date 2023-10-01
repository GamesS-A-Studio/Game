using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move2 : MonoBehaviour
{
    public static Move2 instancia;
    public Image staminaBar;

    public float jumpForce1 = 5f;
    public float jumpTime = 1f;
    float duraçãoPulo;
    public float jumpDuration;
    public int jumpQuant;

    float checkRadius = 0.8f;
    private Rigidbody2D rb;
    public AudioSource SonsPlayer;
    public AudioClip[] audios;
    bool canDashDir;
    bool canDashEsqu;
    bool isGrounded;
    bool isWall;
    public float speed;
    public float SpeedMax;
    public float speedPadrão;
    private float moveInput;
    private int numeroplat;
    float tapDashEsqu;
    float tapDashDir;
    float resetDash = 0.15f;
    Direção direção;
    public Estates estados;
    public Transform GroundCheck;
    public LayerMask whatIsGround;
    Animator anim;
    private bool facingRight = true;
   
    [SerializeField]
    [Range(0,150)]
    public float estamina;

    IEnumerator dashcourotine;
    public bool isdashing;

    public float tempoduradash;
    public float coldowndash;
    public float forçaDash;
    bool dashlivre;
    bool podeMovimentar;
    Vector2 velocidade;
    Vector2 originalVelocity;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        instancia = this;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        speedPadrão = speed;
    }  
    private void Update()
    {
        string tipo = estados.ToString();
        limitaçãoVel();
        switch (tipo)
        {
            case "Menu":
                velocidade.x = 0;
                this.rb.velocity = velocidade;
                anim.Play("PIdle", -1);
                anim.SetBool("MoveP",false);
                podeMovimentar = false;            
                break;
            case "Stun":
                velocidade.x = 0;
                this.rb.velocity = velocidade;
                anim.Play("PStun", -1);
                anim.SetBool("MoveP", false);
                podeMovimentar = false;
                break;
            default:
                podeMovimentar = true;
                break;
        }    
        if(podeMovimentar)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (jumpQuant>0)
                {
                    duraçãoPulo = jumpDuration;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpQuant =0;
                anim.SetBool("RetornaJump", false);
                duraçãoPulo = 0;
            }
            if (duraçãoPulo > 0)
            {
                Jump();
                duraçãoPulo -= 5 * Time.deltaTime;
            }
           
            ///
            if (!dashlivre)
            {

                if (Input.GetKeyDown(KeyCode.D) && tapDashDir <=0)
                {
                    tapDashDir = resetDash;
                    tapDashEsqu = 0;
                }   
                if (Input.GetKeyDown(KeyCode.A) && tapDashEsqu <= 0)
                {
                    tapDashDir = 0;
                    tapDashEsqu = resetDash;
                }
                if (Input.GetKeyUp(KeyCode.D))
                {
                    canDashDir = true;
                    canDashEsqu = false;
                }
                if (Input.GetKeyUp(KeyCode.A))
                {
                    canDashDir = false;
                    canDashEsqu = true;
                }
                if (tapDashEsqu > 0 && canDashEsqu)
                {
                    tapDashEsqu -= Time.deltaTime;
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        if (dashcourotine != null && estamina >= 30)
                        {
                            StopCoroutine(dashcourotine);
                        }
                        dashcourotine = Dash(tempoduradash, coldowndash);
                        StartCoroutine(dashcourotine);
                        canDashEsqu = false;
                        tapDashEsqu = 0;
                    }
                }
                if(tapDashDir >0 && canDashDir)
                {
                    tapDashDir -= Time.deltaTime;
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        if (dashcourotine != null && estamina >= 30)
                        {
                            StopCoroutine(dashcourotine);
                        }
                        dashcourotine = Dash(tempoduradash, coldowndash);
                        StartCoroutine(dashcourotine);
                        canDashDir = false;
                        tapDashEsqu = 0;
                    }
                }
            }

        }
    
    }
    private void FixedUpdate()
    {
        Move();
        updadeBarraStamina();
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, whatIsGround);
        if (isGrounded)
        {
            jumpQuant = 1;
            anim.SetBool("RetornaJump", false);
        }
        else if(!isdashing)
        {
            rb.gravityScale = 10f;
            anim.SetBool("MoveP", false);
        }
        if (isdashing)
        {
            rb.AddForce(new Vector2(moveInput * forçaDash, 0), ForceMode2D.Impulse);
        }

    }
 
    private void Jump()
    {
         rb.velocity = new Vector2(rb.velocity.x, jumpForce1);
         anim.SetBool("RetornaJump", true);
    
    }
    public void Move()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if(moveInput != 0)
        {
            velocidade = this.rb.velocity;
            velocidade.x = moveInput * this.speed;
            this.rb.velocity = velocidade;
            if (isGrounded)
            {
                if (moveInput > 0 || moveInput < 0)
                {
                    anim.SetBool("MoveP", true);
                }
                else
                {
                    anim.SetBool("MoveP", false);

                }
            }

            if (facingRight == false && moveInput > 0)
            {
                Flip();
                direção = Direção.Direita;
            }
            else if (facingRight == true && moveInput < 0)
            {
                Flip();
                direção = Direção.Esquerda;
            }
        }
       
      
    }
    IEnumerator Dash(float dashduração, float dashcoldown)
    {
        float g = rb.gravityScale;
        originalVelocity = rb.velocity;
        rb.velocity = Vector2.zero;
        isdashing = true;
        anim.Play("PDash", 0);
        estamina -= 25;
        SonsPlayer.clip = audios[0];
        SonsPlayer.Play(0);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(dashduração);
        isdashing = false;
        SonsPlayer.Stop();
        rb.gravityScale = g;
        rb.velocity = originalVelocity;
        yield return new WaitForSeconds(dashcoldown);
        StopAllCoroutines();

    }
    public void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.CompareTag("Plataforma"))
        {
            isGrounded = true;
            PlatformEffector2D plat = coll.GetComponent<PlatformEffector2D>();
            if(plat != null )
            {
                if(Input.GetKey(KeyCode.S))
                {
                    numeroplat = numeroplat+1;
                    if(numeroplat >=2)
                    {
                        plat.rotationalOffset = 180;
                        numeroplat = 0;
                    } 
                }
            }
        }
        else { numeroplat = 0; }

    }

    public void OnTriggerExit2D(Collider2D coll)
    {
        if ( coll.CompareTag("Plataforma"))
        {

            PlatformEffector2D plat = coll.GetComponent<PlatformEffector2D>();
            if (plat != null)
            {
                plat.rotationalOffset = 0;
            }
        }
    }
    void limitaçãoVel()
    {
        Vector3 velocidadeH = new Vector3(rb.velocity.x, 0, 0);
        if (velocidadeH.magnitude > SpeedMax)
        {
            Vector3 limite = velocidadeH.normalized * SpeedMax;
            rb.velocity = new Vector3(limite.x, rb.velocity.y, limite.z);
        }
    }
    public void updadeBarraStamina()
    {
        staminaBar.fillAmount = estamina / 150;
        if (estamina < 150)
        {
            estamina = estamina + 30 * Time.deltaTime;
        }
        else if (estamina > 150) { estamina = 150; }
        if (estamina <= 0)
        {
            estamina = 0;
        }

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GroundCheck.transform.position, checkRadius);
    }
}