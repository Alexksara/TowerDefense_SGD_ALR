using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public abstract class Tower : MonoBehaviour
{
    public float FireCooldown = 1f;
    protected float currentFireCooldown = 0f;

    [SerializeField]protected List<Enemy> enemiesInRange = new List<Enemy>();
    [SerializeField] protected GameObject projectilePrefab;

    protected virtual void Update() 
    {
        if(enemiesInRange.Count > 0)
        {
            currentFireCooldown -= Time.deltaTime;
            Enemy targetEnemy = GetTargetEnemy();
            if (targetEnemy != null && currentFireCooldown <= 0f)
            {
                FireAt(targetEnemy);
                currentFireCooldown = FireCooldown;
            }
        }
        
    }

    protected virtual void FireAt(Enemy target)
    {
        if (projectilePrefab != null)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().SetTarget(target.transform);
        }
    }

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
