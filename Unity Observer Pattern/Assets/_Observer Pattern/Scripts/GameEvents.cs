using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameEvents
{
    // Static events for different game states and actions.
    // These events allow other parts of the game to subscribe and react to game state changes.

    public static event Action OnGameStarted; // Event triggered when the game starts
    public static event Action<int> OnScored; // Event triggered when the player scores (int parameter represents the current score)
    public static event Action<int> OnColliderHitted; // Event triggered when the player hits a collider (int parameter represents remaining lives)
    public static event Action OnGameEnded; // Event triggered when the game ends
    public static event Action<int, int> OnGameReset; // Event triggered when the game resets (parameters: current score and lives)
    public static event Action OnHomeTriggered; // Event triggered when the home screen is shown (typically after a reset)

    // Method to raise the OnGameStarted event
    public static void RaiseOnGameStarted()
    {
        OnGameStarted?.Invoke(); // Invoke the OnGameStarted event if there are subscribers
        Debug.Log("Game Started event raised"); // Log the event for debugging purposes
    }

    // Method to raise the OnScored event
    public static void RaiseOnScored(int score)
    {
        OnScored?.Invoke(score); // Invoke the OnScored event, passing the current score to subscribers
        Debug.Log("Scored event raised"); // Log the event for debugging purposes
    }

    // Method to raise the OnColliderHitted event
    public static void RaiseOnColliderHitted(int lives)
    {
        OnColliderHitted?.Invoke(lives); // Invoke the OnColliderHitted event, passing the remaining lives to subscribers
        Debug.Log("Collider Hitted event raised"); // Log the event for debugging purposes
    }

    // Method to raise the OnGameEnded event
    public static void RaiseOnGameEnded()
    {
        OnGameEnded?.Invoke(); // Invoke the OnGameEnded event if there are subscribers
        Debug.Log("Game Ended event raised"); // Log the event for debugging purposes
    }

    // Method to raise the OnHomeTriggered event
    public static void RaiseOnHomeTriggered()
    {
        OnHomeTriggered?.Invoke(); // Invoke the OnHomeTriggered event if there are subscribers
        Debug.Log("Go Home event raised"); // Log the event for debugging purposes
    }

    // Method to raise the OnGameReset event
    public static void RaiseOnGameReset(int score, int lives)
    {
        OnGameReset?.Invoke(score, lives); // Invoke the OnGameReset event, passing the current score and lives to subscribers
        Debug.Log("Game Reset event raised"); // Log the event for debugging purposes
    }
}
