using UnityEngine;
using System.Collections.Generic;

[System.Serializable] //序列化
public class CharacterData {
    //该类在初始化读取后不再调用，以替代数据库或表格
    [Tooltip("BaseCharacter")]
    public int id = 0;
    public string name = "无名";
    public string information;
    public int baseHP = 5;
    public int baseEP = 50;
    public float baseSpeed = 100;
    public float baseToughness = 0;
    #region 暂时把手持物、佩戴物的加成放这
    public int handheldID = 0;
    public List<int> wearsID = new List<int>{0, 0, 0, 0};
    public string spriteAnimator;
    public string spriteIcon;
    #endregion
    public GameObject handheld;
    public GameObject characterDisplayer;
    public GameObject characterUI;

    public void ReadData(int listnum =0)
    {
        if (id != 0) {
            name = CSV.GetInstance().GetString(id, 1);
            information = CSV.GetInstance().GetString(id, 3);
            baseHP = (int)CSV.GetInstance().GetFloat(id, 4);
            baseEP = (int)CSV.GetInstance().GetFloat(id, 5);
            baseSpeed = CSV.GetInstance().GetFloat(id, 6);
            baseToughness = CSV.GetInstance().GetFloat(id, 7);
            handheldID = (int)CSV.GetInstance().GetFloat(id, 8);
            wearsID.Clear();
            int wearscount = (int)CSV.GetInstance().GetFloat(id, 9);
            for (int i = 0; i < wearscount; i++) {
                wearsID.Add((int)CSV.GetInstance().GetFloat(id, 10 + i));
            }
            spriteAnimator = CSV.GetInstance().GetString(id, 10 + wearscount);
            spriteIcon = CSV.GetInstance().GetString(id, 11 + wearscount);
        }
    }
}
