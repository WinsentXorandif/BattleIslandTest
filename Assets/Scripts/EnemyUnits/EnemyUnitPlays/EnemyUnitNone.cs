public class EnemyUnitNone : IEnemyUnitPlay
{
    public void BeginPlay()
    {

    }

    public void EndPlay()
    {

    }

    public EnemyUnitStates UpdatePlay()
    {
        //animator.Play("Idle");
        return EnemyUnitStates.None;
    }
}
