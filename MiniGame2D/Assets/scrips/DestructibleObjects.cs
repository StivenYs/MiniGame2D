using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DestructibleObjects : MonoBehaviour
{
    //aqui se ejecuta la animacion de los objetos destructibles en el mapa 
   public Animator Tree;
    

    void Start()
    {
        Tree.GetComponent<Animator>();
    }


    //objetos destruibles 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //colcion del rango de ataque con el obejto destruible 
        if (collision.gameObject.CompareTag("RangeAttack"))
        {
            Tree.SetBool("Tree", true);

            StartCoroutine(ContTreeDestroy(1.5f, DestroyTree));
            


        }
    }
    IEnumerator ContTreeDestroy(float time,Action action)
    {
        yield return new WaitForSecondsRealtime(time);
        action();
    }

    void DestroyTree()
    {
        Destroy(this.gameObject);
    }
    
}
