using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatButton : MonoBehaviour
{
    public CatData catData;
    public Image image;
    public Image mask;
    public Button button;
    public TextMeshProUGUI costText;
    public int catid;
    public int cost;
    public float waitTime;
    public bool canSummon;

    public void Init(CatData data)
    {
        image = GetComponent<Image>();
        mask = transform.GetChild(0).GetComponent<Image>();
        costText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        button = GetComponent<Button>();
        catData = data;
        catid = data.id;
        cost = data.summonCost;
        waitTime = data.waitTime;
        costText.text = cost + "$";
        mask.gameObject.SetActive(false);
        canSummon = true;
        button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        if (GameManager.Instance.ourBase.SpawnEntity(catData) && canSummon)
        {
            this.PostEvent(EventID.Summon_Cat, cost);
            button.interactable = false;
            canSummon = false;
            mask.fillAmount = 1f;
            //var maskColor = mask.color;
            //maskColor.a = 0.2f;
            //mask.color = maskColor;
            mask.gameObject.SetActive(true);

            //var imageColor = image.color;
            //imageColor.a = 0.6f;
            //image.color = imageColor;

            mask.DOFillAmount(0f, waitTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                button.interactable = true;
                canSummon = true;
                //imageColor.a = 1f;
                //image.color = imageColor;
                //mask.gameObject.SetActive(false);
            });
        }
    }
}
