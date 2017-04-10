using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

    private GameObject displayLayer;
    private float speed;
    private bool direction_index = true; //记录前后朝向
    private bool direction_left = true;  //记录左右朝向
    private Animator animator;
    private string standAnime = "Anime_stand_index";
    private float deltaX;
    private float deltaY;
    private bool iswalking = false;
    private bool isMoveReady = false;     //MoveAnimation的转身记录

    void Start()
    {
        this.speed = gameObject.GetComponent<CharacterData>().speed/100;
        animator = gameObject.GetComponentInChildren<Animator>();
        displayLayer = gameObject.GetComponent<CharacterData>().characterDisplayer;
    }

    public void Move()
    {
        deltaX = Input.GetAxis("Horizontal");
        deltaY = Input.GetAxisRaw("Vertical");
        MoveAnimation(deltaX,deltaY);  //调整动画 返回目标位置的deltaX、Y
        if (iswalking)
        {
            //根据两个朝向bool，确认移动方向；
            if (direction_index)
            {
                deltaX = 10; deltaY = -10;
                if (direction_left) deltaX = 0;
                else deltaY = 0;
            }
            else
            {
                deltaX = -10; deltaY = 10;
                if (direction_left) deltaY = 0;
                else deltaX = 0;
            }
            Vector3 currentPosition = transform.position;
            Vector3 moveTowardPosition = currentPosition;
            moveTowardPosition.x += deltaX;
            moveTowardPosition.y += deltaY;
            float maxDistanceDelta = Time.deltaTime * speed;
            transform.position = Vector3.MoveTowards(currentPosition, moveTowardPosition, maxDistanceDelta);
        }

    }

    private void MoveAnimation(float h,float v)  //移动动画及移动状态
    {
        if (v < -0.05f)
        {
            if (!direction_index) iswalking = false;
            if (!iswalking)
            {
                standAnime = "Anime_stand_index";
                animator.Play(standAnime);
                direction_index = true;
                animator.SetBool("move_walk", true);
                v = -1;
                iswalking = true;
            }
        }
        else if (v > 0.05f)
        {
            if (direction_index) iswalking = false;
            if (!iswalking)
            {
                standAnime = "Anime_stand_back";
                animator.Play(standAnime);
                direction_index = false;
                animator.SetBool("move_walk", true);
                v = 1;
                iswalking = true;
            }
        }
        if (h < -0.05f)
        {
            displayLayer.transform.localScale = new Vector3(1, 1, 1);
            direction_left = true;
            if (iswalking) return;
            else if (h < -0.25f)
            {
                h = -1;
                animator.SetBool("move_walk", true);
                iswalking = true;
            }
        }
        else if (h > 0.05f)
        {
            displayLayer.transform.localScale = new Vector3(-1, 1, 1);
            direction_left = false;
            if (iswalking) return;
            else if (h > 0.25f)
            {
                h = 1;
                animator.SetBool("move_walk", true);
                iswalking = true;
            }
        }
        //停止
        if (iswalking && System.Math.Abs(h) < 1 && System.Math.Abs(v) < 1)
        {
            animator.SetBool("move_walk", false);
            animator.Play(standAnime);
            iswalking = false;
        }
    }


}
