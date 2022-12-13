using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public Action OnWin;
    public Action OnLoose;

    [SerializeField]
    private GamePlayData gameData;

    [Header("Players Units position")]
    [SerializeField]
    private Transform playersUnitPosition;

    [Header("Enemy Units position")]
    [SerializeField]
    private Transform enemysUnitPosition;

    [Header("Enemy Units position")]
    [SerializeField]
    private Transform[] enemysBoatPosition;

    private int enemyWaves;

    private UnitsFactory unitsFactory;

    private List<PlayerUnitPlay> playerUnitsList = new();
    private List<EnemyUnitPlay> enemyUnitsList = new();

    private List<HouseHeals> houseList = new();

    private BoatEnemy newBoat;


    private bool START_GAME;

    #region UnitsCreate
    private void CreatePlayerUnits()
    {
        for (int i = 0; i < gameData.playerBattleUnits.Length; i++)
        {
            for (int a = 0; a < gameData.playerBattleUnits[i].countBattleUnits; a++)
            {
                PlayerUnitPlay go = unitsFactory.CreatePlayerUnit(gameData.playerBattleUnits[i].prefab, playersUnitPosition);
                playerUnitsList.Add(go);

                Vector3 newPos = playersUnitPosition.position;
                newPos.x += 1.5f;
                playersUnitPosition.position = newPos;

            }
        }
    }

    private void CreateEnemyUnits()
    {
        for (int i = 0; i < gameData.enemyBattleUnits.Length; i++)
        {
            for (int a = 0; a < gameData.enemyBattleUnits[i].countBattleUnits; a++)
            {
                EnemyUnitPlay go = unitsFactory.CreateEnemyUnit(gameData.enemyBattleUnits[i].prefab, enemysUnitPosition);
                enemyUnitsList.Add(go);

                Vector3 newPos = enemysUnitPosition.position;
                newPos.x += 1.5f;
                enemysUnitPosition.position = newPos;
            }
        }
    }

    private EnemyUnitPlay CreateEnemyUnitsBoat(Transform enemyBoatTr)
    {
        EnemyUnitPlay go = unitsFactory.CreateEnemyUnit(gameData.enemyBattleUnits[0].prefab, enemyBoatTr);
        go.transform.parent = enemyBoatTr;
        return go;
    }

    private Vector3 TargetBoatPoint()
    {
        float minDistance = 1000f;
        Transform nearestPoint = enemysBoatPosition[0];

        foreach (var t in enemysBoatPosition)
        {
            float distance = Vector3.Distance(t.position, Camera.main.transform.position);
            if (distance < minDistance) { minDistance = distance; nearestPoint = t; }
        }

        return nearestPoint.position;
    }


    private void CreateEnemyBoat(int countWave)
    {
        Vector3 targetPoint = TargetBoatPoint();
        Vector3 spawnPoint = targetPoint * 3f;

        newBoat = unitsFactory.CreateBoatEnemy(gameData.enemyBoatPrefab, spawnPoint, Quaternion.identity);



        newBoat.transform.LookAt(targetPoint);

        for (int i = 0; i < gameData.enemyWaves[countWave].EnemyCount; i++)
        {
            enemyUnitsList.Add(CreateEnemyUnitsBoat(newBoat.unitPoints[i]));
        }
        OnEnableEnemysActions();

        newBoat.Boating(targetPoint, gameData.enemyWaves[countWave].boatSpeed, enemyUnitsList);
    }

    #endregion


    public void StartGameOn()
    {
        CreatePlayerUnits();
        OnEnableActions();

        enemyWaves = 0;
        if (gameData.enemyWaves.Length > 0)
        {
            CreateEnemyBoat(enemyWaves);
        }

        START_GAME = true;

    }

    private void Awake()
    {
        START_GAME = false;
        unitsFactory = new UnitsFactory();
        houseList = FindObjectsOfType<HouseHeals>().ToList();
    }

    private void OnEnableEnemysActions()
    {
        for (int i = 0; i < enemyUnitsList.Count; i++)
        {
            enemyUnitsList[i].OnUnitDie += EnemyUnitDie;
        }
    }

    private void OnEnableActions()
    {
        for (int i = 0; i < playerUnitsList.Count; i++)
        {
            playerUnitsList[i].OnUnitDie += PlayerUnitDie;
            playerUnitsList[i].OnActivateUnit += PlayerDiActivateUnit;
        }

        for (int i = 0; i < houseList.Count; i++)
        {
            houseList[i].OnUnitDie += HouseUnitDie;
        }

    }

    private void HouseUnitDie(HouseHeals unit)
    {
        Debug.Log("PlayerUnitDie");
        houseList.Remove(unit);
        if (houseList.Count < 1)
        {
            Debug.Log("Pleyer LOSS!");
            START_GAME = false;
            OnLoose?.Invoke();
        }
    }

    private void PlayerUnitDie(PlayerUnitPlay unit)
    {
        Debug.Log("PlayerUnitDie");
        playerUnitsList.Remove(unit);
        if (playerUnitsList.Count < 1)
        {

            Debug.Log("Pleyer LOSS!");
            START_GAME = false;
            OnLoose?.Invoke();
        }
    }

    private void PlayerDiActivateUnit()
    {
        foreach (PlayerUnitPlay a in playerUnitsList)
        {
            a.ActivateUnit = false;
        }
    }

    private void NewEnemyWave()
    {
        enemyWaves++;

        if (gameData.enemyWaves.Length > enemyWaves)
        {
            CreateEnemyBoat(enemyWaves);
            return;
        }

        Debug.Log("Victory!!!!!");
        START_GAME = false;
        OnWin?.Invoke();
    }

    private void EnemyUnitDie(EnemyUnitPlay unit)
    {
        enemyUnitsList.Remove(unit);
        if (enemyUnitsList.Count < 1)
        {
            enemyUnitsList.Clear();
            Destroy(newBoat.gameObject);
            NewEnemyWave();
        }
    }


    private void OnDisable()
    {
        for (int i = 0; i < playerUnitsList.Count; i++)
        {
            playerUnitsList[i].OnUnitDie -= PlayerUnitDie;
            playerUnitsList[i].OnActivateUnit -= PlayerDiActivateUnit;
        }

        for (int i = 0; i < enemyUnitsList.Count; i++)
        {
            enemyUnitsList[i].OnUnitDie -= EnemyUnitDie;
        }

        for (int i = 0; i < houseList.Count; i++)
        {
            houseList[i].OnUnitDie -= HouseUnitDie;
        }

    }

    private void Update()
    {
        if (!START_GAME) return;

        try
        {
            foreach (PlayerUnitPlay a in playerUnitsList)
            {
                if (a != null)
                {
                    a.OnUpdate();
                }
            }

            foreach (EnemyUnitPlay b in enemyUnitsList)
            {
                if (b != null)
                {
                    b.OnUpdate();
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log($"e Update = {e.Message}");
        }

    }



}
