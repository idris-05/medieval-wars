using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

//,, With The Same Logic Used In This Script , We Can Create And Control Our AI System .

public class HandelPlayerInput : MonoBehaviour
{

    public bool getWalkableTilesActivated;  // we controle select and deselect unit , with the same button , so we need a bool to make defference .
    public bool getAttackableTilesActivated;  // same things for the attackable tiles .

    public Unit selectedUnit;  // to make sure that we have only one selected unit at a time .
    public Unit unitWithGetAttackableTilesActivated; // to make sure that we have only one unit with attackable tiles activated at a time .

    // + for now , we will not display the attackable tiles and the walckable tiles in the same time , 
    // we can fix that later by implementing this :  While The Player Keep the right (or the left)click pressed , the attackable tiles will be displayed 
    // and when he release the click , the attackable tiles will be hidden .





    // hadi zyada pour l'instant , i try to implement it to work exactly like advance wars , but i faced a lot of difficulties so i don't want to wast time for thing we could change later ! . 


    //  Analyze the game from Advance Wars:

    // For units:
    //     - 1. avec click [L] , (click gouche? (n9dro ndbloha) dans notre cas ), get all the possible move cases (getWalkable tiles) for that enemy unit .
    //             tclicki 5tra berk 3la L ydir getWalkable Tiles .
    //             more ma tclicki L , mt9der tdir walo men ghir tclicker L 3la TILES li t9der tmchilhom , wla tclicki K (a n'omport qulle ou ) pour annuler .
    //             tclicki L , tchof win t9der temchi , tzid tclicki L 3la w7da men li t'highlight'awlek temchi liha (move) .



    //     - 2. avec click [K] , (click droite?  dans notre cas ), get all the possible attack cases for that enemy unit . (tssema dir get walkables tiles ,
    //              apres pour chaque tile men li y9der ychmilhom , t'afficher win y9der t'attacker (dirha b get enemy))
    //              lazem teb9a m3bezz 3la K fo9 unit ta3ek 
    //              kayen probleme m3a la method kifach ndetecti beli clickit 3la unit , swa ndir la detecteur fl class unit , whowa ydir la defference bin right w left click , wla ndirha direct f handleplayerinput w hadi fiha probleme ki tweli lmaps kbira 3la l camera wtss7e9 fiha colliders ll objects ta3ek ... 
    //              tbanli ndirha flunit 5ir , mais lprobkleme t3 hadik tb9a m3bezz 3la key m3balalich ndirlo . 


    //__________________________________________________________________________________________________________________________________________________________



    // all the possible that  actions can hapen in the game ... (we can add others as needed).  
    public enum Action
    {
        None,
        Move,
        Attack,
        SelectUnit,
        UnselectUnit,
        HighlightEnemyForUnit,
        UnHighlightEnemyForUnit,
        DisplayMenu,
        // Add more actions as needed
    }



    // Method to determine the action to be taken by the unit when clicked by the player
    // NOT COMPLETED YET .
    // from unit clicks only , 
    public Action DetermineUnitAction(Unit unit, int playerTurn, MouseButton mouseButton)
    {

        // you cannot see the attackable tiles and the walckables tiles in the same time 
        if (mouseButton == MouseButton.LeftMouse && getAttackableTilesActivated == false)
        {
            //  getWalkableTilesActivated = false means that there is no unit selected , so we will select the unit that the player has clicked
            if (getWalkableTilesActivated == false)
            {
                getWalkableTilesActivated = true;
                selectedUnit = unit;
                return Action.SelectUnit;
            }
            else
            {   // there is a unit selected and you click on a unit agian 
                // if it is the previous selected unit we will diselect it , but if it is another unit we will do nothing .
                // ...  we will chnage this later , in other word : we you have a previuos sleected unit and you click on another ,
                //  if it is atransporter unit there is something to do ... , if it was another unit that you can merge with it .
                if (unit == selectedUnit)
                {
                    getWalkableTilesActivated = false;
                    selectedUnit = null;
                    return Action.UnselectUnit;
                }
                else
                {
                    return Action.None;
                }
            }
        }


        // untill now , the player can highlight the attackable tiles for more than only one unit , we should fix that .
        // we can use unitWithGetAttackableTilesActivated and reopeat the same logic with selected unit above .

        // we can do like advance wars , while the player keep the right click pressed , the attackable tiles will be displayed 
        // and when he release the click , the attackable tiles will be hidden .

        // you cannot see the attackable tiles and the walckables tiles in the same time 
        if (mouseButton == MouseButton.RightMouse && getWalkableTilesActivated == false)
        {
            if (getAttackableTilesActivated == false)
            {
                getAttackableTilesActivated = true;
                unitWithGetAttackableTilesActivated = unit;
                return Action.HighlightEnemyForUnit;
            }
            else
            {
                if (unitWithGetAttackableTilesActivated == unit)
                {
                    getAttackableTilesActivated = false;
                    unitWithGetAttackableTilesActivated = null;
                    return Action.UnHighlightEnemyForUnit;
                }
                else
                {
                    return Action.None;
                }
            }
        }

        return Action.None;



        // // If no unit is currently selected
        // if (SelectedUnitFromAttacker == null)
        // {
        //     // If the selected unit belongs to the opponent, display its attack range if needed
        //     if (unit.playerNumber != playerTurn)
        //     {
        //         // Code to display attack range or handle opponent unit selection
        //         // (To be discussed...)
        //         return Action.None;
        //     }
        //     else
        //     {
        //         // If the selected unit has not moved yet, select it and display its movement and attack range
        //         if (unit.hasMoved == false)
        //         {
        //             return Action.Select;
        //         }
        //         else
        //         {
        //             // The selected unit has already moved
        //             return Action.None;
        //         }
        //     }
        // }
        // else // A unit is already selected by the attacker
        // {
        //     // If the selected unit belongs to the current player, deselect it
        //     if (unit.playerNumber == playerTurn)
        //     {
        //         return Action.Unselect;
        //     }
        //     else
        //     {
        //         // If the selected unit is within the attack range of the attacker and hasn't been attacked yet, attack it
        //         if (SelectedUnitFromAttacker.hasAttacked == false && SelectedUnitFromAttacker.enemiesInRange.Contains(unit))
        //         {
        //             return Action.Attack;
        //         }
        //         else
        //         {
        //             // The selected unit is not within the attack range or has already been attacked
        //             return Action.Unselect;
        //         }
        //     }
        // }

    }



    // Method to determine the action to be taken by the cell when clicked by the player
    // NOT COMPLETED YET ....
    public Action DetermineCellAction(GridCell cell, Unit unit, int playerTurn)
    {
        // 1. If there is a unit currently selected (it's the last unit selected before this cell was clicked), weach means unit!= null  . + the player click on a cell ,
        //     then the unit should move to that cell if it's walkable , do nothing else (if not walkable) .
    
        if (unit != null)
        {
            if (unit.walkableGridCells.Contains(cell))
            {
                if (unit.hasMoved == false && unit.playerNumber == playerTurn)
                {
                    getWalkableTilesActivated = false;
                    return Action.Move;  // move the unit ( passed as parameter ) to the cell
                }
                else
                { // unit already has moved , or it's not the turn of the player who owns the unit
                    return Action.None;
                }
            }
            else
            {
                // cell not walkable and there is a unit selected => do nothing .
                return Action.None;
            }
        }
        else  // unit == null : there is no selected unit, and you click on a cell => display menu (i'am just doing like advance wars). 
        {
            return Action.DisplayMenu;
        }
      


    }





}