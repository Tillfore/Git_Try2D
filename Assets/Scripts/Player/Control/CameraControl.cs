using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public int maxSpeed = 500;         //最快镜头速度
    public float maxDistance = 1;    //镜头跟随缓冲距离
    private bool isStatic;          //镜头目标是否静止
    private Transform shootingTarget;
    private float mSpeed;           //镜头速度
//  public Transform roomLeftDown;
//  public Transform roomRightUp;
    public Vector3 mTargetPos = Vector3.zero;

    public void GetShootingTarget(GameObject shootingObject,int speed)
    {
        mSpeed = speed / 100;
        if (shootingTarget == shootingObject.transform && !isStatic) isStatic = true;
        else isStatic = false;
        shootingTarget = shootingObject.transform;
    }
    void LateUpdate()
    {
        mTargetPos = GetCameraMovePos();
        if (mSpeed > maxSpeed/100) mSpeed = maxSpeed/100;
        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, mTargetPos, mSpeed * Time.deltaTime);
    }

    Vector3 GetCameraMovePos()
    {
        Vector3 pos = shootingTarget.position;
        pos.z -= 10;   //摄像机高度
        var distance = (pos - Camera.main.transform.position).magnitude;
        if(!isStatic) pos = Camera.main.transform.position;
        mSpeed *= distance/maxDistance ;
        /*float screenZ = SceneToWorldSize(Screen.width * 0.5f, Camera.main, pos.z);
        pos.y = Camera.main.transform.position.y;
        pos.x = Camera.main.transform.position.x;
        float maxZ = roomRightUp.position.z;
        float minZ = roomLeftDown.position.z;
        if (pos.z - screenZ < minZ)
        {
            pos.z = minZ + screenZ;
        }
        else if (pos.z + screenZ > maxZ)
        {
            pos.z = maxZ - screenZ;
        }*/
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
