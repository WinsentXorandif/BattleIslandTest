using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitData", menuName = "New Unit Data")]
public class UnitData : ScriptableObject
{
    [SerializeField]
    private int unitHitPoint;
    [SerializeField]
    private int unitDefence;
    [SerializeField]
    private int unitAttack;
    [SerializeField]
    private float unitFindTargetRange;
    [SerializeField]
    private float unitAttackRange;
    [SerializeField]
    private float unitHouseAttackRange;
    [SerializeField]
    private float unitMoveSpeed;

    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private GameObject projectile;



    public int GetHitPoint()
    {
        return unitHitPoint;
    }
    public int GetDefence()
    {
        return unitDefence;
    }
    public int GetAttack()
    {
        return unitAttack;
    }
    public float GetFindTargetRange()
    {
        return unitFindTargetRange;
    }

    public float GetAttackRange()
    {
        return unitAttackRange;
    }

    public float GetHouseAttackRange()
    {
        return unitHouseAttackRange;
    }
    public float GetMoveSpeed()
    {
        return unitMoveSpeed;
    }

    public LayerMask GetEnemyLayer()
    {
        return enemyLayer;
    }

    public GameObject GetProjectile()
    {
        return projectile;
    }

}
