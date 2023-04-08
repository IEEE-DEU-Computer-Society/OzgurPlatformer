using System;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    //assign
    public EnemyState state;
    public GameObject right;
    public GameObject left;

    //variables
    public RaycastHit2D rightCheck;
    public RaycastHit2D leftCheck;
    public float enemySightRange = 5f;

    void Update()
    {
        rightCheck = Physics2D.Raycast(right.transform.position, Vector2.right, enemySightRange);
        leftCheck = Physics2D.Raycast(left.transform.position, Vector2.left, enemySightRange);

        Debug.DrawLine(right.transform.position,
            new Vector2(right.transform.position.x + enemySightRange, right.transform.position.y));

        if (rightCheck.collider != null)
        {
            state.isAttacking = rightCheck.collider.CompareTag("Player");
            state.isPatrolling = !state.isAttacking;
        }

        else if (leftCheck.collider != null)
        {
            state.isAttacking = leftCheck.collider.CompareTag("Player");
            state.isPatrolling = !state.isAttacking;
        }

        else
        {
            state.isAttacking = false;
            state.isPatrolling = true;
        }
    }
}