using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialogueUI;          // Assign your dialogue UI GameObject here
    public GameObject interactPromptUI;    // Assign your "Press E to interact" UI here
    public float interactionRange = 2f;    // The distance required for interaction
    private Transform player;              // Reference to the player's Transform
    private bool isPlayerInRange = false;  // Track if the player is close enough

    void Start()
    {
        if (dialogueUI != null)
            dialogueUI.SetActive(false);

        if (interactPromptUI != null)
            interactPromptUI.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check distance between player and NPC
        float distance = Vector3.Distance(player.position, transform.position);
        isPlayerInRange = distance <= interactionRange;

        if (isPlayerInRange)
        {
            // Show the interaction prompt
            if (interactPromptUI != null && !interactPromptUI.activeSelf)
            {
                interactPromptUI.SetActive(true);
                interactPromptUI.transform.position = new Vector3(Screen.width / 2, Screen.height / 2 - 50, 0);
            }

            // Check for the interaction key
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleDialogueUI();
            }
        }
        else
        {
            // Hide the interaction prompt and dialogue UI when out of range
            if (interactPromptUI != null && interactPromptUI.activeSelf)
                interactPromptUI.SetActive(false);

            if (dialogueUI != null && dialogueUI.activeSelf)
                dialogueUI.SetActive(false);
        }
    }

    void ToggleDialogueUI()
    {
        // Toggle the active state of the dialogue UI
        if (dialogueUI != null)
        {
            dialogueUI.SetActive(!dialogueUI.activeSelf);

            // If activated, center the dialogue UI on the screen
            if (dialogueUI.activeSelf)
            {
                dialogueUI.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            }
        }
    }
}
