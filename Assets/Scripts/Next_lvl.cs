using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//trocar de cena

public class Next_lvl : MonoBehaviour
{

    public string lvlName;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Checkpoint.Checkpoint_checked = false;
            SceneManager.LoadScene(lvlName);
        }
    }


}
