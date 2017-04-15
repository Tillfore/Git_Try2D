using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Property : ModifiedStat {

    protected PropertyName _type;
    protected int _value;
    //能力值
    public Property(int i)
    {
        _type = (PropertyName) i ;
        _value = 0;
    }

    public PropertyName Type
    {
        get { return _type; }
    }
    public int Value
    {
        get { return _value; }
        set { _value = value; }
    }
}
public enum PropertyName {
    Attack,
    AttackSpeed,
    Defend,
    Toughness,
    Speed,
    /*下面是PlayerCharacter专有*/
    Agility, //灵巧
    FantasyAttack,
    FantasyAttackSpeed,
}
