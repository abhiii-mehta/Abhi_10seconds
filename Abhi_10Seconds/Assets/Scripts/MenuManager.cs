using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject soundPanel;
    private bool soundPanelVisible = false;
    public void ToggleSoundPanel()
    {
        soundPanelVisible = !soundPanelVisible;
        soundPanel.SetActive(soundPanelVisible);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("CreditsScene");
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
    }

}
