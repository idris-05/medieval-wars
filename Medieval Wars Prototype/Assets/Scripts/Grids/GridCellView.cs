using UnityEngine;
using UnityEngine.UIElements;

public class GridCellView : MonoBehaviour
{
    private GridCell gridCell;
    public SpriteRenderer rend;

    public MapGrid mapGrid; // needed this for UI
    public UserInterfaceUtil userInterfaceUtil; // needed this for UI

    public bool  isHighlightedAsAttackble = false ; // we need it in get attackable .
    public bool  isHighlighted = false ; // I need this for UI ( RuleTiles )

    private int indexOfThePrefab;

    

    void Start()
    {
        gridCell = GetComponent<GridCell>();
        rend = GetComponent<SpriteRenderer>();
        mapGrid = FindObjectOfType<MapGrid>();
        userInterfaceUtil = FindObjectOfType<UserInterfaceUtil>();
    }



    void OnMouseDown()
    {
        GridCellController.Instance.OnCellSelection(gridCell);
        //! GETWALKABLE TILES YOU NEED TO CHECK IF A LOADED TRANSPORTER IS THERE ____AHMED AND RAYANE !!!!!!!!!!
    }


// !!! hilghiht hado nwello fi plasset la coleure nbdlo sprite .


    public void HighlightAsWalkable()
    {
        this.rend.sortingLayerID = SortingLayer.NameToID("FoundationWhenHighlighted");
        this.gridCell.occupantTerrain.spriteRenderer.sortingLayerID = SortingLayer.NameToID("Terrain&BuildingWhenHighlighted");

        AddBoundsGlowIfNeeded();



        // rend.color = Color.green;
        // gridCell.occupantTerrain.spriteRenderer.color = Color.green;
         // isWalkable = true; //! ???????????????????
    }

    public void HighlightAsDropable()
    {
        this.rend.sortingLayerID = SortingLayer.NameToID("FoundationWhenHighlighted");
        this.gridCell.occupantTerrain.spriteRenderer.sortingLayerID = SortingLayer.NameToID("Terrain&BuildingWhenHighlighted");

        AddBoundsGlowIfNeeded();

        //rend.color = Color.blue;
        //gridCell.occupantTerrain.spriteRenderer.color = Color.blue;
    }


    public void HighlightAsAttackable()
    {
        isHighlightedAsAttackble = true ;

        this.rend.sortingLayerID = SortingLayer.NameToID("FoundationWhenHighlighted");
        this.gridCell.occupantTerrain.spriteRenderer.sortingLayerID = SortingLayer.NameToID("Terrain&BuildingWhenHighlighted");

        AddBoundsGlowIfNeeded();


        // rend.color = Color.red;
        // gridCell.occupantTerrain.spriteRenderer.color = Color.red;
    }


    public void ResetHighlitedCell()
    {
        this.rend.sortingLayerID = SortingLayer.NameToID("Foundation");
        this.gridCell.occupantTerrain.spriteRenderer.sortingLayerID = SortingLayer.NameToID("Terrain&Building");



        gridCell.isWalkable = false ;
        isHighlightedAsAttackble = false ; //!! ??????
        // rend.color = Color.white;
        // gridCell.occupantTerrain.spriteRenderer.color = Color.white;
    }

    public void AddBoundsGlowIfNeeded()
    {
        GridCell upperGridCell = mapGrid.grid[this.gridCell.row - 1, this.gridCell.column];
        GridCell LowerGridCell = mapGrid.grid[this.gridCell.row + 1, this.gridCell.column];
        GridCell RightGridCell = mapGrid.grid[this.gridCell.row, this.gridCell.column + 1];
        GridCell LeftGridCell = mapGrid.grid[this.gridCell.row, this.gridCell.column - 1];

        if (upperGridCell.gridCellView.isHighlighted == false) UpperGlowLineHighlight();
        if (LowerGridCell.gridCellView.isHighlighted == false) LowerGlowLineHighlight();
        if (RightGridCell.gridCellView.isHighlighted == false) RightGlowLineHighlight();
        if (LeftGridCell.gridCellView.isHighlighted == false)  LeftGlowLineHighlight();


    }

    // 0 : bh
    // 1 : bv
    // 2 : gh
    // 3 : gv
    // 4 : rh
    // 5 : rv

    public void UpperGlowLineHighlight()
    {

        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.ATTACK) SetIndexOfThePrefab(4);
        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.MOVE) SetIndexOfThePrefab(2);
        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.DROP) SetIndexOfThePrefab(0);

        Vector3 position = new Vector3(this.gridCell.transform.position.x, this.gridCell.transform.position.y ,0);
        position.x = position.x + 0.5f;
        position.y = position.y + 1;

        Debug.Log(indexOfThePrefab);

        userInterfaceUtil.GlowLinesThatExistOnTheScene.Add(Instantiate(userInterfaceUtil.GlowLinesWhenHighlightedPrefabs[indexOfThePrefab], position , Quaternion.identity));
    }

    public void LowerGlowLineHighlight()
    {

        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.ATTACK) SetIndexOfThePrefab(4);
        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.MOVE) SetIndexOfThePrefab(2);
        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.DROP) SetIndexOfThePrefab(0);



        Vector3 position = new Vector3(this.gridCell.transform.position.x, this.gridCell.transform.position.y, 0);
        position.x = position.x + 0.5f;

        Debug.Log(indexOfThePrefab);


        userInterfaceUtil.GlowLinesThatExistOnTheScene.Add(Instantiate(userInterfaceUtil.GlowLinesWhenHighlightedPrefabs[indexOfThePrefab], position, Quaternion.identity));
    }

    public void RightGlowLineHighlight()
    {

        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.ATTACK) SetIndexOfThePrefab(5);
        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.MOVE) SetIndexOfThePrefab(3);
        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.DROP) SetIndexOfThePrefab(1);

        Vector3 position = new Vector3(this.gridCell.transform.position.x, this.gridCell.transform.position.y, 0);
        position.x = position.x + 1;
        position.y = position.y + 0.5f;

        Debug.Log(indexOfThePrefab);


        userInterfaceUtil.GlowLinesThatExistOnTheScene.Add(Instantiate(userInterfaceUtil.GlowLinesWhenHighlightedPrefabs[indexOfThePrefab], position, Quaternion.identity));

    }

    public void LeftGlowLineHighlight()
    {
        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.ATTACK) SetIndexOfThePrefab(5);
        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.MOVE) SetIndexOfThePrefab(3);
        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.DROP) SetIndexOfThePrefab(1);

        Vector3 position = new Vector3(this.gridCell.transform.position.x, this.gridCell.transform.position.y, 0);
        position.y = position.y + 0.5f;

        Debug.Log(indexOfThePrefab);

        userInterfaceUtil.GlowLinesThatExistOnTheScene.Add(Instantiate(userInterfaceUtil.GlowLinesWhenHighlightedPrefabs[indexOfThePrefab], position, Quaternion.identity));
    }

    private void SetIndexOfThePrefab(int index)
    {
        this.indexOfThePrefab = index;
    }


}