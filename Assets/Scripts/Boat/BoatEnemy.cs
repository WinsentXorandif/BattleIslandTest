using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;


public class BoatEnemy : MonoBehaviour
{

    public Transform[] unitPoints;

    [SerializeField]
    public Transform pushEnemy;



    public async void Boating(Vector3 targetPoint, float speed, List<EnemyUnitPlay> enemys)
    {

        try
        {
            await transform.DOMove(targetPoint, speed).AsyncWaitForCompletion();

            foreach (var enemyUnit in enemys)
            {
                if (enemyUnit != null)
                {
                    await enemyUnit.transform.DOMove(pushEnemy.position, 1f).AsyncWaitForCompletion();

                    enemyUnit.transform.parent = null;
                    enemyUnit.GetNavMeshAgent().enabled = true;
                    enemyUnit.SetNewUnitPlay(EnemyUnitStates.Move);
                }
            }

        }
        catch (Exception e)
        {
            Debug.Log($"e = {e.Message}");
        }

        //Destroy(gameObject);

    }


}
