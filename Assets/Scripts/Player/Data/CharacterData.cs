using UnityEngine;

[System.Serializable] //序列化
public class CharacterData {
    //该类在初始化读取后不再调用，以替代数据库或表格
    [Tooltip("BaseCharacter")]
    public uint id;
    public string name = "无名";
    public int baseHP = 5;
    public int baseEP = 25;
    public int toughness = 0;
    public float speed = 100;
    #region 暂时把手持物、佩戴物的加成放这
    public int attack = 0;
    public float dps = 1;
    public int defend = 0;
    #endregion
    public GameObject handheld;
    public GameObject characterDisplayer;
    public GameObject characterUI;

    public void GetData()
    {
        CSV.GetInstance().LoadFile(Application.dataPath, "test.CSV");
        speed = CSV.GetInstance().GetFloat(1, 6);
    }
 
}
