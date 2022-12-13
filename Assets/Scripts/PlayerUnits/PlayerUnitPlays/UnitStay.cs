using UnityEngine;
using UnityEngine.AI;


public class UnitStay : IUnitPlay
{

    private bool IsPlay;
    private PlayerUnitPlay unit;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private Transform unitTransform;
    private float findRange;
    private float attackRange;
    //private float minDistance;
    private LayerMask enemyLayer;

    private RaycastHit hitInfo;


    public UnitStay(PlayerUnitPlay currentUnit)
    {
        unit = currentUnit;

        animator = currentUnit.GetAnimator();
        navMeshAgent = currentUnit.GetNavMeshAgent();

        unitTransform = currentUnit.transform;
        findRange = currentUnit.GetFindRange();
        attackRange = currentUnit.GetAttackRange();
        enemyLayer = currentUnit.GetEnemyLayer();

        hitInfo = new RaycastHit();
        IsPlay = false;

    }

    public void BeginPlay()
    {
        //unit.enemyCol = null;
        //minDistance = MAX_DISTANCE;
        //navMeshAgent.enabled = false;
        IsPlay = true;
    }

    public void EndPlay()
    {
        IsPlay = false;
    }

    public UnitStates UpdatePlay()
    {
        if (!IsPlay) return UnitStates.None;


        if (unit.ActivateUnit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000f))
                {
                    //Debug.Log($"UNIT_STAY ActivateUnit and GetMouseButtonDown = {minDistance}");
                    if (hitInfo.transform.TryGetComponent<Graund>(out _))
                    {
                        //Debug.Log($"UNIT_STAY ActivateUnit and GetMouseButtonDown = {minDistance}");
                        //navMeshAgent.enabled = true;
                        unit.moveTargetPos = hitInfo.point;
                        return UnitStates.Move;
                    }
                }
            }
        }

        //navMeshAgent.enabled = false;
        animator.Play("Idle");
        return unit.FindEnemy(UnitStates.Stay);

        //return UnitStates.Stay;

    }
}
