using UnityEngine;

public class GridCellView : MonoBehaviour
{
    private GridCell gridCell;
    public SpriteRenderer rend;


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
        rend.color = Color.red;
    }


    public void ResetHighlitedCell()
    {
        rend.color = Color.white;
    }


}