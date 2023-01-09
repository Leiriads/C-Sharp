using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SawBoss : MonoBehaviour
{

    [Header("Other")]

    [SerializeField] Transform groundcheckRight;
    [SerializeField] Transform groundcheckLeft;


    [SerializeField] float groundcheckradius;
    [SerializeField] LayerMask groundlayer; //referenciando a layer do ground


    private bool isTouchingLeft;
    private bool isTouchingRight;



    public float speed;
    public bool dirRigth = true; //sempre começa da direita
    private float timer;

    public float moveTime;

    

    private Rigidbody2D sawBossRb;

    // Start is called before the first frame update
    void Start()
    {
        sawBossRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        isTouchingLeft = Physics2D.OverlapCircle(groundcheckLeft.position, groundcheckradius, groundlayer);
        
        isTouchingRight = Physics2D.OverlapCircle(groundcheckRight.position, groundcheckradius, groundlayer);

       

     


        
        if (dirRigth)
        {//se verdadeiro a serra vai para direita
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {//se nao vai para esquerda
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

       
        timer += Time.deltaTime; //timer vai somar retorna o tempo real no jogo. 0 , 1, 2, 3, 4, 5
        if (timer >= moveTime)
        {
            dirRigth = !dirRigth; //boleano vai ser invertido
            timer = 0f;
        }
         

        if (Lever.on == true)
        {
            Destroy(gameObject);
        }



       

       



    }//update
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(groundcheckLeft.position, groundcheckradius);
        Gizmos.DrawWireSphere(groundcheckRight.position, groundcheckradius);
    }


}
