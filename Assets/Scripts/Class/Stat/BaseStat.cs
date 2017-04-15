public class BaseStat
{

    private float _basicValue; //基础能力值，一般只在初始化读取及升级加点时变动，白字
    private float _adjustValue; //手持物等调整后的能力值，有相关操作后才变动，绿字
    private bool _isPermitted;

    public BaseStat()
    {
        _basicValue = 0;
        _isPermitted = true;
        _adjustValue = 0;
    }

    #region public  Getters and Setters
    public float BasicValue
    {
        get { return _basicValue; }
        set {
            if (_isPermitted)
                _basicValue = value;
                _isPermitted = false;
        }
    }
    public float AdjustValue
    {
        get { return _adjustValue; }
        set { _adjustValue = value; }
    }
    #endregion

    protected void Permit()
    {
        _isPermitted = true;
    }
    public float Value
    {
        get { return _basicValue + _adjustValue; }
    }
}