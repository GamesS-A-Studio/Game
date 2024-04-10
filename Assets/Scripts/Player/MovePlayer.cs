
using UnityEngine;
using UnityEngine.UI;
using FirstGearGames.SmoothCameraShaker;
using System.Collections;

public class MovimentPlayer : MonoBehaviour
{
    GameManager gm;
    Vector3 offsetOriginal;

    public bool Debug_Enabled = false;
    public CharacterAnimationSounds characterSounds; // Reference to CharacterAnimationSounds script.

    [Header("_________Jump Proprietars________________________________")]
    [Header("____________________________________________________________")]
    private bool wasGrounded = false; // Track if the character was grounded in the previous frame. 
    public bool isJumping { get; private set; } // Track if the character is currently jumping.
    private bool isJumpingWhileSliding = false; // Track if the player jumps while sliding.
    public string JumpLandSFX = "Play_JumpLand";
    public string JumpWallSFX = "Play_JumpWall";
    public float jumpForce = 5f;
    public float jumpTime = 1f;
    float duraoPulo;
    public float jumpDuration;
    public Vector2 foraJumpWall;
    public bool iswall;
    public float radiusWall;
    public LayerMask layerWall;
    public float wallslidinSpeed;
    public string WallSlideSFX = "Play_WallSlide"; // Wwise event for wall slide sound.
    private bool isWallSliding = false; // Track if the player is currently wall sliding.


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
    private bool isStealthKeyDown = false; // Track if the stealth key is currently held down.
    private float stealthKeyDownTime = 0f; // Track how long the stealth key has been held down.
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
        if (speed < speedFixo / 2)
        {
            speed = speedFixo / 2;
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

        // Call SthealtMecanic method with appropriate condition.
        if (isStealthKeyDown && !fazendodash) // Check if stealth key is held down and not dashing.
        {
            SthealtMecanic();
        }

        Flip();
        KeyClick();
        SthealtMecanic();
        animacoes();
        Contagens();
    }
    // 

    private void FixedUpdate()
    {
        float targetVelocityX = horizontal * speed;

        // if (horizontal == 0 && isgrounded)
        // {
        //     // If no horizontal input and grounded, set velocity to 0 directly.
        //     rb.velocity = new Vector2(0f, rb.velocity.y);
        // }
        // else
        // {
        //     // Smoothly adjust the velocity based on input.
        //     rb.velocity = new Vector3(Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref currentVelocity.x, smoothTime), rb.velocity.y, 0);
        // }
        rb.velocity = new Vector2(targetVelocityX, rb.velocity.y);

        isgrounded = Physics2D.OverlapCircle(transform.position, radiusGroundCheck, groundLayer);
        iswall = (gm.direcaoPlayer == DirecaoPlayer.direita) ? Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 0.5f), radiusWall, layerWall) :
            Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y + 0.5f), radiusWall, layerWall);




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

        //      JumpLandSFX

        // Check if the character was previously not grounded but is now grounded.
        if (!wasGrounded && isgrounded)
        {
            // Trigger the Jump Land sound effect.
            characterSounds.Play_JumpLand();
            AkSoundEngine.PostEvent("Stop_WallSlide", gameObject); 
            Debug.Log("WallSlide Stopped");
        }

        // Update wasGrounded for the next frame.
        wasGrounded = isgrounded;

        // -X-X-X-

        if (horizontal == 0 && !iswall &&  isgrounded)
        {
            sliding = false; // Stop sliding when not moving horizontally and not on a wall.
            AkSoundEngine.PostEvent("Stop_WallSlide", gameObject); 
            Debug.Log("WallSlide Stopped");
        }
        
        
        

        if (!isWallSliding && iswall && !isgrounded)
        {
            // Trigger the Wall Slide sound effect when the player starts wall sliding.
            characterSounds.Play_WallSlide();
        }

        if (!isWallSliding && !iswall )
        {
            AkSoundEngine.PostEvent("Stop_WallSlide", gameObject); 
            Debug.Log("WallSlide Stopped");
        }

        if (isWallSliding && isJumpingWhileSliding)
        {
            // Stop the looping sound of WallSlideSFX.
            AkSoundEngine.PostEvent("Stop_WallSlide", gameObject); 
            Debug.Log("WallSlide Stopped");

            // Trigger the Play_JumpWall sound.
            characterSounds.Play_JumpWall();
            characterSounds.Play_WallSlide();





            // Reset the flag.
            isJumpingWhileSliding = false;
        }

        // Update wasWallSliding for the next frame.
        isWallSliding = iswall;

       
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
        if (isgrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                duraoPulo = jumpDuration;
                // Reset the flag when the player jumps.
                isJumpingWhileSliding = false;
            }
            sliding = false;
        }
        else
        {
            if (iswall && horizontal != 0)
            {
                slidingWall();
                sliding = true;
                if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && cdJumpWall <= 0)
                {
                    JumpWall();
                    // Set the flag when the player jumps while sliding.
                    isJumpingWhileSliding = true;
                }
            }
            else if (horizontal == 0)
            {
                sliding = false;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W) && !iswall)
            {
                duraoPulo = 0;
            }
        }
        // If 'W' key is held down, player looks up
        if (Input.GetKey(KeyCode.W))
        {
            upLook = true;
        }
        else if (Input.GetKeyUp(KeyCode.W)) { upLook = false; }
        if (Input.GetKeyDown(KeyCode.Z) && CDcontador <= 0 && !fazendodash)
        {
            if (gm.movimentPlayer.staminaAtual >= 50)
            {
                if (dashNum == null)
                {
                    gm.movimentPlayer.staminaAtual -= 50; // Subtract stamina cost for dash.
                    StartCoroutine(dash());
                    dashNum = dash();

                }
            }
        }
        // Check if the down direction arrow/S is held down.
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            // Increment the time the key has been held down.
            stealthKeyDownTime += Time.deltaTime;

            // Check if the key has been held down for more than 0.5 seconds.
            if (stealthKeyDownTime >= 0.5f)
            {
                isStealthKeyDown = true;
            }
        }
        else
        {
            // Reset the stealth key variables if it's released.
            isStealthKeyDown = false;
            stealthKeyDownTime = 0f;
        }
        
    }

    public void animacoes()
    {
        anim.SetBool("Move", Mov() > 0.3f && isgrounded || Mov() > 0.3f && isgrounded);
        anim.SetBool("MoveLow", Mov() > 0.3f && isgrounded || Mov() > 0.3f && isgrounded);
        anim.SetBool("DownLook", isgrounded);
        anim.SetBool("LookUp", upLook && isgrounded);
        anim.SetBool("slide", sliding && !isgrounded && isWallSliding);
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
            rb.velocity = new Vector2(-1 * foraJumpWall.x, 1 * foraJumpWall.y);
        }
        else
        {
            rb.velocity = new Vector2(1 * foraJumpWall.x, 1 * foraJumpWall.y);
        }
        Flip();
    }
    private void slidingWall()
    {
        if (!isgrounded && iswall)
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
        }
        else if (Mov() <= 1 && areaStalth)
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
                sp.color = new Color(1, 1, 1, 1);
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
        anim.Play("Dash", 0);

        yield return new WaitForSeconds(0.1f);
        float endTime = Time.time + DuraoDash;

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

    //



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

        if (gm != null)
        {
            if (gm.direcaoPlayer == DirecaoPlayer.direita)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + 0.5f), radiusWall);
            }
            else if (gm.direcaoPlayer == DirecaoPlayer.esquerda)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + 0.5f), radiusWall);
            }
        }


    }
}