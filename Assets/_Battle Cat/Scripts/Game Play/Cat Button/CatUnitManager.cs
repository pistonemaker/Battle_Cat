using System.Collections.Generic;
using UnityEngine;

public class CatUnitManager : Singleton<CatUnitManager>
{
    public List<CatData> catData;
    public List<CatButton> catButtons;
    public CatButton catButtonPrefab;
    public Transform page1;
    public Transform page2;

    protected override void Awake()
    {
        base.Awake();
        
        Init();
    }

    public void Init()
    {
        GetCatInUse();
        CreateCatButtons();
    }

    private void GetCatInUse()
    {
        var catList = GameManager.Instance.data.cats;
        for (int i = 0; i < catList.Count; i++)
        {
            if (catList[i].hasOwn && catList[i].isUsed)
            {
                catData.Add(catList[i]);
            }
        }
    }

    private void CreateCatButtons()
    {
        for (int i = 0; i < catData.Count; i++)
        {
            var catButton = PoolingManager.Spawn(catButtonPrefab, transform.position, Quaternion.identity);
            
            if (i < 5)
            {
                catButton.transform.SetParent(page1);
            }
            else if (i < 10)
            {
                catButton.transform.SetParent(page2);
            }
            
            catButton.transform.localScale = Vector3.one;
            catButton.Init(catData[i]);
        }
    }
}
