using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter {

    private uint _level;
    private uint _freeExp; //这里也可理解成能力点或技能点
    /*public CharacterData characterData;
    private GameObject displayLayer;
    private float speed;
    private string standAnime = "Anime_stand_index";
    private bool iswalking = false;
    private bool isMoveReady = false;  */   //MoveAnimation的转身记录

    public override void AwakeAddition()
    {
        _level = 0;
        _freeExp = 0;
    }
    public void AddExp(int exp)
    {
        if (exp >= 0) _freeExp += (uint)exp;
        else if (-exp <= _freeExp) _freeExp -= (uint)exp;
        else return;//待填
        CaculateLevel();
    }

    public override void Start()
    {
        /*this.speed = characterData.speed / 100;
        displayLayer = characterData.characterDisplayer;*/
        animator = gameObject.GetComponentInChildren<Animator>();
        SetObjectZ();  
    }

   /* #region 移动及移动动画和状态
    public void Move(float deltaX,float deltaY)
    {
        MoveAnimation(deltaX, deltaY);  //调整动画 返回目标位置的deltaX、Y
        if (iswalking) {
            //根据两个朝向bool，确认移动方向；
            if (direction_index) {
                deltaY = -1; deltaX = 1;
                if (direction_left) deltaX = -1;
            }
            else {
                deltaY = 1; deltaX = 1;
                if (direction_left) deltaX = -1;
            }
            Vector3 moveTowardPosition = transform.position;
            moveTowardPosition.x += deltaX;
            moveTowardPosition.y += deltaY;
            float maxDistanceDelta = Time.deltaTime * speed;
            transform.position = Vector3.MoveTowards(transform.position, moveTowardPosition, maxDistanceDelta);
        }
        //SetObjectZ();
    }
    private void MoveAnimation(float h, float v)  //移动动画及移动状态
    {
        if (v < -0.05f) {
            if (!direction_index) iswalking = false;
            if (!iswalking) {
                standAnime = "Anime_stand_index";
                animator.Play(standAnime);
                direction_index = true;
                animator.SetBool("move_walk", true);
                v = -1;
                iswalking = true;
            }
        }
        else if (v > 0.05f) {
            if (direction_index) iswalking = false;
            if (!iswalking) {
                standAnime = "Anime_stand_back";
                animator.Play(standAnime);
                direction_index = false;
                animator.SetBool("move_walk", true);
                v = 1;
                iswalking = true;
            }
        }
        if (h < -0.05f) {
            displayLayer.transform.localScale = new Vector3(1, 1, 1);
            direction_left = true;
            if (iswalking) return;
            else if (h < -0.25f) {
                h = -1;
                animator.SetBool("move_walk", true);
                iswalking = true;
            }
        }
        else if (h > 0.05f) {
            displayLayer.transform.localScale = new Vector3(-1, 1, 1);
            direction_left = false;
            if (iswalking) return;
            else if (h > 0.25f) {
                h = 1;
                animator.SetBool("move_walk", true);
                iswalking = true;
            }
        }
        //停止
        if (iswalking && System.Math.Abs(h) < 1 && System.Math.Abs(v) < 1) {
            animator.SetBool("move_walk", false);
            animator.Play(standAnime);
            iswalking = false;
        }
    }
    #endregion

    #region public 存取等级、自由经验的私有变量    cheracterName
    public uint cheracterLevel
    {
        get { return _level; }
        set { _level = value; }
    }
    public uint characterFreeExp
    {
        get { return _freeExp; }
        set { _freeExp = value; }
    }
    #endregion */
}
