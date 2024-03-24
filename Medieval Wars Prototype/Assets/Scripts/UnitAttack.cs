using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : Unit
{
    public int attackRange;
    public int minAttackRange;
    public int durability;
    public bool hasAttacked;

    public List<Unit> enemiesInRange = new List<Unit>(); // this list will contain enemies that ca be attacked by a unit





    public void UpdateAttributsAfterAttack()
    {
        hasAttacked = true;
    }


    // public void GetEnemies()
    // {
    //     if (hasAttacked == false)
    //     {
    //         enemiesInRange.Clear(); // Clear the List

    //         foreach (Unit unitEnemyCondidat in FindObjectsOfType<Unit>())
    //         {
    //             // if it's an [ enemy unit ] and [ enemy unit in range ]

    //             if ((unitEnemyCondidat.playerNumber != gm.playerTurn) && (MathF.Abs(unitEnemyCondidat.row - row) + MathF.Abs(unitEnemyCondidat.col - col) <= attackRange))
    //             {
    //                 enemiesInRange.Add(unitEnemyCondidat); // add this attackble enemy to the list of attackble enemies

    //                 // no visuals for the moment , just pure code
    //                 // we can sote them in a list ... 
    //                 highlightEnemyInRange(unitEnemyCondidat);
    //             }
    //         }
    //     }
    // }




}