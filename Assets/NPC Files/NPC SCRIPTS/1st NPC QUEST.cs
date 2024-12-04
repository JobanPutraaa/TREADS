using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public GameObject dialogueUI;       // Dialogue UI
    public GameObject interactPromptUI; // Interaction prompt UI
    public Transform[] items;           // The items to collect in order
    public GameObject questGiver;       // The NPC giving the quest
    public float interactionRange = 2f; // Range for interaction
    private Transform player;           // Player reference
    private int currentItemIndex = 0;   // Index of the item to collect
    private bool questActive = false;   // Is the quest active?
    private bool questCompleted = false; // Is the quest completed?
    private Coroutine hideDialogueCoroutine; // Reference to the active coroutine

    private List<string> introDialogues = new List<string>(); // List to hold the introductory dialogues
    private int currentIntroDialogueIndex = 0; // Index to track which intro dialogue is currently being displayed

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (dialogueUI != null)
            dialogueUI.SetActive(false);

        if (interactPromptUI != null)
            interactPromptUI.SetActive(false);

        // Initialize the introductory dialogues
        introDialogues.Add("Hello! I have a quest for you. Are you ready to start?");
        introDialogues.Add("I need you to collect 3 brown grass in a specific order.");
        introDialogues.Add("You will be unable to get an item if it is not in order.");
        introDialogues.Add("The first brown grass would be near my house.");
        introDialogues.Add("The second brown grass is near the house to my right.");
        introDialogues.Add("And the last brown grass should be near the house to my left.");
    }

    void Update()
    {
        float distanceToNPC = Vector3.Distance(player.position, questGiver.transform.position);

        // Show "Press E" when near the NPC
        if (distanceToNPC <= interactionRange && !questCompleted)
        {
            if (interactPromptUI != null && !interactPromptUI.activeSelf)
            {
                interactPromptUI.SetActive(true);
                interactPromptUI.transform.position = new Vector3(Screen.width / 2, Screen.height / 2 - 50, 0);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!questActive && currentIntroDialogueIndex < introDialogues.Count)
                {
                    // Display the next introductory dialogue
                    ShowDialogue(introDialogues[currentIntroDialogueIndex]);
                    currentIntroDialogueIndex++;
                }
                else if (!questActive && currentIntroDialogueIndex >= introDialogues.Count)
                {
                    // Start the quest after all introductory dialogues are shown
                    StartQuest();
                }
                else if (questActive && currentItemIndex == items.Length)
                {
                    CompleteQuest();
                }
                else if (questActive)
                {
                    ShowDialogue("You still need to collect the remaining items!");
                }
            }
        }
        else if (interactPromptUI != null)
        {
            interactPromptUI.SetActive(false);
        }

        // Check for item collection
        if (questActive && currentItemIndex < items.Length)
        {
            float distanceToItem = Vector3.Distance(player.position, items[currentItemIndex].position);
            if (distanceToItem <= interactionRange)
            {
                CollectItem();
            }
        }
    }

    void StartQuest()
    {
        questActive = true;
        ShowDialogue("Please collect all the items in order!");
    }

    void CollectItem()
    {
        // Collect the current item
        Debug.Log($"Collected item {currentItemIndex + 1}");
        items[currentItemIndex].gameObject.SetActive(false); // Disable the collected item
        currentItemIndex++;

        // Check if all items are collected
        if (currentItemIndex == items.Length)
        {
            ShowDialogue("You have collected all items! Return to the NPC.");
        }
        else
        {
            ShowDialogue($"Collected item {currentItemIndex}. Find the next one!");
        }
    }

    void CompleteQuest()
    {
        questActive = false;
        questCompleted = true;
        ShowDialogue("Thank you for completing the quest!");
        // Add reward logic here
    }

    void ShowDialogue(string message)
    {
        if (dialogueUI != null)
        {
            dialogueUI.SetActive(true);
            dialogueUI.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            dialogueUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = message;

            // Stop any existing coroutine before starting a new one
            if (hideDialogueCoroutine != null)
            {
                StopCoroutine(hideDialogueCoroutine);
            }

            // Start a new coroutine to hide the dialogue after a delay
            hideDialogueCoroutine = StartCoroutine(HideDialogueAfterDelay(3f)); // Adjust 3f for desired time
        }
    }

    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (dialogueUI != null && dialogueUI.activeSelf)
            dialogueUI.SetActive(false);
    }
}
