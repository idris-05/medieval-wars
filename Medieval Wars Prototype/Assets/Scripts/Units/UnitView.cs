using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Purchasing;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UIElements;


public class UnitView : MonoBehaviour
{
    //!!! WE MUST FIX THIS , unit is referenced inside unitView and unitView is referenced inside unit 
    // ida llah ghaleb makach solution , hadi tkon priavte , wlo5ra public normal .
    public Unit unit;

    public Transform unitTransform; // I needed this to fix a problem where the z coordinate was set back to 0 after movement

    public SpriteRenderer spriteRenderer;

    public BoxCollider2D boxCollider2D;
    public float moveSpeed;


    bool isUnitHovered = false;
    bool rightButtonHolded = false;


    public Animator animator;
    public UnitUtil.AnimationState currentAnimatonState;

    GridCell gridCellTheUnitIsMovingTowards; // i need this to animate the movement

    public UnitHealthIcon HealthIcon;

    public GameObject redDagger; // this will be only when unit is highlighted as attackable

    public int WhereTOSpawnDamageIcon;    // 0 : above the unit
                                          // 1 : on the right side of the unit
                                          // 2 : on the left side of the unit


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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            InfoCardController.Instance.UpdateTerrainBIGIntel(unit.occupiedCell.occupantTerrain, Input.mousePosition);
        }

    }
    // Check if right mouse button is held down display the attackableCells
    void Update()
    {
        if (!isUnitHovered || UnitController.Instance.CurrentActionStateBasedOnClickedButton != UnitUtil.ActionToDoWhenButtonIsClicked.NONE) return;

        if (Input.GetMouseButton(1) && !rightButtonHolded)
        {
            rightButtonHolded = true;
            // Debug.Log(" ba9i mclickiii");
            if ((unit is UnitAttack) == false)
            {  // only UnitAttack has attackableCells List 
                return;
            }

            AttackSystem.Instance.GetAttackableCells(unit as UnitAttack, MapGrid.Instance);

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
        Vector3 position = new Vector3(-16 + newCol + 0.5f, 9 - newRow - 0.5f + 0.125f, unitTransform.position.z);
        transform.position = position;
    }

    public void AnimateMovement(int row, int column, bool loadIntended)
    {
        gridCellTheUnitIsMovingTowards = MapGrid.Instance.grid[row, column];

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
            ChangeAnimationState(WhichMoveAnimationToPlayWhenMoving(targetPosition));
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

        transform.position = new Vector3(MapGrid.Instance.grid[row, column].transform.position.x, MapGrid.Instance.grid[row, column].transform.position.y + 0.125f, -1);
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
            (MapGrid.Instance.grid[row, column].occupantUnit as UnitTransport).Load(unit);
        }

    }

    private UnitUtil.AnimationState WhichMoveAnimationToPlayWhenMoving(Vector3 targetPosition)
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
        GameObject RedDagger = Instantiate(UserInterfaceUtil.Instance.RedDaggerPrefab, this.transform.position, Quaternion.identity);
        this.redDagger = RedDagger;
        this.spriteRenderer.color = Color.red;
    }

    public void HighlightAsSelected()
    {

        this.spriteRenderer.material.color = new Color(171, 0, 255, 255);
    }

    public void HighlightAsInNumbState()
    {
        this.spriteRenderer.color = Color.gray;
        this.spriteRenderer.material.color = Color.gray;
    }

    public void ResetHighlightedUnit()
    {
        if (spriteRenderer != null)
        {
            if (this.unit.playerOwner == GameController.Instance.player1) unit.unitView.spriteRenderer.material.color = Color.black;
            if (this.unit.playerOwner == GameController.Instance.player2) unit.unitView.spriteRenderer.material.color = new Color(255, 0, 0, 255);
            spriteRenderer.color = Color.white;
            Destroy(this.redDagger);
        }
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
        this.spriteRenderer.color = Color.white;
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
        UserInterfaceUtil.Instance.CellhighlightHolder.transform.position = this.unit.transform.position;
        UserInterfaceUtil.Instance.CellhighlightLines.SetActive(true);
        UserInterfaceUtil.Instance.CellhighlightLines.GetComponent<SpriteRenderer>().color = Color.green;

        unit.walkableGridCells.ForEach(walkableGridCell => walkableGridCell.gridCellView.isHighlighted = true);

        unit.walkableGridCells.ForEach(walkableGridCell => walkableGridCell.gridCellView.HighlightAsWalkable());

    }

    public void ResetHighlitedWalkableCells()
    {
        UserInterfaceUtil.Instance.CellhighlightLines.SetActive(false);

        unit.walkableGridCells.ForEach(walkableGridCell => walkableGridCell.gridCellView.isHighlighted = false); // i need this for UI

        unit.walkableGridCells.ForEach(walkableGridCell => walkableGridCell.gridCellView.ResetHighlitedCell());

        UserInterfaceUtil.Instance.GlowLinesThatExistOnTheScene.ForEach(glowLine => Destroy(glowLine)); // DESTROY ALL THE GLOWLINES THAT HAVE ALREADY BEEN CREATED
        UserInterfaceUtil.Instance.GlowLinesThatExistOnTheScene.Clear();
        unit.walkableGridCells.Clear();
    }


    public void HighlightAttackableCells()
    {
        UserInterfaceUtil.Instance.CellhighlightHolder.transform.position = this.unit.transform.position;
        UserInterfaceUtil.Instance.CellhighlightLines.SetActive(true);
        UserInterfaceUtil.Instance.CellhighlightLines.GetComponent<SpriteRenderer>().color = Color.red;

        (unit as UnitAttack).attackableGridCells.ForEach(attackableGridCell => attackableGridCell.gridCellView.isHighlighted = true); // i need this for UI

        (unit as UnitAttack).attackableGridCells.ForEach(attackableGridCell => attackableGridCell.gridCellView.HighlightAsAttackable());
    }

    public void ResetHighlitedAttackableCells()
    {
        if ((unit is UnitAttack) == false) return;


        UserInterfaceUtil.Instance.CellhighlightLines.SetActive(false);

        (unit as UnitAttack).attackableGridCells.ForEach(attackableGridCell => attackableGridCell.gridCellView.isHighlighted = false); // i need this for UI

        (unit as UnitAttack).attackableGridCells.ForEach(attackableGridCell => attackableGridCell.gridCellView.ResetHighlitedCell());

        UserInterfaceUtil.Instance.GlowLinesThatExistOnTheScene.ForEach(glowLine => Destroy(glowLine)); // DESTROY ALL THE GLOWLINES THAT HAVE ALREADY BEEN CREATED

        UserInterfaceUtil.Instance.GlowLinesThatExistOnTheScene.Clear();
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
            case UnitUtil.AnimationState.UP_ATTACK:
                return "up side attack";
            case UnitUtil.AnimationState.DOWN_ATTACK:
                return "down side attack";
            case UnitUtil.AnimationState.RIGHT_SIDE_ATTACK:
                return "right side attack";
            case UnitUtil.AnimationState.LEFT_SIDE_ATTACK:
                return "left side attack";
            case UnitUtil.AnimationState.DIE_ANIMATION:
                return "die animation";
            case UnitUtil.AnimationState.SHADOW_CLONE_JUTSU:
                return "shadow clone jutsu";
            case UnitUtil.AnimationState.GROUND_EXPLOSION:
                return "GroundExplosion";
            default:
                return "Unknown";
        }
    }

    public void RecieveDamageUI(int inflictedDamage)
    {

        Vector3 position = RelativePositionToInstantiateDamageIcon(); // this is the position of the damage icon relative to the unit's position

        // instantiate the damage icon after receiving damage
        DamageIcon damageIconInstance = Instantiate(UserInterfaceUtil.Instance.damageIconPrefab, this.transform.position + position, Quaternion.identity);
        damageIconInstance.SetupDamageToDisplay(inflictedDamage);
        // UPDATE THE HEALTH ICON OF THE DEFENDING UNIT ( ALWAYS TAKES ONLY THE SECOND DIGIT OF THE HEALTH )
        this.HealthIcon.GetComponent<SpriteRenderer>().sprite = UserInterfaceUtil.Instance.numbersFromZeroToTenSpritesForHealth[GameUtil.GetHPToDisplayFromRealHP(this.unit.healthPoints)];
    }

    private Vector3 RelativePositionToInstantiateDamageIcon()
    {
        // 0 : above the unit
        // 1 : on the right side of the unit
        // 2 : on the left side of the unit

        if (WhereTOSpawnDamageIcon == 0) return new Vector3(-0.1f, +0.675f, 0);
        if (WhereTOSpawnDamageIcon == 1) return new Vector3(0.5f, 0.5f, 0);
        if (WhereTOSpawnDamageIcon == 2) return new Vector3(-0.7f, -0.5f, 0);

        Debug.Log("ERROR AT DEFINING POSITION TO INSTANTIATE DAMAGE ICON");
        return new Vector3(0, 0, 0);
    }



}
