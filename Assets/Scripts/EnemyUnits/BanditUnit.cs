public class BanditUnit : EnemyUnitPlay
{



    protected override void OnAttack()
    {
        if (enemyCol != null)
        {
            if (enemyCol.TryGetComponent<Unit>(out var unit))
            {
                unit.OnDamage(attack);
            }

            if (enemyCol.TryGetComponent<HouseHeals>(out var house))
            {
                house.OnDamage(attack);
            }

        }
    }

}
