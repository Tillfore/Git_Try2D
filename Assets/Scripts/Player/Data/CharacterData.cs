using UnityEngine;

[System.Serializable] //序列化
public class CharacterData {

    [Tooltip("BaseCharacter")]
    public uint id;
    public string name = "无名";
    public int baseHP = 5;
    public int baseEP = 25;
    public float ePr = 10;
    public int attack =0;
    public float dps =1;
    public int defend =0;
    public int speed = 100;
    public int toughness = 0;
    public GameObject handheld;
    public GameObject characterDisplayer;
    public GameObject characterUI;

}
