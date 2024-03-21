using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class Unit : MonoBehaviour
{

   

    public SpriteRenderer spriteRenderer;
    GameMaster gm;
    public MapGrid mapGrid;
    public GridCell occupiedCell;

    // public enum unit name
    public int unitIndex;
    public bool isSelected;
    public int row;
    public int col;
    public float moveSpeed;
    public int playerNumber;


    // state variables

    public bool hasMoved;
    public bool hasAttacked;



    public int healthPoints;

    // the next 4 attributes maybe removed from here later

    public int attackBoost;
    public int specialAttackBoost;
    public int defenseBoost = 0; //!!!!!!!!! pour l'instant 0
    public int specialDefenseBoost = 0; //!!!!!!!!! pour l'instant 0;



    public int moveRange;
    public int ration; 
    public float rationPerDay;
    public int lineOfSight;
    public int cost;
    public int attackRange;
    public int minAttackRange;
    public int ammo;

    //!! we can use ENUMS for the moveType (i find it better that strings) 

    public string moveType; 
    // "foot"
    // "sea"
    // "tires"
    // "lander"
    // "horses"


    public List<GridCell> walkableGridCells = new List<GridCell>(); // this list will contain the grid cells that the unit can move to
    public List<Unit> enemiesInRange = new List<Unit>(); // this list will contain enemies that ca be attacked by a unit




    void Start()
    {  

        // Get the GameMaster component from the scene
        gm = FindObjectOfType<GameMaster>(); // singleton paradigm
        // Get the MapGrid component from the scene
        mapGrid = FindObjectOfType<MapGrid>();
        // Get the spriteRenderer Component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Method to highlight the GridCell when the mouse hovers over it 
    // there is a problem with OnMouseDown , it is way too sensitive , you click once and it considers as if you clicked n times
    // and then it depends on your luck , if n is "impair" then your unit will be selected
    // if n is is "pair" then your unit will not be selected
    // // and this is why you can observe it is buggy when you try to select your unit 


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) // left click
        {
            // Debug.Log(this.col + " left  " + this.row);
            gm.OnUnitSelection(this, MouseButton.LeftMouse);
        }

        if (Input.GetMouseButtonDown(1)) // right click
        {
            // Debug.Log(this.col + " right  " + this.row);
            gm.OnUnitSelection(this, MouseButton.RightMouse);
        }
    }


    public void Move(int row, int column)
    {
        UpdateAttributsAfterMove(row, column);
        EndTurnForUnitIfPossible();

        Vector2 position = new Vector2(-MapGrid.Horizontal + column + 0.5f, MapGrid.Vertical - row - 0.5f);
        StartCoroutine(StartMovement(position));

        ResetWalkableGridCells();
        ResetHighlightedEnemyInRange();
        GetEnemies();  //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! est ce que nzidoha ? ghie tmovi yhighlightilek li t9der t'attakihom ?
    }

    public void UpdateAttributsAfterMove(int row, int column)
    {
        occupiedCell.occupantUnit = null; // remove the unit from the old grid cell
        occupiedCell = mapGrid.grid[row, column]; // set the occupiedCell of the unit to the grid cell
        //! ??? , here we are modify an atribut of the MapGrid, is it a good practice ? 
        mapGrid.grid[row, column].occupantUnit = this; // set the occupantUnit of the new grid cell to the unit

        hasMoved = true;
        // IsSelected = false; //!!!! ????
        this.row = row;
        this.col = column;

        // hasAttacked = ???? nbdloha wla non ? 3la 7ssab type t3 unit , kayen li lazem 7eta tour ljay bch t'attackiw . 
        // na9esss energy wla ration or whatever it's name .
    }


    public void UpdateAttributsAfterAttack()
    {
        hasAttacked = true;
    }

    public void EndTurnForUnitIfPossible() // numb state
    {
        if (hasMoved && hasAttacked)
        {
            spriteRenderer.color = Color.gray;
        }
    }

    // // Method to move the unit to the specified position
    // // IEnumerator is used to make the movement smooth // and to wait for the movement to finish before executing the next line of code // IEnumerator is used to make the movement smooth // and to wait for the movement to finish before executing the next line of code
    IEnumerator StartMovement(Vector2 position)
    {
        while (transform.position.x != position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(position.x, transform.position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }

        while (transform.position.y != position.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }
    }


    // I NOTICED THAT IN ADVANCE WARS , the unit aknowleges the enemies it can attack only after moving
    // if you dont move the unit eventhough the enemy is in range you can not attack it 
    // i don't think we'll be doing that here
    // as soon as you select a unit , you will be able to attack ( seems much better right ? )
    public void GetEnemies()
    {
        if (hasAttacked == false)
        {
            enemiesInRange.Clear(); // Clear the List

            foreach (Unit unitEnemyCondidat in FindObjectsOfType<Unit>())
            {
                // if it's an [ enemy unit ] and [ enemy unit in range ]

                if ((unitEnemyCondidat.playerNumber != gm.playerTurn) && (MathF.Abs(unitEnemyCondidat.row - row) + MathF.Abs(unitEnemyCondidat.col - col) <= attackRange))
                {
                    enemiesInRange.Add(unitEnemyCondidat); // add this attackble enemy to the list of attackble enemies

                    // no visuals for the moment , just pure code
                    // we can sote them in a list ... 
                    highlightEnemyInRange(unitEnemyCondidat);
                }
            }
        }
    }


    public void highlightEnemyInRange(Unit unit)
    {
        unit.spriteRenderer.color = Color.red;
    }


    public void ResetHighlightedEnemyInRange()
    {
        foreach (Unit unitEnemy in enemiesInRange)  //!!!! normalment foreach in enemiesInRange
        {
            unitEnemy.spriteRenderer.color = Color.white;
        }
        enemiesInRange.Clear();
    }


    public void ResetWalkableGridCells()
    {
        foreach (GridCell cell in walkableGridCells)
        {
            cell.ResetGridCell();
        }
        walkableGridCells.Clear();
    }

    // n3awdo nchofo fhad la method , est ce que realement nss79oha !
    public void ResetUnitPropritiesInEndTurn()
    {
        isSelected = false;  // reset the selected property to false
        hasMoved = false;   // reset the hasMoved and hasAttacked variables to false  
        hasAttacked = false;
        spriteRenderer.color = Color.white; // reset the color of the unit to white
    }


    public void DestroyIfPossible()
    {
        if (this.healthPoints <= 0)
        {
            this.occupiedCell.occupantUnit = null;   // remove the unit from the grid cell
            Destroy(this.gameObject);                // remove the unit from UNITY !!!!
        }
    }

    public void RecieveDamage(int inflictedDamage)
    {
        this.healthPoints -= inflictedDamage;
    }


}