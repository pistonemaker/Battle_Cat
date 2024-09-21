using System.Collections.Generic;
using UnityEngine;

public class EnermyBase : Base
{
    public int playerPower;
    public List<MonsterData> monsterInLevel;
    public List<float> spawnTimes;

    private float spawnTimer = 0f;
    private float spawnInterval = 6f;
    private int idRate;

    private void OnEnable()
    {
        monsterInLevel = GameManager.Instance.data.monsters;
        SortByPower();
        EventDispatcher.Instance.RegisterListener(EventID.Summon_Cat, RecountPower);
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        AdjustSpawnInterval();
        
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer -= spawnInterval;
            CheckAndSpawnMonster();
        }
    }

    private void CheckAndSpawnMonster()
    {
        var monsterData = FindSuitableMonster();
        
        if (monsterData != null)
        {
            SpawnEntity(monsterData);
        }
    }

    public void SpawnEntity(MonsterData monsterData)
    {
        if (!canSpawn)
        {
            Debug.LogError("Can Not Spawn!");
            return;
        }
        
        var monster = PoolingManager.Spawn(monsterData.monster, spawnPos.position, Quaternion.identity);
        entities.Add(monster);
        GetSumPower();
    }

    private void SortByPower()
    {
        monsterInLevel.Sort((a, b) => a.monster.power.CompareTo(b.monster.power));
    }

    private void RecountPower(object param)
    {
        playerPower = opponentBase.power;
    }

    private void AdjustSpawnInterval()
    {
        idRate = (playerPower + 1)/ (power + 1);
        
        if (idRate >= spawnTimes.Count)
        {
            idRate = spawnTimes.Count - 1;
        }

        spawnInterval = spawnTimes[idRate];
    }

    private MonsterData FindSuitableMonster()
    {
        if (monsterInLevel.Count == 1)
        {
            return monsterInLevel[0];
        }

        int index = Mathf.Clamp(idRate, 0, monsterInLevel.Count - 1);
        return monsterInLevel[index];
    }
}