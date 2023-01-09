using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiwi : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circle;

    public GameObject collected;

    public int Score;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }

   

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            sr.enabled= false; //desativando sprite render
            circle.enabled= false;

            collected.SetActive(true);

            GameController.instance.totalscore += Score;

            GameController.instance.UpdateScoreText();


            Destroy(gameObject,0.25f); //referencia o objeto desse script
        }
    }

    


}
