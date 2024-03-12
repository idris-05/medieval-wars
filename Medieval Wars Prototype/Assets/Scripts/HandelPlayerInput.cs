using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class HandelPlayerInput : MonoBehaviour
{
    // all the actions that the unit can do when player click on it .
    public enum UnitAction
    {
        None,
        Move,
        Attack,
        Select,
        Unselect,
        // Add more actions as needed
    }

    // Method to determine the action to be taken by the unit when clicked by the player
    // NOT COMPLETED YET .
    public UnitAction DetermineUnitAction(Unit unit, Unit SelectedUnitFromAttacker, int playerTurn)
    {
        // If no unit is currently selected
        if (SelectedUnitFromAttacker == null)
        {
            // If the selected unit belongs to the opponent, display its attack range if needed
            if (unit.playerNumber != playerTurn)
            {
                // Code to display attack range or handle opponent unit selection
                // (To be discussed...)
                return UnitAction.None;
            }
            else
            {
                // If the selected unit has not moved yet, select it and display its movement and attack range
                if (unit.hasMoved == false)
                {
                    return UnitAction.Select;
                }
                else
                {
                    // The selected unit has already moved
                    return UnitAction.None;
                }
            }
        }
        else // A unit is already selected by the attacker
        {
            // If the selected unit belongs to the current player, deselect it
            if (unit.playerNumber == playerTurn)
            {
                return UnitAction.Unselect;
            }
            else
            {
                // If the selected unit is within the attack range of the attacker and hasn't been attacked yet, attack it
                if (SelectedUnitFromAttacker.hasAttacked == false && SelectedUnitFromAttacker.enemiesInRange.Contains(unit))
                {
                    return UnitAction.Attack;
                }
                else
                {
                    // The selected unit is not within the attack range or has already been attacked
                    return UnitAction.Unselect;
                }
            }
        }

    }

}