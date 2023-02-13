using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    //assign
    public LayerMask fallGround;
    public Checkpoint checkpoint;
    public GameObject character;
    public GameObject feet;
    //assign
    
    public bool isFalled = false;

    private void Update()
    {
        isFalled = Physics2D.OverlapBox(feet.transform.position, feet.transform.localScale, 0, fallGround);

        if (isFalled)
        {
            character.transform.position = checkpoint.lastCheckpoint;
        }
    }
}
