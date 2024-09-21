using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyButton : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI moneyText;

    public void Init(int level, int money)
    {
        button = GetComponent<Button>();
        levelText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        moneyText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        button.onClick.AddListener(() => {EventDispatcher.Instance.PostEvent(EventID.Money_Game_LevelUp);});
        SetText(level, money);
    }
    
    private void OnDisable()
    {
        button.onClick.RemoveListener(() => {EventDispatcher.Instance.PostEvent(EventID.Money_Game_LevelUp);});
    }

    public void SetText(int level, int money)
    {
        levelText.text = "Level " + level;
        moneyText.text = money + " $";
    }
}
