using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


public class GameMaster : MonoBehaviour
{
    //!! had les lien bzzaf w y3yo , lazem nl9aw kifach nminiziwhom .
    public HandelPlayerInput handelPlayerInput;
    public MapGrid mapGrid;
    public AttackButton attackButton;
    public GetWalkableTiles getWalkableTiles;
    public AttackSystem attackSystem;

    // public GridCell GridCellPrefab;
    public Unit Infantry1Prefab; // test
    public Unit Infantry2Prefab; // test
    public Unit selectedUnit;   // pour le movement , pour l'instant 
    public Terrain TerrainGrassPrefab;
    public int playerTurn = 1;
    public Unit SelectedUnitFromAttacker; // hadi lazem tetna7a 


    // This method is called when the object is first enabled in the scene.
    void Start()
    {
        // Find the AttackButton GameObject and get its AttackButton component
        AttackButton attackButton = FindObjectOfType<AttackButton>();
        // Subscribe to the event in AttackButton and define the attack logic
        // listeners are methods that will be called when the event is triggered
        attackButton.onAttackButtonClickEvent.AddListener(OnAttackButtonClick);


        handelPlayerInput = FindObjectOfType<HandelPlayerInput>();
        getWalkableTiles = FindObjectOfType<GetWalkableTiles>();
        attackSystem = FindObjectOfType<AttackSystem>();

        mapGrid.CalculateMapGridSize();
        mapGrid.InitialiseMapGridCells();

        SpawnUnit(1, 5, 5, Infantry1Prefab); // test 
        SpawnUnit(2, 8, 8, Infantry2Prefab);
    }

    // This method is called when the attack button is clicked EVENET IS TRIGGERED
    private void OnAttackButtonClick()
    {
        // implement logic here to select one enemy inordre to attack it .(loop hadi ghir doka brk lel test)
        foreach (Unit enemy in selectedUnit.enemiesInRange)
        {
            ExecuteUnitAction(enemy, selectedUnit, HandelPlayerInput.Action.Attack);
        }
    }

    // this function is used to spawn a unit on the map
    private void SpawnUnit(int playerNumber, int row, int column, Unit unitPrefab)
    {

        // instantiate the unit at the specified position , the position is calculated based on the row and column of the grid cell 
        Unit unit = Instantiate(unitPrefab, new Vector3(-MapGrid.Horizontal + column + 0.5f, MapGrid.Vertical - row - 0.5f), Quaternion.identity);

        // adjust the size of the unit sprite to fit the grid cell size , this function is defined in GameUtil.cs    
        unit.gameObject.AdjustSpriteSize();

        // set the occupantUnit of the grid cell to the unit 
        mapGrid.grid[row, column].occupantUnit = unit;

        // set the occupiedCell of the unit to the grid cell
        unit.occupiedCell = mapGrid.grid[row, column];

        // set the (row,column) of the unit to the (row,column) of the grid cell
        unit.row = row;
        unit.col = column;

        unit.playerNumber = playerNumber;

    }


    void Update()
    {
        // je pense lokan nl9aw 3fssa w7do5ra ndetectiw biha end turn , 5ir .
        // deja kima advance wars , r7 tkon button flMENU , tclicker 3liha .
        CheckEndTurnInput();
    }


