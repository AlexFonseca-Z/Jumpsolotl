using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] // Let's you modify this value in the Unity Inspector
    private GameObject _obstacle; // Reference to the obstacle prefab
    [SerializeField] // Let's you modify this value in the Unity Inspector
    private float _y0offsetMin = 0f, _y0offsetMax = 3f; // Range for random Y offset of the obstacle spawn position
    [SerializeField] // Let's you modify this value in the Unity Inspector
    private float _spawnRate = 3f; // Frequency of obstacle spawning in seconds
    [SerializeField]
    private PlayerControl _player; // Reference to the player to check if they are alive

    private float _nextSpawn = 0f; // Time until the next obstacle spawn

    // Update is called once per frame
    void Update()
    {
        if (!PlayerControl.GameStarted) return;
        if (_nextSpawn <= 0f && _player.IsAlive)
        {
            Vector3 newPosition = new Vector3(transform.position.x, Random.Range(_y0offsetMin, _y0offsetMax)); // Calculate a new position with a random Y offset
            Instantiate(_obstacle, newPosition, transform.rotation); // Spawn a new obstacle at the spawner's position and rotation
            _nextSpawn = _spawnRate; // Reset the spawn timer
        }
        else
        {
            _nextSpawn -= Time.deltaTime; // Decrease the timer by the time elapsed since the last frame
        }
    }
}
