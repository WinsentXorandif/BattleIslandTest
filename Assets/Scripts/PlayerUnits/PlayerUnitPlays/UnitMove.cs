using UnityEngine;
using UnityEngine.AI;

public class UnitMove : IUnitPlay
{
    private bool IsPlay;
    private PlayerUnitPlay unit;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private Transform unitTransform;
    private float attackRange;

    private RaycastHit hitInfo;

    public UnitMove(PlayerUnitPlay currentUnit)
    {
        unit = currentUnit;

        animator = currentUnit.GetAnimator();
        navMeshAgent = currentUnit.GetNavMeshAgent();
        navMeshAgent.speed = currentUnit.GetMoveSpeed();
        unitTransform = currentUnit.transform;
        attackRange = currentUnit.GetAttackRange();

        hitInfo = new RaycastHit();
        IsPlay = false;

    }

    public void BeginPlay()
    {
        navMeshAgent.enabled = true;
        navMeshAgent.destination = unit.moveTargetPos;
        IsPlay = true;
    }

    public void EndPlay()
    {
        IsPlay = false;
    }

    public UnitStates UpdatePlay()
    {
        if (!IsPlay) return UnitStates.None;

        animator.Play("Walk");

        if (unit.ActivateUnit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000f))
                {
                    if (hitInfo.transform.TryGetComponent<Graund>(out _))
                    {
                        //Debug.Log($"{unit.gameObject.name} UNIT_MOVE ActivateUnit and GetMouseButtonDown !!!!!!!");

                        navMeshAgent.enabled = true;
                        unit.moveTargetPos = hitInfo.point;
                        navMeshAgent.destination = hitInfo.point;
                        return UnitStates.Move;
                    }
                }
            }
        }


        //float minDistance = unit.FindEnemy();

        float distance = Vector3.Distance(navMeshAgent.destination, unitTransform.position);

        if (distance < 1.0f)
        {
            //Debug.Log($"{unit.gameObject.name} UNIT_MOVE distance = {distance} !!!!!!!");

            navMeshAgent.enabled = false;

            unit.ActivateUnit = false;
            return UnitStates.Stay;

        }

        /*
        if (unit.enemyCol != null)
        {
            if(minDistance < attackRange)
            {
               // Debug.Log($"{unit.gameObject.name} UNIT_MOVE distanceAttack = {distance} !!!!!!!");

                unitTransform.LookAt(unit.enemyCol.transform.position);
                navMeshAgent.enabled = false;
                return UnitStates.Attack;
            }
        }
        */

        //animator.Play("Walk");
        return unit.FindEnemy(UnitStates.Move);
    }
}
