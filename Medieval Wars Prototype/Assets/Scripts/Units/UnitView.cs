using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Purchasing;


public class UnitView : MonoBehaviour
{
    //!!! WE MUST FIX THIS , unit is referenced inside unitView and unitView is referenced inside unit 
    // ida llah ghaleb makach solution , hadi tkon priavte , wlo5ra public normal .
    private Unit unit;

    public Transform unitTransform; // I needed this to fix a problem where the z coordinate was set back to 0 after movement
    public MapGrid mapGrid; //!!!!! rahi kayna deja mapgrid fl unit 

    public SpriteRenderer spriteRenderer;
    public float moveSpeed = 5;


    bool isUnitHovered = false;
    bool rightButtonHolded = false;

    public Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        mapGrid = FindObjectOfType<MapGrid>();  // ttna7a
        unit = GetComponent<Unit>();
        unitTransform = GetComponent<Transform>();
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
            Debug.Log("left click on unit");
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


    //! I CHANGED ANIMATE MOVEMENT AND STARTMOVEMENT
    //! SO THAT WHEN THE PLAYER MOVES HE MOVES IN 3D INSTEAD OF IN 2D
    //! BECAUSE IF YOU MOVE IN 2D THE Z COORDINATE WILL BE RESET TO 0 WHICH TOTALLY RUINS THE LAYER ORDER WE DEFINED IN OUR SCENE
    //! THEREFOR I USED VECTOR3 INSTEAD OF VECTOR2

    public void SetUnitPosition(int newRow, int newCol)
    {
        Vector3 position = new Vector3(-MapGrid.Horizontal + newCol + 0.5f, MapGrid.Vertical - newRow - 0.5f, unitTransform.position.z);
        transform.position = position;
    }

    public void AnimateMovement(int row, int column)
    {
        Vector3 position = new Vector3(-MapGrid.Horizontal + column + 0.5f, MapGrid.Vertical - row - 0.5f, unitTransform.position.z);
        StartCoroutine(StartMovement(position));
    }

    // Method to move the unit to the specified position
    // IEnumerator is used to make the movement smooth // and to wait for the movement to finish before executing the next line of code
    public IEnumerator StartMovement(Vector2 position)
    {
        // The unit is moving horizontally therefore the horizontal movement animation must start
        // Horizontal movement can be right -> left 
        // Horizontal movement can be left -> right

        animator.SetTrigger("right side walking trigger");

        while (transform.position.x != position.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(position.x, transform.position.y, unitTransform.position.z), moveSpeed * Time.deltaTime);
            yield return null;
        }

        while (transform.position.y != position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, position.y, unitTransform.position.z), moveSpeed * Time.deltaTime);
            yield return null;
        }
    }





    public void HighlightAsEnemy()
    {
        spriteRenderer.color = Color.red;
    }

    public void HighlightAsSelected()
    {
        spriteRenderer.color = Color.green;
    }

    public void HighlightAsSuppliable()
    {
        spriteRenderer.color = Color.yellow;
    }

    public void ResetHighlightedUnit()
    {
        spriteRenderer.color = Color.white;
        //!!!!!!!!!!!!
    }


    // Method to hide the unit when load it to the transporter unit
    public void HideUnitWhenLoaded()
    {
        gameObject.SetActive(false);
        // hide unit when it get loaded on transporter unit .
    }
    public void ShowUnitAfterDrop()
    {
        gameObject.SetActive(true);
        // show unit after it get dropped from the transporter unit .
    }

    public void MakeUnitInteractable()
    {
        Transform transform = GetComponent<Transform>();
        Vector3 newPosition = transform.position;
        newPosition.z = -3;
        transform.position = newPosition;

    }
    public void ResetUnitBackToTheirOriginalLayer()
    {
        Transform transform = GetComponent<Transform>();
        Vector3 newPosition = transform.position;
        newPosition.z = -1;
        transform.position = newPosition;

    }


    public void HighlightWalkablesCells()
    {
        unit.walkableGridCells.ForEach(walkableGridCell => walkableGridCell.gridCellView.HighlightAsWalkable());
    }
    public void ResetHighlitedWalkableCells()
    {
        unit.walkableGridCells.ForEach(walkableGridCell => walkableGridCell.gridCellView.ResetHighlitedCell());
        unit.walkableGridCells.Clear();
    }


    public void HighlightAttackableCells()
    {
        (unit as UnitAttack).attackableGridCells.ForEach(attackableGridCell => attackableGridCell.gridCellView.HighlightAsAttackable());
    }
    public void ResetHighlitedAttackableCells()
    {
        if ((unit is UnitAttack) == false) return;

        (unit as UnitAttack).attackableGridCells.ForEach(attackableGridCell => attackableGridCell.gridCellView.ResetHighlitedCell());
        (unit as UnitAttack).attackableGridCells.Clear();
    }




}