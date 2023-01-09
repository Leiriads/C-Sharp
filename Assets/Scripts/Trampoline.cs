using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce;


    private Animator anim;


    private SpriteRenderer renderization;
    private BoxCollider2D boxColl;

    private void Start()
    {
        anim = GetComponent<Animator>();
        renderization = GetComponent<SpriteRenderer>();
        boxColl= GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) //esse colision retorna o objeto que bateu no trampolin
    {
        if (collision.gameObject.tag == "Player")
        {

            anim.SetTrigger("jump");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);


        }
    }
    private void Update()
    {
        if (Lever.on == true)
        {
            renderization.enabled = true;
            boxColl.isTrigger = false;

        }
    }

    // Invoke("Falling", renderTrampoline);


    //Fan force angle 0 direita, 90 cima, 180 esquerda ,270 baixo 




}
