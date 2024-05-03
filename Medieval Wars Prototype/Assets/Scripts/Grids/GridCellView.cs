using UnityEngine;

public class GridCellView : MonoBehaviour
{
    private GridCell gridCell;
    public SpriteRenderer rend;

    public bool isHighlightedAsAttackble = false; // we need it in get attackable .


    void Start()
    {
        gridCell = GetComponent<GridCell>();
        rend = GetComponent<SpriteRenderer>();
    }



    public void OnMouseDown()
    {
        GridCellController.Instance.OnCellSelection(gridCell);
        //! GETWALKABLE TILES YOU NEED TO CHECK IF A LOADED TRANSPORTER IS THERE ____AHMED AND RAYANE !!!!!!!!!!
    }

    // // this methode is called every frame 
    void OnMouseOver()
    {
        MiniIntelController.Instance.HandleMINIIntel(gridCell);
    }


    // !!! hilghiht hado nwello fi plasset la coleure nbdlo sprite .


    public void HighlightAsWalkable()
    {
        rend.color = Color.green;
        gridCell.occupantTerrain.spriteRenderer.color = Color.green;

        // Assuming you have a reference to the SpriteRenderer component named 'spriteRenderer'
        Color color = Color.green; // Set color to white
        color.a = 210 / 255f; // Set alpha value to 210 out of 255
        rend.color = color; // Apply the modified color to the sprite renderer

        // isWalkable = true; //! ???????????????????
    }

    public void HighlightAsDropable()
    {
        rend.color = Color.blue;
    }


    public void HighlightAsAttackable()
    {
        isHighlightedAsAttackble = true;
        rend.color = Color.red;
    }


    public void ResetHighlitedCell()
    {
        gridCell.isWalkable = false;
        isHighlightedAsAttackble = false; //!! ??????
        rend.color = Color.white;
        gridCell.occupantTerrain.spriteRenderer.color = Color.white;
    }


}