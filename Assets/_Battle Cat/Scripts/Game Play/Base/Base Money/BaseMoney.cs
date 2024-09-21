using TMPro;
using UnityEngine;

public class BaseMoney : MonoBehaviour
{
    public MoneyButton moneyButton;
    public MoneyData data;
    public TextMeshProUGUI moneyText;
    public int level;
    public int minilevel;
    public float timeToUp1Money;
    public float moneyTime;
    public int moneyToLevelUp;
    public int maxMoney;
    public int curMoney;
    
    public bool canLevelUp;
    
    public void Init()
    {
        level = 1;
        minilevel = 1;
        moneyTime = 0f;
        GetData();
        moneyButton.Init(level, moneyToLevelUp);
    }

    private void GetData()
    {
        var miniMoneylevel = data.moneyLevel[level - 1].miniMoneyLevels[minilevel - 1];
        timeToUp1Money = miniMoneylevel.timeToUp1Money;
        moneyToLevelUp = miniMoneylevel.moneyToLevelUp;
        maxMoney = miniMoneylevel.maxMoney;
        canLevelUp = miniMoneylevel.canLevelUp;
    }
    
    private void OnEnable()
    {
        this.RegisterListener(EventID.Summon_Cat, param => OnSummonCat((int) param));
        EventDispatcher.Instance.RegisterListener(EventID.Money_Game_LevelUp, OnCheckLevelUp);
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.Summon_Cat, param => OnSummonCat((int) param));
        EventDispatcher.Instance.RemoveListener(EventID.Money_Game_LevelUp, OnCheckLevelUp);
    }

    private void Update()
    {
        CheckAddMoney();
    }

    private void CheckAddMoney()
    {
        if (moneyTime < timeToUp1Money)
        {
            moneyTime += Time.deltaTime;
            return;
        }

        if (curMoney >= maxMoney)
        {
            curMoney = maxMoney;
            return;
        }
        
        moneyTime -= timeToUp1Money;
        curMoney++;
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = curMoney + "/" + data.moneyLevel[level - 1].miniMoneyLevels[minilevel - 1].maxMoney;
    }

    private void OnSummonCat(int cost)
    {
        if (CanPay(cost))
        {
            curMoney -= cost;
            UpdateMoneyText();
        }
    }

    private void OnCheckLevelUp(object param)
    {
        if (CanPay(moneyToLevelUp) && canLevelUp)
        {
            curMoney -= moneyToLevelUp;
            LevelUp();
            moneyButton.SetText(minilevel, moneyToLevelUp);
        }
    }

    private void LevelUp()
    {
        minilevel++;
        GetData();
        UpdateMoneyText();
    }

    public bool CanPay(int cost)
    {
        return curMoney >= cost;
    }
}
