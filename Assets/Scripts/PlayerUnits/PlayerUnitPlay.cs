using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitPlay : Unit
{
    public Action<PlayerUnitPlay> OnUnitDie;
    public Action OnActivateUnit;

    private const float DESTROY_TIME = 1f;
    private const int MAX_ENEMYS = 5;
    private const float MAX_DISTANCE = 1000.0f;

    protected Dictionary<UnitStates, IUnitPlay> unitPlayDict;
    protected IUnitPlay iUnitPlayCurrent;
    protected UnitStates unitStateCurrent;

    public bool ActivateUnit { get; set; }

    private void InitPlayDict()
    {
        unitPlayDict = new Dictionary<UnitStates, IUnitPlay>
        {
            { UnitStates.None, new UnitNone() },
            { UnitStates.Stay, new UnitStay(this)},
            { UnitStates.Move, new UnitMove(this)},
            { UnitStates.Attack, new UnitAttack(this)},
            { UnitStates.Die, new UnitDie(this)},
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
        ActivateUnit = false;
        healthBar.SetMaxHealth(hitPoint);

        unitStateCurrent = UnitStates.Stay;
        iUnitPlayCurrent = unitPlayDict[unitStateCurrent];
        iUnitPlayCurrent.BeginPlay();

    }

    public UnitStates SetNewUnitPlay(UnitStates newUnitPlay)
    {
        iUnitPlayCurrent?.EndPlay();

        unitStateCurrent = newUnitPlay;
        iUnitPlayCurrent = unitPlayDict[newUnitPlay];
        iUnitPlayCurrent.BeginPlay();
        return unitStateCurrent;
    }

    protected virtual void OnMouseDown()
    {
        OnActivateUnit?.Invoke();

        ActivateUnit = true;
        Debug.Log($"Unit name = {this.gameObject.name} ActivateUnit = {ActivateUnit}");

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
        SetNewUnitPlay(UnitStates.Die);
        OnUnitDie?.Invoke(this);
        Destroy(gameObject, DESTROY_TIME);
    }

    public UnitStates FindEnemy(UnitStates states)
    {
        float minDistance = MAX_DISTANCE;

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

        if (minDistance < attackRange)
        {
            navMeshAgent.enabled = false;
            return UnitStates.Attack;
        }
        moveTargetPos = enemyCol.transform.position;
        navMeshAgent.enabled = true;
        navMeshAgent.destination = moveTargetPos;
        return UnitStates.Move;

    }



    public void OnUpdate()
    {
        UnitStates newUnitPlay = iUnitPlayCurrent.UpdatePlay();

        if (newUnitPlay != unitStateCurrent)
        {
            SetNewUnitPlay(newUnitPlay);
        }
    }


}
