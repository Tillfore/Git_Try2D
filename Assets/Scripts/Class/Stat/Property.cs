
public class Property : ModifiedStat {

    private PropertyName _type;
    private NatureStruct _natureStruct;
    //能力值
    public Property(int i)
    {
        _type = (PropertyName) i ;
        _natureStruct.normal = 1;
        _natureStruct.fire = 0;
    }


    public PropertyName Type
    {
        get { return _type; }
    }
    public NatureStruct Nature {
        get { return _natureStruct; }
        set { _natureStruct = value; }
    }

}
public enum PropertyName {
    Attack=0,
    Defend = 1,
    AttackSpeed =2,
    Speed = 3,
    Toughness =4,
    Agility=5, //灵巧
    /*下面是PlayerCharacter专有*/
    FantasyAttack = 6,
    FantasyAttackSpeed=7,
}
public struct NatureStruct {
    public float normal;
    public float fire;
}
