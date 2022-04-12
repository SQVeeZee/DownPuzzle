using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer = null;

    public void SetupLineRenderer(Vector2[,] positions, GridBorderConfigs borderConfigs)
    {
        float widthMultiplier = borderConfigs.BorderSize;
        float displacement = widthMultiplier / 2f;

        _lineRenderer.positionCount = positions.Length;
        _lineRenderer.widthMultiplier = widthMultiplier;

        _lineRenderer.SetPosition(0, positions[0, 0] + new Vector2(-displacement, -displacement));
        _lineRenderer.SetPosition(1, positions[0, 1] + new Vector2(-displacement, displacement));
        _lineRenderer.SetPosition(2, positions[1, 1] + new Vector2(displacement, displacement));
        _lineRenderer.SetPosition(3, positions[1, 0] + new Vector2(displacement, -displacement));
    }
}
