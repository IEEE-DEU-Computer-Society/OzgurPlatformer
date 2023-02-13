using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyAI : MonoBehaviour
{
    //TODO bugfix.
    
    //assign
    public GameObject eyeR;
    public GameObject eyeL;
    //assign
    
    public RaycastHit2D hitR;
    public RaycastHit2D hitL;
    public float enemySightRange = 5f;
    public Vector2 enemySightVectorRight;
    public Vector2 enemySightVectorLeft;
    void Update()
    {
        enemySightVectorRight = new Vector2(transform.position.x + enemySightRange, transform.position.y);
        enemySightVectorLeft = new Vector2(transform.position.x - enemySightRange, transform.position.y);
        
        hitR = Physics2D.Linecast(eyeR.transform.position, enemySightVectorRight);
        Debug.DrawLine(eyeR.transform.position,enemySightVectorRight,Color.red);
        
        hitL = Physics2D.Linecast(eyeL.transform.position, enemySightVectorLeft);
        Debug.DrawLine(eyeL.transform.position,enemySightVectorLeft,Color.red);
        
        //working
        if (hitL.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("vurdu");
        }
        
        //not working
        if (hitR.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("2vurdu");
        }
        
        //switch places. upper one works, other doesn't. why?
    }
}
