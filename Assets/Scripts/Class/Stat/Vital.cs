//生命体力能力
public class Vital : ModifiedStat {

    private VitalName _type;
    private float _curValue;

    public Vital(int i)
    {
        _type = (VitalName)i;
        _curValue = 0;
    }

    public VitalName Type {
        get { return _type; }
    }

    public float CurValue
    {
        get
        {
            if (_curValue > Value)
                _curValue = Value;
            return _curValue;
        }
        set { _curValue = value; }
    }
}

public enum VitalName
{
    Health,
    Energy,
}