using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    //assign
    public EnemySight enemySight;
    public EnemyPatrol enemyPatrol;
    public EnemyAttack enemyAttack;

    //movement states
    public bool isIdle;
    public bool isMoving;
    
    //patrol states
    public bool isPatrolling;
    
    //attack states
    public bool isAttacking;
    
    void Update()
    {
        
    }
}
