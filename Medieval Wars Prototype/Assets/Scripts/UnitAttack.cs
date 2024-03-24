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
        // numb state ?
    }

    public void PrepareUnitToGetLoadedInTransporter()
    {
        occupiedCell.occupantUnit = null;
        gameObject.SetActive(false);
    }


/*     public void GetEnemies()
     {
             enemiesInRange.Clear(); // Clear the List

             foreach (Unit CondidateUnitToGetAttacked in FindObjectsOfType<Unit>())
             {
                 // if it's an [ enemy unit ] and [ enemy unit in range ]

                 if ((MathF.Abs(CondidateUnitToGetAttacked.row - row) + MathF.Abs(CondidateUnitToGetAttacked.col - col) <= attackRange)) // this condition ain't enough
                 {
                     enemiesInRange.Add(CondidateUnitToGetAttacked); // add this attackble enemy to the list of attackble enemies

                     // no visuals for the moment , just pure code
                     // we can sote them in a list ... 
                    // highlightEnemyInRange(CondidateUnitToGetAttacked);
                 }
             }
         }
     }

    */

}