using UnityEngine;

[CreateAssetMenu(fileName = "MovableElement", menuName = "ScriptableObjects/Grid element/MovableElement", order = 2)]
public class MovableElementConfigs : BaseElementConfigs
{
    [SerializeField] private float _moveSpeed = 2;

    public float MoveSpeed => _moveSpeed;
}
