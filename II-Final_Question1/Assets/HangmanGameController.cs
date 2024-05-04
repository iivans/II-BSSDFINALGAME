using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HangmanGameController : MonoBehaviour
{
    public GameObject[] hangmanPartPrefabs; // Array of hangman part prefabs
    public Transform[] spawnPoints; // Array of spawn points for hangman parts

    public TMP_InputField inputField;
    public TMP_Text wordDisplayText;
    public TMP_Text resultText;
    public Button guessButton;
    public Button restartButton;
    public TMP_Text lifeCounterText;
    public TMP_Dropdown difficultyDropdown;

    private List<string> easyWords = new List<string>() { "dog", "cat" };
    private List<string> hardWords = new List<string>() { "elephant", "giraffe" };

    private string targetWord;
    private List<char> correctGuesses = new List<char>();
    private int incorrectGuesses = 0;
    private int maxLives = 6; // Maximum number of lives

    private List<GameObject> hangmanParts = new List<GameObject>(); // List to store instantiated hangman parts

    void Start()
    {
        guessButton.onClick.AddListener(GuessLetter);
        restartButton.onClick.AddListener(RestartGame);

        // Initialize dropdown options
        List<string> options = new List<string>() { "Easy", "Hard" };
        difficultyDropdown.AddOptions(options);

        StartGame();
    }

    void StartGame()
    {
        // Set target word based on selected difficulty
        string difficulty = difficultyDropdown.options[difficultyDropdown.value].text;
        targetWord = (difficulty == "Easy") ? easyWords[Random.Range(0, easyWords.Count)] : hardWords[Random.Range(0, hardWords.Count)];

        // Update UI
        UpdateWordDisplay();
        UpdateLifeCounter();

        // Instantiate hangman parts at spawn points
        for (int i = 0; i < hangmanPartPrefabs.Length; i++)
        {
            GameObject part = Instantiate(hangmanPartPrefabs[i], spawnPoints[i].position, Quaternion.identity);
            part.SetActive(false);
            hangmanParts.Add(part);
        }
    }


    void GuessLetter()
    {
        // Get the guess from the input field
        string guess = inputField.text.ToLower();

        // Validate input (single character)
        if (guess.Length != 1 || !char.IsLetter(guess[0]))
        {
            // Display error message or ignore invalid input
            inputField.text = "";
            return;
        }

        // Check if the guessed letter is in the word
        char guessedLetter = guess[0];
        if (targetWord.Contains(guessedLetter))
        {
            // Add correct guess to the list and update display
            correctGuesses.Add(guessedLetter);
            UpdateWordDisplay();
        }
        else
        {
            // Incorrect guess, update hangman parts and life counter
            UpdateHangman();
            UpdateLifeCounter();
        }

        // Check win/lose conditions
        if (correctGuesses.Count == targetWord.Length)
        {
            // Win condition
            resultText.text = "You win!";
            guessButton.interactable = false;
        }
        else if (incorrectGuesses == maxLives)
        {
            // Lose condition
            resultText.text = "You lose! The word was: " + targetWord;
            guessButton.interactable = false;
        }

        // Clear input field after each guess
        inputField.text = "";
    }

    void UpdateWordDisplay()
    {
        // Display the word with asterisks for unrevealed letters
        string displayedWord = "";
        foreach (char letter in targetWord)
        {
            if (correctGuesses.Contains(letter))
            {
                displayedWord += letter;
            }
            else
            {
                displayedWord += "*";
            }
        }
        wordDisplayText.text = displayedWord;
    }

    void UpdateHangman()
    {
        // Display the next part of the hangman figure
        hangmanParts[incorrectGuesses].SetActive(true);
        incorrectGuesses++;
    }

    void UpdateLifeCounter()
    {
        // Display remaining life count
        int remainingLives = maxLives - incorrectGuesses;
        lifeCounterText.text = "Life: " + remainingLives;
    }

    void RestartGame()
    {
        // Reset game state
        correctGuesses.Clear();
        incorrectGuesses = 0;
        resultText.text = "";
        guessButton.interactable = true;

        // Reset life counter
        UpdateLifeCounter();

        // Deactivate all hangman parts
        foreach (GameObject part in hangmanParts)
        {
            part.SetActive(false);
        }

        StartGame();
    }
}