using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public abstract class Tower : MonoBehaviour
{
    public float FireCooldown = 1f;
    public int CostToPlace = 0;
    public int CostToUpgrade = 5;
    public int CurrentLevel = 1;
    protected float M_currentFireCooldown = 0f;

    [SerializeField] protected List<Enemy> M_enemiesInRange = new List<Enemy>();
    [SerializeField] protected GameObject M_projectilePrefab;
    [SerializeField] protected Vector3 M_projectileOffset = new Vector3(0,1f);
    [SerializeField] protected GameObject m_turret;

    private Enemy m_targetEnemy;

    protected virtual void Update() 
    {
        M_currentFireCooldown -= Time.deltaTime;
        if (M_enemiesInRange.Count > 0)
        {
            Enemy targetEnemy = GetTargetEnemy();
            if (targetEnemy != null)
            {
                m_turret.transform.LookAt(targetEnemy.transform.position);
                if (M_currentFireCooldown <= 0f)
                {
                    FireAt(targetEnemy);
                    M_currentFireCooldown = FireCooldown;
                }
            }

        }
    }

    //Summary
    //default functionality can be override
    // spawns a projectile and sets its target to the target passed in
    protected virtual void FireAt(Enemy target)
    {
        if (M_projectilePrefab != null)
        {
            GameObject projectileInstance = Instantiate(M_projectilePrefab, this.transform.position + M_projectileOffset, Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().SetTarget(target.transform);
        }
    }

    //Summary
    // each tower has their own logic to determine which enemy to shoot
    protected virtual Enemy GetTargetEnemy()
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

    //Summary
    //removes null spots (destroyed enemies) from the list of enemies
    protected void ClearDestroyedEnemies()
    {
        for (int i = M_enemiesInRange.Count - 1; i >= 0; i--)
        {
            if (M_enemiesInRange[i] == null)
            {
                M_enemiesInRange.RemoveAt(i);
            }
        }
    }
    //Summary
    // adds enemy who walks within range to a list of enemies
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && !M_enemiesInRange.Contains(enemy))
            {
            M_enemiesInRange.Add(enemy);
        }
    }
    //Summary
    // removes enemy who walks out of range from list
    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && M_enemiesInRange.Contains(enemy))
        {
            M_enemiesInRange.Remove(enemy);
        }
    }
}
