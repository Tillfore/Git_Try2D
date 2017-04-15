using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;       //添加对Enum class的使用

public class BaseCharacter : MonoBehaviour {

    #region 临时数据库
    public CharacterData characterData;//暂时代替数据库
    #endregion

    private uint _id;
    private string _name;


    private Vital[] _vital;
    private Property[] _property;
    protected int m_deltaPropertyLength = 3;//一般角色少继承3个能力值

    private string _standAnime = "Anime_stand_index";

    protected GameObject m_displayLayer;
    protected Animator m_animator;
    protected bool m_direction_index = true; //记录前后朝向
    protected bool m_direction_left = true;  //记录左右朝向
    protected bool m_iswalking = false;
    protected bool m_isMoveReady = false;     //MoveAnimation的转身记录
    protected float m_speed;

    public void Awake()
    {
        _name = string.Empty;
        SetupCharacterData();
        _vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
        _property = new Property[Enum.GetValues(typeof(PropertyName)).Length-m_deltaPropertyLength];
        //创建各属性能力值列表
        SetupVitals();
        SetupPropertys();
        //子类附加

    }

    public virtual void SetupCharacterData()
    {
        #region 临时数据读取
        this.m_speed = characterData.speed / 100;
        m_displayLayer = characterData.characterDisplayer;
        #endregion
    }

    public virtual void Start()
    {
        m_animator = gameObject.GetComponentInChildren<Animator>();
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
    public uint id
    {
        get { return _id; }
        set { _id = value; }
    }
    public string characterName
    {
        get { return _name; }
        set { _name = value; }
    }
    #endregion

    #region private 创建属性、生命体力、技能    SetupVitals

    private void SetupVitals()
    {
        for (int i = 0; i < _vital.Length; i++) {
            _vital[i] = new Vital(i);
        }
    }
    private void SetupPropertys()
    {
        for (int i = 0; i < _property.Length; i++) {
            _property[i] = new Property(i);
        }
    }
    #endregion

    #region public 获取属性、生命体力、技能值   GetVital

    public Vital GetVital(int index)
    {
        return _vital[index];
    }
    public Property GetProperty(int index)
    {
        return _property[index];
    }
    /*public Skill GetSkill(int index)
    {
        return _skill[index];
    }*/
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
        for(int i = 0; i < _property.Length; i++) {
            _property[i].Update();
        }
    }

    #region 移动及移动动画和状态
    public void Move(float deltaX, float deltaY)
    {
        MoveAnimation(deltaX, deltaY);  //调整动画 返回目标位置的deltaX、Y
        if (m_iswalking) {
            //根据两个朝向bool，确认移动方向；
            if (m_direction_index) {
                deltaY = -1; deltaX = 1;
                if (m_direction_left) deltaX = -1;
            }
            else {
                deltaY = 1; deltaX = 1;
                if (m_direction_left) deltaX = -1;
            }
            Vector3 moveTowardPosition = transform.position;
            moveTowardPosition.x += deltaX;
            moveTowardPosition.y += deltaY;
            float maxDistanceDelta = Time.deltaTime * m_speed;
            transform.position = Vector3.MoveTowards(transform.position, moveTowardPosition, maxDistanceDelta);
        }
        //SetObjectZ();
    }
    private void MoveAnimation(float h, float v)  //移动动画及移动状态
    {
        if (v < -0.05f) {
            if (!m_direction_index) m_iswalking = false;
            if (!m_iswalking) {
                _standAnime = "Anime_stand_index";
                m_animator.Play(_standAnime);
                m_direction_index = true;
                m_animator.SetBool("move_walk", true);
                v = -1;
                m_iswalking = true;
            }
        }
        else if (v > 0.05f) {
            if (m_direction_index) m_iswalking = false;
            if (!m_iswalking) {
                _standAnime = "Anime_stand_back";
                m_animator.Play(_standAnime);
                m_direction_index = false;
                m_animator.SetBool("move_walk", true);
                v = 1;
                m_iswalking = true;
            }
        }
        if (h < -0.05f) {
            m_displayLayer.transform.localScale = new Vector3(1, 1, 1);
            m_direction_left = true;
            if (m_iswalking) return;
            else if (h < -0.25f) {
                h = -1;
                m_animator.SetBool("move_walk", true);
                m_iswalking = true;
            }
        }
        else if (h > 0.05f) {
            m_displayLayer.transform.localScale = new Vector3(-1, 1, 1);
            m_direction_left = false;
            if (m_iswalking) return;
            else if (h > 0.25f) {
                h = 1;
                m_animator.SetBool("move_walk", true);
                m_iswalking = true;
            }
        }
        //停止
        if (m_iswalking && Math.Abs(h) < 1 && Math.Abs(v) < 1) {
            m_animator.SetBool("move_walk", false);
            m_animator.Play(_standAnime);
            m_iswalking = false;
        }
    }
    #endregion

}
