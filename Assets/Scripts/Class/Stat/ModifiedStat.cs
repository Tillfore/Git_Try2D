using System.Collections.Generic;

//修改后的能力值 主要为佩戴物、BUFF和DEBUFF修正，变动频繁
public class ModifiedStat : BaseStat {
    private List<BuffAndDebuff> _mods;  //属性修改列表
    private float _modValue;                     //储存准备修改的值；
    private int _expValue;

    public ModifiedStat()
    {
        _mods = new List<BuffAndDebuff>();
        _modValue = 0;
    }

    public void AddModifier(BuffAndDebuff mod)
    {
        _mods.Add(mod);
    }
    public void RemoveModifier(BuffAndDebuff mod)
    {
        _mods.Remove(mod);
    }

    protected virtual void CalculateModValue()
    {
        _modValue = 0;
        var modratio = 1f;
        var modfigure = 0f;
        if (_mods.Count > 0) {
            foreach (BuffAndDebuff mod in _mods) {
                modratio += mod.ratio;
                modfigure += mod.figure;
            }
        }
        _modValue += _modValue * modratio + modfigure;
    }
    public float ModifiedValue
    {
        get { return BasicValue + AdjustValue + _modValue; }
    }
    public float ModValue {     //包括BUFF和DEBUFF
        get { return _modValue; }
    }


    public void Update()  //不同于继承MonoBehavior的Update
    {
        CalculateModValue();
    }
}

public struct BuffAndDebuff {
    public float ratio;  //比例
    public float figure;     //增值
}