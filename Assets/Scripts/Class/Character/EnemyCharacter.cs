using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCharacter : BaseCharacter {

    #region 临时数据库
    public EnemyCharacterData enemyCharacterData;//暂时代替数据库
    #endregion

    protected override void SecondAwake()
    {
        m_displayLayer = characterData.characterDisplayer;
    }

    protected override void Start()
    {
        m_animator = gameObject.GetComponentInChildren<Animator>();
        SetObjectZ();
    }

}
