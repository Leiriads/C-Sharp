using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//canvas
using UnityEngine.SceneManagement; // pra reiniciar a cena

public class GameController : MonoBehaviour
{

    public int totalscore;

    public Text scoreText;

    public GameObject gameOver;

    public float teste;

    public static GameController instance; //tudo que tem aqui pode ser chamado em qualquer classe tipo metodos globais...

    public static bool Die;

    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
    }

    public void UpdateScoreText()
    {
        scoreText.text = totalscore.ToString(); //score text ui . pega o elemento text
    }

    public void ShowGameOver()
    {
        Invoke("gameover", 0.7f);
       

    }

    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
        Die= true;
 
        
    }
  
    void gameover()
    {
        gameOver.SetActive(true); //setactive ativa um objeto que está desativado na cena
    }

 
}
