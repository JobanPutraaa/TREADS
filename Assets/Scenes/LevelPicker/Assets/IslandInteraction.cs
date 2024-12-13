using UnityEngine;
using UnityEngine.SceneManagement;

public class IslandInteraction : MonoBehaviour
{
    public string sceneToLoad; // The scene to load when interacting
    public float interactionRange = 5f; // The range at which the prompt appears
    public GameObject promptUI; // UI for the interaction prompt

    private Transform player; // Reference to the player
    private bool isInRange = false; // Is the player in range of this island?

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (promptUI != null)
            promptUI.SetActive(false); // Hide the prompt initially
    }

    void Update()
    {
        if (player == null) return;

        // Check distance between player and island
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactionRange)
        {
            if (!isInRange)
            {
                isInRange = true;
                if (promptUI != null)
                    promptUI.SetActive(true); // Show the prompt
            }

            // Check for interaction key press
            if (Input.GetKeyDown(KeyCode.E))
            {
                LoadScene();
            }
        }
        else
        {
            if (isInRange)
            {
                isInRange = false;
                if (promptUI != null)
                    promptUI.SetActive(false); // Hide the prompt
            }
        }
    }

    void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene to load is not assigned in the inspector!");
        }
    }
}
