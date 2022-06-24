using UnityEngine;

public class ElementsStylePresenter : MonoBehaviour
{
    [SerializeField] private ElementStyleController _styleController = null;

    [Header("Configs")] 
    [SerializeField] private ColorGroupConfigs _colorGroupConfigs = null;
    
    private ElementStyleModel _elementStyleModel = null;

    public void Initialize()
    {
        _elementStyleModel = new ElementStyleModel(_colorGroupConfigs);

        _styleController.SetElementColor(_elementStyleModel.GetDefaultColor());
    }

    public void SetSelectionState(bool state) => _styleController.SetColorDependsOnSelection(_elementStyleModel, state);

    public ECellsColorType GetColorType => _elementStyleModel.GetColorType();
}
