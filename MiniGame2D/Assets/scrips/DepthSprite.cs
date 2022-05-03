using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthSprite : MonoBehaviour
{
    [SerializeField]private bool DepthFram;
    
    public SpriteRenderer Sprites;

    void Start()
    {

    
        Sprites.GetComponent<SpriteRenderer>();
        Sprites.sortingLayerName = "Player";
        Sprites.gameObject.tag = "Player";

      
        

    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("collision"))
        {
            Sprites.sortingOrder = -1;
        }
      
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("collision"))
        {
            Sprites.sortingOrder = 2;
        }

    }
}
