using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;       //添加对Enum class的使用

public class BaseCharacter : MonoBehaviour {

    #region 临时数据库
    public CharacterData characterData;//暂时代替数据库
    private float speed;
    #endregion

    private string _name;

    private Attribute[] _primaryAttribute;
    private Vital[] _vital;
    private Property[] _property;
    private Skill[] _skill;

    private string _standAnime = "Anime_stand_index";

    protected GameObject displayLayer;
    protected Animator animator;
    protected bool direction_index = true; //记录前后朝向
    protected bool direction_left = true;  //记录左右朝向
    protected bool iswalking = false;
    protected bool isMoveReady = false;     //MoveAnimation的转身记录

    public void Awake()
    {
        _name = string.Empty;
        _primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
        _vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
        _property = new Property[Enum.GetValues(typeof(PropertyName)).Length];
        _skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];
        //创建各属性能力值列表
        SetupPrimaryAttributes();
        SetupVitals();
        SetupPropertys();
        SetupSkills();
        #region 临时数据读取
        this.speed = characterData.speed / 100;
        displayLayer = characterData.characterDisplayer;
        #endregion
        //子类附加
        AwakeAddition();
    }

    public virtual void AwakeAddition()
    {
        Debug.Log("AwakeAddition没写");
    }

    public virtual void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        SetObjectZ();
    }

    public virtual void Update()
    {
        SetObjectZ();
    }

    public void CaculateLevel()
    {

    }

    public void SetObjectZ(Transform trans = null)
    {
        if (!trans) trans = gameObject.transform;
        var pos = trans.position;
        pos.z = pos.y * 0.001f;
        gameObject.transform.position = pos;
    }

    #region public 存取名称的私有变量    cheracterName
    public string characterName
    {
        get { return _name; }
        set { _name = value; }
    }
    #endregion

    #region private 创建属性、生命体力、技能    SetupVitals
    private void SetupPrimaryAttributes()
    {
        for (int i = 0; i < _primaryAttribute.Length; i++) {
            _primaryAttribute[i] = new Attribute();
        }
    }
    private void SetupVitals()
    {
        for (int i = 0; i < _vital.Length; i++) {
            _vital[i] = new Vital();
        }
    }
    private void SetupPropertys()
    {
        for (int i = 0; i < _property.Length; i++) {
            _property[i] = new Property();
        }
    }
    private void SetupSkills()
    {
        for (int i = 0; i < _skill.Length; i++) {
            _skill[i] = new Skill();
        }
    }
    #endregion

    #region public 获取属性、生命体力、技能值   GetVital
    public Attribute GetPrimaryAttribute(int index)
    {
        return _primaryAttribute[index];
    }
    public Vital GetVital(int index)
    {
        return _vital[index];
    }
    public Property GetProperty(int index)
    {
        return _property[index];
    }
    public Skill GetSkill(int index)
    {
        return _skill[index];
    }
    #endregion

    /*#region private 修改属性、生命体力、技能值
    private void SetupVitalModifiers()
    {
        //health
        GetVital((int)VitalName.Health).AddModifier(
            new ModifyingAttribute {
                attribute = GetPrimaryAttribute((int)AttributeName.Strong), ratio = 1.1f
            });
        //energy
        GetVital((int)VitalName.Energy).AddModifier(
            new ModifyingAttribute {
                attribute = GetPrimaryAttribute((int)AttributeName.Dexterity), ratio = 1.34f
            });
    }
    private void SetupPropertyModifiers()
    {
        GetProperty((int)PropertyName.Attack).AddModifier(
            new ModifyingAttribute {
                attribute = GetPrimaryAttribute((int)AttributeName.Strong), ratio = 0f
            }); 
    }
    private void SetupSkillModifiers()
    {
        
    }
    #endregion */

    public void StatUpdate()
    {
        for(int i = 0; i < _vital.Length; i++) {
            _vital[i].Update();
        }
        for(int i = 0; i < _skill.Length; i++) {
            _skill[i].Update();
        }
    }

    #region 移动及移动动画和状态
    public void Move(float deltaX, float deltaY)
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
                _standAnime = "Anime_stand_index";
                animator.Play(_standAnime);
                direction_index = true;
                animator.SetBool("move_walk", true);
                v = -1;
                iswalking = true;
            }
        }
        else if (v > 0.05f) {
            if (direction_index) iswalking = false;
            if (!iswalking) {
                _standAnime = "Anime_stand_back";
                animator.Play(_standAnime);
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
            animator.Play(_standAnime);
            iswalking = false;
        }
    }
    #endregion

}
