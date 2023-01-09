using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class FallingPlatform : MonoBehaviour
{

    public float fallingTime; //ta recebendo 2sec pelo unity

    private TargetJoint2D target; //efeito de mola na plataforma
    private BoxCollider2D boxColl;

    // Start is called before the first frame update
    void Start()
    {
       target= GetComponent<TargetJoint2D>();
        boxColl= GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) //verificar se esta colidindo com o personagem, se bateu ela cai
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke("Falling",fallingTime); //invoke chama o metodo atraves da string com tempo, vai ser invocado depois de x sec
        }

        

    }

    //
    void OnTriggerEnter2D(Collider2D collider) // tem trigger e se bater na layer 9 é destruida
    {
            if (collider.gameObject.layer == 9)
            {
                Destroy(gameObject, 0.25f); //referencia o objeto desse script
            }   
    }


    void Falling()
    {
        target.enabled= false;
        boxColl.isTrigger = true;
    }



}
