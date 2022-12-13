using UnityEngine;

public class EnemyUnitAttackHouse : IEnemyUnitPlay
{
    private bool IsPlay;

    private EnemyUnitPlay unit;

    private Animator animator;
    private float attackRangeHouse;

    private Transform unitTransform;


    public EnemyUnitAttackHouse(EnemyUnitPlay enemyUnit)
    {
        IsPlay = false;

        unit = enemyUnit;
        animator = enemyUnit.GetAnimator();
        attackRangeHouse = enemyUnit.GetAttackRangeHouse();
        unitTransform = enemyUnit.transform;

    }

    public void BeginPlay()
    {
        IsPlay = true;
    }

    public void EndPlay()
    {
        IsPlay = false;
    }

    public EnemyUnitStates UpdatePlay()
    {
        if (!IsPlay) return EnemyUnitStates.None;

        if (unit.enemyCol != null)
        {
            float distance = Vector3.Distance(unit.enemyCol.transform.position, unitTransform.position);
            if (distance > attackRangeHouse)
            {
                //unit.enemyCol = null;
                return EnemyUnitStates.Stay;
            }
            unitTransform.LookAt(unit.enemyCol.transform.position);
            animator.Play("Attack");
            return EnemyUnitStates.AttackHouse;
        }

        return EnemyUnitStates.Stay; //unit.FindEnemy(EnemyUnitStates.AttackHouse);
    }
}
