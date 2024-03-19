
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveEnemy : MonoBehaviour
{
    public bool voador;
    public bool randomMoviment;
    Vector3 positionInitial;
    public float maxDistance;
    [Header("___________Componentes________")]
    public Transform groundchecktransform;
    public Transform wallchecktransform;
    public Rigidbody2D rb;
    public VisionEnemy vv;
    [Header("___________Variaveis________")]
    public LayerMask wallLayer;
    public Vector2 box;
    public LayerMask groundLayer;
    public bool AtivaGizmos;
    public bool Patrulheiro;
    public bool stopAll;
    public bool isground;
    public bool iswall;
    public float groundCircleRadius;
    public float speed;
    public float distanciaAtacar;
    public bool começouAtaque;
    public Animator anim;

    Vector2 randomPosition;
    float impulseForce =7;
    float temposalto;
    public enum direção
    {
        direita, esquerda
    }
    public direção orientacao;
    private void Start()
    {
        positionInitial = transform.position;
        if(randomMoviment)
        {
            SetRandomPosition();
        }
    }
    void Update()
    {
        if (!stopAll)
        {
            if (Patrulheiro)
            {
                if (vv.transPlayer == null)
                {
                    if (!voador)
                    {
                        if (isground)
                        {
                            if (iswall)
                            {
                                Flip();
                            }
                            else
                            {
                                float moveDirection = (transform.rotation.y == 0) ? 1f : -1f;
                                rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
                            }
                        }
                        else if (!isground)
                        {
                            Flip();
                        }
                    }
                    else
                    {

                        if (iswall || isground)
                        {
                            if(randomMoviment)
                            {
                                randomPosition = transform.position;
                                rb.velocity = (positionInitial - (Vector3)transform.position).normalized * speed;
                            }
                            else
                            {
                                positionInitial = transform.position;
                                Flip();
                            }
                        }
                        else
                        {
                            if (randomMoviment)
                            {
                                if (Vector2.Distance(transform.position, randomPosition) <= 0.1f)
                                {
                                    randomPosition = transform.position;
                                    if (Vector2.Distance(transform.position, positionInitial) >= 0.2f)
                                    {

                                        rb.velocity = (positionInitial - (Vector3)transform.position).normalized * speed;
                                    }
                                    else
                                    {
                                        SetRandomPosition();
                                    }
                                }
                                else
                                {
                                    rb.velocity = (randomPosition - (Vector2)transform.position).normalized * speed;
                                }
  
                            }
                            else
                            {
                                float moveDirectionX = (transform.rotation.y == 0) ? 1f : -1f;
                                rb.velocity = new Vector2(moveDirectionX * speed, rb.velocity.y);
                                if (Mathf.Abs(transform.position.x - positionInitial.x) >= maxDistance)
                                {
                                    Flip();
                                    positionInitial = transform.position;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!começouAtaque)
                    {
                        float distanceToPlayer = vv.transPlayer.transform.position.x - transform.position.x;
                        if (Mathf.Abs(distanceToPlayer) > distanciaAtacar)
                        {
                            float moveDirection = Mathf.Sign(distanceToPlayer);
                           
                            if(isground)
                            {
                                if (iswall)
                                {
                                    if(temposalto<=0 )
                                    {
                                        salto(impulseForce, vv.transPlayer.position, true);
                                       
                                    }
                                    else
                                    {
                                        temposalto -= 1 * Time.deltaTime;
                                    }
                               
                                }
                                else 
                                {
                                    rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);
                                }
                            }
                            transform.localRotation = (moveDirection > 0) ? transform.rotation = Quaternion.Euler(0, 0, 0) : transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                        else
                        {
                            começouAtaque = true;
                            rb.velocity = Vector2.zero; 
                        }
                    }
                }
            }
        }
        orientacao = (transform.rotation.y == 0) ? direção.direita : direção.esquerda;
        anim.SetBool("Move", Mov() > 0.3f);
    }
    public void salto(float distance, Vector3 ud, bool considera)
    {
        Vector2 directionToPlayer = (ud - transform.position).normalized;
        Vector2 jumpDirection;
        if (considera)
        {
            float wallHeight = Mathf.Abs(ud.y - transform.position.y);
            wallHeight = Mathf.Clamp(wallHeight, 0.1f, 1f);
            jumpDirection = new Vector2(directionToPlayer.x, wallHeight);
            rb.AddForce(jumpDirection * distance, ForceMode2D.Impulse);

        }
        temposalto = 1f;
    }
    void SetRandomPosition()
    {
        randomPosition = new Vector2(Random.Range(positionInitial.x - maxDistance, positionInitial.x + maxDistance),
                                     Random.Range(positionInitial.y - maxDistance, positionInitial.y + maxDistance));
    }
    public float Mov()
    {

        return rb.velocity.magnitude;
    }
    void Flip()
    {
        if (transform.rotation.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void FixedUpdate()
    {
        iswall = Physics2D.OverlapBox(wallchecktransform.transform.position, box, 0f, wallLayer);
        isground = Physics2D.OverlapCircle(groundchecktransform.position, groundCircleRadius, groundLayer);
    }
    public void OnDrawGizmos()
    {
        if(AtivaGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundchecktransform.position, groundCircleRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(wallchecktransform.transform.position, box);

        }
    }
}
