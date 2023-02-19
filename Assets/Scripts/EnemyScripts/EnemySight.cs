using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemySight : MonoBehaviour
{
    //assign
    public EnemyState enemyState;
    public GameObject right;
    public GameObject left;

    //variables
    public RaycastHit2D rightCheck;
    public RaycastHit2D leftCheck;
    public float enemySightRange = 5f;
    public Vector2 enemySightVectorRight;
    public Vector2 enemySightVectorLeft;

    void Update()
    {
        enemySightVectorRight = new Vector2(transform.position.x + enemySightRange, transform.position.y);
        enemySightVectorLeft = new Vector2(transform.position.x - enemySightRange, transform.position.y);

        rightCheck = Physics2D.Raycast(right.transform.position, Vector2.right, enemySightRange);
        leftCheck = Physics2D.Raycast(left.transform.position, Vector2.left, enemySightRange);

        if (rightCheck.collider != null)
        {
            enemyState.isAttacking = rightCheck.collider.CompareTag("Player");
            enemyState.isPatrolling = !enemyState.isAttacking;
        }

        else if(leftCheck.collider != null)
        {
            enemyState.isAttacking = leftCheck.collider.CompareTag("Player");
            enemyState.isPatrolling = !enemyState.isAttacking;
        }
    }
}