using UnityEngine;

[System.Serializable] //序列化
public class PlayerCharacterData {

    [Tooltip("PlayerCharacter")]
    public bool gender = true; //true男 false女
    public int hpLevel = 0;
    public int epLevel = 0;
    public int freeExp = 0;
    public float agility = 0; //灵巧
    public int fan_attack = 0;
    public float fan_dps = 1;

}
