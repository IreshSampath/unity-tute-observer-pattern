using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] GameObject _homePanel;      // Panel displayed at the home screen
    [SerializeField] GameObject _scorePanel;     // Panel displayed when the player scores
    [SerializeField] GameObject _lifeLostPanel;  // Panel displayed when the player loses a life
    [SerializeField] GameObject _endPanel;       // Panel displayed at the end of the game

    [Header("Score Texts")]
    [SerializeField] TMP_Text _scoreGameTxt;     // Text showing the current score in the game
    [SerializeField] TMP_Text _scorePopupTxt;    // Text showing the popup score
    [SerializeField] TMP_Text _scoreGameEndTxt;  // Text showing the final score at the game end

    [Header("Lives Texts")]
    [SerializeField] TMP_Text _livesGameTxt;     // Text showing the number of lives during the game
    [SerializeField] TMP_Text _livesPopupTxt;    // Text showing the remaining lives as a popup

    [Header("Settings")]
    [SerializeField] private float panelCloseDelay = 1f; // Delay in seconds for automatically closing popup panels

    Coroutine _closePanelCoroutine; // Used to manage and avoid overlapping auto-close panel coroutines

    void OnEnable()
    {
        // Subscribe to game events when the object is enabled
        GameEvents.OnGameStarted += DisplayGame;
        GameEvents.OnScored += DisplayScored;
        GameEvents.OnColliderHitted += DisplayLifeLost;
        GameEvents.OnGameEnded += DisplayEnd;
        GameEvents.OnHomeTriggered += DisplayHome;
        GameEvents.OnGameReset += ResetUI;
    }

    void OnDisable()
    {
        // Unsubscribe from game events when the object is disabled to prevent memory leaks
        GameEvents.OnGameStarted -= DisplayGame;
        GameEvents.OnScored -= DisplayScored;
        GameEvents.OnColliderHitted -= DisplayLifeLost;
        GameEvents.OnGameEnded -= DisplayEnd;
        GameEvents.OnHomeTriggered -= DisplayHome;
        GameEvents.OnGameReset -= ResetUI;
    }

    // Display the home screen panel
    void DisplayHome()
    {
        SetActivePanel(_endPanel, false);  // Ensure the end panel is hidden
        SetActivePanel(_homePanel, true); // Show the home panel
    }

    // Display the game screen by hiding the home panel
    void DisplayGame()
    {
        SetActivePanel(_homePanel, false); // Hide the home panel
    }

    // Handle the display and updates when the player scores
    void DisplayScored(int score)
    {
        UpdateScoreTexts(score);        // Update score-related UI texts
        ShowPopupPanel(_scorePanel);   // Temporarily show the score popup panel
    }

    // Handle the display and updates when the player loses a life
    void DisplayLifeLost(int lives)
    {
        UpdateLivesTexts(lives);        // Update lives-related UI texts
        ShowPopupPanel(_lifeLostPanel); // Temporarily show the life lost popup panel
    }

    // Display the game end panel
    void DisplayEnd()
    {
        SetActivePanel(_endPanel, true); // Show the end panel
    }

    // Update the score-related UI elements
    void UpdateScoreTexts(int score)
    {
        _scoreGameTxt.text = $"{score}";         // Update the in-game score text
        _scorePopupTxt.text = $"{score}";        // Update the popup score text
        _scoreGameEndTxt.text = $"You scored {score}"; // Update the end game score text
    }

    // Update the lives-related UI elements
    void UpdateLivesTexts(int lives)
    {
        _livesGameTxt.text = $"{lives}";                  // Update the in-game lives text
        _livesPopupTxt.text = $"Remaining lives: {lives}"; // Update the popup lives text
    }

    // Reset the UI elements to their initial state
    void ResetUI(int score, int lives)
    {
        UpdateScoreTexts(score); // Reset score texts
        UpdateLivesTexts(lives); // Reset lives texts
    }

    // Temporarily show a panel and close it automatically after a delay
    void ShowPopupPanel(GameObject panel)
    {
        SetActivePanel(panel, true); // Activate the panel

        if (_closePanelCoroutine != null)
        {
            StopCoroutine(_closePanelCoroutine); // Stop any existing coroutine for this panel
        }

        // Start a new coroutine to close the panel after a delay
        _closePanelCoroutine = StartCoroutine(ClosePanelAfterDelay(panel, panelCloseDelay));
    }

    // Helper method to set a panel's active state if it's not already set
    void SetActivePanel(GameObject panel, bool isActive)
    {
        if (panel.activeSelf != isActive)
        {
            panel.SetActive(isActive); // Change the active state only if needed
        }
    }

    // Coroutine to close a panel after a specified delay
    IEnumerator ClosePanelAfterDelay(GameObject panel, float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        SetActivePanel(panel, false);          // Deactivate the panel
    }
}
