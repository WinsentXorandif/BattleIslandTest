using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private UnitData unitData;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    protected NavMeshAgent navMeshAgent;
    [SerializeField]
    protected HealthBar healthBar;

    protected Collider coll;

    protected int hitPoint;
    protected int defence;
    protected int attack;
    protected float findRange;
    protected float attackRange;
    protected float attackRangeHouse;
    protected float speed;
    protected LayerMask enemyLayer;
    protected GameObject projectile;

    public Collider enemyCol { get; set; }
    public Vector3 moveTargetPos { get; set; }
    public Animator GetAnimator() { return animator; }
    public NavMeshAgent GetNavMeshAgent() { return navMeshAgent; }
    public float GetFindRange() { return findRange; }
    public float GetAttackRange() { return attackRange; }
    public float GetAttackRangeHouse() { return attackRangeHouse; }
    public float GetMoveSpeed() { return speed; }
    public LayerMask GetEnemyLayer() { return enemyLayer; }

    protected void InitUnitData()
    {
        coll = GetComponent<Collider>();
        hitPoint = unitData.GetHitPoint();
        healthBar.SetMaxHealth(hitPoint);
        defence = unitData.GetDefence();
        attack = unitData.GetAttack();
        findRange = unitData.GetFindTargetRange();
        attackRange = unitData.GetAttackRange();
        attackRangeHouse = unitData.GetHouseAttackRange();
        speed = unitData.GetMoveSpeed();
        enemyLayer = unitData.GetEnemyLayer();
        projectile = unitData.GetProjectile();
    }

    protected virtual void OnAttack() { }
    public virtual void OnDamage(int damage) { }

}
