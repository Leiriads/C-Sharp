using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


 public static CameraController instance;

 Animator anim;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

   public void shake()
    {
        anim.Play("Shake");
    }
}
