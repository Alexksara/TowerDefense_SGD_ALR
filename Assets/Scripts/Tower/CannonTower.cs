using NUnit.Framework;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class CannonTower : Tower
{
    //Summary
    // clears removed enemies from the list
    // as long as 1 enemy is inside the towers range, sorts, then finds the middle distanced enemy and returns it
    protected override Enemy GetTargetEnemy()
    {
        ClearDestroyedEnemies();
        Enemy middleEnemy = null;
        if (M_enemiesInRange.Count > 0)
        {
            M_enemiesInRange = SortByPosition();
            int middleNum = Mathf.CeilToInt((float)(M_enemiesInRange.Count / 2));
            middleEnemy = M_enemiesInRange[middleNum];
        }
        return middleEnemy;

    }
    //Summary
    // Insertion sort algorithm that sorts the enemies into an ordered list based of their distance to the player
    private List<Enemy> SortByPosition()
    {
        List<Enemy> sortedEnemiesInRange = new List<Enemy>();
        float enemyDistanceKey = 0f;
        //sortedEnemiesInRange.Add(enemiesInRange[0]);
        for (int i = 0; i < M_enemiesInRange.Count; i++)
        {
            enemyDistanceKey = Vector3.Distance(this.transform.position, M_enemiesInRange[i].transform.position);
            int j = i - 1;

            while (j >= 0 && Vector3.Distance(this.transform.position, M_enemiesInRange[j].transform.position) > enemyDistanceKey)
            {
                j--;
            }
            sortedEnemiesInRange.Insert(j + 1, M_enemiesInRange[i]);
        }
        return sortedEnemiesInRange;
    }

}
