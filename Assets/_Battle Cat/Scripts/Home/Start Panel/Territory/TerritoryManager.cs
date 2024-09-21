using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerritoryManager : Singleton<TerritoryManager>
{
    public TerritoryUnitsData data;
    public TerritoryButton territoryButtonPrefab;
    public Transform territoryTrf;
    public ScrollRect scrollRect;
    public float zoomFactor = 1.5f; 
    public float maxDistance = 200f; 
    public List<RectTransform> territories;
    
    public void Init()
    {
        CreateTerritory();
    }
    
    private void OnEnable()
    {
        Init();
    }

    private void CreateTerritory()
    {
        for (int i = 0; i < data.territoryUnitsData.Count; i++)
        {
            var territory = PoolingManager.Spawn(territoryButtonPrefab, transform.position, Quaternion.identity);
            territory.transform.SetParent(territoryTrf);
            territory.transform.localScale = Vector3.one;
            territory.Init(data.territoryUnitsData[i]);
            territories.Add(territory.GetComponent<RectTransform>());
        }
    }

    private void Update()
    {
        // Vị trí giữa của viewport (giữa màn hình)
        float centerPosition = scrollRect.viewport.rect.width / 2;

        // Duyệt qua tất cả các Territory và điều chỉnh scale dựa trên khoảng cách đến giữa
        foreach (RectTransform territory in territories)
        {
            // Lấy vị trí X của Territory so với viewport
            Vector3 viewportPos = scrollRect.viewport.InverseTransformPoint(territory.position);
            float distance = Mathf.Abs(viewportPos.x - centerPosition);

            // Tính toán tỉ lệ scale dựa trên khoảng cách đến giữa
            float scale = Mathf.Clamp(zoomFactor - (distance / maxDistance) * (zoomFactor - 1), 1, zoomFactor);

            // Cập nhật scale của Territory
            territory.localScale = new Vector3(scale, scale, 1);
        }
    }
}
