using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    //assign
    public EnemyState state;
    public Rigidbody2D rb;
    public List<GameObject> patrolPointList = new List<GameObject>();
    
    //variables
    public float speed = 5f;
    
    private void Update()
    {
        if (state.isPatrolling)
        {
            if (state.isStarting)
            {
                rb.velocity = new Vector2(speed, 0f);
            }
            
            else if (state.isReturning)
            {
                rb.velocity = new Vector2(speed * -1f, 0f);
            }
        }    
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("PatrolPoint"))
        {
            if (patrolPointList.IndexOf(col.gameObject) == 0)
            {
                state.isStarting = true;
                state.isReturning = false;
            }
            
            else if (patrolPointList.IndexOf(col.gameObject) == patrolPointList.Count - 1)
            {
                state.isStarting = false;
                state.isReturning = true;
            }
        }
    }
}
