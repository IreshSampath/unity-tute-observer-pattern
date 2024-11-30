using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Configurable values to set the starting score and lives, editable in the Inspector
    [SerializeField] int _startedScore = 0; // Default score when the game starts or resets
    [SerializeField] int _startedLives = 3; // Default number of lives when the game starts or resets

    // Private variables to track the current game state
    int _currentScore = 0; // Tracks the player's current score
    int _currentLives = 0; // Tracks the player's remaining lives

    // Method to start the game
    public void StartGame()
    {
        ResetGame(); // Resets the game state to default values
        GameEvents.RaiseOnGameStarted(); // Raises an event to notify that the game has started
    }

    // Method to increment the player's score
    public void IncrementScore()
    {
        _currentScore++; // Increase the score by 1
        GameEvents.RaiseOnScored(_currentScore); // Raises an event to notify listeners of the new score
    }

    // Method to reduce the player's lives
    public void ReduceLives()
    {
        _currentLives--; // Decrease the player's lives by 1

        // If the player has no lives left, end the game
        if (_currentLives <= 0)
        {
            GameEnd(); // Calls the method to handle game-over logic
        }
        else
        {
            GameEvents.RaiseOnColliderHitted(_currentLives); // Raises an event to notify listeners of the remaining lives
        }
    }

    // Method to handle returning to the home screen
    public void GoHome()
    {
        ResetGame(); // Resets the game state to default values
        GameEvents.RaiseOnHomeTriggered(); // Raises an event to notify that the game has returned to the home state
    }

    // Method to reset the game state to default values
    void ResetGame()
    {
        _currentScore = _startedScore; // Resets the current score to the starting score
        _currentLives = _startedLives; // Resets the current lives to the starting lives

        GameEvents.RaiseOnGameReset(_currentScore, _currentLives); // Raises an event to notify that the game has been reset
    }

    // Method to handle game-over logic
    void GameEnd()
    {
        GameEvents.RaiseOnGameEnded(); // Raises an event to notify that the game has ended
    }
}
