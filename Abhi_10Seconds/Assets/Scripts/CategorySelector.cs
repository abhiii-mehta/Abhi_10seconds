using UnityEngine;
using UnityEngine.SceneManagement;

public class CategorySelector : MonoBehaviour
{
    public void SelectCategory(int index)
    {
        GameData.selectedCategory = (WordCategory)index;
        SceneManager.LoadScene("GameScene");
    }

    public void GoBackToMainMenu(GameObject mainMenuPanel, GameObject categoryPanel)
    {
        mainMenuPanel.SetActive(true);
        categoryPanel.SetActive(false);
    }
}
