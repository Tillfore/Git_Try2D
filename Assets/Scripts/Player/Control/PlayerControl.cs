using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public PlayerProperty plyerProperty;
    public GameObject defaultControlObject;
    private GameObject controlObject;
    private GameObject targetObject;
    private int controlObject_speed;
    private CharacterControl characterControl;
    private CameraControl cameraControl;


    void Start()
    {
        controlObject = defaultControlObject;
        targetObject = defaultControlObject;
        characterControl = controlObject.GetComponent<CharacterControl>();
        cameraControl = gameObject.GetComponent<CameraControl>();
        controlObject_speed = characterControl.characterData.speed;
    }

    void Update()
    {
        if (controlObject != null)
        {
            MoveControl(); //移动操作
            cameraControl.GetShootingTarget(controlObject, controlObject_speed); //调用摄像机
        }
    }


    void MoveControl()
    {
        characterControl.Move();
    }

}
