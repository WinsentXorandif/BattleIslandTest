using UnityEngine;
using UnityEngine.AI;

public class EnemyBoat : IEnemyUnitPlay
{

    private bool IsPlay;
    private Animator animator;
    private EnemyUnitPlay unit;
    private NavMeshAgent navMeshAgent;




    public EnemyBoat(EnemyUnitPlay enemyUnit)
    {
        IsPlay = false;
        unit = enemyUnit;

        animator = enemyUnit.GetAnimator();
        navMeshAgent = enemyUnit.GetNavMeshAgent();


    }

    public void BeginPlay()
    {
        navMeshAgent.enabled = false;
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

        return EnemyUnitStates.Boat;
    }

}
