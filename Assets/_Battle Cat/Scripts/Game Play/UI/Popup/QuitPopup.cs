using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitPopup : Singleton<QuitPopup>
{
    public Button yesButton;
    public Button noButton;

    private void OnEnable()
    {
        yesButton.onClick.AddListener(LoadToMenu);
        noButton.onClick.AddListener(HidePopup);
    }

    private void OnDisable()
    {
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
    }

    private void LoadToMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    private void HidePopup()
    {
        gameObject.SetActive(false);
    }
}
