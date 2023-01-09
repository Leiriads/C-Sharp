using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float speed;
    public bool dirRigth = true; //sempre começa da direita
    private float timer;

    public float moveTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dirRigth)
        {//se verdadeiro a serra vai para direita
            transform.Translate(Vector2.right* speed * Time.deltaTime);
        }
        else
        {//se nao vai para esquerda
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }


        timer += Time.deltaTime; //timer vai somar retorna o tempo real no jogo. 0 , 1, 2, 3, 4, 5
        if(timer >= moveTime)
        {
            dirRigth = !dirRigth; //boleano vai ser invertido
            timer= 0f;
        }


        if (Lever.on == true)
        {
            Destroy(gameObject);
        }

    }

    


}
