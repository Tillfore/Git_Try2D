using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using System;

public class EnemyCharacter : BaseCharacter {

    #region 数据中转
    public EnemyCharacterData enemyCharacterData;//暂时代替数据库
    #endregion

    public EnemyCharacter()
    {
        m_deltaDifferentCharacter = 2; //向父类变量声明自己是PlayerCharacter
    }

    protected override void SecondAwake()
    {
        Speed.BasicValue = characterData.baseSpeed;
    }

    protected override void Start()
    {
        m_animator = gameObject.GetComponentInChildren<Animator>();
        SetObjectZ();
    }

}
