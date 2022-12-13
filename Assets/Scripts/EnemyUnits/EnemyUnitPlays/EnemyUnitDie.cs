using UnityEngine;

public class EnemyUnitDie : IEnemyUnitPlay
{
    private bool IsPlay;

    private EnemyUnitPlay unit;

    private Animator animator;
    private Transform unitTransform;

    public EnemyUnitDie(EnemyUnitPlay enemyUnit)
    {
        unit = enemyUnit;
        animator = enemyUnit.GetAnimator();
        unitTransform = enemyUnit.transform;

        IsPlay = false;
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
        animator.Play("Death");
        IsPlay = false;
        return EnemyUnitStates.None;
    }
}
