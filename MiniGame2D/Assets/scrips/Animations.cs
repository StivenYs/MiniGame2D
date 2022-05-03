using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Animations : MonoBehaviour
{
    private Animator AnimaChest;
    public GameObject Espirit;
    public CircleCollider2D collition;

    

    void Start()
    {
        AnimaChest = GameObject.FindGameObjectWithTag("Chest").GetComponent<Animator>();
        collition.GetComponent<CircleCollider2D>();
    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collicion para abrir el cofre al entrar en su rango

        if (collision.CompareTag("Player"))
        {
            AnimaChest.SetBool("Open",true);
            StartCoroutine(InstanciateObject(0.2f, StateChes));
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AnimaChest.SetBool("Open", false);    
        }
    }
    IEnumerator  InstanciateObject(float time,Action action)
    {
        yield return new WaitForSecondsRealtime(time);
        action();
        StopAllCoroutines();
    }

    void StateChes()
    {
        Espirit.SetActive(true);
        collition.enabled = false;
    }


}
