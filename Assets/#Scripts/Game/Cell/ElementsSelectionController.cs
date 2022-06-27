using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ElementsSelectionController
{
    private readonly ClosestCellsController _closestCellsController;
    
    public event Action onDisablingCells = default;

    private Stack<GridCell> _selectedElements = new Stack<GridCell>();
    private int SelectedCount => _selectedElements.Count;

    private int _minGroupSizeForDisabling = 2;
    private ScoreSystem _scoreSystem = null;
    
    private ElementsSelectionController()
    {
        _closestCellsController = new ClosestCellsController();

        _closestCellsController.onSelectElements += InitializeSelectedElements;
    }

    public ElementsSelectionController(ScoreSystem scoreSystem): this()
    {
        _scoreSystem = scoreSystem;
    }

    ~ElementsSelectionController()
    {
        _closestCellsController.onSelectElements -= InitializeSelectedElements;
    }

    private void InitializeSelectedElements(Stack<GridCell> selectedElements) => 
        _selectedElements = selectedElements;

    public void DefineRuleBasedOnClickedCell(GridCell gridCell)
    {
        if (gridCell == null) return;

        if (SelectedCount == 0)
        {
            SelectCells(gridCell);

            return;
        }
            
        if(gridCell.IsSelected && SelectedCount >= _minGroupSizeForDisabling)
        {
            DisablingCells();
            
            onDisablingCells?.Invoke();
        }
        else
        {
            UpdateSelection(gridCell);
        }
    }

    private void UpdateSelection(GridCell gridCell)
    {
        UnSelectCells();

        SelectCells(gridCell);
    }
    
    private void SelectCells(GridCell gridCell)
    {
        if (gridCell.CellState != ECellState.FILL) return;

        _closestCellsController.SetClickedCell(gridCell);
        
        SetElementsSelectState(true);
    }
    
    private void UnSelectCells()
    {
        SetElementsSelectState(false);
        
        ClearSelectedGroup();
    }

    private void DisablingCells()
    {
        foreach(var selectedElement in _selectedElements)
        {
            var element = selectedElement.Element;
            
            if (element is MovableElement movableElement)
            {
                movableElement.DestroyCell();
            }
        }

        _scoreSystem.UpdateScore(SelectedCount);
        
        ClearSelectedGroup();
    }
    
    private void SetElementsSelectState(bool state)
    {
        foreach (var selectedElement in _selectedElements)
        {
            if (selectedElement.Element is MovableElement movableCell)
            {
                selectedElement.IsSelected = state;
                
                if (state)
                {
                    movableCell.Select();
                }
                else
                {
                    movableCell.UnSelect();
                }
            }
        }
    }
    
    private void ClearSelectedGroup() => _selectedElements.Clear();
}
