using System.Collections;
using UnityEngine;


public class UnitView : MonoBehaviour
{
    //!!! WE MUST FIX THIS , unit is referenced inside unitView and unitView is referenced inside unit 
    // ida llah ghaleb makach solution , hadi tkon priavte , wlo5ra public normal .
    private Unit unit;

    public MapGrid mapGrid;

    public SpriteRenderer spriteRenderer;
    public float moveSpeed = 5;


    bool isUnitHovered = false;
    bool rightButtonHolded = false;


    void Start()
    {
        mapGrid = FindObjectOfType<MapGrid>();  // ttna7a
        unit = GetComponent<Unit>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    //!!!!!!! l7kaya t3 reset highlight hadi lazem ttssgem , swa pour unitView wla pour GridCell

    // onMouseExit onMouseOver Update , omb3d nriglohom 3la 7ssab wch ndifiniw f jeux ta3na .

    // Reset isUnitHovered flag when the mouse exits the unit
    private void OnMouseExit()
    {
        isUnitHovered = false;
        rightButtonHolded = false;
        ResetHighlitedAttackableCells();
    }

    private void OnMouseOver()   // est ce que advance wars ay unit tclicker 3liha yhighlightiha ? swa ta3ek 2wla t3 li contrek
    {
        isUnitHovered = true;
        // left click on unit
        if (Input.GetMouseButtonDown(0))
        {
            if (unit.numbState)
            {
                return;
            }
            // Debug.Log("left click on unit");
            UnitController.Instance.OnUnitSelection(unit); // singleton
        }

    }
    // Check if right mouse button is held down display the attackableCells

    void Update()
    {
        if (!isUnitHovered) return;

        if (Input.GetMouseButton(1) && !rightButtonHolded)
        {
            rightButtonHolded = true;
            // Debug.Log(" ba9i mclickiii");
            if ((unit is UnitAttack) == false)
            {  // only UnitAttack has attackableCells List 
                return;
            }
            AttackSystem.GetAttackableCells(unit as UnitAttack, mapGrid);
            HighlightAttackableCells(); // Display attackable cells
        }
        else if (Input.GetMouseButtonUp(1)) // Check if right mouse button is released
        {
            // Reset highlighting
            rightButtonHolded = false;
            ResetHighlitedAttackableCells();
        }
    }





    public void AnimateMovement(int row, int column)
    {
        Vector2 position = new Vector2(-MapGrid.Horizontal + column + 0.5f, MapGrid.Vertical - row - 0.5f);
        StartCoroutine(StartMovement(position));
    }

    // Method to move the unit to the specified position
    // IEnumerator is used to make the movement smooth // and to wait for the movement to finish before executing the next line of code
    public IEnumerator StartMovement(Vector2 position)
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



    public void DeathAnimation()
    {
        return;
        //!!!!!!!!11 
    }




    public void HighlightAsEnemy()
    {
        spriteRenderer.color = Color.red;
    }

    public void HighlightAsSelected()  //HighlightUnitOnSelection
    {
        spriteRenderer.color = Color.green;
        //!!!!!!!!!!!!
    }

    public void HighlightAsSuppliable()
    {
        return;
    }

    //!!!!!!!!!!
    public void ResetHighlightedUnit()
    {
        spriteRenderer.color = Color.white;
        //!!!!!!!!!!!!
    }


    // Method to hide the unit when load it to the transporter unit
    public void HideUnitWhenLoaded()
    {
        return;
        // hide unit when it get loaded on transporter unit .
    }


    public void MakeUnitInteractable()
    {
        Transform transform = GetComponent<Transform>();
        Vector3 newPosition = transform.position;
        newPosition.z = -15;
        transform.position = newPosition;

    }


    public void ResetUnitBackToTheirOriginalLayer()
    {
        Transform transform = GetComponent<Transform>();
        Vector3 newPosition = transform.position;
        newPosition.z = 0; //!!!!!!!!!!!!!!!!!!!!!!!!!!! ch7al original value ?
        transform.position = newPosition;

    }


    public void HighlightWalkablesCells()
    {
        foreach (GridCell cell in unit.walkableGridCells)
        {
            cell.HighlightAsWalkable();
        }
    }

    //!!!!!!1 win rahi reset ta3ha ??? 



    public void HighlightAttackableCells()
    {
        foreach (GridCell cell in (unit as UnitAttack).attackableGridCells)
        {
            cell.HighlightAsAttackable();
        }
    }


    public void ResetHighlitedAttackableCells()
    {
        foreach (GridCell cell in (unit as UnitAttack).attackableGridCells)
        {
            cell.ResetHighlitedCell();
        }
    }



}