using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] // Let's you modify this value in the Unity Inspector
    private float _jumpForce = 10f; // Force applied when the player jumps
    [SerializeField]
    private GameObject _menu; // Reference to the menu GameObject
    [SerializeField]
    private float rotationMultiplier = 2f; // Multiplier for rotation based on vertical velocity
    [SerializeField]
    private GameObject _scoreUI; // Reference to the score UI GameObject

    private Rigidbody2D _rigidBody; // Reference to the Rigidbody2D component
    private int _score; // Player's score
    public static bool GameStarted = false; // Static variable to track if the game has started


    public static event Action OnPlayerDisabled; // Event triggered when the player dies

    void OnDisable()
    {
        OnPlayerDisabled?.Invoke(); // Invoke the event if there are any subscribers
    }

    public int Score 
    { 
        get 
        { 
            return _score; // Return the current score
        }
    }


    public bool IsAlive 
    {
        get
        {
            return gameObject.activeSelf; // Return true if the player GameObject is active
        } 
    } 

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameStarted) return; // Do nothing if the game hasn't started
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) // Check for mouse button press (0 is left button)
        {
            _rigidBody.linearVelocityY = _jumpForce; // Set the vertical velocity to 10 units per second
        }

        transform.rotation = Quaternion.Euler(0f, 0f, _rigidBody.linearVelocity.y * rotationMultiplier); // Rotate the player based on vertical velocity
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false); // Deactivate the player GameObject on collision
        _menu.SetActive(true); // Activate the menu GameObject
        _score = 0; // Reset the score to 0
        OnPlayerDisabled?.Invoke();  // Trigger event for other scripts (like background)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("We hit a trigger");
        _score++; // Increment the score by 1
    }
}
