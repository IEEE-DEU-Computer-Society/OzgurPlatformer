using System;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    //movement states
    public bool isIdle;
    public bool isMoving;
    
    //patrol states
    public bool isPatrolling;
    public bool isStarting;
    public bool isReturning;
    
    //attack states
    public bool isAttacking;
}
