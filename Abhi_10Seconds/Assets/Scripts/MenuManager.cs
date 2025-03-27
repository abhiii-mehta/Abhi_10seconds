using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject soundPanel;
    private bool soundPanelVisible = false;

    public GameObject creditsPanel;
    private bool creditsPanelVisible = false;
    public void ToggleSoundPanel()
    {
        soundPanelVisible = !soundPanelVisible;
        soundPanel.SetActive(soundPanelVisible);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CloseSoundPanel()
    {
        soundPanelVisible = false;
        soundPanel.SetActive(false);
    }

    private void Update()
    {
        if (soundPanelVisible && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSoundPanel();
        }
        else if (creditsPanelVisible && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCreditsPanel();
        }
    }

    public void OpenCreditsPanel()
    {
        creditsPanelVisible = true;
        creditsPanel.SetActive(true);
    }

    public void CloseCreditsPanel()
    {
        creditsPanelVisible = false;
        creditsPanel.SetActive(false);
    }

}
