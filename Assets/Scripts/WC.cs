using UnityEngine;

public class WC : MonoBehaviour
{
    public bool check;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Wall"))
        {
            check = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            check = false;
        }
    }
}

//ADD THIS TO PLAYER CONTROL TO ACTIVATE COLLIDER TYPE WALL CHECK
//DONT FORGET TO ENABLE COLLIDERS FOR RIGHT AND LEFT
//DONT FORGET TO COMMENT RAYCAST WALL CHECK SECTION
//DONT FORGET TO ADD TWO WC GAME OBJECTS TO PLAYER CONTROL

/*if (WCRight.check)
{
    state.isRightWalled = true;
}

else
{
    state.isRightWalled = false;
}

if (WCLeft.check)
{
    state.isLeftWalled = true;
}

else
{
    state.isLeftWalled = false;
}

state.isWalled = state.isRightWalled || state.isLeftWalled;*/
