using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorGroupConfigs", menuName = "ScriptableObjects/ColorGroup/ColorGroupConfigs", order = 2)]
public class ColorGroupConfigs : ScriptableObject
{
    [SerializeField] private List<ElementColorConfigs> _elementColorConfigs = new List<ElementColorConfigs>();

    public ElementColorConfigs GetRandomColorConfigs() =>
        _elementColorConfigs[Random.Range(0, _elementColorConfigs.Count)];

    public ElementColorConfigs GetColorConfigs(ECellsColorType cellsColorType) =>
        _elementColorConfigs.Find(configs => configs.CellsColorTypeType == cellsColorType);
}
