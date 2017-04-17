using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;       //添加对Enum class的使用

public class BaseCharacter : MonoBehaviour {

    #region 临时数据库
    public CharacterData characterData;//暂时代替数据库
    #endregion

    private int _id;
    private string _name;


    private Vital[] _vital;
    private Property[] _property;
    protected int m_deltaDifferentCharacter = 0;    //声明子类的具体类别

    private string _standAnime = "Anime_stand_index";

    protected GameObject m_displayLayer;
    protected Animator m_animator;
    protected bool m_direction_index = true; //记录前后朝向
    protected bool m_direction_left = true;  //记录左右朝向
    protected bool m_iswalking = false;
    protected bool m_isMoveReady = false;     //MoveAnimation的转身记录

    public BaseCharacter()
    {
    }

    #region public 封装角色名称的读写    ID  CheracterName
    public int ID {
        get { return _id; }
        set { _id = value; }
    }
    public string CharacterName {
        get { return _name; }
        set { _name = value; }
    }
    #endregion
    #region public 封装能力值的读写
    public Vital Hp {
        get { return GetVital(0); }
        set { SetVital(value, 0); }
    }
    public Vital Ep {
        get { return GetVital(1); }
        set { SetVital(value, 1); }
    }
    public Property Attack {
        get { return GetProperty(0); }
        set { SetProperty(value, 0); }
    }
    public Property AttackSpeed {
        get { return GetProperty(2); }
        set { SetProperty(value, 2); }
    }
    public Property Defend {
        get { return GetProperty(1); }
        set { SetProperty(value, 1); }
    }
    public Property Speed {
        get { return GetProperty(3); }
        set { SetProperty(value, 3); }
    }
    public Property Toughness {
        get { return GetProperty(4); }
        set { SetProperty(value, 4); }
    }
    public Property Agility {
        get { return GetProperty(5); }
        set { SetProperty(value, 5); }
    }
    public Property FantasyAttack {
        get { return GetProperty(6); }
        set { SetProperty(value, 6); }
    }
    public Property FantasyAttackSpeed {
        get { return GetProperty(7); }
        set { SetProperty(value, 7); }
    }
    #endregion

    void Awake()
    {
        if (characterData.id != 0) _id = characterData.id;
        var deltaPropertyLength = 0;
        if (m_deltaDifferentCharacter != 1) {deltaPropertyLength = 2;} //一般角色少继承2个能力值
        _name = string.Empty;
        _vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
        _property = new Property[Enum.GetValues(typeof(PropertyName)).Length - deltaPropertyLength];
        //创建各属性能力值列表
        SetupVitals();
        SetupPropertys();
        //子类附加
        SecondAwake();
    }

    protected virtual void SecondAwake()   //补充Setup 
    {
        characterData.ReadData();
        Speed.BasicValue = characterData.baseSpeed;
        #region 临时数据读取
        m_displayLayer = characterData.characterDisplayer;
        #endregion
    }

    protected virtual void Start()
    {
        m_animator = gameObject.GetComponentInChildren<Animator>();
        SetObjectZ();
    }

    protected virtual void Update()
    {
        SetObjectZ();
    }

    public void CaculateLevel()
    {

    }

    protected void SetObjectZ(Transform trans = null)
    {
        if (!trans) trans = gameObject.transform;
        var pos = trans.position;
        pos.z = pos.y * 0.001f;
        gameObject.transform.position = pos;
    }


    #region public 能力值的创建、读写与更新   SetupVitals GetVital SetVital ModifiedStatsUpdate
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

    public Vital GetVital(int index)
    {
        if (index < _vital.Length) return _vital[index];
        else { Debug.Log(_vital + "取得非法索引：" + index); return _vital[0]; }
    }
    public Property GetProperty(int index)
    {
        if (index < _property.Length) return _property[index];
        else { Debug.Log(_property + "取得非法索引：" + index); return _property[0]; }
    }

    protected void SetVital(Vital value, int index = -1)
    {
        if (index < 0) {
            for (int i = 0; i < _vital.Length; i++) {
                if (_vital[i].Type == value.Type) {
                    _vital[i] = value;return;
                }
            }
        }
        else if(_vital[index].Type == value.Type) {
            _vital[index] = value;return;
        }
        else { Debug.Log("Set失败，属性不匹配！"); }
    }
    protected void SetProperty(Property value,int index = -1)
    {
        if (index < 0) {
            for (int i = 0; i < _property.Length; i++) {
                if (_property[i].Type == value.Type) {
                    _property[i] = value; return;
                }
            }
        }
        else if (_property[index].Type == value.Type) {
            _property[index] = value; return;
        }
        else { Debug.Log("Set失败，属性不匹配！"); }
    }
    public void ModifiedStatsUpdate()
    {
        for (int i = 0; i < _vital.Length; i++) {
            _vital[i].Update();
        }
        for (int i = 0; i < _property.Length; i++) {
            _property[i].Update();
        }
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
            float maxDistanceDelta = Time.deltaTime * Speed.ModifiedValue / 100;
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
