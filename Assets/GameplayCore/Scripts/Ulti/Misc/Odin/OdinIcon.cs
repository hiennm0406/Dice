using System;


public class IconLabelAttribute : Attribute
{
    public string AssetPath { get; private set; }
    public IconLabelAttribute(string assetPath)
    {
        this.AssetPath = assetPath;
    }

}
