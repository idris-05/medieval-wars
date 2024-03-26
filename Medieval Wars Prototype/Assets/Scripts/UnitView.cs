using System.Collections;
using UnityEngine;


public class UnitView : MonoBehaviour
{
    //!!! WE MUST FIX THIS , unit is referenced inside unitView and unitView is referenced inside unit 
    // ida llah ghaleb makach solution , hadi tkon priavte , wlo5ra public normal .

    private Unit unit;

    // private MovementSystem movementSystem;   // hadi omb3d tro7 b event

    public SpriteRenderer spriteRenderer;
    [SerializeField] float moveSpeed = 5;


    void Start()
    {
        // movementSystem = FindObjectOfType<MovementSystem>();
        unit = GetComponent<Unit>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnMouseOver()
    {
        if (unit.numbState == true)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)) // left click
        {
            Debug.Log("touchit unit");
            UnitController.Instance.OnUnitSelection(unit); // singleton
            // EventManager.InvokeUnitSelectedEvent(unit); //event
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


    public void HighlightAsEnemy()
    {
        spriteRenderer.color = Color.red;
    }


    public void ResetHighlightedEnemyInRange(UnitAttack unit)
    {
        // mb3d nweliwelha

        foreach (Unit unitEnemy in unit.enemiesInRange)  //!!!! normalment foreach in enemiesInRange
        {
            unitEnemy.unitView.spriteRenderer.color = Color.white;
        }
        unit.enemiesInRange.Clear();
    }


    public void HighlightUnitOnSelection()
    {
        spriteRenderer.color = Color.green;
        //!!!!!!!!!!!!
    }

    //!!!!!!!!!!
    public void ResetHighlightingWhenNotSelected()
    {
        spriteRenderer.color = Color.white;
        //!!!!!!!!!!!!
    }


    public void DeathAnimation()
    {
        return;
        //!!!!!!!!11 
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

}