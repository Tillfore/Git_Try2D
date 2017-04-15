//生命体力能力
public class Vital : ModifiedStat {
    private int _curValue;

    public Vital(int i)
    {
        _curValue = 0;
    }

    public int CurValue
    {
        get
        {
            if (_curValue > AdjustedBaseValue)
                _curValue = AdjustedBaseValue;
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