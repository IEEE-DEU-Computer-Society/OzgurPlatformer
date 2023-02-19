using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    //assign
    public EnemyState enemyState;
    public List<GameObject> patrolPointList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PatrolPoint"))
        {
            
        }
    }
}
