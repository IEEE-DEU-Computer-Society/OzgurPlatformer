using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    public Vector3 characterPos;
    public Transform camTransform;
    public GameObject character;
    public float cameraDelay = 5f;
    void Start()
    {
        character = GameObject.Find("Character");
        camTransform = GetComponent<Transform>();
    }
    
    void Update()
    {
        characterPos = new Vector3(character.transform.position.x,character.transform.position.y, -10);
        camTransform.position = Vector3.Lerp(camTransform.position, characterPos, cameraDelay * Time.deltaTime);
    }
}
