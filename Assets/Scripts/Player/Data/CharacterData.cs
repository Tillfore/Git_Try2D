using UnityEngine;
[System.Serializable] //序列化
public class CharacterData {

    public string characterName = "无名";
    public int level = 0;
    public int hP = 5;
    public int eP = 25;
    public float ePr = 10;
    public int attack;
    public int defend;
    public int speed = 100;
    public int toughness = 0;
    public GameObject handheld;
    public GameObject characterDisplayer;
    public GameObject characterSensor;
    public GameObject characterUI;

}
