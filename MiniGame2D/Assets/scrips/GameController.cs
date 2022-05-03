using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{




    // Entre scenas

    public PlayerController _playerController;
    public int _NextScene;
    public int _DeathScene;
    public Image transitionsScenes;
    [HideInInspector]
    public float transitionColorD;


    private void Awake()
    {
        
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
       
        transitionsScenes.GetComponent<Image>();
    }
 

    private void FixedUpdate()
    {
        ChageOfScene();

        //interpolacion del color al cambiar de scena desde el actual al deaseado ,con un decremento de el alpha en cada frem
        
        float COlorActualTransition = Mathf.Lerp(transitionsScenes.color.a, transitionColorD, 0.1f);
        transitionsScenes.color = new Color(0, 0, 0, COlorActualTransition);
        
    }

    public void ChageOfScene()
    {
        //nos confirmara si el jugador ya ha colicionado con una de las puestas para poder cambar de scena y asu ves activar la animacion de la transicion 
        //se activa las corrutinas para dar un tiempo de ejecucion de la transicion y el cambio de scena 

        if (_playerController.NextScene == true)
        {
            transitionsScenes.enabled = true;
            transitionColorD = 1;

            StartCoroutine(LoadScene(1.5f));
        }   
        if (_playerController.DeathScene == true)
        {
            //me lleva a la secena al morir, al llegar la confirmacion del Player de que su vida <= 0
            transitionsScenes.enabled = true;
            transitionColorD = 1;
            StartCoroutine(LoadSceneDead(1.5f));
        }
      
    }
    IEnumerator LoadScene(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadScene(_NextScene);
    }
    IEnumerator LoadSceneDead(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        Destroy(GameObject.FindGameObjectWithTag("LevelScene"));
        SceneManager.LoadScene(_DeathScene);
    }
   
  
       

    

}
