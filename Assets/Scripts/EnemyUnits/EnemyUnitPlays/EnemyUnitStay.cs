using UnityEngine;
using UnityEngine.AI;

public class EnemyUnitStay : IEnemyUnitPlay
{
    private bool IsPlay;

    private EnemyUnitPlay unit;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    //private Transform unitTransform;
    private float findRange;
    private float attackRange;
    //private float minDistance;
    //private LayerMask enemyLayer;

    public EnemyUnitStay(EnemyUnitPlay enemyUnit)
    {
        IsPlay = false;

        unit = enemyUnit;

        animator = enemyUnit.GetAnimator();
        navMeshAgent = enemyUnit.GetNavMeshAgent();

        //unitTransform = enemyUnit.transform;
        //findRange = enemyUnit.GetFindRange();
        //attackRange = enemyUnit.GetAttackRange();
        //enemyLayer = enemyUnit.GetEnemyLayer();

    }

    public void BeginPlay()
    {
        //unit.enemyCol = null;
        //navMeshAgent.enabled = false;

        IsPlay = true;
    }

    public void EndPlay()
    {
        IsPlay = false;
    }

    public EnemyUnitStates UpdatePlay()
    {
        if (!IsPlay) return EnemyUnitStates.None;

        animator.Play("Idle");

        return unit.FindEnemy(EnemyUnitStates.Stay);
    }
}
