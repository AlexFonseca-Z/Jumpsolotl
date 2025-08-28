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
    private TextMeshProUGUI _scoreText, _bestScoreText; // Reference to the score TextMeshProUGUI component
    [SerializeField]
    private GameObject _scoreUI; // Reference to the score UI GameObject


    private PlayerControl _playerControl; // Reference to the PlayerControl script
    private int _bestScore=0; // Player's best score


    private void Start()
    {
        _playerControl = _player.gameObject.GetComponent<PlayerControl>();
        _player.SetActive(false);  // Ensure the player is inactive at the start
        _scoreUI.SetActive(false); // Hide score UI at the start
    }

    private void Update()
    {
        _scoreText.text = _playerControl.Score.ToString(); // Update the score display

        if(_playerControl.Score > _bestScore)
        {
            _bestScore = _playerControl.Score; // Update the best score if the current score is higher
            _bestScoreText.text = _bestScore.ToString(); // Update the best score display
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