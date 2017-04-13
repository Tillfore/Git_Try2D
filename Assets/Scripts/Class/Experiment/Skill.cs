
public class Skill : ModifiedStat {
    private bool _known;

    public Skill()
    {
        _known = false;
    }

    public bool Known
    {
        get { return _known; }
        set { _known = value; }
    }
}

public enum SkillName
{
    Standard_Attack,
    Time_Fantasy
}