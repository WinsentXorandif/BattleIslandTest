using System;
using UnityEngine;

public class HouseHeals : MonoBehaviour
{
    public Action<HouseHeals> OnUnitDie;

    [SerializeField]
    private int hitPoint;

    [SerializeField]
    private HealthBar healthBar;


    private void Start()
    {
        healthBar.SetMaxHealth(hitPoint);
    }


    public void OnDamage(int damage)
    {
        hitPoint -= damage;
        healthBar.SetHealth(hitPoint);
        if (hitPoint <= 0)
        {
            OnUnitDie?.Invoke(this);
            Destroy(gameObject);
        }
    }


}
