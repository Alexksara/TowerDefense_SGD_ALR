using UnityEngine;

public class EnemyThief : Enemy
{
    protected override void ReachedEnd()
    {
        GameManager.Instance.AddMoney(-moneyValue);
        base.ReachedEnd();
    }
}
