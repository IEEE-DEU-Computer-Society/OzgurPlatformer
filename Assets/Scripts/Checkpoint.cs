using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //known bugs:
    //doesn't prevent previous checkpoint to become new checkpoint
    
    //TODO tags instead of layers, bug fix

    public Vector2 lastCheckpoint;
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.layer == 8) //layer number 8 is checkpoint layer
        {
            lastCheckpoint = other.gameObject.transform.position;
        }
    }
}
