using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitPlay : Unit
{
    public Action<EnemyUnitPlay> OnUnitDie;

    private const float DESTROY_TIME = 1f;
    private const int MAX_ENEMYS = 5;
    private const float MAX_DISTANCE = 1000.0f;

    protected Dictionary<EnemyUnitStates, IEnemyUnitPlay> enemyPlayDict;
    protected IEnemyUnitPlay iEnemyPlayCurrent;
    protected EnemyUnitStates enemyStateCurrent;

    private void InitPlayDict()
    {
        enemyPlayDict = new Dictionary<EnemyUnitStates, IEnemyUnitPlay>
        {
            { EnemyUnitStates.None, new EnemyUnitNone() },
            { EnemyUnitStates.Boat, new EnemyBoat(this)},
            { EnemyUnitStates.Stay, new EnemyUnitStay(this)},
            { EnemyUnitStates.Move, new EnemyUnitMove(this)},
            { EnemyUnitStates.AttackUnit, new EnemyUnitAttack(this)},
            { EnemyUnitStates.AttackHouse, new EnemyUnitAttackHouse(this)},
            { EnemyUnitStates.Die, new EnemyUnitDie(this)}
        };
    }

    private void Awake()
    {
        InitUnitData();
        InitPlayDict();
        InitPlayStart();
    }

    private void InitPlayStart()
    {
        //navMeshAgent.enabled = false;

        enemyStateCurrent = EnemyUnitStates.Boat;
        iEnemyPlayCurrent = enemyPlayDict[enemyStateCurrent];
        iEnemyPlayCurrent.BeginPlay();
    }


    public EnemyUnitStates SetNewUnitPlay(EnemyUnitStates newUnitPlay)
    {
        iEnemyPlayCurrent?.EndPlay();

        enemyStateCurrent = newUnitPlay;
        iEnemyPlayCurrent = enemyPlayDict[newUnitPlay];
        iEnemyPlayCurrent.BeginPlay();
        return enemyStateCurrent;
    }

    public override void OnDamage(int damage)
    {
        hitPoint -= Mathf.Abs(damage - defence);
        healthBar.SetHealth(hitPoint);
        if (hitPoint <= 0)
        {
            OnDie();
        }
    }

    protected virtual void OnDie()
    {
        navMeshAgent.enabled = false;
        Destroy(healthBar.gameObject);
        Destroy(coll);
        SetNewUnitPlay(EnemyUnitStates.Die);
        OnUnitDie?.Invoke(this);
        Destroy(gameObject, DESTROY_TIME);
    }

    public EnemyUnitStates FindEnemy(EnemyUnitStates states)
    {
        float minDistance = MAX_DISTANCE;

        //Debug.Log($"findRange = {findRange}");

        Collider[] hitColliders = new Collider[MAX_ENEMYS];
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, findRange, hitColliders, enemyLayer);

        if (numColliders == 0) return states;

        int nearestEnemy = 0;

        for (int i = 0; i < numColliders; i++)
        {
            float distance = Vector3.Distance(hitColliders[i].transform.position, transform.position);
            if (distance < minDistance) { minDistance = distance; nearestEnemy = i; }
        }
        enemyCol = hitColliders[nearestEnemy];

        if (enemyCol.TryGetComponent<HouseHeals>(out var hh))
        {
            if (minDistance < attackRangeHouse)
            {
                //navMeshAgent.enabled = false;
                //navMeshAgent.isStopped = true;
                return EnemyUnitStates.AttackHouse;
            }
        }
        if (minDistance < attackRange)
        {
            //navMeshAgent.enabled = false;
            //navMeshAgent.isStopped = true;
            return EnemyUnitStates.AttackUnit;
        }
        moveTargetPos = enemyCol.transform.position;
        navMeshAgent.enabled = true;
        //navMeshAgent.isStopped = false;
        navMeshAgent.destination = moveTargetPos;
        return EnemyUnitStates.Move;
    }

    public void OnUpdate()
    {
        EnemyUnitStates newEnemyPlay = iEnemyPlayCurrent.UpdatePlay();

        if (newEnemyPlay != enemyStateCurrent)
        {
            SetNewUnitPlay(newEnemyPlay);
        }
    }


}
