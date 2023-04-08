using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //assign
    public PlayerState state;
    public Camera camera;
    public GameObject right;
    public GameObject left;

    //facing variable
    public Vector2 mousePosition;
    
    //attack variables
    public float meleeAttackRange = 3f;
    public RaycastHit2D attackCheck;
    public GameObject enemy;
    
    private void Update()
    {
        //facing states
        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x - transform.position.x > 0f)
        {
            state.isFacingRight = true;
            state.isFacingLeft = false;
        }

        else
        {
            state.isFacingLeft = true;
            state.isFacingRight = false;
        }
        //facing states
        
        //attack check
        if (state.isFacingRight)
        {
            attackCheck = Physics2D.Raycast(right.transform.position, Vector2.right, meleeAttackRange);
        }

        else
        {
            attackCheck = Physics2D.Raycast(left.transform.position, Vector2.left, meleeAttackRange);
        }
        //attack check
        
        //enemy selection
        if (Input.GetMouseButtonDown(0) && attackCheck.collider != null)
        {
            if (attackCheck.collider.CompareTag("Enemy"))
            {
                enemy = attackCheck.collider.gameObject;
            }
        }
        //enemy selection
    }
}
