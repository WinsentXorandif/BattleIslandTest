using UnityEngine;

public class UnitAttack : IUnitPlay
{
    private bool IsPlay;
    private PlayerUnitPlay unit;

    private Animator animator;
    private Transform unitTransform;
    private float attackRange;

    private RaycastHit hitInfo;

    public UnitAttack(PlayerUnitPlay currentUnit)
    {
        unit = currentUnit;

        animator = currentUnit.GetAnimator();
        unitTransform = currentUnit.transform;

        attackRange = currentUnit.GetAttackRange();
        //battleRange = currentUnit.GetFindRange();
        //enemyLayer = currentUnit.GetEnemyLayer();

        hitInfo = new RaycastHit();
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

    public UnitStates UpdatePlay()
    {
        if (!IsPlay) return UnitStates.None;

        if (unit.ActivateUnit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000f))
                {

                    if (hitInfo.transform.TryGetComponent<Graund>(out _))
                    {

                        //navMeshAgent.enabled = true;
                        unit.moveTargetPos = hitInfo.point;
                        return UnitStates.Move;
                    }
                }
            }
        }

        if (unit.enemyCol != null)
        {
            float distance = Vector3.Distance(unit.enemyCol.transform.position, unitTransform.position);
            if (distance > attackRange)
            {
                //unit.enemyCol = null;
                return UnitStates.Stay;
            }
            unitTransform.LookAt(unit.enemyCol.transform.position);
            animator.Play("Attack");
            return UnitStates.Attack;
        }


        return UnitStates.Stay;
    }
}
