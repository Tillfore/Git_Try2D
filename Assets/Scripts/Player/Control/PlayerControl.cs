using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    public PlayerInformation plyerInformation;
    public GameObject defaultControlObject;
    private Transform controlTrans;
    private Transform targetTrans;
    private int controlObject_speed;
    private PlayerCharacter characterControl;
    private CameraControl cameraControl;


    void Start()
    {
        controlTrans = defaultControlObject.transform;
        targetTrans = defaultControlObject.transform;
        characterControl = controlTrans.GetComponent<PlayerCharacter>();
        cameraControl = gameObject.GetComponent<CameraControl>();
        controlObject_speed = (int)characterControl.characterData.speed;
    }

    void Update()
    {
        if (characterControl != null)
        {
            MoveControl(); //移动操作
            SelectTargetObject();
            cameraControl.GetShootingTarget(controlTrans, controlObject_speed); //调用摄像机
        }
    }

    void SelectTargetObject()
    {
        if (Input.GetMouseButtonDown(0)) //确认鼠标点击
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //创建射线ray为鼠标点
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.down);
            targetTrans = hit.collider.transform;
            //如果对象在Player层，则将其选为操作对象。不过LayerMask.GetMask("Layer_Player")不好用，所以直接写12
            if(targetTrans.gameObject.layer == 12 && targetTrans != controlTrans) SelectControlObject();
        }
    }
    void SelectControlObject(Transform trans=null,int legalLayer = 12)
    {
        if (!trans) trans = targetTrans;
        if (trans.gameObject.layer == legalLayer && trans != controlTrans) {
            controlTrans = trans;
            characterControl = controlTrans.GetComponent<PlayerCharacter>();
            Debug.Log("操作对象更换为：" + controlTrans);
        }
    }

    void MoveControl()
    {
        characterControl.Move(Input.GetAxis("Horizontal"),Input.GetAxisRaw("Vertical"));
    }

}
