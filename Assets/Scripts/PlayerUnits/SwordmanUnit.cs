public class SwordmanUnit : PlayerUnitPlay
{

    protected override void OnAttack()
    {
        if (enemyCol != null)
        {
            if (enemyCol.TryGetComponent<Unit>(out var unit))
            {
                unit.OnDamage(attack);
            }
        }
    }

}
