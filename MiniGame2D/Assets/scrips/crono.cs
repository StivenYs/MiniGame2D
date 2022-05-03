using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class crono : MonoBehaviour
{
    // Start is called before the first frame update

    //contador del jefe

    public Text ContTime;
    [SerializeField]
    private float Tim = 60;
    BoxCollider2D starCrono;
    private bool activeCrono;


    //cambio de scena,transicio en negro

    public GameController gameController;
    

    public EnemyController enemy;

    void Start()
    {
        //inicializacion de los valores 

        activeCrono = false;
        ContTime.text= "" + Tim;
        starCrono = GameObject.FindGameObjectWithTag("Crono").GetComponent<BoxCollider2D>();
        enemy.GetComponent<EnemyController>();

        //lamado del gameController para poder usar la transicion de cambio de scena ya establecida

        gameController.GetComponent<GameController>();
        
    }

    
    void Update()
    {
        // comprobacion en cada frem si el el tiempo del cronometro ya se puede activar 

        if (activeCrono == true && Tim > 0)
        {
            Tim -= Time.deltaTime;
            ContTime.text = "" + Tim.ToString("f0");
        }
        else if (activeCrono == true && Tim <= 0)
        {
            //verifica si el cronometro esta activo y si es menor a 0 para pasar a la scena ganar
            //y traemos la varible de la transicion al cambiar de scena y la corrutina para dar tiempo de ejecucion de esta y cambair de scena 

           gameController.transitionsScenes.enabled = true;
           gameController.transitionColorD = 1;
           StartCoroutine(LoadSceneWin(1.1f));
        }
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detecta si el jugador a pasado la lina de incio para dar paso al conteo regresivo 
        //a su vez se aumneta la velocidad del enemigo para hacerlo masw complicado

        if (collision.gameObject.CompareTag("Player"))
        {
            activeCrono = true;
            starCrono.enabled = false;
            enemy.Speed = 0.08f;
        }

    }
    IEnumerator LoadSceneWin(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadScene("GameOver");
    }

}
