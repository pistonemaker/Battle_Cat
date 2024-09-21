using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryPopup : Singleton<VictoryPopup>
{
    public TextMeshProUGUI xpText;
    public Button okButton;

    private void OnEnable()
    {
        xpText.text = "Gain " + 1000 + " XP";
        okButton.onClick.AddListener(LoadToMenu);
    }

    private void OnDisable()
    {
        okButton.onClick.RemoveAllListeners();
    }

    private void LoadToMenu()
    {
        gameObject.SetActive(false);
        SceneManager.LoadSceneAsync("Menu");
    }
}
