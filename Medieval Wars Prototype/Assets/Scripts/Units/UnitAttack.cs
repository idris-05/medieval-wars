using System;
using System.Collections.Generic;

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
        // numb state ?
    }

   


    public void GetEnemiesInRange()
    {
        enemiesInRange.Clear(); // Clear the List

        foreach (Unit CondidateUnitToGetAttacked in FindObjectsOfType<Unit>()) // hna balak tparcouri direct les ennemis ( besah 1v1v1v1 )
        {
             // lazem t3 base damage tetbedel ( adem w ahmed rahoum y5dmou 3liha )
            if (AttackSystem.baseDamage[this.unitIndex, CondidateUnitToGetAttacked.unitIndex] != -1) // est ce que y9der yattakih aslan kho
            {

                float distance = MathF.Abs(CondidateUnitToGetAttacked.row - row) + MathF.Abs(CondidateUnitToGetAttacked.col - col);

                if (CondidateUnitToGetAttacked.playerNumber != GameController.Instance.playerTurn && distance <= attackRange && distance >= minAttackRange) // yverifyi ida rah f range t3ou
                {
                    enemiesInRange.Add(CondidateUnitToGetAttacked); // add this attackble enemy to the list of attackble enemies
                }

            }

        }
    }



    // public void GetAttackableGridCells()
    // {
    // Hadi rani 7atha f attack system , mais normalment tji hna
    // }



    public void HighlightEnemyInRange()
    {
        foreach (Unit enemy in enemiesInRange)
        {
            enemy.unitView.HighlightAsEnemy();
        }
    }


    public void HighlitAttackableGridCells()
    {
        foreach (GridCell cell in attackableGridCells)
        {
            cell.HighlightAsAttackable();
        }
        attackableGridCells.Clear();
    }


    public void ResetHighlightedEnemyInRange()
    {
        foreach (Unit enemy in enemiesInRange)
        {
            enemy.unitView.ResetHighlightedUnit();
        }
        enemiesInRange.Clear();
    }

    public void ResetHighlitedAttackableGridCells()
    {
        foreach (GridCell cell in attackableGridCells)
        {
            cell.ResetHighlitedCell();
        }
        attackableGridCells.Clear();
    }


    public void CaptureBuilding(Building building)
    {
        return ;
    }


}

