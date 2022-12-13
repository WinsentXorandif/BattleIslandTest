using System.Collections;
using UnityEngine;

public class ArcherArrow : MonoBehaviour
{
    private const float DESTROY_TIME = 2f;
    private const float LIFE_TIME = 60f;

    private int attackDamage;
    private Rigidbody arrowbody;
    private Collider coll;
    private bool IsHIT;

    private void Awake()
    {
        coll = GetComponent<Collider>();
        arrowbody = GetComponent<Rigidbody>();
    }

    public void InitArcherArrow(int attack, float speed)
    {
        attackDamage = attack;
        arrowbody.velocity = transform.forward * speed;

        IsHIT = false;
        StartCoroutine(ArrowFly());
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsHIT = true;
        StopCoroutine(ArrowFly());

        if (collision.collider.TryGetComponent<Unit>(out var unit))
        {
            unit.OnDamage(attackDamage);
            transform.parent = unit.gameObject.GetComponentInChildren<ArrowHitParent>().transform;
        }

        Destroy(coll);
        Destroy(arrowbody);

        Destroy(gameObject, DESTROY_TIME);

    }

    IEnumerator ArrowFly()
    {
        float lifeTimer = 0.0f;

        while (lifeTimer < LIFE_TIME)
        {
            if (!IsHIT)
            {
                transform.rotation = Quaternion.LookRotation(arrowbody.velocity, Vector3.up);
            }
            lifeTimer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

}
