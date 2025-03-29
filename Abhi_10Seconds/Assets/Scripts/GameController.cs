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
        public List<string> hints;
    }

    public TMP_Text hintText;
    public TMP_Text maskedWordText;
    public TMP_InputField inputField;
    public TMP_Text timerText;
    public TMP_Text feedbackText;

    public GameObject gameOverPanel;
    public GameObject pausePanel;

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
            {"Penguin", "Bird that can't fly, wears a tuxedo"},
            {"Kangaroo", "Jumps high, carries baby in a pouch"},
            {"Dolphin", "Smart sea animal that clicks and jumps"},
            {"Cheetah", "Fastest land animal"},
            {"Gorilla", "Big primate, walks on knuckles"},
            {"Parrot", "Colorful bird that can mimic speech"},
            {"Octopus", "Has eight arms, lives in the sea"},
            {"Sloth", "Super slow animal that lives in trees"},
            {"Tiger", "Big cat with orange and black stripes"},
            {"Koala", "Tree hugger from Australia"},
            {"Whale", "Ocean giant, breathes through blowhole"},
            {"Panda", "Eats bamboo, black and white fur"},
            {"Camel", "Has humps, lives in deserts"},
            {"Ostrich", "World's largest bird, can't fly"},
            {"Zebra", "Black and white striped animal"},
            {"Crocodile", "Big reptile with powerful jaws"},
            {"Chimpanzee", "Intelligent relative of humans"},
            {"Bat", "The only flying mammal"},
            {"Hedgehog", "Small spiky animal"},
            {"Seahorse", "Fish where males carry the babies"},
            {"Raccoon", "Night bandit with a mask-like face"},
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
            {"Germany", "Country known for Oktoberfest"},
            {"Japan", "Land of the rising sun"},
            {"Brazil", "Home of the Amazon rainforest"},
            {"Canada", "Maple leaf on the flag"},
            {"Egypt", "Famous for pyramids"},
            {"India", "Country with the Taj Mahal"},
            {"France", "Known for Eiffel Tower and croissants"},
            {"Italy", "Birthplace of pizza and pasta"},
            {"China", "Great Wall is here"},
            {"Australia", "Kangaroos and koalas live here"},
            {"Russia", "Largest country in the world"},
            {"Mexico", "Known for tacos and sombreros"},
            {"Spain", "Country of flamenco and paella"},
            {"Greece", "Birthplace of democracy and Olympics"},
            {"Norway", "Famous for fjords and Vikings"},
            {"Sweden", "IKEA, ABBA, and meatballs"},
            {"Argentina", "Famous for tango and Messi"},
            {"Thailand", "Known for spicy food and beaches"},
            {"Kenya", "Safari capital of the world"},
            {"SouthAfrica", "Home to Cape Town and Table Mountain"},
            {"Iceland", "Land of fire and ice"},
            {"Netherlands", "Windmills and tulips"},
            {"Portugal", "Next to Spain, loves custard tarts"},
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
            fullWord = "Algorithm",
            hints = new List<string> {
            "Step-by-step solution",
            "Like a recipe for computers",
            "Used in programming and AI"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Paradox",
            hints = new List<string> {
            "A logical contradiction",
            "Seems false but may be true",
            "Like 'This statement is false'"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Enigma",
            hints = new List<string> {
            "Something mysterious or puzzling",
            "Hard to understand",
            "Also a famous WW2 code machine"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Labyrinth",
            hints = new List<string> {
            "Complex maze",
            "Twisting paths, hard to escape",
            "Found in Greek mythology"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Cipher",
            hints = new List<string> {
            "Secret code",
            "Used in encryption",
            "Requires a key to decode"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Obsidian",
            hints = new List<string> {
            "Volcanic glass",
            "Black and shiny",
            "Used in crafting in Minecraft"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Chronology",
            hints = new List<string> {
            "Order of events",
            "Timeline-based",
            "Used in history and storytelling"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Quantum",
            hints = new List<string> {
            "Smallest unit in physics",
            "Subatomic particle theory",
            "Often used in sci-fi and tech"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Aesthetic",
            hints = new List<string> {
            "Related to beauty",
            "Often about visual style",
            "Used in design and art"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Hypothesis",
            hints = new List<string> {
            "Educated guess",
            "Starting point for experiments",
            "Needs testing to confirm"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Oxymoron",
            hints = new List<string> {
            "Two contradictory terms",
            "Like 'jumbo shrimp' or 'deafening silence'",
            "Common in poetry and humor"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Epitome",
            hints = new List<string> {
            "Perfect example of something",
            "Ideal representation",
            "Like the poster child of something"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Synthesis",
            hints = new List<string> {
            "Combining different elements",
            "Opposite of analysis",
            "Happens in writing and chemistry"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Catalyst",
            hints = new List<string> {
            "Triggers change",
            "Speeds up a reaction",
            "Doesn’t get consumed"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Obsolete",
            hints = new List<string> {
            "No longer useful",
            "Outdated technology",
            "Like floppy disks or VHS tapes"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Mirage",
            hints = new List<string> {
            "Optical illusion",
            "Seen in deserts",
            "Looks real but isn’t"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Binary",
            hints = new List<string> {
            "Made of 1s and 0s",
            "Digital language",
            "Has only two states"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Pinnacle",
            hints = new List<string> {
            "Highest point",
            "Top of a career or achievement",
            "Like reaching a mountain peak"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Quandary",
            hints = new List<string> {
            "Difficult situation",
            "Confusing dilemma",
            "Hard to make a decision"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Nostalgia",
            hints = new List<string> {
            "Sentimental longing for the past",
            "Triggered by old songs or places",
            "Often a warm or bittersweet feeling"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Ambiguous",
            hints = new List<string> {
            "Open to multiple meanings",
            "Not clearly defined",
            "Can lead to misinterpretation"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Nebula",
            hints = new List<string> {
            "Cloud of gas in space",
            "Birthplace of stars",
            "Often seen in deep space photos"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Conundrum",
            hints = new List<string> {
            "A confusing or difficult problem",
            "No obvious solution",
            "Like a riddle or puzzle"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Constellation",
            hints = new List<string> {
            "Group of stars forming a pattern",
            "Often named after myths",
            "Used in astrology and navigation"
        }
        });
        wordList.Add(new WordEntry
        {
            fullWord = "Parallax",
            hints = new List<string> {
            "Difference in viewpoint",
            "Used to measure distance in space",
            "Common in 3D games and astronomy"
        }
        });
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
}
