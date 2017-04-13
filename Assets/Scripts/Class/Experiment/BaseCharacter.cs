using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;       //添加对Enum class的使用

public class BaseCharacter : MonoBehaviour {

    private string _name;
    private uint _level;
    private uint _freeExp;

    private Attribute[] _primaryAttribute;
    private Vital[] _vital;
    private Skill[] _skill;

    public void Awake()
    {
        _name = string.Empty;
        _level = 0;
        _freeExp = 0;
        _primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
        _vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
        _skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];
        SetupPrimaryAttributes();
        SetupVitals();
        SetupSkills();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void AddExp(int exp)
    {
        if (exp >= 0) _freeExp += (uint)exp;
        else _freeExp -= (uint)exp;
        CaculateLevel();
    }

    public void CaculateLevel()
    {

    }

    #region public 存取私有变量
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public uint Level
    {
        get { return _level; }
        set { _level = value; }
    }
    public uint FreeExp
    {
        get { return _freeExp; }
        set { _freeExp = value; }
    }
    #endregion

    #region private 设置属性、生命体力、技能
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
    private void SetupSkills()
    {
        for (int i = 0; i < _skill.Length; i++) {
            _skill[i] = new Skill();
        }
    }
    #endregion

    #region public 获取属性、生命体力、技能值
    public Attribute GetPrimaryAttribute(int index)
    {
        return _primaryAttribute[index];
    }
    public Vital GetVital(int index)
    {
        return _vital[index];
    }
    public Skill GetSkill(int index)
    {
        return _skill[index];
    }
    #endregion

    #region private 安置属性、生命体力、技能值
    private void SetupVitalModifiers()
    {
        //health
        /*ModifyingAttribute health = new ModifyingAttribute();
        health.attribute = GetPrimaryAttribute((int)AttributeName.Strong);
        health.ratio = 10f; //每点力量换1.1生命*/
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
    private void SetupSkillModifiers()
    {
        
    }
    #endregion
    public void StatUpdate()
    {
        for(int i = 0; i < _vital.Length; i++) {
            _vital[i].Update();
        }
        for(int i = 0; i < _skill.Length; i++) {
            _skill[i].Update();
        }
    }

}
