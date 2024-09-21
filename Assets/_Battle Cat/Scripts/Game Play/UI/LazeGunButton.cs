using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LazeGunButton : MonoBehaviour
{
    public LazeGunData data;
    public int level = 1;
    public Image image;
    public Image mask;
    public Button button;
    public float waitTime;
    public bool canUse;

    public void Init()
    {
        image = GetComponent<Image>();
        mask = transform.GetChild(0).GetComponent<Image>();
        button = GetComponent<Button>();
        canUse = true;
        waitTime = data.waitTimes[level - 1];
        button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        if (canUse)
        {
            EventDispatcher.Instance.PostEvent(EventID.Use_Laze_Gun);
            canUse = false;
            mask.fillAmount = 1f;
            var maskColor = mask.color;
            maskColor.a = 0.2f;
            mask.color = maskColor;
            mask.gameObject.SetActive(true);

            var imageColor = image.color;
            imageColor.a = 0.6f;
            image.color = imageColor;

            mask.DOFillAmount(0f, waitTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                canUse = true;
                imageColor.a = 1f;
                image.color = imageColor;
                mask.gameObject.SetActive(false);
            });
        }
    }
}