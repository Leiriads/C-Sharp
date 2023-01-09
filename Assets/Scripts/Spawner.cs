using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate; //tempo em sec em cada spawn

    private float nextSpawn = 0f;

    private new Animator animation;

    void Start()
    {
        animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
     if(Time.time > nextSpawn)
        {

            nextSpawn= Time.time + spawnRate;
            animation.SetTrigger("Spawn");
            Instantiate(enemy,transform.position, enemy.transform.rotation); //instanciando na cena
            

        }   
    }
}
