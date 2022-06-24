using UnityEngine;

[CreateAssetMenu(fileName = "ElementColorConfigs", menuName = "ScriptableObjects/ColorGroup/ElementColorConfigs", order = 2)]
public class ElementColorConfigs : ScriptableObject
{
    [SerializeField] private ECellsColorType cellsColorTypeType = default;
    [SerializeField] private Color _defaultColor = default;
    [SerializeField] private Color _activeColor = default;
    
    public ECellsColorType CellsColorTypeType => cellsColorTypeType;
    public Color DefaultColor => _defaultColor;
    public Color ActiveColor => _activeColor;
}
