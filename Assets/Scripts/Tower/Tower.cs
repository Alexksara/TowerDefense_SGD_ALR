using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public abstract class Tower : MonoBehaviour
{
    public float FireCooldown = 1f;

    protected float currentFireCooldown = 0f;
    protected List<Enemy> enemiesInRange = new List<Enemy>();

    protected virtual void Update()
    {
        currentFireCooldown -= Time.deltaTime;
        Enemy closestEnemy = GetTargetEnemy();
        if(closestEnemy != null && currentFireCooldown <= 0f)
        {
            FireAt(closestEnemy);
            currentFireCooldown = FireCooldown;
        }
    }

    protected abstract void FireAt(Enemy target);

    protected abstract Enemy GetTargetEnemy();

    protected void ClearDestroyedEnemies()
    {
        for (int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
            if (enemiesInRange[i] == null)
            {
                enemiesInRange.RemoveAt(i);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && !enemiesInRange.Contains(enemy))
            {
            enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
        }
    }
}
