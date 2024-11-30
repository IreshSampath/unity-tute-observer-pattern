using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // List of audio clips to be used for different game events
    [SerializeField] List<AudioClip> _sounds;

    // AudioSource component used to play audio clips
    [SerializeField] AudioSource _audioSource;

    // Subscribe to game events when the object is enabled
    void OnEnable()
    {
        GameEvents.OnGameStarted += PlayGameStartSound;       // Subscribe to the game start event
        GameEvents.OnScored += (v1) => { PlayScoredSound(); }; // Subscribe to the scored event
        GameEvents.OnColliderHitted += (v1) => { PlayLostSound(); }; // Subscribe to the collider hit event
        GameEvents.OnGameEnded += PlayEndSound;               // Subscribe to the game end event
        GameEvents.OnHomeTriggered += PlayTransitionSound;    // Subscribe to the home triggered event
    }

    // Unsubscribe from game events when the object is disabled
    void OnDisable()
    {
        GameEvents.OnGameStarted -= PlayGameStartSound;       // Unsubscribe from the game start event
        GameEvents.OnScored -= (v1) => { PlayScoredSound(); }; // Unsubscribe from the scored event
        GameEvents.OnColliderHitted -= (v1) => { PlayLostSound(); }; // Unsubscribe from the collider hit event
        GameEvents.OnGameEnded -= PlayEndSound;               // Unsubscribe from the game end event
        GameEvents.OnHomeTriggered -= PlayTransitionSound;    // Unsubscribe from the home triggered event
    }

    // Play the sound for transitioning back to the home screen
    void PlayTransitionSound()
    {
        PlaySound("Transition"); // Play the transition sound
    }

    // Play the sound for starting the game
    void PlayGameStartSound()
    {
        PlaySound("GameStart"); // Play the game start sound
    }

    // Play the sound for scoring
    void PlayScoredSound()
    {
        PlaySound("Scored"); // Play the scored sound
    }

    // Play the sound for losing a life
    void PlayLostSound()
    {
        PlaySound("LifeLost"); // Play the life lost sound
    }

    // Play the sound for ending the game
    void PlayEndSound()
    {
        PlaySound("GameEnd"); // Play the game end sound
    }

    // Helper method to play a sound by its name
    void PlaySound(string soundName)
    {
        // Find the audio clip with the matching name in the list
        AudioClip clip = _sounds.Find(sound => sound.name == soundName);

        // If the clip is found, play it using the AudioSource
        if (clip != null)
        {
            _audioSource.PlayOneShot(clip); // Play the sound as a one-shot
        }
        else
        {
            // Log a warning if the sound is not found in the list
            Debug.LogWarning($"Sound '{soundName}' not found in the list!");
        }
    }
}
