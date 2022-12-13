using UnityEngine;

public class ArcherUnit : PlayerUnitPlay
{
    private const float MAX_ANGLE_DEGREES = 40f;

    [SerializeField]
    private Transform arrowStartPoint;


    private ProjectileFactory projectileFactory;
    private float g = Physics.gravity.y;
    private float AngleInDegrees;


    private void Start()
    {
        projectileFactory = new ProjectileFactory(projectile, attack);
    }

    //  private Vector3 FindEnemyPoint()
    //  {
    //      float timeProjectile = (enemyCol.transform.position - transform.position).magnitude / projectileSpeed;
    //      Vector3 enemyNewPos = enemyCol.transform.position + enemyCol.GetComponent<Rigidbody>().velocity * timeProjectile;
    //      return enemyNewPos;
    //  }


    public void Shoot()
    {

        Vector3 enemyTarget = enemyCol.transform.position;
        enemyTarget.y -= 0.5f;

        Vector3 fromTo = enemyTarget - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);


        AngleInDegrees = Mathf.Floor(fromTo.magnitude * MAX_ANGLE_DEGREES / findRange);
        arrowStartPoint.localEulerAngles = new Vector3(-AngleInDegrees, 0f, 0f);

        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float AngleInRadians = AngleInDegrees * Mathf.PI / 180;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        projectileFactory.CreateArrow(arrowStartPoint, v);

    }

    protected override void OnAttack()
    {
        if (enemyCol != null)
        {
            Shoot();
        }
    }
}
