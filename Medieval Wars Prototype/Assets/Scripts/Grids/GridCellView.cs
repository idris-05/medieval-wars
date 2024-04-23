using UnityEngine;

public class GridCellView : MonoBehaviour
{
    private GridCell gridCell;
    public SpriteRenderer rend;

   public bool  isHighlightedAsAttackble = false ; // we need it in get attackable .


    void Start()
    {
        gridCell = GetComponent<GridCell>();
        rend = GetComponent<SpriteRenderer>();
    }



    void OnMouseDown()
    {
        GridCellController.Instance.OnCellSelection(gridCell);
        //! GETWALKABLE TILES YOU NEED TO CHECK IF A LOADED TRANSPORTER IS THERE ____AHMED AND RAYANE !!!!!!!!!!
    }


// !!! hilghiht hado nwello fi plasset la coleure nbdlo sprite .


    public void HighlightAsWalkable()
    {
        rend.color = Color.green;
        // isWalkable = true; //! ???????????????????
    }

    public void HighlightAsDropable()
    {
        rend.color = Color.blue;
    }


    public void HighlightAsAttackable()
    {
        isHighlightedAsAttackble = true ;
        rend.color = Color.red;
    }


    public void ResetHighlitedCell()
    {
        gridCell.isWalkable = false ;
        rend.color = Color.white;
    }


}