using NUnit.Framework;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class CannonTower : Tower
{
    protected override Enemy GetTargetEnemy()
    {
        ClearDestroyedEnemies();
        Enemy middleEnemy = null;
        if (enemiesInRange.Count > 0)
        {
            enemiesInRange = SortByPosition();
            int middleNum = Mathf.CeilToInt((float)(enemiesInRange.Count / 2));
            middleEnemy = enemiesInRange[middleNum];
        }
        return middleEnemy;

    }
    //Summary
    //
    private List<Enemy> SortByPosition()
    {
        List<Enemy> sortedEnemiesInRange = new List<Enemy>();
        float enemyDistanceKey = 0f;
        //sortedEnemiesInRange.Add(enemiesInRange[0]);
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            enemyDistanceKey = Vector3.Distance(this.transform.position, enemiesInRange[i].transform.position);
            int j = i - 1;

            while (j >= 0 && Vector3.Distance(this.transform.position, enemiesInRange[j].transform.position) > enemyDistanceKey)
            {
                j--;
            }
            sortedEnemiesInRange.Insert(j + 1, enemiesInRange[i]);
        }
        return sortedEnemiesInRange;
    }

}
