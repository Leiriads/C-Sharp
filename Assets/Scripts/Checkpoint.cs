using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Checkpoint : MonoBehaviour
{

    private Animator anim ;
 
    public static bool Checkpoint_checked;

    private bool anim_true;

    public static float position_x;
    public static float position_y;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if (!anim_true)
        {
            Checkpoint_checked = false;
            anim.SetBool("checkpoint_on", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim_true = true;
            Checkpoint_checked = true;
                anim.SetBool("checkpoint_on", true);
                position_x = transform.position.x;
                position_y = transform.position.y;
            
            
        }

    }




}
