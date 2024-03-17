using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using System;




//!!! we can follow the Singleton Pattern , or any other way to make sure that we have only one instance of the GridCell in the scene . 

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

    // we should start work with the private attribut with get & set methods ,  
    // public Unit OccupantUnit
    // {
    //     get { return occupantUnit; }
    //     set { occupantUnit = value; } 
    // }

    public Terrain terrain;
    public int movecoast = 1;

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
        gm.OnCellSelection(this);
    }

    // Method to highlight the GridCell when the mouse hovers over it
    //!- if Any One Can Do This : Plaese Find A Better Way For Highlighting Things
    public void Highlight()
    {
        // Change the color of the GridCell to the highlighted color and the properties of the GridCell
        // rend.color = highlightedColor;
        rend.color = Color.green;
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