using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class Enemy_mask : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rig;
    private Animator anim;

    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;

    public float speed;

    public Transform rightCol;
    public Transform leftCol;

    public Transform headPoint;

    private bool colliding;

    public LayerMask layer;

    public static bool enemy_kill = false;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(11, 10, true);

        //veloicity é uma classe do rigidybody que adiciona velocidade  a um corpo| rig.velocity.y significa que nao quer alterar o eixo y. apenas o X
        rig.velocity = new Vector2(speed, rig.velocity.y);


        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);  // desenha um colisor invisivel em formato de linha
        //retorna verdadeiro ou falso

        if (colliding)
        {
            
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y); //invertendo a rotação do enemy
            speed *= -1f; //inverte a velocidade, positivo é direta negativo esquerda
        }
    }

    bool playerDestroyed = false;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.tag == "Player")
        {
            float height = coll.contacts[0].point.y - headPoint.position.y;

            if (height > 0 && !playerDestroyed)
            {
                coll.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                speed = 0;
                boxCollider2D.enabled= false; //desligandio os colisores de outra forma 
                circleCollider2D.enabled= false;
                rig.bodyType = RigidbodyType2D.Kinematic;

                anim.SetTrigger("anim_Die");

                Destroy(gameObject, 0.7f);
            }
            else
            {
                playerDestroyed = true;

                enemy_kill = true; // variavel pra controlar caso o inimigo o mate, verificando estado no player
                //GameController.instance.ShowGameOver();
                //Destroy(coll.gameObject);
              
            }

        }
    }

}
