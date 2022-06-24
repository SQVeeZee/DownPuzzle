using UnityEngine;

public class ElementStyleModel
{
    private readonly ElementColorConfigs _elementColorConfigs;

    public ElementStyleModel(ColorGroupConfigs colorGroupConfigs)
    {
        _elementColorConfigs = colorGroupConfigs.GetRandomColorConfigs();
    }
    
    public ElementStyleModel(ElementColorConfigs elementColorConfigs)
    {
        _elementColorConfigs = elementColorConfigs;
    }    

    public Color GetActiveColor() => _elementColorConfigs.ActiveColor;
    public Color GetDefaultColor() => _elementColorConfigs.DefaultColor;
    public ECellsColorType GetColorType() => _elementColorConfigs.CellsColorTypeType;
}
