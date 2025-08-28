using UnityEngine;



public class ObjectHandle : MonoBehaviour
{
    [SerializeField] // Let's you modify this value in the Unity Inspector
    private float _speed = 10f; // Speed of the object movement
    [SerializeField] // Let's you modify this value in the Unity Inspector
    private float _minX = -10f; // Minimum X position before the object is destroyed
    [SerializeField]
    private PlayerControl _player; // Reference to the player to check if they are alive

    private void OnEnable()
    {
        PlayerControl.OnPlayerDisabled += DestroySelf;
    }

    private void OnDisable()
    {
        PlayerControl.OnPlayerDisabled -= DestroySelf;
    }


    // Update is called once per frame
    void Update()
    {
        if (!PlayerControl.GameStarted) return;
        transform.position += Vector3.left * _speed * Time.deltaTime; // Move the object left at a constant speed

        if(transform.position.x < _minX || (_player != null && !_player.IsAlive)) // Check if the object has moved beyond the minimum X position
        {
            Destroy(gameObject); // Destroy the object if it has moved beyond the minimum X position
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
