using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCharacter : BaseCharacter {

    #region 临时数据库
    public PlayerCharacterData playerCharacterData;//暂时代替数据库
    #endregion

    private Attribute[] _attribute;

    private int _hp;
    private int _ep;
    
    public override void SetupCharacterData()
    {
        _hp = 1;
        _ep = 0;
        m_deltaPropertyLength = 0;
        _attribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
        m_speed = characterData.speed / 100;
        m_displayLayer = characterData.characterDisplayer;
        SetupAttributes();
    }

    public override void Start()
    {
        /*this.speed = characterData.speed / 100;
        displayLayer = characterData.characterDisplayer;*/
        m_animator = gameObject.GetComponentInChildren<Animator>();
        SetObjectZ();
    }

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

}
