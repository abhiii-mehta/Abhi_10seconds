using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public GameObject restartButton;
    public TMP_Text feedbackText;

    [Header("Word Data")]
    public List<WordEntry> wordList = new List<WordEntry>();

    private WordEntry currentWord;
    private float timer = 10f;
    private bool isGameActive = false;

    private void Start()
    {
        SetupWordList();          
        PickRandomWord();         
        restartButton.SetActive(false); 
        isGameActive = true;     
    }

    private void Update()
    {
        if (!isGameActive) return;

        timer -= Time.deltaTime;
        timerText.text = timer.ToString("F1") + "s";

        string guess = inputField.text.Trim().ToLower();
        if (guess == currentWord.fullWord.ToLower())
        {
            feedbackText.text = "Correct!";
            CancelInvoke(nameof(ClearFeedback));
            Invoke(nameof(ClearFeedback), 1f);

            inputField.text = "";
            PickRandomWord(); 
        }

        if (timer <= 0)
        {
            EndGame();
        }
    }

    private void PickRandomWord()
    {
        int index = Random.Range(0, wordList.Count);
        currentWord = wordList[index];

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
        maskedWordText.text = "The word was: " + currentWord.fullWord;
        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        inputField.text = "";
        feedbackText.text = "";
        restartButton.SetActive(false);
        PickRandomWord();
        isGameActive = true;
    }

    private void ClearFeedback()
    {
        feedbackText.text = "";
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
            new WordEntry { fullWord = "Sims", hint = "Life simulation game" },
            new WordEntry { fullWord = "Minecraft", hint = "Blocky sandbox, build anything" },
            new WordEntry { fullWord = "Amongus", hint = "Who’s the imposter?" },
            new WordEntry { fullWord = "Spyro", hint = "Purple dragon who breathes fire" },
            new WordEntry { fullWord = "Ryu", hint = "Fights in Street Fighter" },
            new WordEntry { fullWord = "Pikachu", hint = "Iconic electric Pokémon" },
            new WordEntry { fullWord = "Luigi", hint = "Mario’s taller, greener brother" },
            new WordEntry { fullWord = "Kratos", hint = "Angry bald guy with a red tattoo" },
            new WordEntry { fullWord = "Portalgun", hint = "Shoots portals (from Portal)" },
            new WordEntry { fullWord = "Flappy", hint = "Very annoying bird that keeps dying" },
            new WordEntry { fullWord = "Snake", hint = "Solid or Liquid? Stealthy dude" },
        };
    }
}
