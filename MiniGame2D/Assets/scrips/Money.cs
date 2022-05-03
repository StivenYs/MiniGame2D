using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    //Cuantas monedas tengo

    public int _Money;
    public Text TexMoney;

    PlayerController playerController;
    void Start()
    {
        //inicializacion

        _Money = 0;
        //llamos el script del jugador para coomprobar las colciones con las monedas y poder llevar una cuenta 

        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        //se mantiene una constante visualizacion de la cantidad de monedas 

        TexMoney.text = "" + _Money;


        if (_Money >= 20 && playerController._Contlife <= 90f)
        {
            _Money -= 20;
        }
    }
}
