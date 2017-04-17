using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public int maxSpeed = 500;         //最快镜头速度
    public float maxDistance = 1;    //镜头跟随缓冲距离
    private bool isStatic;          //镜头目标是否静止
    private Transform shootingTarget;
    private float _cameraSpeed;           //镜头速度
//  public Transform roomLeftDown;
//  public Transform roomRightUp;
    public Vector3 mTargetPos = Vector3.zero;

    public void GetShootingTarget(Transform trans,int speed)
    {
        _cameraSpeed = speed / 100;
        if (shootingTarget == trans && !isStatic) isStatic = true;
        else isStatic = false;
        shootingTarget = trans;
    }
    void LateUpdate()
    {
        mTargetPos = GetCameraMovePos();
        if (_cameraSpeed > maxSpeed/100) _cameraSpeed = maxSpeed/100;
        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, mTargetPos, _cameraSpeed * Time.deltaTime);
    }

    Vector3 GetCameraMovePos()
    {
        Vector3 pos = shootingTarget.position;
        pos.z -= 10;   //摄像机相对高度
        var distance = (pos - Camera.main.transform.position).magnitude;
        if(!isStatic) pos = Camera.main.transform.position;
        _cameraSpeed *= distance/maxDistance ;
        return pos;
    }

   /* public float SceneToWorldSize(float size, Camera ca, float Worldz)
    {
        if (ca.orthographic)
        {
            float height = Screen.height / 2;
            float px = (ca.orthographicSize / height);
            return px * size;
        }
        else
        {
            float halfFOV = (ca.fieldOfView * 0.5f);//摄像机夹角 的一半//
            halfFOV *= Mathf.Deg2Rad;//弧度转角度//

            float height = Screen.height / 2;
            float px = height / Mathf.Tan(halfFOV);//得到应该在的Z轴//
            Worldz = Worldz - ca.transform.position.z;
            return (Worldz / px) * size-10;
        }
    }*/
}
