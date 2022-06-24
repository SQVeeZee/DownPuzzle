using UnityEngine;

public class ElementStyleController : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer _spriteRenderer = null;
    
    [SerializeField] 
    private GameObject _selectCellIdentifier = null;

    public void Awake()
    {
        SetIdentifierState(false);
    }
    
    public void SetColorDependsOnSelection(ElementStyleModel styleModel, bool state)
    {
        SetElementColor(state ? styleModel.GetActiveColor() : styleModel.GetDefaultColor());

        SetIdentifierState(state);
    }

    public void SetElementColor(Color color)
    {
        _spriteRenderer.color = color;
    }
    
    private void SetIdentifierState(bool state) => _selectCellIdentifier.SetActive(state);
}
