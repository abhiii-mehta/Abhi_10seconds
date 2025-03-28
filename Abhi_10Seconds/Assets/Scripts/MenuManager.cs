using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject soundPanel;
    public GameObject creditsPanel;
    public GameObject mainMenuPanel;
    public GameObject categoryPanel;

    private bool soundPanelVisible = false;
    private bool creditsPanelVisible = false;
    private bool categoryPanelVisible = false;

    // ====== PANEL TOGGLE FUNCTIONS ======

    public void ToggleSoundPanel()
    {
        soundPanelVisible = !soundPanelVisible;
        soundPanel.SetActive(soundPanelVisible);
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

    public void CloseSoundPanel()
    {
        soundPanelVisible = false;
        soundPanel.SetActive(false);
    }

    public void OpenCategoryPanel()
    {
        categoryPanelVisible = true;
        categoryPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void BackToMainMenu()
    {
        categoryPanelVisible = false;
        categoryPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // ====== BUTTON ACTIONS ======

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    // ====== ESCAPE INPUT ======

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
        else if (categoryPanelVisible && Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMainMenu();
        }
    }
}
