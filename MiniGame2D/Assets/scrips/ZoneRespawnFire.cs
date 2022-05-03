using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneRespawnFire : MonoBehaviour
{
    
    public float AttackRadius;
    [Space] public GameObject FireBall;
    [SerializeField] private float SpeedFireBall;
    [SerializeField] private float NetxFireBall;
    private float tiempo;
    private bool inZone;

    private void Start()
    {
        inZone = false;
    }

    private void Update()
    {
        AttackInZONE();
    }
    //se detecta la entrada del jugador a el cuarto 2 del mapa 2 para poder instanciar las bolitas de fuego y afectar al jugador 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inZone = false;
        }
    }



    //Dibujamos Gizmos en el Enemigo Para conocer sus radios 

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }

    // se intancia las bolitas de fuego por todo el cuarto (el rango del cuarto ) para afectar al enemigo 
    //esto es muy parecido al la forma de atacar de los enemigos 


    void AttackInZONE()
    {
        if (inZone == true)
        {
            if (Time.time > tiempo)
            {

                Vector2 t = transform.position;

                GameObject _NewFireBall;


                float instanciateRangex = Random.Range(-AttackRadius, AttackRadius);
                float instanciateRangeY = Random.Range(-AttackRadius, AttackRadius);


                Vector2 _RandomPositionAttac = t + new Vector2(instanciateRangex, instanciateRangeY);

                _NewFireBall = Instantiate(FireBall, _RandomPositionAttac, FireBall.transform.rotation);

                tiempo = Time.time + NetxFireBall;

                Destroy(_NewFireBall, 3.5f);
            }

        }
        
    }
}
