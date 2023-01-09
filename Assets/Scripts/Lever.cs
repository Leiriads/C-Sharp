using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Lever : MonoBehaviour
{
    
    private Animator anime;

    public static bool on = false ;
    private int click;

   

    private void Start()
    {
        
        anime = GetComponent<Animator>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (click == 1)
            {
                anime.SetBool("Lever_on", false);
               // Debug.Log("of");
                on = false;
                click = 0;

                
            }
            else
            {
                anime.SetBool("Lever_on", true);
               // Debug.Log("on");
                on = true;
                click++;
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
        }
    }


}
