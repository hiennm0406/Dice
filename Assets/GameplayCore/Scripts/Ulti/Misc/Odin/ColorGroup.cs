using Sirenix.OdinInspector;

public class ColorFoldoutGroupAttribute : PropertyGroupAttribute
{
    public string red, green, blue;

    public ColorFoldoutGroupAttribute(string group, float order = 0) : base(group, order)
    {

    }

    public ColorFoldoutGroupAttribute(string group, string r, string g, string b, float a = 1f) : base(group)
    {
        this.red = r;
        this.green = g;
        this.blue = b;
    }
}
