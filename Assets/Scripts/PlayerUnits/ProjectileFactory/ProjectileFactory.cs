using UnityEngine;

public class ProjectileFactory
{
    private GameObject arrow;
    private int damageAttack;
    public ProjectileFactory(GameObject arrowPrefab, int attack)
    {
        arrow = arrowPrefab;
        damageAttack = attack;

    }

    public GameObject CreateArrow(Transform trans, float speed)
    {
        GameObject go = Object.Instantiate(arrow, trans.position, trans.rotation);
        go.GetComponent<ArcherArrow>().InitArcherArrow(damageAttack, speed);
        return go;
    }
}
