
using UnityEngine;
using UnityEngine.UI;
using FirstGearGames.SmoothCameraShaker;
using System.Collections;

public class MovimentPlayer : MonoBehaviour
{
    GameManager gm;
    Vector3 offsetOriginal;

    [Header("_________Jump Proprietars________________________________")]
    [Header("____________________________________________________________")]
    public float jumpForce = 5f;
    public float jumpTime = 1f;
    float duraoPulo;
    public float jumpDuration;
    public Vector2 foraJumpWall;
    public bool iswall;
    public float radiusWall;
    public LayerMask layerWall;
    public float wallslidinSpeed;
    [Header("_________Geral Proprietars________________________________")]
    [Header("____________________________________________________________")]
    public Color corSthealt;
    public Color corSemiSthealt;
    public float speed;
    public float speedFixo;
    public Rigidbody2D rb;
    public float horizontal;
    public SpriteRenderer sp;
    public Image estamIm;
    public Animator anim;
    public LayerMask groundLayer;
    public float lokDistanceCamera;
    public bool upLook;
    public bool isgrounded;
    public float radiusGroundCheck;
    public float staminaMax = 100;
    public float staminaAtual = 100;
    float cdJumpWall;
    public float quedaJump;
    bool sliding;
    public GameObject particulaTerra;
    [Header("_________Stealth Proprietars________________________________")]
    [Header("____________________________________________________________")]
    public float radiusAreaStealth = 0.15f;
    public LayerMask layerStealth;
    float contStealth;
    public float contStealthValue;
    public bool areaStalth;
    Collider2D[] coll;
    private Vector3 currentVelocity;
    public float smoothTime = 0.2f;
    public CinemachineCameraOffset cc;
    [Header("__________Dash_____________")]
    [Header("__________Dash_____________")]
    public bool fazendodash;
    public float DuraoDash;
    public float CDDash;
    public float foraDash;
    public ShakeData dashShake;
    public GameObject particleDash;
    IEnumerator dashNum;
    float CDcontador;
    private void Start()
    {       
        speedFixo = speed;
        rb = GetComponent<Rigidbody2D>();
        staminaAtual = staminaMax;
        gm = GameManager.gmInstance;

        if (cc != null)
        { 
            offsetOriginal = cc.m_Offset;
        }
    }
    void Update()
    {
        if (speed < speedFixo/2)
        {
            speed = speedFixo/2;
            gm.iconSthealt.color = corSemiSthealt;
        }
          
        if (!upLook)
        {

            if (cc != null)
            {
                cc.m_Offset.y = Mathf.Lerp(cc.m_Offset.y, offsetOriginal.y, 5f * Time.deltaTime);                   
            }
        }
        else if (upLook)
        {
            if (cc != null)
            {
                if (cc.m_Offset.y < offsetOriginal.y + lokDistanceCamera / 2.5f)
                {
                    cc.m_Offset.y += Time.deltaTime;
                    
                }
            }

        }
  
        if (!upLook || !fazendodash)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }       
        Flip();
        KeyClick();
        SthealtMecanic();
        animacoes();
        Contagens();
    }
    private void FixedUpdate()
    {
        float targetVelocityX = horizontal * speed;
        rb.velocity = new Vector3( Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref currentVelocity.x, smoothTime),  rb.velocity.y, 0  );
        isgrounded = Physics2D.OverlapCircle(transform.position, radiusGroundCheck, groundLayer);
        iswall = (gm.direcaoPlayer == DirecaoPlayer.direita) ? Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y+0.5f), radiusWall, layerWall):
            Physics2D.OverlapCircle(new Vector2(transform.position.x , transform.position.y + 0.5f), radiusWall, layerWall);

        if (duraoPulo > 0)
        {
            Jump();
            duraoPulo -= 5 * Time.deltaTime;
           
        }
        coll = Physics2D.OverlapCircleAll(transform.position, radiusAreaStealth, layerStealth);
        if (coll.Length > 0)
        {
            foreach (Collider2D collider in coll)
            {
                if (collider.gameObject.layer == 12)
                {
                    areaStalth = true;
                }
                else
                {
                    areaStalth = false;
                }
            }
        }
        else
        {
            areaStalth = false;
            gm.visibilidadePlayer = VisibilidadePlayer.Visivel;
        }
      
    }
    public void Contagens()
    {
        if (CDcontador > 0)
        {
            CDcontador -= Time.deltaTime;
        }
        if (staminaAtual > staminaMax)
        {
            staminaAtual = staminaMax;
        }
        else
        {

            staminaAtual = staminaAtual + 15f * Time.deltaTime;
            estamIm.fillAmount = staminaAtual / staminaMax;
        }
        if (cdJumpWall > 0)
        {
            cdJumpWall -= Time.deltaTime;
        }
        if (fazendodash && !isgrounded)
        {
            if (duraoPulo <= 0 && !iswall)
            {
                rb.velocity += Vector2.down * quedaJump * Time.deltaTime;
                sliding = false;
            }

        }
        particulaTerra.SetActive(iswall);
    }
    public void KeyClick()
    {
       
        if(isgrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                duraoPulo = jumpDuration;
            }
            sliding = false;
        }
        else
        {
            if (iswall && horizontal != 0)
            {
                slidingWall();
                sliding = true;
                if (Input.GetKeyDown(KeyCode.Space) && cdJumpWall<=0)
                {
                    JumpWall();    
                }
            }
            else if (horizontal == 0)
            {
                sliding = false;
            }
            if (Input.GetKeyUp(KeyCode.Space) && !iswall)
            {
                duraoPulo = 0;
            }
        }
        if(Input.GetKey(KeyCode.W))
        {
            upLook = true;
        }
        else if (Input.GetKeyUp(KeyCode.W)) { upLook = false; }
        if (Input.GetKeyDown(KeyCode.Mouse1) && CDcontador<=0)
        {
            if (gm.movimentPlayer.staminaAtual >= 50)
            {
                if (dashNum == null)
                {
                    gm.movimentPlayer.staminaAtual = gm.movimentPlayer.staminaAtual - 50;
                    StartCoroutine(dash());
                    dashNum = dash();
                }

            }
        }
    }
    public void animacoes()
    {
        anim.SetBool("Move", Mov() > 0.3f && isgrounded || Mov() > 0.3f &&  isgrounded);
        anim.SetBool("MoveLow", Mov() > 0.3f && isgrounded || Mov() > 0.3f && isgrounded);
        anim.SetBool("DownLook", isgrounded);
        anim.SetBool("LookUp", upLook && isgrounded);
        anim.SetBool("slide", sliding );
    }
    private void Jump()
    {
        anim.Play("jump", 0);
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(rb.velocity.x, jumpForce));

    }
    private void JumpWall()
    {
        anim.Play("jump", 0);
        if (gm.direcaoPlayer == DirecaoPlayer.direita)
        {
            rb.velocity = new Vector2(-1 * foraJumpWall.x, 1* foraJumpWall.y );
        }
        else
        {
            rb.velocity = new Vector2(1 * foraJumpWall.x,1 * foraJumpWall.y);
        }
        Flip();
    }
    private void slidingWall()
    {
        if(!isgrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallslidinSpeed, float.MaxValue));
        }
       
        
    }
    public float Mov()
    {

        return rb.velocity.magnitude;
    }
    public void SthealtMecanic()
    {     
        if (Mov() > 1f)
        {
            contStealth = contStealthValue;
        }else if(Mov()<=1 && areaStalth)
         {
            if (contStealth > 0)
            {
                contStealth -= Time.deltaTime;
            }
         }
        ///colorSprite
        if (contStealth <= 0)
        {
           
            if (areaStalth)
            {
                gm.visibilidadePlayer = VisibilidadePlayer.NaoVisivel;
                gm.iconSthealt.color = corSthealt;
                sp.color = new Color(1, 1, 1, 0.37f);
            }
            else
            {
                gm.visibilidadePlayer = VisibilidadePlayer.Visivel;
                gm.iconSthealt.color = Color.clear;
                sp.color = new Color(1, 1, 1,1);
            }

        }
 

    }
    IEnumerator dash()
    {
        float gravityNormal = rb.gravityScale;
        float down = quedaJump;
        float fors = foraDash;
        Vector2 dashDirection = gm.direcaoPlayer == DirecaoPlayer.direita ? Vector2.right.normalized : Vector2.left.normalized;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        GameObject oo = Instantiate(particleDash, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        float endTime = Time.time + DuraoDash;
        anim.Play("Dash", 0);
        fazendodash = true;
        while (Time.time < endTime)
        {
            rb.AddForce(dashDirection * foraDash, ForceMode2D.Force);
            foraDash--;
            yield return null;
        }
        
        rb.gravityScale = gravityNormal;
        quedaJump = down;
        CDcontador = CDDash;
        fazendodash = false;
        foraDash = fors;
        yield return new WaitForSeconds(CDDash);
        dashNum = null;
    }
    private void Flip()
    {
        if (horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }  
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusAreaStealth);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusGroundCheck);
       
        if(gm != null)
        {
            if (gm.direcaoPlayer == DirecaoPlayer.direita)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(new Vector2(transform.position.x , transform.position.y + 0.5f), radiusWall);
            }
            else if (gm.direcaoPlayer == DirecaoPlayer.esquerda)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(new Vector2(transform.position.x , transform.position.y + 0.5f), radiusWall);
            }
        }
     
        
    }
}