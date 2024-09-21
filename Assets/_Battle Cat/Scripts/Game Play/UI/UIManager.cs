using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private VictoryPopup victoryPopup;
    private LosePopup losePopup;
    public Button pauseButton;
    public Sprite continueSprite;
    public Sprite pauseSprite;

    private void OnEnable()
    {
        victoryPopup = VictoryPopup.Instance;
        losePopup = LosePopup.Instance;
        victoryPopup.gameObject.SetActive(false);
        losePopup.gameObject.SetActive(false);
        pauseButton.onClick.AddListener(ShowPausePopup);
        EventDispatcher.Instance.RegisterListener(EventID.On_Player_Win, OnPlayerWin);
        EventDispatcher.Instance.RegisterListener(EventID.On_Player_Lose, OnPlayerLose);
    }
    
    private void OnDisable()
    {
        
        pauseButton.onClick.RemoveAllListeners();
    }

    private void ShowPausePopup()
    {
        Time.timeScale = 0f;
        PausePopup.Instance.gameObject.SetActive(true);
        pauseButton.image.sprite = continueSprite;
    }

    public void HidePausePopup()
    {
        Time.timeScale = 1f;
        pauseButton.image.sprite = pauseSprite;
    }

    private void OnPlayerWin(object param)
    {
        victoryPopup.gameObject.SetActive(true);
    }

    private void OnPlayerLose(object param)
    {
        losePopup.gameObject.SetActive(true);
    }
}
