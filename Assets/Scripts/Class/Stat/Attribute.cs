public class Attribute : BaseStat {

    private AttributeName _type;
    //属性值
    public Attribute(int i)
    {
        _type = (AttributeName)i;
    }

    public AttributeName Type {
        get { return _type; }
    }
}

public enum AttributeName {
    STRONG,
    INTELLIGENCE,
    DEXTERITY,
    LUCK,
}
