using UnityEngine;

public class UnitsFactory
{
    public PlayerUnitPlay CreatePlayerUnit(GameObject prefab, Transform trans)
    {
        return Object.Instantiate(prefab, trans.position, trans.rotation).GetComponent<PlayerUnitPlay>();
    }

    public EnemyUnitPlay CreateEnemyUnit(GameObject prefab, Transform trans)
    {
        return Object.Instantiate(prefab, trans.position, trans.rotation).GetComponent<EnemyUnitPlay>();
    }

    public BoatEnemy CreateBoatEnemy(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        return Object.Instantiate(prefab, pos, rot).GetComponent<BoatEnemy>();
    }

}
