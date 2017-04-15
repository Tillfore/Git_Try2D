using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCharacter : BaseCharacter {

    #region 临时数据库
    public PlayerCharacterData playerCharacterData;//暂时代替数据库
    #endregion

    private Attribute[] _attribute;

    public PlayerCharacter()
    {
        m_deltaDifferentCharacter = 1; //向父类变量声明自己是PlayerCharacter
    }

    #region public 封装属性的读写
    public Attribute Strong {
        get { return GetAttribute(0); }
        set { SetAttribute(value,0); }
    }
    public Attribute Intelligence {
        get { return GetAttribute(1); }
        set { SetAttribute(value, 1); }
    }
    public Attribute Dexterity {
        get { return GetAttribute(2); }
        set { SetAttribute(value, 2); }
    }
    public Attribute Lucky {
        get { return GetAttribute(3); }
        set { SetAttribute(value, 3); }
    }
    #endregion


    public override void SecondAwake()
    {
        _attribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
        m_displayLayer = characterData.characterDisplayer;
        characterData.GetData();
        Speed.BasicValue = characterData.speed;
        SetupAttributes();
    }

    public override void Start()
    {
        m_animator = gameObject.GetComponentInChildren<Animator>();
        SetObjectZ();
    }

    #region 属性值的创建和读写
    private void SetupAttributes()
    {
        for (int i = 0; i < _attribute.Length; i++) {
            _attribute[i] = new Attribute(i);
        }
    }
    public Attribute GetAttribute(int index)
    {
        return _attribute[index];
    }
    protected void SetAttribute(Attribute value, int index = -1)
    {
        if (index < 0) {
            for (int i = 0; i < _attribute.Length; i++) {
                if (_attribute[i].Type == value.Type) {
                    _attribute[i] = value; return;
                }
            }
        }
        else if (_attribute[index].Type == value.Type) {
            _attribute[index] = value; return;
        }
        else { Debug.Log("Set失败，属性不匹配！"); }
    }
    #endregion

}
