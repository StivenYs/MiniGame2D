using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   //te permite cambiar de scena a tu eleccion //es la eleccion del mapa por medio de un boton en el menu

    public void Button()
    {
        Application.Quit();
    }
    public void starGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Leves1()
    {
        SceneManager.LoadScene(1);
    }
    public void Leves2()
    {
        SceneManager.LoadScene(2);
    }
    public void Leves3()
    {
        SceneManager.LoadScene(4);
    }
    public void OneMore()
    {
        SceneManager.LoadScene(0);
    }
}
