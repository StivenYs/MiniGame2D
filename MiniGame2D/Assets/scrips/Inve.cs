using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inve : MonoBehaviour
{

    public GameObject[] Item;

    //Monedas

    Money ScripteMoney;

    //palanca

    public Animator animaLever;
    public GameObject[] Obstacle;
    private int contObstacles;
    public GameObject[] ButtonLever;
    private int ContButtonLever;
    //objetos recogidos 

    private int ContObjects;

    //palaca de salida

    public GameObject Lever;

    //corazon Objeto

    public GameObject heart;
    public GameObject ButtoHeart;

    //cuerpo objeto
    public GameObject Body;
    public GameObject BodyButton;

    //puesta secreta 

    GameObject SecretDoor;


    //se encuntra si , no en el radio de la palanca para activarla

    private bool RadiusLever;

    private void Start()
    {
        //monedas

        ScripteMoney = GameObject.FindGameObjectWithTag("LevelScene").GetComponent<Money>();

        //Animator de la palanca

        animaLever.SetBool("LeverActive", false);
        contObstacles = 0;
        ContButtonLever = 0;

        ContObjects = 0;

        //se encuntra si , no en el radio de la palanca para activarla

        RadiusLever = false;

        //puesta secreta 
        //en el mapa de la casahay una puerta secreta que solos e revelara una vez que se recojan los 3 objetos 

        SecretDoor = GameObject.FindGameObjectWithTag("Door");
        SecretDoor.SetActive(true);

    }

    private void Update()
    {
        //confirmamos si los objetos que tenemos en el inventarios son mayor o igual a 3

        if (ContObjects >= 3)
        {
            Lever.SetActive(true);
            SecretDoor.SetActive(false);

        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //monedas

        //cada ves que el el jugador detecta una colicion con un objeto "moneda " la recogera y la sumara  a su contador 
        //destruyendo asi la moneda ya recogida 

        if (collision.gameObject.CompareTag("Money"))
        {
            ScripteMoney._Money += 1;

            Destroy(collision.gameObject);

        }

        //al jugador colcionar con un objeto "item " lo recogera y lo ubicara en una casilla de su inventario en orden el cual se recojan 

        //una ves todado el item el for nos buscara entre todas las casillas para ver si hay espacion dende ponerla.

        if (collision.gameObject.CompareTag("Item"))
        {
            for (int i = 0; i < Item.Length; i++)
            {
                if (Item[i].GetComponent<Image>().enabled == false)
                {
                    Item[i].GetComponent<Image>().enabled = true;
                    Item[i].GetComponent<Image>().sprite = collision.GetComponent<SpriteRenderer>().sprite;
                    ContObjects += 1;
                    Destroy(collision.gameObject);
                    break;
                }
            }
        }

        //para dar una distancia minima para poder acionar las palancas 

        if (collision.gameObject.CompareTag("RadiusLever"))
        {
            RadiusLever = true;
        }

        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //detectar la colicion en el rango de la palanca 

        if (collision.gameObject.CompareTag("RadiusLever"))
        {
            RadiusLever = false;
        }
    }

    public void LeverAcitive()
    {
        //activar la animacion de la palanca y dessactivar el objeto
        if (RadiusLever == true)
        {
            ContButtonLever = 0;
            contObstacles = 0;
            ActivationLever();
            
        }
        
    }
    public void LeverAcitive2()
    {
        //activar la animacion de la palanca y dessactivar el objeto
        if (RadiusLever == true)
        {
            ContButtonLever = 1;
            contObstacles = 1;
            ActivationLever();
        }
    }
       
    public void LeverAcitive3()
    {
        //activar la animacion de la palanca y dessactivar el objeto
        if (RadiusLever == true)
        {
            ContButtonLever = 2;
            contObstacles = 2;
            ActivationLever();
        }
        
    }

    void ActivationLever()
    {
        Destroy(ButtonLever[ContButtonLever]);
        animaLever.SetBool("LeverActive", true);
        Obstacle[contObstacles].SetActive(false);
    }

    public void heartObject()
    {
        heart.SetActive(true);
        ButtoHeart.SetActive(false);
    }

    public void BodyObjet()
    {
        //desativamos el boton con el cual se activo el obejto para evitar que sea acionado una vez mas 

        BodyButton.SetActive(false);
        Body.SetActive(true);
    }
    
}


