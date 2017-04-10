using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public GameObject defaultControlObject;
    private GameObject controlObject;
    private GameObject targetObject;
    private int controlObject_speed;


    void Start()
    {
        controlObject = defaultControlObject;
        targetObject = defaultControlObject;
        controlObject_speed = controlObject.GetComponent<CharacterData>().speed;
    }

    void Update()
    {
        if (controlObject != null)
        {
            MoveControl();
            gameObject.GetComponent<CameraControl>().GetShootingTarget(controlObject, controlObject_speed);
        }
    }


    void MoveControl()
    {
        controlObject.GetComponent<CharacterControl>().Move();
    }

}
