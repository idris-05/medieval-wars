using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

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
        }

        // hado walo 

        // if (Input.GetMouseButtonDown(0)) // left click
        // {
        //     movementSystem.Movement(unit, 3, 3); //!!!!!!!!!!!!!!
        // }

        // if (Input.GetMouseButtonDown(1)) // right click
        // {
        //     movementSystem.Movement(unit, 4, 5); //!!!!!!!!!!!!!!
        // }
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


    public void HighlightEnemyInRange()
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


}