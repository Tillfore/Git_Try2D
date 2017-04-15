using UnityEngine;

[System.Serializable] //序列化
public class PlayerCharacterData {
    //该类在初始化读取后不再调用，以替代数据库或表格
    [Tooltip("PlayerCharacter")]
    public bool gender = true; //true男 false女
    //public int hpLevel = 0;
    //public int epLevel = 0;
    //public float freeExp = 0;
    public float agility = 0; //灵巧
    public float fan_attack = 0;
    public float fan_dps = 1;

}
