using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [System.Serializable]
    public class WordEntry
    {
        public string fullWord;
        public string hint;
    }

    [Header("UI References")]
    public TMP_Text hintText;
    public TMP_Text maskedWordText;
    public TMP_InputField inputField;
    public TMP_Text timerText;
    public TMP_Text feedbackText;

    public GameObject gameOverPanel;
    public GameObject pausePanel;

    public GameObject restartButton_GameOver;
    public GameObject restartButton_Pause;

    [Header("Word Data")]
    public List<WordEntry> wordList = new List<WordEntry>();

    private List<WordEntry> shuffledList;
    private int currentWordIndex = 0;

    private WordEntry currentWord;
    private float timer = 10f;
    private bool isGameActive = false;

    private void Start()
    {
        SetupWordList();
        shuffledList = new List<WordEntry>(wordList);
        ShuffleWordList(shuffledList);
        currentWordIndex = 0;

        PickNextWord();

        // Set all panels and game state
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        isGameActive = true;

        // No need to set buttons manually here if they're already active
    }

    private void Update()
    {
        // Toggle pause panel with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeSelf)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        if (!isGameActive) return;

        // Timer countdown
        timer -= Time.deltaTime;
        timerText.text = timer.ToString("F1") + "s";

        // Input checking
        string guess = inputField.text.Trim().ToLower();
        if (guess == currentWord.fullWord.ToLower())
        {
            feedbackText.text = "Correct!";
            CancelInvoke(nameof(ClearFeedback));
            Invoke(nameof(ClearFeedback), 1f);

            inputField.text = "";
            PickNextWord();
        }

        if (timer <= 0)
        {
            EndGame();
        }
    }

    private void PickNextWord()
    {
        if (currentWordIndex >= shuffledList.Count)
        {
            ShuffleWordList(shuffledList);
            currentWordIndex = 0;
        }

        currentWord = shuffledList[currentWordIndex];
        currentWordIndex++;

        hintText.text = "Hint: " + currentWord.hint;
        maskedWordText.text = MaskWord(currentWord.fullWord);
        timer = 10f;
    }

    private string MaskWord(string word)
    {
        char[] masked = word.ToCharArray();
        int numToHide = Mathf.Clamp(word.Length / 2, 1, word.Length - 1);

        List<int> indices = new List<int>();
        for (int i = 0; i < word.Length; i++) indices.Add(i);

        for (int i = 0; i < numToHide; i++)
        {
            int randIndex = Random.Range(0, indices.Count);
            int charIndex = indices[randIndex];
            indices.RemoveAt(randIndex);
            masked[charIndex] = '_';
        }

        return new string(masked);
    }

    private void EndGame()
    {
        isGameActive = false;
        Time.timeScale = 1f;
        gameOverPanel.SetActive(true);
        maskedWordText.text = "The word was: " + currentWord.fullWord;
    }

    public void RestartGame()
    {
        inputField.text = "";
        feedbackText.text = "";
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);

        ShuffleWordList(shuffledList);
        currentWordIndex = 0;
        PickNextWord();

        isGameActive = true;
        Time.timeScale = 1f;
    }

    public void RestartGameFromMenu()
    {
        RestartGame();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void ClearFeedback()
    {
        feedbackText.text = "";
    }

    private void ShuffleWordList(List<WordEntry> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            WordEntry temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private void SetupWordList()
    {
        wordList = new List<WordEntry>
        {
            new WordEntry { fullWord = "Mario", hint = "Famous plumber with a red hat" },
            new WordEntry { fullWord = "Zelda", hint = "Princess from a legendary Nintendo series" },
            new WordEntry { fullWord = "Link", hint = "Hero who fights Ganon" },
            new WordEntry { fullWord = "Sonic", hint = "Blue hedgehog who runs fast" },
            new WordEntry { fullWord = "Tetris", hint = "Falling block puzzle game" },
            new WordEntry { fullWord = "Portal", hint = "Game with a gun that makes holes in space" },
            new WordEntry { fullWord = "Creeper", hint = "It goes “sss...” and explodes" },
            new WordEntry { fullWord = "Pacman", hint = "Eats dots, afraid of ghosts" },
            new WordEntry { fullWord = "Pong", hint = "First popular video game ever" },
            new WordEntry { fullWord = "Snake", hint = "Classic phone game, eats apples" },
            new WordEntry { fullWord = "Lara", hint = "Tomb Raider’s first name" },
            new WordEntry { fullWord = "Doom", hint = "Classic FPS with demons" },
            new WordEntry { fullWord = "Halo", hint = "Sci-fi shooter with Master Chief" },
            new WordEntry { fullWord = "Kirby", hint = "Cute pink puff who swallows enemies" },
        };
    }
}
