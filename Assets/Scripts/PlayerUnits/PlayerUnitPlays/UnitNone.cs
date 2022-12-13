public class UnitNone : IUnitPlay
{
    public void BeginPlay()
    {
    }

    public void EndPlay()
    {
    }

    public UnitStates UpdatePlay()
    {
        return UnitStates.None;
    }
}
