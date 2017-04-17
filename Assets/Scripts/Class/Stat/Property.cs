
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
    ATTACK=0,
    DEFEND = 1,
    ATTACK_SPEED =2,
    SPEED = 3,
    TOUGHNEES =4,
    AGILITY=5, //灵巧
    /*下面是PlayerCharacter专有*/
    FANTASY_ATTACK = 6,
    FANTASY_ATTACK_SPEED=7,
}
public struct NatureStruct {
    public float normal;
    public float fire;
}
