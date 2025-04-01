using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    [System.Serializable]
    public class WordEntry
    {
        public string fullWord;
        public List<string> hints;
    }

    private BGMController bgmController;
    public TMP_Text hintText;
    public TMP_Text maskedWordText;
    public TMP_InputField inputField;
    public TMP_Text timerText;
    public TMP_Text feedbackText;

    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject victoryPanel;


    public GameObject restartButton_GameOver;
    public GameObject restartButton_Pause;

    public List<WordEntry> wordList = new List<WordEntry>();

    private List<WordEntry> shuffledList;
    private int currentWordIndex = 0;

    private WordEntry currentWord;
    private float timer = 10f;
    private bool isGameActive = false;
    
    private void Start()
    {
        bgmController = FindFirstObjectByType<BGMController>();

        SetupWordList();
        shuffledList = new List<WordEntry>(wordList);
        ShuffleWordList(shuffledList);
        currentWordIndex = 0;

        PickNextWord();

        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        isGameActive = true;
    }

    private void Update()
    {
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

        timer -= Time.deltaTime;
        timerText.text = timer.ToString("F1") + "s";
        bgmController.UpdatePitch(timer, 10f);

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

    private void SetupWordList()
    {
        wordList = new List<WordEntry>();

        switch (GameData.selectedCategory)
        {
            case WordCategory.Games:
                AddGameWords();
                break;
            case WordCategory.Animals:
                AddAnimalWords();
                break;
            case WordCategory.Countries:
                AddCountryWords();
                break;
            case WordCategory.Food:
                AddFoodWords();
                break;
            case WordCategory.Smart:
                AddSmartWords();
                break;
        }
    }

    private void AddGameWords()
    {
        string[,] data = {
            {"Mario", "Famous plumber with a red hat"},
            {"Zelda", "Princess from a legendary Nintendo series"},
            {"Link", "Hero who fights Ganon"},
            {"Sonic", "Blue hedgehog who runs fast"},
            {"Tetris", "Falling block puzzle game"},
            {"Portal", "Game with a gun that makes holes in space"},
            {"Creeper", "It goes \"sss...\" and explodes"},
            {"Pacman", "Eats dots, afraid of ghosts"},
            {"Pong", "First popular video game ever"},
            {"Snake", "Classic phone game, eats apples"},
            {"Lara", "Tomb Raider’s first name"},
            {"Doom", "Classic FPS with demons"},
            {"Halo", "Sci-fi shooter with Master Chief"},
            {"Kirby", "Cute pink puff who swallows enemies"},
            {"Sims", "Life simulation game"},
            {"Minecraft", "Blocky sandbox, build anything"},
            {"Amongus", "Who’s the imposter?"},
            {"Spyro", "Purple dragon who breathes fire"},
            {"Ryu", "Fights in Street Fighter"},
            {"Pikachu", "Iconic electric Pokémon"},
            {"Luigi", "Mario’s taller, greener brother"},
            {"Kratos", "Angry bald guy with a red tattoo"},
            {"Portalgun", "Shoots portals (from Portal)"},
            {"Flappy", "Very annoying bird that keeps dying"},
            {"Snake", "Solid or Liquid? Stealthy dude"}
        };
        for (int i = 0; i < data.GetLength(0); i++)
        {
            wordList.Add(new WordEntry { fullWord = data[i, 0], hints = new List<string> { data[i, 1] } });

        }
    }

    private void AddAnimalWords()
    {
        string[,] data = {
            {"Elephant", "Largest land animal"},
            {"Giraffe", "Tall animal with a long neck"},
            {"Penguin", "can't fly but wears tux"},
            {"Kangaroo", "Punches like Mike Tyson"},
            {"Dolphin", "Smart sea animal that clicks and jumps"},
            {"Cheetah", "I am Fast AF boi"},
            {"Gorilla", "Big primate, walks on knuckles"},
            {"Parrot", "Steals your words"},
            {"Octopus", "Eight"},
            {"Sloth", "Sid from Ice Age"},
            {"Tiger", "Orange cat with built in barcode"},
            {"Koala", "Tree hugger from Australia"},
            {"Whale", "Chonky sea opera singer"},
            {"Panda", "Dumb Bears"},
            {"Camel", "Desert Truck"},
            {"Ostrich", "Tall chicken that skipped leg day"},
            {"Zebra", "Horse in prison pajamas"},
            {"Crocodile", "Big reptile with powerful jaws"},
            {"Chimpanzee", "Intelligent relative of humans"},
            {"Bat", "covid"},
            {"Hedgehog", "Small spiky animal"},
            {"Seahorse", "Fish where males carry the babies"},
            {"Raccoon", "Trash Panda"},
            {"Meerkat", "Stands up to look out for danger"},
            {"Llama", "Fluffy and spits when annoyed"}
        };
        for (int i = 0; i < data.GetLength(0); i++)
        {
            wordList.Add(new WordEntry { fullWord = data[i, 0], hints = new List<string> { data[i, 1] } });

        }
    }

    private void AddCountryWords()
    {
        string[,] data = {
            {"Germany", "Oktoberfest"},
            {"Japan", "Land of the rising sun"},
            {"Brazil", "Amazon rainforest"},
            {"Canada", "Maple leaf on the flag"},
            {"Egypt", "Pyramids"},
            {"India", "Country with the Taj Mahal"},
            {"France", "Croissants"},
            {"Italy", "Mamma mia"},
            {"China", "Great Wall is here"},
            {"Australia", "Spiders"},
            {"Russia", "Largest country in the world"},
            {"Mexico", "Known for tacos and sombreros"},
            {"Spain", "Country of flamenco and paella"},
            {"Greece", "Birthplace of democracy and Olympics"},
            {"Norway", "Famous for fjords and Vikings"},
            {"Sweden", "IKEA and meatballs"},
            {"Argentina", "Famous for tango and Messi"},
            {"Thailand", "Known for spicy food and beaches"},
            {"Kenya", "Safari capital of the world"},
            {"SouthAfrica", "Home to Cape Town and Table Mountain"},
            {"Iceland", "Land of fire and ice"},
            {"Netherlands", "Windmills and tulips"},
            {"Portugal", "SUII"},
            {"Turkey", "Crossroads of Europe and Asia"},
            {"Switzerland", "Watches, chocolate, and neutrality"}
        };
        for (int i = 0; i < data.GetLength(0); i++)
        {
            wordList.Add(new WordEntry { fullWord = data[i, 0], hints = new List<string> { data[i, 1] } });

        }
    }

    private void AddFoodWords()
    {
        string[,] data = {
            {"Pizza", "Cheesy flatbread loved worldwide"},
            {"Burger", "Patty in a bun, often with fries"},
            {"Sushi", "Raw fish and rice from Japan"},
            {"Pancake", "Fluffy breakfast, great with syrup"},
            {"Popcorn", "Crunchy snack for movie nights"},
            {"Noodles", "Long, slurpy and served in bowls"},
            {"Taco", "Folded tortilla filled with spicy goodies"},
            {"Donut", "Ring-shaped, glazed and sweet"},
            {"Spaghetti", "Long pasta often served with meatballs"},
            {"Chocolate", "Sweet treat made from cacao"},
            {"Icecream", "Cold, creamy, comes in many flavors"},
            {"Curry", "Spicy dish from India"},
            {"Bread", "Basic food, often used for sandwiches"},
            {"Cheese", "Made from milk, often yellow"},
            {"Sandwich", "Meal between slices of bread"},
            {"Steak", "Grilled piece of meat"},
            {"Fries", "Thin, salty, golden potato sticks"},
            {"Muffin", "Sweet baked good, often with blueberries"},
            {"Apple", "Fruit a day to keep the doctor away"},
            {"Banana", "Yellow fruit monkeys love"},
            {"Soup", "Warm liquid meal"},
            {"Salad", "Healthy mix of veggies"},
            {"Kebab", "Meat on a stick or skewer"},
            {"Cookie", "Crunchy or chewy, with chocolate chips"},
            {"Hotdog", "Sausage in a bun, often with ketchup"}
        };
        for (int i = 0; i < data.GetLength(0); i++)
        {
            wordList.Add(new WordEntry { fullWord = data[i, 0], hints = new List<string> { data[i, 1] } });

        }
    }

    private void AddSmartWords()
    {
        wordList.Add(new WordEntry
        {
            fullWord = "Theory",
            hints = new List<string> {
        "Explains how something works",
        "Often scientific",
        "Like 'Big Bang' or 'Relativity'"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Fusion",
            hints = new List<string> {
        "Combining two things into one",
        "Happens in the sun",
        "Opposite of fission"
    }
        });
        
        wordList.Add(new WordEntry
        {
            fullWord = "Echo",
            hints = new List<string> {
        "You say it, it comes back",
        "Sound reflection",
        "Common in canyons"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Orbit",
            hints = new List<string> {
        "Path around a planet/star",
        "The moon does this",
        "Related to gravity"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Fractal",
            hints = new List<string> {
        "Pattern inside a pattern",
        "Repeats itself at every scale",
        "Seen in nature (like snowflakes)"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Method",
            hints = new List<string> {
        "Step-by-step way of doing something",
        "Common in science",
        "Like a process"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Signal",
            hints = new List<string> {
        "Sends a message",
        "Can be wireless or visual",
        "Phones use it"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Matrix",
            hints = new List<string> {
        "Grid or system",
        "Also a sci-fi movie",
        "Used in math"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Static",
            hints = new List<string> {
        "Unchanging or electric crackle",
        "Opposite of dynamic",
        "Often on TVs or radios"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Vision",
            hints = new List<string> {
        "Sense of sight",
        "Also means a future goal",
        "You lose it without your eyes"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Logic",
            hints = new List<string> {
        "Makes sense",
        "Foundation of reasoning",
        "Used in math and computers"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Pattern",
            hints = new List<string> {
        "Repeats in a sequence",
        "You can predict it",
        "Seen in wallpapers, rhythms, code"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Source",
            hints = new List<string> {
        "Starting point",
        "Where something comes from",
        "Often cited in research"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Module",
            hints = new List<string> {
        "A part of something bigger",
        "Found in courses or code",
        "Can be plugged in/out"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Aspect",
            hints = new List<string> {
        "A feature or part of something",
        "One side of a situation",
        "Used in design and storytelling"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Factor",
            hints = new List<string> {
        "Something that influences a result",
        "Found in math and cause/effect",
        "Contributes to an outcome"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Context",
            hints = new List<string> {
        "Surrounding information",
        "Helps understanding",
        "Read in context"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Impulse",
            hints = new List<string> {
        "Sudden urge or motion",
        "Happens in physics and behavior",
        "Hard to resist"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Concept",
            hints = new List<string> {
        "An abstract idea",
        "The base of something",
        "Before a theory"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Impact",
            hints = new List<string> {
        "A strong effect",
        "Can be physical or metaphorical",
        "Like a crash or influence"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Balance",
            hints = new List<string> {
        "Not too much or too little",
        "Physical or metaphorical",
        "Needed in life, money, and yoga"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Reason",
            hints = new List<string> {
        "Why something happens",
        "Based on facts or logic",
        "We ask for it all the time"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Density",
            hints = new List<string> {
        "Compactness of matter",
        "Mass per volume",
        "Oil floats on water because of this"
    }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Network",
            hints = new List<string> {
        "System of connections",
        "Internet is one",
        "Also social or neural"
    }
        });

    }


    private void PickNextWord()
    {
        bgmController.PlayFromStart();

        if (currentWordIndex >= shuffledList.Count)
        {
            ShowVictoryPanel();
            return;
        }

        currentWord = shuffledList[currentWordIndex];
        currentWordIndex++;

        if (GameData.selectedCategory == WordCategory.Smart && currentWord.hints.Count > 1)
        {
            hintText.text = "Hints:\n- " + string.Join("\n- ", currentWord.hints);
        }
        else
        {
            hintText.text = "Hint: " + currentWord.hints[0];
        }

        maskedWordText.text = MaskWord(currentWord.fullWord);
        timer = 10f;
    }

    private string MaskWord(string word)
    {
        char[] masked = word.ToCharArray();

        int numToHide = GameData.selectedCategory == WordCategory.Smart
            ? Mathf.Clamp(word.Length - 2, 3, word.Length - 1)
            : Mathf.Clamp(word.Length / 2, 1, word.Length - 1);

        List<int> indices = new List<int>();
        for (int i = 0; i < word.Length; i++) indices.Add(i);

        for (int i = 0; i < numToHide; i++)
        {
            int randIndex = Random.Range(0, indices.Count);
            int charIndex = indices[randIndex];
            indices.RemoveAt(randIndex);
            masked[charIndex] = '_';
        }

        return string.Join(" ", masked).ToUpper();

    }

    private void EndGame()
    {
       
        isGameActive = false;
        Time.timeScale = 1f;

        feedbackText.text = $"Time's up! The word was: {currentWord.fullWord.ToUpper()}";
        bgmController.Stop();
        StartCoroutine(ShowGameOverAfterDelay(2f));
    }

    private IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
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

    private void ShowVictoryPanel()
    {
        isGameActive = false;
        Time.timeScale = 1f;
        victoryPanel.SetActive(true);
    }

    public void PlayAnotherCategory()
    {
        GameData.returningToCategory = true;
        SceneManager.LoadScene("MainMenu");
    }


}
