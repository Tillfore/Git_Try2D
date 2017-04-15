using UnityEngine;

[System.Serializable] //序列化
public class EnemyCharacterData {
    //该类在初始化读取后不再调用，以替代数据库或表格
    [Tooltip("PlayerCharacter")]
    public int enemyLevel = 0;

}
