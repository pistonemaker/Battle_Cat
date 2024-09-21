using UnityEngine;

public class OurBase : Base
{
    public BaseMoney baseMoney;
    public BaseLazeGun baseLazeGun;

    protected override void Awake()
    {
        base.Awake();
        baseLazeGun.Init();
        baseMoney.Init();
    }
    
    public bool SpawnEntity(CatData catData)
    {
        if (!canSpawn)
        {
            Debug.LogError("Can Not Spawn!");
            return false;
        }
        
        if (baseMoney.CanPay(catData.summonCost))
        {
            var cat = PoolingManager.Spawn(catData.cat, spawnPos.position, Quaternion.identity);
            cat.transform.Rotate(new Vector3(0f, -180f, 0f));
            entities.Add(cat);
            cat.faceDirection = -1;
            cat.SetBases(this, opponentBase);
            GetSumPower();
            return true;
        }
        
        
        Debug.Log("Not Enough Money To Summon Cat!!!");
        return false;
    }
}