    // this function is used to check if the player has pressed the space key to end his turn
    private void CheckEndTurnInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
    }

    // this function is used to end the turn of the current player
    private void EndTurn()
    {
        SelectedUnitFromAttacker = null;
        selectedUnit = null;

        SwitchPlayeTurn();

        // ga3 les methods hadi t3 reset lazem n3awdo nchofhom mli7  psq 5tra 3la 5tra tssra 3fssa malazemch ! (manich sure)
        ResetGridCells();  // reset the grid cells to their original state (white color) and isWalkable = false for all cells
        ResetUnitsPropritiesInEndTurn();

    }

    // this function is used to switch the turn of the players    
    public void SwitchPlayeTurn()
    {
        playerTurn = (playerTurn == 1) ? 2 : 1;  // if playerTurn == 1, then playerTurn = 2, else playerTurn = 1
    }


    // this function is used to reset the grid cells to their original state (white color) and isWalkable = false for all cells 
    public void ResetGridCells()
    {
        foreach (GridCell gridCell in FindObjectsOfType<GridCell>())
        {
            gridCell.ResetGridCell();
        }
    }

    // this function is used to reset the proprities of the units in the end of the turn
    public void ResetUnitsPropritiesInEndTurn()
    {
        foreach (Unit unit in FindObjectsOfType<Unit>()) // FindObjectsOfType<Unit>() returns an array of all the units in the scene
        {
            unit.ResetUnitPropritiesInEndTurn(); // others proprities
        }
    }


    // in this function we handle the selection of a unit by the player
    public void OnUnitSelection(Unit unit, MouseButton mouseButton)
    {
        HandelPlayerInput.Action action = handelPlayerInput.DetermineUnitAction(unit, playerTurn, mouseButton);
        ExecuteUnitAction(unit, SelectedUnitFromAttacker, action);
    }

    public void ExecuteUnitAction(Unit unit, Unit SelectedUnitFromAttacker, HandelPlayerInput.Action action)
    {
        switch (action)
        {
            case HandelPlayerInput.Action.SelectUnit:
                SelectUnit(unit);
                break;
            case HandelPlayerInput.Action.UnselectUnit:
                DeselectUnit(unit);
                break;
            case HandelPlayerInput.Action.HighlightEnemyForUnit:
                getWalkableTiles.highlightAttackableCells(unit);
                break;
            case HandelPlayerInput.Action.UnHighlightEnemyForUnit:
                getWalkableTiles.unHighlightAttackableCells(unit);
                break;

            case HandelPlayerInput.Action.DisplayMenu:
                Debug.Log("Display Menu"); // menu or information ..., (lmohim , kima advance wars)
                break;

            case HandelPlayerInput.Action.Attack:
                AttackUnit(SelectedUnitFromAttacker, unit);
                // attackButton.DesableAttackButton(); // disable the attack button after the attack 

                break;
            case HandelPlayerInput.Action.None:
                break;
            default:
                break;
        }
    }



    // select , unselect , attack  methods , are not conmplete yet (details 5faf berk machi 7aja rahom ymchiw normal wkolch) .


    // Method to select a unit and display its movement and attack range
    public void SelectUnit(Unit unit)
    {
        SelectedUnitFromAttacker = unit;
        selectedUnit = unit;
        unit.IsSelected = true;
        getWalkableTiles.getWalkableTilesMethod(unit);
        // unit.GetEnemies();
    }

    // Method to deselect a unit
    private void DeselectUnit(Unit unit)
    {
        SelectedUnitFromAttacker = null;
        selectedUnit = null;
        unit.IsSelected = false;
        unit.ResetWalkableGridCells();
        // unit.ResetHighlightedEnemyInRange();
    }


    // Method to attack a unit
    private void AttackUnit(Unit attacker, Unit defender)
    {
        attackSystem.Attack(attacker, defender);   // caculate the damage and apply it to the defender
        attacker.UpdateAttributsAfterAttack();     // update the attributes of the attacker after the attack
        DeselectUnit(attacker);                    // Deselect the attacker after the attack
        DeselectUnit(defender);                    // Deselect the defender after the attack
        attacker.EndTurnForUnitIfPossible();       // End the turn for the attacker if possible can't use the attacker anymore in the same turn
        defender.DestroyIfPossible();              // Destroy the defender if its health points <= 0

    }



    // // Method to handle the selection of a GridCell
    public void OnCellSelection(GridCell cell)
    // i should contunue the same what i did for the unit selection
    {
        // 
        HandelPlayerInput.Action action = handelPlayerInput.DetermineCellAction(cell, selectedUnit, playerTurn);
        ExecuteCellAction(cell, selectedUnit, SelectedUnitFromAttacker, action);


        // If the GridCell is not walkable and a unit is selected , unselect that unit
        // if (SelectedUnitFromAttacker != null && cell.isWalkable == false)
        // {
        //     SelectedUnitFromAttacker.IsSelected = false;
        //     SelectedUnitFromAttacker.ResetRedEffectOnAttackbleEnemies();
        //     SelectedUnitFromAttacker = null;
        //     selectedUnit = null;
        //     ResetGridCells(); // Reset the grid cells to their original state
        // }

        // if (cell.isWalkable && SelectedUnitFromAttacker != null && SelectedUnitFromAttacker.hasMoved == false)
        // {
        //     // Move the selected unit to the GridCell
        //     SelectedUnitFromAttacker.Move(cell.row, cell.column);
        //     SelectedUnitFromAttacker.row = cell.row;
        //     SelectedUnitFromAttacker.col = cell.column;
        //     selectedUnit.occupiedCell = cell;
        //     // Set the selected unit's hasMoved property to true to prevent it from moving again in the same turn 
        //     SelectedUnitFromAttacker.hasMoved = true;
        //     // Unselect the unit 
        //     SelectedUnitFromAttacker.IsSelected = false;
        //     SelectedUnitFromAttacker.ResetRedEffectOnAttackbleEnemies();
        //     SelectedUnitFromAttacker = null;
        //     selectedUnit = null;
        //     // Reset the grid cells to their original state 
        //     ResetGridCells();

        // }
    }



    void ExecuteCellAction(GridCell cell, Unit unit, Unit SelectedUnitFromAttacker, HandelPlayerInput.Action Action)
    {
        switch (Action)
        {
            case HandelPlayerInput.Action.Move:
                unit.Move(cell.row, cell.column);
                // attack if possible after move ...
                break;

            case HandelPlayerInput.Action.None:
                break;

            case HandelPlayerInput.Action.DisplayMenu:
                Debug.Log("Display Menu");
                break;

            default:
                break;
        }
    }



}