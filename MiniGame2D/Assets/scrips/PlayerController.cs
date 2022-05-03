using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
    //Movimiento

    [Header("Movimiento Jugador")]

    [SerializeField] private int speed;
    public Animator Animation;
    public Rigidbody2D Player;
    private Vector2 _movement;

    //Joystick

    [Header("Control De Movimiento Joystick")]   

    public Joystick joystick;

    //Vida Jugador

    public float _Contlife;
    public Image _Hp;

    //ataque jugador 

    CircleCollider2D _RangeAttack;
    [SerializeField] bool _Active_RangeAttack;

    //jugador entre scenas

    [HideInInspector]
    public bool NextScene;
    [HideInInspector]
    public bool DeathScene;

    //llamdo del scrip de Monedas

    Money money;

    //sonido 

   public AudioSource Walking;
   public AudioSource Attacking;



  


    void Start()
    {

        

        //Jugador

        //sonidos de atacar y caminar

       Walking.GetComponent<AudioSource>();
       Attacking.GetComponent<AudioSource>();

        //ajutamos los colaider al tamaño deseado desde el incio del juego.
        //tambien congelando la rotacion del jugador para evitar efectos extraños 

       Player.GetComponent<Rigidbody2D>();
       Player.GetComponent<BoxCollider2D>().size = new Vector2(1.024788f, 0.5783129f);
       Player.GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.12f);
       Player.gravityScale = 0;
       Player.freezeRotation =true;

        //Vida Jugador

       _Contlife = 100;

        //aTaque Jugador 

        //buscamos un colider que tiene el Player como hijo 
        //desativamos ese colider para que solo se active al momneto de atacar

        _RangeAttack = transform.GetChild(0).GetComponent<CircleCollider2D>();
        _RangeAttack.enabled = false;
        _Active_RangeAttack = false;

        //jugador entre scenas 

        NextScene = false;

        //scena de Muerte 

        DeathScene = false;
      

        //llamdo del scrip de Monedas

        money = GameObject.FindGameObjectWithTag("LevelScene").GetComponent<Money>();


    }


    private void FixedUpdate()
    {
       

        //Vida Jugador

        LifePlayer();

        ActiveRange();

        MoveOffsetRange();

        MovePlayer();
        
            


    }


    public void Attack()
    {
        //este evento se ejeutara una ves que se precione el boton de atacar
        //verificamos cual es el estado actual de la animacion de atacar del jugador 
        //star confirmacion nos devolvera un verdadero

        AnimatorStateInfo stateInfo = Animation.GetCurrentAnimatorStateInfo(0);

        bool attacking = stateInfo.IsName("Attack");

        //aqui activamos el colider hijo del jugador para ver si hay una colicion con el enemigo 

        _Active_RangeAttack = true;

         
        StartCoroutine(ActiveRangeAttack(0.50f, TrueRangeAttack));

        if (!attacking)
        {
            Animation.SetTrigger("Attacking");
            Attacking.Play();

        }



    }
    IEnumerator ActiveRangeAttack(float time,Action action)
    {
        //esta corrutina dos dira cuando se desativa el colider hijo del jugador el cual es el que nos da la colicion con enemigo 
        //para evitar seguir haciendo daño despues de atacar 

        yield return new WaitForSecondsRealtime(time);
        action();
    }

      void TrueRangeAttack()
      {
        _Active_RangeAttack = false;
       

      }

    




    //vida Jugador

    private void OnTriggerStay2D(Collider2D collision)
    {
        //confirmamos si hibo colicion del los proyectiles enemigos con el jugador 

        if (collision.gameObject.CompareTag("FireBall"))
        {
            _Contlife -= 0.5f;
            LifePlayer();
        }
        if (collision.gameObject.CompareTag("Distance"))
        {
            _Contlife -= 0.5f;
            LifePlayer();
        }
        

    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        //Teleport
        //En cada scena se agrego un numero de scena para que cada vez que el jugador colliciones con el colider "teleport1" me pueda cambiar de scena 

        if (collision.gameObject.CompareTag("Teleport1"))
        {
            NextScene = true;
        }
    }
    public void LifePlayer()
    {
        //definimos una vida minima y maxima que tendra el jugador y esta se agregara a una barra que tendra una funcion de llenado 

        //Vida Jugador

        _Contlife = Mathf.Clamp(_Contlife, 0, 100);
        _Hp.fillAmount = _Contlife / 100;

        if (_Contlife <= 0)
        {
           
            //Al llegar Tu vida a 0
            //cargamos la animacion  de muerte y esperamos un timepo para cambiar de scena 

            Animation.SetTrigger("Dead");
            StartCoroutine(DeadPlayer(1.5f));


        }
        if (money._Money == 20 && _Contlife <= 90f)
        {  
            //hacenos una confimacion del contador de modenas para saber cuntas tenemos en total al largo de todas las scenas 
            //si esta condiciones se cumplen nuestra vida se llenara por completo 

            //recuperas el total de tu vida 
            _Contlife = 100;

        }
    }
    IEnumerator DeadPlayer(float time)
    {
        yield return new WaitForSeconds(time);
        DeathScene = true;
    
    }
  


    void MoveOffsetRange()
    {
        //con esto moveremos el colider de ataque para que cada ves que el jugador gira asi mismo el offse del colider lo haga.
        //se divide para matentener una pequeña distancia entre el jugador 

        if (_movement != Vector2.zero)
        {
            _RangeAttack.offset = new Vector2(_movement.x, _movement.y / 2);
        }
    }
    
    void ActiveRange()
    {
        // este evento se llama en la accion de atacar para activar el rango de ataque y una vez alli se dessativa con la corrutina 

        if (_Active_RangeAttack == true)
        {
            _RangeAttack.enabled = true;
        }
        else
        {
            _RangeAttack.enabled = false;
        }
    }

    void MovePlayer()
    {

       //A un vector 2 le asiganamos los ejes al jystick para que me detecte hacia donde se esta moviendo 
       //si el movimiento del joystick es mayor al vector2 en 0 , y = 0 ,X = 0 entoces si esto es lo contrario a 0 entoces me ejeutara las aniamciones  
       //y me mantendra congelada en la aultima que se ejecuto //ecepto la de caminar 

        _movement = new Vector2(joystick.Horizontal, joystick.Vertical);

   
        if (_movement != Vector2.zero)
        {
           
            Animation.SetFloat("verX", _movement.x);
            Animation.SetFloat("verY", _movement.y);
            Animation.SetBool("Walking", true);
          

        }
        else
        {
            Animation.SetBool("Walking", false);
           
        }

        //le agregamos el destino al cual se movera

        Player.MovePosition(Player.position + _movement * speed * Time.fixedDeltaTime);

    }

    



}
