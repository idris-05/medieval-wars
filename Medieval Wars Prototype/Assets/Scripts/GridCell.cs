using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GridCell : MonoBehaviour
{


    public bool isHighlighted;

    public SpriteRenderer rend;
    public bool isWalkable;
    public int row;
    public int column;

    public Color highlightedColor;

    GameMaster gm; 



   public Unit occupantUnit ;

    public void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        
        gm = FindObjectOfType<GameMaster>();
    }

      private void OnMouseDown()
      {
        if ( gm.selectedUnit != null && isWalkable == false)
        {
            gm.selectedUnit = null;
            gm.ResetGridCells();
        }

       if ( isWalkable && gm.selectedUnit != null)
       {
           gm.selectedUnit.Move(this.row, this.column);
           gm.selectedUnit.hasMoved = true;
           gm.selectedUnit = null;
           gm.ResetGridCells();
       }

      }

    public void Highlight()
    {
        rend.color = highlightedColor;
        isWalkable = true;
        isHighlighted = true;
    }
    public void ResetGridCell()
    {
        rend.color = Color.white;
        isWalkable = false;
        isHighlighted = false;
    }

    /*  private void OnMouseDown()
      {
          if (isWalkable && gm.selectedUnit != null)
          {
              gm.selectedUnit.Move(this.transform.position);
          }
      } */



}