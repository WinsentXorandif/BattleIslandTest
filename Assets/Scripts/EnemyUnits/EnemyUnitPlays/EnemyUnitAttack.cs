using UnityEngine;

public class EnemyUnitAttack : IEnemyUnitPlay
{
    private bool IsPlay;

    private EnemyUnitPlay unit;

    private Animator animator;
    private float attackRange;

    private Transform unitTransform;



    public EnemyUnitAttack(EnemyUnitPlay enemyUnit)
    {
        IsPlay = false;

        unit = enemyUnit;
        animator = enemyUnit.GetAnimator();
        attackRange = enemyUnit.GetAttackRange();
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
            if (distance > attackRange)
            {
                //unit.enemyCol = null;
                return EnemyUnitStates.Stay;
            }
            unitTransform.LookAt(unit.enemyCol.transform.position);
            animator.Play("Attack");
            return EnemyUnitStates.AttackUnit;
        }

        return EnemyUnitStates.Stay;//unit.FindEnemy(EnemyUnitStates.AttackUnit);
    }
}
