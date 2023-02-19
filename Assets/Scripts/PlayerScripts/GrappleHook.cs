using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    //assign
    public PlayerState state;
    public Camera camera;
    public GameObject top;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;

    public Rigidbody2D rb;

    //variables
    public bool isCeiling;
    public Vector2 mousePosition;
    public RaycastHit2D ceilingCheck;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            ceilingCheck = Physics2D.Linecast(top.transform.position, mousePosition);
            if (ceilingCheck.collider != null)
            {
                isCeiling = ceilingCheck.collider.CompareTag("Ceiling");
            }

            else
            {
                isCeiling = false;
            }
        }
        
        else if (Input.GetMouseButtonUp(0))
        {
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;
            state.isGrappled = false;
            isCeiling = false;
        }
        
        if (isCeiling)
        {
            distanceJoint.connectedAnchor = ceilingCheck.transform.position;
            distanceJoint.enabled = true;
            
            lineRenderer.SetPosition(1,transform.position);
            lineRenderer.SetPosition(0, ceilingCheck.transform.position);
            lineRenderer.enabled = true;
        }

        if (distanceJoint.enabled)
        {
            state.isGrappled = true;
            lineRenderer.SetPosition(1, transform.position);

            if (Input.GetMouseButtonDown(1))
            {
                state.isPulled = true;
                distanceJoint.autoConfigureDistance = false;
            }

            if (state.isPulled)
            {
                //rb.gravityScale = 0f;                                           //aot
                distanceJoint.distance -= 20f * Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(1))
            {
                //rb.gravityScale = 4f;                                           //aot
                state.isPulled = false;
                distanceJoint.autoConfigureDistance = true;
            }
        }
    }
}
