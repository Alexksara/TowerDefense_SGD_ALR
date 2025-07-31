using UnityEngine;

public class BallistaTower : Tower
{
    //Summary
    // clears removed enemies
    // as long as 1 enemy is within range finds the shortest distance and returns it
    protected override Enemy GetTargetEnemy()
    {
        ClearDestroyedEnemies();
        Enemy closestEnemy = null;
        if (M_enemiesInRange.Count > 0)
        {
            float closestDistance = float.MaxValue;
            foreach (Enemy enemy in M_enemiesInRange)
            {

                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }
        return closestEnemy;
    }
}
