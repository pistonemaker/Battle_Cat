using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePopup : Singleton<LosePopup>
{
    public Button okButton;
    
    private void OnEnable()
    {
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
