using UnityEngine;

public class UnitDie : IUnitPlay
{
    private bool IsPlay;
    private PlayerUnitPlay unit;

    private Animator animator;

    public UnitDie(PlayerUnitPlay currentUnit)
    {
        unit = currentUnit;
        animator = currentUnit.GetAnimator();
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

        animator.Play("Death");
        IsPlay = false;

        return UnitStates.None;
    }
}

