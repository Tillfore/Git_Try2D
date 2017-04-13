public class BaseStat
{

    private int _baseValue;
    private int _buffValue;

    public BaseStat()
    {
        _baseValue = 0;
        _buffValue = 0;
    }

    #region public  Getters and Setters
    public int BaseValue
    {
        get { return _baseValue; }
        set { _baseValue = value; }
    }
    public int BuffValue
    {
        get { return _buffValue; }
        set { _buffValue = value; }
    }
    #endregion

    public void Promote()
    {
        _baseValue++;
    }
    public int AdjustedBaseValue
    {
        get { return _baseValue + _buffValue; }
    }
}