using System.Collections.Generic;
using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f; // Speed of background movement
    [SerializeField] private int poolSize = 3;       // Number of background copies
    [SerializeField] private PlayerControl _player; // Reference to the player to check if they are alive

    private float bgWidth;
    private List<GameObject> backgrounds = new List<GameObject>();

    private void OnEnable()
    {
        PlayerControl.OnPlayerDisabled += ResetBackground;
    }

    public void ResetBackground()
    {
        if (backgrounds.Count == 0)
        {
            return;
        }

        float bgWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;

        for (int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].transform.position = new Vector3(i * bgWidth, 0, 0);
        }
    }

    void Start()
    {
        GameObject template = transform.GetChild(0).gameObject;
        bgWidth = template.GetComponent<SpriteRenderer>().bounds.size.x;
        template.SetActive(false);

        // Spawn initial backgrounds
        for (int i = 0; i < poolSize; i++)
        {
            Vector3 pos = new Vector3(i * bgWidth, 0, 0);
            GameObject bg = Instantiate(template, pos, Quaternion.identity, transform);
            bg.SetActive(true);
            backgrounds.Add(bg); // <-- Use Add, not Enqueue
        }
        PlayerControl.OnPlayerDisabled += ResetBackground;
    }

    void Update()
    {
        if (!PlayerControl.GameStarted || !_player.IsAlive) return;

        foreach (var bg in backgrounds)
            bg.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        // Recycle leftmost background
        GameObject first = backgrounds[0];
        if (first.transform.position.x <= -bgWidth)
        {
            backgrounds.RemoveAt(0);  // <-- remove first element
            GameObject last = backgrounds[backgrounds.Count - 1];
            first.transform.position = new Vector3(last.transform.position.x + bgWidth, 0, 0);
            backgrounds.Add(first);    // <-- add it to the end
        }
    }


}
