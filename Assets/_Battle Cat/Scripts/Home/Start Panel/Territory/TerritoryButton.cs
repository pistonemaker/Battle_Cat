using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TerritoryButton : MonoBehaviour
{
    public TerritoryData data;
    public Button button;
    public TextMeshProUGUI territoryName;
    public TextMeshProUGUI energyCostText;
    public TextMeshProUGUI clearCountText;

    public void Init(TerritoryData territoryData)
    {
        data = territoryData;
        territoryName.text = data.territoryName;
        energyCostText.text = data.energyCost.ToString();
        clearCountText.text = data.clearCount.ToString();
        
    }
    
    private void OnEnable()
    {
        button = GetComponent<Button>();
        territoryName = transform.Find("Territory Name").GetComponent<TextMeshProUGUI>();
        energyCostText = transform.Find("Energy Cost Text").GetComponent<TextMeshProUGUI>();
        clearCountText = transform.Find("Clear Count Text").GetComponent<TextMeshProUGUI>();
        button.onClick.AddListener(LoadGamePlay);
    }

    private void LoadGamePlay()
    {
        
    }
}
