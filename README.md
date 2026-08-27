# Unity Hangman Game

Unity Hangman Game is a classic word-guessing project developed in Unity version 2022.3.17f1. The project features a clean user interface powered by TextMeshPro, dynamic difficulty levels, and a progressive visual penalty system.

## Project Overview

The application is controlled by the HangmanGameController script, which manages user input, validates guesses, tracks remaining lives, and evaluates win or lose conditions. Players can select a difficulty level using a dropdown menu, which determines whether the target word is drawn from an easy list containing words like "dog" and "cat" or a hard list containing words like "elephant" and "giraffe".

The game provides a maximum of six lives to the player. Each incorrect letter guess decreases the remaining life counter and progressively activates modular visual components of the hangman figure, including the body, right arm, and right leg prefabs. When the word is fully guessed or all lives are exhausted, the interface displays the final result and deactivates the guess button until the user restarts the game.

## Getting Started

To set up and run the project locally, open Unity Hub and choose the option to add a project from disk by selecting the repository folder. Ensure you open and run the project using Unity version 2022.3.17f1. Once loaded, navigate to the scenes folder within the assets directory to open the main game scene and begin playing.
