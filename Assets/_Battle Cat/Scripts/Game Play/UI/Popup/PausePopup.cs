using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePopup : Singleton<PausePopup>
{
    public Button closeButton;
    public Button musicButton;
    public Button soundButton;
    public Button restartButton;
    public Button escapeButton;

    public Sprite musicOn;
    public Sprite musicOff;
    public Sprite soundOn;
    public Sprite soundOff;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(ClosePopup);
        musicButton.onClick.AddListener(ChangeMusicSprite);
        soundButton.onClick.AddListener(ChangeSoundSprite);
        restartButton.onClick.AddListener(ReloadGame);
        escapeButton.onClick.AddListener(ShowQuitPopup);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveAllListeners();
        musicButton.onClick.RemoveAllListeners();
        soundButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        escapeButton.onClick.RemoveAllListeners();
    }

    private void ClosePopup()
    {
        gameObject.SetActive(false);
        UIManager.Instance.HidePausePopup();
    }

    private void ChangeMusicSprite()
    {
        if (musicButton.image.sprite == musicOn)
        {
            musicButton.image.sprite = musicOff;
        }
        else
        {
            musicButton.image.sprite = musicOn;
        }
    }

    private void ChangeSoundSprite()
    {
        if (soundButton.image.sprite == soundOn)
        {
            soundButton.image.sprite = soundOff;
        }
        else
        {
            soundButton.image.sprite = soundOn;
        }
    }

    private void ReloadGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Game");
    }

    private void ShowQuitPopup()
    {
        QuitPopup.Instance.gameObject.SetActive(true);
    }
}
