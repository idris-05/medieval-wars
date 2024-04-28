using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class UnitAttack : Unit
{
    public int attackRange;
    public int minAttackRange;
    public int durability;
    public bool hasAttacked;

    public List<Unit> enemiesInRange = new List<Unit>(); // this list will contain enemies that ca be attacked by a unit
    public List<GridCell> attackableGridCells = new List<GridCell>(); // this list will contain the grid cells that the unit can attack



    public void UpdateAttributsAfterAttack()
    {
        hasAttacked = true;
        unitView.ResetHighlightedUnit();
        ResetHighlightedEnemyInRange();
    }



    public void GetEnemiesInRange()
    {
        enemiesInRange.Clear();

        foreach (Unit CondidateUnitToGetAttacked in FindObjectsOfType<Unit>()) // hna balak tparcouri direct les ennemis ( besah 1v1v1v1 )
        {
            if (CondidateUnitToGetAttacked.playerOwner != GameController.Instance.currentPlayerInControl)
            {
                // lazem t3 base damage tetbedel ( adem w ahmed rahoum y5dmou 3liha )
                if (AttackSystem.baseDamage[this.unitIndex, CondidateUnitToGetAttacked.unitIndex] != -1) // est ce que y9der yattakih aslan kho
                {

                    float distance = MathF.Abs(CondidateUnitToGetAttacked.row - row) + MathF.Abs(CondidateUnitToGetAttacked.col - col);

                    if (distance <= attackRange && distance >= minAttackRange)
                    {
                        enemiesInRange.Add(CondidateUnitToGetAttacked); // add this attackble enemy to the list of attackble enemies
                    }
                }

            }

        }
    }


    public void HighlightEnemyInRange()
    {
        enemiesInRange.ForEach(enemy => enemy.unitView.HighlightAsEnemy());
    }
    public void ResetHighlightedEnemyInRange()
    {
        enemiesInRange.ForEach(enemy => enemy.unitView.ResetHighlightedUnit());
        enemiesInRange.Clear();
    }



    public void HighlitAttackableGridCells()
    {
        attackableGridCells.ForEach(cell => cell.gridCellView.HighlightAsAttackable());
    }
    public void ResetHighlitedAttackableGridCells()
    {
        attackableGridCells.ForEach(cell => cell.gridCellView.ResetHighlitedCell());
        attackableGridCells.Clear();
    }



    // public void SetAttackRange()
    // {
    //     switch (playerOwner.Co.coName)
    //     {
    //        case COUtil.COName.AHMEDPLAYER:
                
    //             break;
    //         case COUtil.COName.RICHARDTHELIONHEART:
                
    //             break;
    //         case COUtil.COName.SATAN:
                
    //             break;
    //         default:
                
    //             break;
    //     }
    // }
}

