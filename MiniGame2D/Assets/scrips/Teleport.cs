using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform Teleports;
    private GameObject Player;

   public GameObject CameraCinemachine1;
   public GameObject CameraCinemachine2;

 


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          
            CameraCinemachine1.SetActive(false);
            CameraCinemachine2.SetActive(true);
           
            Player.transform.position = Teleports.transform.position;
        }
       
    }
  
     
}
