using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    //variables
    public Vector3 playerPos;
    public GameObject player;
    public float cameraDelay = 5f;
    
    void Update()
    {
        playerPos = new Vector3(player.transform.position.x,player.transform.position.y, -10);
        transform.position = Vector3.Lerp(transform.position, playerPos, cameraDelay * Time.deltaTime);
    }
}
