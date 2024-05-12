using UnityEngine;
using UnityEngine.UIElements;

public class GridCellView : MonoBehaviour
{
    public GridCell gridCell;
    public SpriteRenderer rend;

    public bool isHighlightedAsAttackble = false; // we need it in get attackable .
    public bool isHighlighted = false; // I need this for UI ( RuleTiles )

    public int indexOfThePrefab;

    public void OnMouseDown()
    {
        GridCellController.Instance.OnCellSelection(gridCell);
        //! GETWALKABLE TILES YOU NEED TO CHECK IF A LOADED TRANSPORTER IS THERE ____AHMED AND RAYANE !!!!!!!!!!
    }


    // // this methode is called every frame 
    void OnMouseOver()
    {
        MiniIntelController.Instance.HandleMINIIntel(gridCell);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            InfoCardController.Instance.UpdateTerrainBIGIntel(gridCell.occupantTerrain, Input.mousePosition);
        }
    }




    // !!! hilghiht hado nwello fi plasset la coleure nbdlo sprite .


    public void HighlightAsWalkable()
    {


        this.rend.sortingLayerID = SortingLayer.NameToID("FoundationWhenHighlighted");
        this.gridCell.occupantTerrain.spriteRenderer.sortingLayerID = SortingLayer.NameToID("TerrainBuildingWhenHighlighted");


        AddBoundsGlowIfNeeded(1);



        // rend.color = Color.green;
        // gridCell.occupantTerrain.spriteRenderer.color = Color.green;
        // isWalkable = true; //! ???????????????????
    }

    public void HighlightAsDropable()
    {
        this.rend.sortingLayerID = SortingLayer.NameToID("FoundationWhenHighlighted");
        this.gridCell.occupantTerrain.spriteRenderer.sortingLayerID = SortingLayer.NameToID("TerrainBuildingWhenHighlighted");

        AddBoundsGlowIfNeeded(0);


        //rend.color = Color.blue;
        //gridCell.occupantTerrain.spriteRenderer.color = Color.blue;
    }


    public void HighlightAsAttackable()
    {
        isHighlightedAsAttackble = true;

        this.rend.sortingLayerID = SortingLayer.NameToID("FoundationWhenHighlighted");
        this.gridCell.occupantTerrain.spriteRenderer.sortingLayerID = SortingLayer.NameToID("TerrainBuildingWhenHighlighted");

        AddBoundsGlowIfNeeded(2);


        // rend.color = Color.red;
        // gridCell.occupantTerrain.spriteRenderer.color = Color.red;
    }


    public void ResetHighlitedCell()
    {
        this.rend.sortingLayerID = SortingLayer.NameToID("Foundation");
        this.gridCell.occupantTerrain.spriteRenderer.sortingLayerID = SortingLayer.NameToID("TerrainBuilding");



        gridCell.isWalkable = false;
        isHighlightedAsAttackble = false; //!! ??????
        // rend.color = Color.white;
        // gridCell.occupantTerrain.spriteRenderer.color = Color.white;
    }

    // which highlight 
    // 0 : drop
    // 1 : walkable
    // 2 : attackable


    public void AddBoundsGlowIfNeeded(int WhichHighlight)
    {
        if (this.gridCell.row - 1 >= 0)
        {
            GridCell upperGridCell = MapGrid.Instance.grid[this.gridCell.row - 1, this.gridCell.column];
            if (upperGridCell.gridCellView.isHighlighted == false) UpperGlowLineHighlight(WhichHighlight);
        }

        if (this.gridCell.row + 1 <= MapGrid.Instance.Rows - 1)
        {
            GridCell LowerGridCell = MapGrid.Instance.grid[this.gridCell.row + 1, this.gridCell.column];
            if (LowerGridCell.gridCellView.isHighlighted == false) LowerGlowLineHighlight(WhichHighlight);
        }

        if (this.gridCell.column + 1 <= MapGrid.Instance.Columns - 1)
        {
            GridCell RightGridCell = MapGrid.Instance.grid[this.gridCell.row, this.gridCell.column + 1];
            if (RightGridCell.gridCellView.isHighlighted == false) RightGlowLineHighlight(WhichHighlight);
        }

        if (this.gridCell.column - 1 >= 0)
        {
            GridCell LeftGridCell = MapGrid.Instance.grid[this.gridCell.row, this.gridCell.column - 1];
            if (LeftGridCell.gridCellView.isHighlighted == false) LeftGlowLineHighlight(WhichHighlight);

        }

    }

    // 0 : bh
    // 1 : bv
    // 2 : gh
    // 3 : gv
    // 4 : rh
    // 5 : rv

    // which highlight 
    // 0 : drop
    // 1 : walkable
    // 2 : attackable

    public void UpperGlowLineHighlight(int WhichHighlight)
    {

        if (WhichHighlight == 2) { Debug.Log("4"); SetIndexOfThePrefab(4); };
        if (WhichHighlight == 1) { Debug.Log("2"); SetIndexOfThePrefab(2); }
        if (WhichHighlight == 0) { Debug.Log("0"); SetIndexOfThePrefab(0); }



        Vector3 position = new Vector3(this.gridCell.transform.position.x, this.gridCell.transform.position.y, 0);
        position.y = position.y + 0.5f;


        UserInterfaceUtil.Instance.GlowLinesThatExistOnTheScene.Add(Instantiate(UserInterfaceUtil.Instance.GlowLinesWhenHighlightedPrefabs[indexOfThePrefab], position, Quaternion.identity));
    }

    public void LowerGlowLineHighlight(int WhichHighlight)
    {

        if (WhichHighlight == 2) { Debug.Log("4"); SetIndexOfThePrefab(4); };
        if (WhichHighlight == 1) { Debug.Log("2"); SetIndexOfThePrefab(2); }
        if (WhichHighlight == 0) { Debug.Log("0"); SetIndexOfThePrefab(0); }



        Vector3 position = new Vector3(this.gridCell.transform.position.x, this.gridCell.transform.position.y, 0);
        position.y = position.y - 0.5f;


        UserInterfaceUtil.Instance.GlowLinesThatExistOnTheScene.Add(Instantiate(UserInterfaceUtil.Instance.GlowLinesWhenHighlightedPrefabs[indexOfThePrefab], position, Quaternion.identity));
    }

    public void RightGlowLineHighlight(int WhichHighlight)
    {

        if (WhichHighlight == 2) { Debug.Log("5"); SetIndexOfThePrefab(5); };
        if (WhichHighlight == 1) { Debug.Log("3"); SetIndexOfThePrefab(3); };
        if (WhichHighlight == 0) { Debug.Log("1"); SetIndexOfThePrefab(1); };

        Vector3 position = new Vector3(this.gridCell.transform.position.x, this.gridCell.transform.position.y, 0);
        position.x = position.x + 0.5f;



        UserInterfaceUtil.Instance.GlowLinesThatExistOnTheScene.Add(Instantiate(UserInterfaceUtil.Instance.GlowLinesWhenHighlightedPrefabs[indexOfThePrefab], position, Quaternion.identity));

    }

    public void LeftGlowLineHighlight(int WhichHighlight)
    {
        if (WhichHighlight == 2) { Debug.Log("5"); SetIndexOfThePrefab(5); };
        if (WhichHighlight == 1) { Debug.Log("3"); SetIndexOfThePrefab(3); };
        if (WhichHighlight == 0) { Debug.Log("1"); SetIndexOfThePrefab(1); };
        Vector3 position = new Vector3(this.gridCell.transform.position.x, this.gridCell.transform.position.y, 0);
        position.x = position.x - 0.5f;


        UserInterfaceUtil.Instance.GlowLinesThatExistOnTheScene.Add(Instantiate(UserInterfaceUtil.Instance.GlowLinesWhenHighlightedPrefabs[indexOfThePrefab], position, Quaternion.identity));
    }

    private void SetIndexOfThePrefab(int index)
    {
        this.indexOfThePrefab = index;
    }


}