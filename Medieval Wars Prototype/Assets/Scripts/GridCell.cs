using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public bool isHighlighted;
    public SpriteRenderer rend;  
    public bool isWalkable; // can the selected unit move to this cell 
    public int row;
    public int column;
    public Color highlightedColor;
    GameMaster gm;
    public Unit occupantUnit;

    void Start()
    {
        // Get the SpriteRenderer component of the GridCell from the scene
        rend = GetComponent<SpriteRenderer>();
        // Get the GameMaster component from the scene
        gm = FindObjectOfType<GameMaster>();
    }

    // Method to highlight the GridCell when the mouse hovers over it
    void OnMouseDown()
    {
        // If the GridCell is walkable and a unit is selected 
        if (gm.selectedUnit != null && isWalkable == false)
        {
            gm.selectedUnit.selected = false;
            gm.selectedUnit = null;
            // Reset the grid cells to their original state
            gm.ResetGridCells();
        }

        if (isWalkable && gm.selectedUnit != null)
        {
            // Move the selected unit to the GridCell
            gm.selectedUnit.Move(this.row, this.column);
            gm.selectedUnit.row = this.row;
            gm.selectedUnit.col = this.column;
            gm.selectedUnit.occupiedCell = this;
            // Set the selected unit's hasMoved property to true to prevent it from moving again in the same turn 
            gm.selectedUnit.hasMoved = true;
            // Unselect the unit 
            gm.selectedUnit.selected = false;
            gm.selectedUnit = null;
            // Reset the grid cells to their original state 
            gm.ResetGridCells();
        }

    }

    // Method to highlight the GridCell when the mouse hovers over it
    public void Highlight()
    {
        // Change the color of the GridCell to the highlighted color and the properties of the GridCell
        rend.color = highlightedColor;
        isHighlighted = true;
        isWalkable = true;
    }

    // Method to reset the GridCell to its original state 
    public void ResetGridCell()
    {
        // reset the color ( the color by default ) and the properties of the GridCell
        rend.color = Color.white;
        isWalkable = false;
        isHighlighted = false;
    }

}