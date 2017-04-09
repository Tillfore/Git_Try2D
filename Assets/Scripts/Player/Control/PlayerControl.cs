using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public GameObject defaultControlObject;
    private GameObject controlObject;
    private GameObject targetObject;

    void Start()
    {
        controlObject = defaultControlObject;
        targetObject = defaultControlObject;
    }

    void Update()
    {
        MoveControl();            
    }

    void MoveControl()
    {
        controlObject.GetComponent<CharacterControl>().Move();
    }

}
