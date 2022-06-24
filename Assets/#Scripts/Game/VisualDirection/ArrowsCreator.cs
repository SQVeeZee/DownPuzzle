using Unity.Mathematics;
using UnityEngine;

public class ArrowsCreator : MonoBehaviour
{
    [SerializeField] private GameObject _arrowPrefab = null;

    public void CreateArrow(Vector2 position)
    {
        Instantiate(_arrowPrefab, position, quaternion.identity);
    }
}
