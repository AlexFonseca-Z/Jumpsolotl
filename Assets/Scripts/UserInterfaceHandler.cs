using TMPro;
using UnityEngine;

public class UserInterfaceHandler : MonoBehaviour
{
    [SerializeField] // Let's you modify this value in the Unity Inspector
    private GameObject _player; // Reference to the player GameObject
    [SerializeField]
    private Vector3 _startPosition = Vector3.zero; // Starting position for the player
    [SerializeField]
    private GameObject _menuUI; // Reference to the menuUI GameObject
    [SerializeField]    
    private TextMeshProUGUI _scoreText, _bestScoreText, _lastScoreText; // Reference to the score TextMeshProUGUI component
    [SerializeField]
    private GameObject _scoreUI; // Reference to the score UI GameObject


    private PlayerControl _playerControl; // Reference to the PlayerControl script
    private int _bestScore=0, _lastScore=0; // Player's best score


    private void Start()
    {
        _playerControl = _player.gameObject.GetComponent<PlayerControl>();
        _player.SetActive(false);  // Ensure the player is inactive at the start
        _scoreUI.SetActive(false); // Hide score UI at the start
        _lastScoreText.text = "0"; // Initialize last score display

        _bestScore = PlayerPrefs.GetInt("BestScore", 0); // Load the best score from PlayerPrefs
        _bestScoreText.text = _bestScore.ToString(); // Display the best score
    }

    private void Update()
    {
        if (_player.activeSelf && PlayerControl.GameStarted)
        {
            _scoreText.text = _playerControl.Score.ToString(); // Update the score display

            if (_playerControl.Score > _bestScore)
            {
                _bestScore = _playerControl.Score; // Update the best score if the current score is higher
                _bestScoreText.text = _bestScore.ToString(); // Update the best score display
                PlayerPrefs.SetInt("BestScore", _bestScore); // Save the new best score to PlayerPrefs
                PlayerPrefs.Save(); // Ensure the best score is saved
            }

            if (_playerControl.Score != _lastScore)
            {
                _lastScore = _playerControl.Score; // Update the last score when the player dies
                _lastScoreText.text = _lastScore.ToString(); // Update the last score display
            }
        }else
        {
            _scoreUI.SetActive(false); // Hide score UI when the player is inactive
        }
    }

    public void OnStartPressed()
    {
        PlayerControl.GameStarted = true;
        _player.SetActive(true); // Activate the player GameObject
        _player.transform.position = _startPosition; // Reset the player's position to the starting position
        _menuUI.SetActive(false); // Deactivate the menu GameObject
        _scoreUI.SetActive(true); // Activate the score UI GameObject
    }
}