using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Purchasing;
using System.Linq;
using System.Collections.Generic;


public class UnitView : MonoBehaviour
{
    //!!! WE MUST FIX THIS , unit is referenced inside unitView and unitView is referenced inside unit 
    // ida llah ghaleb makach solution , hadi tkon priavte , wlo5ra public normal .
    private Unit unit;

    public Transform unitTransform; // I needed this to fix a problem where the z coordinate was set back to 0 after movement
    public MapGrid mapGrid; //!!!!! rahi kayna deja mapgrid fl unit 

    public SpriteRenderer spriteRenderer;
    public float moveSpeed;


    bool isUnitHovered = false;
    bool rightButtonHolded = false;


    public Animator animator;
    public UnitUtil.AnimationState currentAnimatonState;

    GridCell gridCellTheUnitIsMovingTowards; // i need this to animate the movement

    MiniIntelController miniIntelController;


    void Start()
    {
        mapGrid = FindObjectOfType<MapGrid>();  // ttna7a
        unit = GetComponent<Unit>();
        unitTransform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        miniIntelController = FindObjectOfType<MiniIntelController>();
    }

    void OnMouseEnter()
    {
        MiniIntelController.Instance.HandleMINIIntel(unit.occupiedCell);
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

            AttackSystem.Instance.GetAttackableCells(unit as UnitAttack, mapGrid);

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
        Vector3 position = new Vector3(-16 + newCol + 0.5f, 9 - newRow - 0.5f, unitTransform.position.z);
        transform.position = position;
    }

    public void AnimateMovement(int row, int column, bool loadIntended)
    {
        gridCellTheUnitIsMovingTowards = mapGrid.grid[row, column];

        // Store the starting position
        Vector3 startPosition = transform.position;

        // Create a list to store all target positions
        List<Vector3> targetPositions = new List<Vector3>();

        // foreach (GridCell gridCell in gridCellTheUnitIsMovingTowards.Pathlist)
        foreach (GridCell gridCell in GameController.Instance.cellsPath)

        {

            // Calculate target position
            Vector3 targetPosition = CalculateWorldPosition(gridCell.row, gridCell.column);
            targetPositions.Add(targetPosition);

        }

        // Start moving the character along the path
        StartCoroutine(MoveAlongPath(targetPositions, row, column, loadIntended));

    }
    private IEnumerator MoveAlongPath(List<Vector3> targetPositions, int row, int column, bool loadIntended)
    {
        foreach (Vector3 targetPosition in targetPositions)
        {
            ChangeAnimationState(WhichAnimationToPlayWhenMoving(targetPosition));
            // Smoothly move towards the target position
            while (Vector3.Distance(transform.position, targetPosition) != 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
            RestoreFlip();
            Destroy(GameController.Instance.arrow[targetPositions.IndexOf(targetPosition)]);
        }

        // this is just to make sure our character gets to the right position

        transform.position = new Vector3(mapGrid.grid[row, column].transform.position.x, mapGrid.grid[row, column].transform.position.y + 0.125f, -1);
        ChangeAnimationState(UnitUtil.AnimationState.IDLE);

        CancelScript.Instance.Cancel();

        // here we need to check if the moved unit still hahve another actions to do , or it will enter the NumbState MODE
        if (!loadIntended)
        {
            ActionsHandler.Instance.FillButtonsToDisplay(this.unit);
            if (ButtonsUI.Instance.buttonsToDisplay.Any() == false) this.unit.TransitionToNumbState();
            ButtonsUI.Instance.buttonsToDisplay.Clear();
        }
        else
        {
            this.unit.TransitionToNumbState();
            (mapGrid.grid[row, column].occupantUnit as UnitTransport).Load(unit);
        }

    }

    private UnitUtil.AnimationState WhichAnimationToPlayWhenMoving(Vector3 targetPosition)
    {
        if (transform.position.x - targetPosition.x > 0 && unit.playerOwner == GameController.Instance.player1)
        {
            this.spriteRenderer.flipX = true;
            return UnitUtil.AnimationState.SIDE_WALK;
        }

        if (transform.position.x - targetPosition.x > 0 && unit.playerOwner == GameController.Instance.player2)
        {
            return UnitUtil.AnimationState.SIDE_WALK;
        }

        if (transform.position.x - targetPosition.x < 0 && unit.playerOwner == GameController.Instance.player1)
        {
            return UnitUtil.AnimationState.SIDE_WALK;
        }

        if (transform.position.x - targetPosition.x < 0 && unit.playerOwner == GameController.Instance.player2)
        {
            this.spriteRenderer.flipX = false;
            return UnitUtil.AnimationState.SIDE_WALK;
        }

        if (transform.position.y - targetPosition.y > 0)
        {
            return UnitUtil.AnimationState.DOWN_WALK;
        }

        if (transform.position.y - targetPosition.y < 0)
        {
            return UnitUtil.AnimationState.UP_WALK;
        }

        return UnitUtil.AnimationState.IDLE;
    }

    public void RestoreFlip()
    {
        if (unit.playerOwner == GameController.Instance.player1) this.spriteRenderer.flipX = false;
        if (unit.playerOwner == GameController.Instance.player2) this.spriteRenderer.flipX = true;
    }

    private Vector3 CalculateWorldPosition(int row, int column)
    {
        // Adjust this calculation based on your grid cell size and layout
        Vector3 position = new Vector3(-16 + column + 0.5f, 9 - row + 0.125f - 0.5f, unitTransform.position.z);
        return position;
    }


    public void HighlightAsEnemy()
    {
        spriteRenderer.color = Color.red;
    }

    public void HighlightAsSelected()
    {
        spriteRenderer.color = Color.green;
    }
    public void HighlightAsInNumbState()
    {
        spriteRenderer.color = Color.black;
    }
    public void ResetHighlightedUnit()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
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

    public void ChangeAnimationState(UnitUtil.AnimationState newAnimationState)
    {
        // to avoid the animation overwriting it self  
        if (currentAnimatonState == newAnimationState) return;

        animator.Play(GetAnimationStateString(newAnimationState));

        currentAnimatonState = newAnimationState;

    }

    public static string GetAnimationStateString(UnitUtil.AnimationState state)
    {
        switch (state)
        {
            case UnitUtil.AnimationState.IDLE:
                return "right side idle";
            case UnitUtil.AnimationState.UP_WALK:
                return "up side walk";
            case UnitUtil.AnimationState.DOWN_WALK:
                return "down side walk";
            case UnitUtil.AnimationState.SIDE_WALK:
                return "right side walk";
            default:
                return "Unknown";
        }
    }




}