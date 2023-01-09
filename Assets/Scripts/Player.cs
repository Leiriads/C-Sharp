using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    private Rigidbody2D rigdbodi;

    public int nJump;
    public bool isGrounded;

    private new Animator animation;

    private bool isdead;
    // ctrl + K + D auto identar

    // Start is called before the first frame update

    void Start()
    {

        


        rigdbodi = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();

        if (Checkpoint.Checkpoint_checked == true && GameController.Die == true)
        {
            Respawn();
            GameController.Die = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
      
        Move();
        Jump();

        isdead = Enemy_mask.enemy_kill;

        if(isdead == true)
        {
            dead_animation();
            Enemy_mask.enemy_kill = false;
        }

    }


    void Move()
    {
        float movement = Input.GetAxis("Horizontal"); // adiciona se é direito ou esquerdo

        // rig.velocity = new Vector2(movement * Speed, rig.velocity.y);

        rigdbodi.velocity = new Vector2(movement * Speed, rigdbodi.velocity.y);

        if (movement > 0f)
        {
            animation.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f); //olhando para direita , eulerangs rotaciona o objeto
        }

        if (movement < 0f)
        {
            animation.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);//rotacionado para esquerda
        }

        if (movement == 0f)
        {
            animation.SetBool("walk", false);
        }


    }

    void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rigdbodi.velocity = Vector2.up * JumpForce;
                //rigdbodi.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse); // é um impulso ou uma força
                animation.SetBool("jump", true);
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump")&& nJump > 0)
            {
                nJump--;
                animation.SetBool("double_jump", true);
                rigdbodi.velocity = Vector2.up * JumpForce; // pulo novo
                // rigdbodi.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse); //pulo antigo que dependendo da gravidade fica fraco
            }     
        }
    }

    void OnCollisionEnter2D(Collision2D collision) //verificar se esta colidindo com algo
    {
        if (collision.gameObject.tag == "Spike")  //ou .layer == 8 
        {
            // Debug.Log("Game over"); //tipo console.log
            dead_animation();
        }

        if (collision.gameObject.tag == "Saw")
        {

            dead_animation();
        }

        if (collision.gameObject.tag == "Trampoline")
        {
            nJump = 1;
            animation.SetBool("jump", true);
            animation.SetBool("double_jump", false);
        }

        if (collision.gameObject.tag == "Boss")
        {

            dead_animation();
        }

    }
    /*
    void OnCollisionExit2D(Collision2D collision) //parou de colidir
    {
        if (collision.gameObject.tag == "Trampoline")
        {
            
          //  animation.SetBool("jump", true);
        }
    }
  */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            nJump = 1;
           // Debug.Log("chao");
            isGrounded = true;
            animation.SetBool("jump", false);
            animation.SetBool("double_jump", false);
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) // layer 8 = ground 
        {
         //   Debug.Log("pulou");
            isGrounded = false;
            animation.SetBool("jump", true);

        }
    }


    public void Respawn()
    {

        if(Checkpoint.Checkpoint_checked == true)
        {
            float checkpointx;
            float checkpointy;

            checkpointx = Checkpoint.position_x;
            checkpointy = Checkpoint.position_y;

            transform.position = new Vector2(checkpointx, checkpointy);
        }
        
    }

    private void dead_animation()
    {
        animation.SetTrigger("die");

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;//zerando velocidade
        gameObject.GetComponent<BoxCollider2D>().enabled= false; //desativando colisao
        gameObject.GetComponent<CircleCollider2D>().enabled= false;//desativando colisao
        gameObject.GetComponent<Rigidbody2D>().bodyType= RigidbodyType2D.Kinematic;//nao vai ser mais afetado por gravidade ou qualquer força
        gameObject.GetComponent<Player>().enabled= false; //desabilitando o script do player
        animation.SetBool("jump", false); 
        animation.SetBool("double_jump", false);

        Destroy(gameObject, 0.7f);

        GameController.instance.ShowGameOver();
    }



}//Player
