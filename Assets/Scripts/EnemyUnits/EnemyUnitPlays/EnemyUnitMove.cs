using UnityEngine;
using UnityEngine.AI;
public class EnemyUnitMove : IEnemyUnitPlay
{
    private bool IsPlay;

    private EnemyUnitPlay unit;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private Transform unitTransform;
    private float findRange;
    private float attackRange;



    public EnemyUnitMove(EnemyUnitPlay enemyUnit)
    {
        IsPlay = false;

        unit = enemyUnit;

        animator = enemyUnit.GetAnimator();
        navMeshAgent = enemyUnit.GetNavMeshAgent();
        navMeshAgent.speed = enemyUnit.GetMoveSpeed();
        unitTransform = enemyUnit.transform;

        findRange = enemyUnit.GetFindRange();
        attackRange = enemyUnit.GetAttackRange();


    }

    public void BeginPlay()
    {
        navMeshAgent.enabled = true;
        //navMeshAgent.destination = unit.moveTargetPos;
        //findRange = enemyUnit.GetFindRange();
        //attackRange = enemyUnit.GetAttackRange();
        IsPlay = true;
    }

    public void EndPlay()
    {
        IsPlay = false;
    }

    public EnemyUnitStates UpdatePlay()
    {

        if (!IsPlay) return EnemyUnitStates.None;

        animator.Play("Walk");

        float distance = Vector3.Distance(navMeshAgent.destination, unitTransform.position);

        if (distance < 1.0f)
        {
            navMeshAgent.enabled = false;
            return EnemyUnitStates.Stay;
        }

        return unit.FindEnemy(EnemyUnitStates.Move);
    }

}
