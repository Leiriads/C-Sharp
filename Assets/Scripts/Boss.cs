using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //life
    //stats combat
    public Transform healthBar; //green bar
    public GameObject healthBarObject; // father bar

    private Vector3 healthBarScale; //size bar
    private float healthPercent; // % health calculate for size bar
    public int health;
    public int healthIniciate;
    private bool fase2;


    //For Idle Stage
    [Header("Idle")]
    [SerializeField] float idleMoveSpeed;
    [SerializeField] Vector2 idleMoveDirection;

    //For Attack up and down Stage
    [Header("AttackUpNDown")]
    [SerializeField] float attackMoveSpeed;
    [SerializeField] Vector2 attackMoveDirection;

    // for Attack Player Stage
    [Header("AttaclPlayer")]
    [SerializeField] float attackPlayerSpeed;
    [SerializeField] Transform Player;
    public GameObject Player1;

    private Vector2 playerPosition;
    private bool hasPlayerPosition;

    //Other
    [Header("Other")]
    [SerializeField] Transform groundCheckUp;
    [SerializeField] Transform groundCheckDown;
    [SerializeField] Transform groundCheckWall;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer; //referenciando a layer do ground

    private bool isTouchingUp;
    private bool isTouchingDown;
    private bool isTouchingWall;
    private bool goindgUp = true;
    private bool facingLeft = true;

    private Rigidbody2D bossRB;

    private new Animator animation;

    // private GameObject player;

    void Start()
    {
        fase2 = true;
        healthBarScale = healthBar.localScale;// inicializando com o tamanho da barra independente da escala
        healthPercent = healthBarScale.x / health; //cada ponto de vida vai representar um % do total da barra 


        idleMoveDirection.Normalize();
        attackMoveDirection.Normalize();

        bossRB = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
    }

    void Update()
    {
     

        isTouchingUp = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadius, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(groundCheckDown.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(groundCheckWall.position, groundCheckRadius, groundLayer);

        if (fase2 == true)
        {
            if (health == 1)
            {
                health = healthIniciate;
                UpdateHealthBar();
                fase2 = false;
            }
        }

        if (fase2 == false && health == 0)
        {
            Destroy(gameObject);

        }


    }

    void UpdateHealthBar()
    {
        healthBarScale.x = healthPercent * health;//depois de calcular ele atribui na barra verde
        healthBar.localScale = healthBarScale;

    }

    void RandomStatePicker()
    {
        int random = Random.Range(0, 2); //0 a 1 
        if (random == 0)
        {
            animation.SetTrigger("AttackUpNDown");
        }
        if (random == 1)
        {
            animation.SetTrigger("AttackPlayer");
        }
    }

    public void idleState()
    {
        if (isTouchingUp && goindgUp)
        {
            //  animation.SetTrigger("top_anim");
            ChangeDirection();

        }
        else if (isTouchingDown && !goindgUp)
        {
            // animation.SetTrigger("bottom_anim");

            ChangeDirection();
        }

        if (isTouchingWall)
        {
            if (facingLeft)
            {
                //animation.SetTrigger("left_anim");
                // animation.Play("left_hit");
                Flip();
            } else if (!facingLeft)
            {
                //  animation.SetTrigger("right_anim");
                //animation.Play("rigth_hit");
                Flip();
            }

        }

        bossRB.velocity = idleMoveSpeed * idleMoveDirection;
    }

    public void AttackUpNDown()
    {
        if (isTouchingUp && goindgUp)
        {
            ChangeDirection();
            CameraController.instance.shake();

        }
        else if (isTouchingDown && !goindgUp)
        {
            ChangeDirection();
            CameraController.instance.shake();
        }

        if (isTouchingWall)
        {
            if (facingLeft)
            {
                Flip();
            }
            else if (!facingLeft)
            {
                Flip();
            }

        }

        bossRB.velocity = attackMoveSpeed * attackMoveDirection;
    }

    public void AttackPlayer()
    {

        if (!hasPlayerPosition)
        {
            //Take player position
            playerPosition = Player.position - transform.position;
            //Normalize player position
            playerPosition.Normalize();
            hasPlayerPosition = true;
        }

        if (hasPlayerPosition)
        {
            //Attack On that position

            bossRB.velocity = playerPosition * attackPlayerSpeed;
        }

        if (isTouchingWall || isTouchingDown)
        {
            bossRB.velocity = Vector2.zero;
            hasPlayerPosition = false;
            //play slamed animation
            animation.SetTrigger("Slamed");
            CameraController.instance.shake();
        }

    }

    void FlipTowardsPlayer() //mirar o player
    {
       
        float playerDirection = Player.position.x - transform.position.x;

        if(GameObject.FindWithTag("Player") != null) //caso o player for morto e o boss mirar ele
        {
            if (playerDirection > 0 && facingLeft)
            {
                Flip();
            }
            else if (playerDirection < 0 && !facingLeft)
            {
                Flip();
            }
        }


            
        

        
    }





    void ChangeDirection()
    {
        goindgUp = !goindgUp; //Invertendo se tiver subindo
        idleMoveDirection.y *= -1;
        attackMoveDirection.y *= -1;

    }

    void Flip()
    {
        facingLeft = !facingLeft;
        idleMoveDirection.x *= -1;
        attackMoveDirection.x *= -1;
        transform.Rotate(0, 180, 0);
        //lfie bar 
        healthBarObject.transform.localScale = new Vector3(healthBarObject.transform.localScale.x * -1,
            healthBarObject.transform.localScale.y,
            healthBarObject.transform.localScale.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckUp.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckDown.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckWall.position, groundCheckRadius);

    }

    //collision

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            health = health - 1;
            UpdateHealthBar();

            Destroy(collision.gameObject);

        }
        /*
        if (collision.gameObject.tag == this.gameObject.tag) //nao colodirem entre si 
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        */


       
    }

}


