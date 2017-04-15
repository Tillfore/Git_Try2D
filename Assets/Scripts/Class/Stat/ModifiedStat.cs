using System.Collections.Generic;

//修改后的能力值
public class ModifiedStat : BaseStat {
    private List<ModifyingAttribute> _mods;  //属性修改列表
    private int _modValue;                     //储存准备修改的值；
    private int _expValue;

    public ModifiedStat()
    {
        _mods = new List<ModifyingAttribute>();
        _modValue = 0;
    }

    public void AddModifier(ModifyingAttribute mod)
    {
        _mods.Add(mod);
    }

    protected virtual void CalculateModValue()
    {
        _modValue = 0;
        if (_mods.Count > 0) {
            foreach (ModifyingAttribute att in _mods) {
                _modValue += (int)(att.attribute.AdjustedBaseValue * att.ratio);
            }
        }
    }
    public int AdjustBaseValue
    {
        get { return BaseValue + BuffValue + _modValue; }
    }
    public void Update()  //不同于继承MonoBehavior的Update
    {
        CalculateModValue();
    }
}

public struct ModifyingAttribute {
    public Attribute attribute;
    public float ratio; //比例

}