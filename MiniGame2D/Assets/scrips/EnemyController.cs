using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyController : MonoBehaviour
{



    //Movimiento

    [Header("Movimiento Enemigo")]

    public float Speed;
    public Rigidbody2D Enemy;
    public Transform[] Positions;

    //Radio de Vision

    [SerializeField] private float VisionRadius;
    [SerializeField] private float AttackRadius;

    //objetivo Jugador

    GameObject Player;

    Vector3 target;
    
    private bool PlayerDetected;


    //variables
    [SerializeField]
    private int NextPosition;
    private bool rute;
    Vector2 view;

    //Animacion

    public Animator Anima;

    //Ataque Enemigo,Bola de fuego

    [Space] public GameObject FireBall;
    [SerializeField] private float SpeedFireBall;
    [SerializeField] private float NetxFireBall;
    private float tiempo;
    public AudioSource AudioFireball;

    //muerte del enemigo 

    public GameObject Money;




    void Start()
    {
        //Inicializacion de componente

        Enemy.GetComponent<Rigidbody2D>();
        NextPosition = 0;
        Enemy.isKinematic = true;
        rute = true;
        Speed = 0.03f;
        


        //Objetivo Jugador

        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerDetected = false;

        //Taque Enemigo

        NetxFireBall = 0.3f;
        tiempo = 0;
        AudioFireball.GetComponent<AudioSource>();



    }



    private void FixedUpdate()
    { 
        

        //Detectar al jugador por medio de RaycastHit2D
        //debemos poner Todo aquel Objeto que no sea una collicion en un Layer Diferente A Default

        RaycastHit2D hit2D = Physics2D.Raycast(
        transform.position, Player.transform.position -
        transform.position, VisionRadius,
        1 << LayerMask.NameToLayer("Default")
        );

        //Direccion En la cuales se encuntra el Player y cuanto es la distacia que le resta del enemigo

        Vector3 DirectionPlayer = transform.TransformDirection(
        Player.transform.position - transform.position
        );

        //Dibujamos el Racast en Direccion del Player
        Debug.DrawRay(transform.position, DirectionPlayer,Color.red);





        //Detectar si la colicion de Raycats es el Player o una pared 

        if (hit2D.collider != null)
        {
            if (hit2D.collider.CompareTag("Player"))
            {
                target = Player.transform.position;

                PlayerDetected = true;
            }
        }
        else
        {
            PlayerDetected = false;
        }

        //Distacia Restante Del enemigo Al Player y Adonde deberia de caminar al llegar la distancia a 0 o menor del radio

        float distance = Vector3.Distance(target, transform.position);

        Vector3 DirectionVision = (target - transform.position).normalized;



        //Desplazamiento del enemigo a un tharget(objetivo)//modo patrulla

        Enemy.MovePosition(Vector2.MoveTowards(
        Enemy.position,Positions[NextPosition].position,Speed )
        );

        Animations();
      
        //Comprobacion de las condiciones del desplazamiento y posicion del cuerpo rigido 
        //si el enemigo no ve al jugador serca entoces no lo seguira y continuara con su patrulla 

        if (Vector2.Distance(Enemy.position,Positions[NextPosition].position) <= 0 &&  PlayerDetected == false)
        {
            if (rute == true)
            {
                NextPosition++;

                if (NextPosition >= Positions.Length -1)
                {
                    rute = false;
                }
            }
            else
            {
                NextPosition--;

                if (NextPosition == 0)
                {
                    rute = true;
                }
            }
         
        }
        //comprobar si la distancia del enemigo y el Player es menor Al radio//para seguirlo

        else if (PlayerDetected == true && distance < VisionRadius)
        {
            Enemy.MovePosition(transform.position + DirectionVision * Speed );
            

            //las animaciones que se ejeutan la haber un moviento y se quedan en la ultima que se ejecuto 

            if (Enemy.transform.position != Vector3.zero)
            {
                Anima.SetFloat("VerX", DirectionVision.x);
                Anima.SetFloat("VerY", DirectionVision.y);
                Anima.SetBool("Walk", true);
            }


            //una ves que esta en el radio de vision entoces el enemigo empezara a tacar al jugador 

            if (distance < AttackRadius)
            {


                if (Time.time > tiempo)
                {
                    //se activa la misica de ataque 

                    AudioFireball.Play();

                    //guardamos en el vector t la ubicacion del enemigo 

                    Vector2 t = transform.position;

                    GameObject _NewFireBall;

                    //el radio de distancia en el cual se instacian las bolitas de fuego aleatoriamente 
                    

                    float instanciateRangex = Random.Range(-AttackRadius, AttackRadius);
                    float instanciateRangeY = Random.Range(-AttackRadius, AttackRadius);

                    //nueva varible vector para sumar la posicion actual del enemigo y el radio para que mantega un contante cambio y no me instacie en un solo lugar con el radio 

                    Vector2 _RandomPositionAttac = t + new Vector2(instanciateRangex, instanciateRangeY);
                    _NewFireBall = Instantiate(FireBall, _RandomPositionAttac, FireBall.transform.rotation);

                   

                    tiempo = Time.time + NetxFireBall;
                    Destroy(_NewFireBall, 3.5f);

                   


                }
                

               
            }
           

        }
        else
        {
            //si no esta detectando al jugador entoces dire que es falso para poder seguir con el modo patrulla 

            PlayerDetected = false;
          
        }
 


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //colicion del ataque con el enemigo
        //cada ves que muere el enemigo al detectar el rango de ataque , me instacia una moneda como recompesa en la posicion del enemigo 

        if (collision.gameObject.CompareTag("RangeAttack"))
        {
            GameObject NewMoney;
            NewMoney = Instantiate(Money, transform.position, transform.rotation);
            Destroy(this.gameObject);
            
        }
    }

    //las animaciones que se ejecutaran cada una en su tiempo 
    //esta es la animacion que se ejecuta cuando ele nemigo esta en modo patruya 
    public void Animations()
    {
         view = (Positions[NextPosition].position - transform.position);

        if (Enemy.transform.position != Vector3.zero)
        {
            Anima.SetFloat("VerX", view.x);
            Anima.SetFloat("VerY", view.y);
            Anima.SetBool("Walk", true);
        }
        else
        {
            Anima.SetBool("Walk", false);
        }

    }
    //Dibujamos Gizmos en el Enemigo Para conocer sus radios de Vision

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, VisionRadius);
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }
    

}
