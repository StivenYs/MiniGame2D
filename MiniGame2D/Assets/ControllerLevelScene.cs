using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerLevelScene : MonoBehaviour
{




    public static ControllerLevelScene ints;
    private GameController gameController;
    public int _levelScene ;


    void Awake()
    {

        
        _levelScene = 0;

        //llamado de El scrip de Controller del juego

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();


        //cramos esta calse estatica para poder llevarnos a cada scena este script , manteniendo los cambios 

        if (ControllerLevelScene.ints == null)
        {
            //esta sera la unica instacia ya que es la primera vez

            ControllerLevelScene.ints = this;
            DontDestroyOnLoad(this.gameObject);
           
        }
        else
        {
            //ya existe una instacia de esta, Asi que hay que destruir esta 
            //en cada secena se tiene este script , pero no pueden haber dos a la vez asi que si llevamos uno , destruiresmos el que haya en la scena 

            Destroy(this.gameObject);
        }
       
        
    }


 
}
