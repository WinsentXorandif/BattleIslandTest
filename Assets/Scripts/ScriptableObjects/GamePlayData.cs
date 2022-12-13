using System;
using UnityEngine;


[CreateAssetMenu(fileName = "NewGamePlayData", menuName = "New GamePlay Data")]
public class GamePlayData : ScriptableObject
{
    private const int MIN_ENEMY_UNIT_BOAT = 1;
    private const int MAX_ENEMY_UNIT_BOAT = 6;

    private const float MIN_BOAT_SPEED = 15;
    private const float MAX_BOAT_SPEED = 30;


    [Header("Player Units")]
    [SerializeField]
    public BattleUnits[] playerBattleUnits;

    [Header("Enemy Units")]
    [SerializeField]
    public BattleUnits[] enemyBattleUnits;


    [Serializable]
    public struct BattleUnits
    {
        public TypeUnits typeUnits;
        public GameObject prefab;
        public int countBattleUnits;
    }


    [SerializeField]
    public GameObject enemyBoatPrefab;


    [SerializeField]
    public BattleWaves[] enemyWaves;

    [Serializable]
    public struct BattleWaves
    {
        [Range(MIN_ENEMY_UNIT_BOAT, MAX_ENEMY_UNIT_BOAT)]
        public int EnemyCount;
        [Range(MIN_BOAT_SPEED, MAX_BOAT_SPEED)]
        public float boatSpeed;
    }


}


