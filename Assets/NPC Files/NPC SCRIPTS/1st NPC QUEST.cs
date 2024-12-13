using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public GameObject dialogueUI;
    public GameObject interactPromptUI;
    public Transform[] items;
    public GameObject questGiver;
    public GameObject exclamationMark;
    public GameObject exclamationMark2;
    public float interactionRange = 2f;
    private Transform player;
    private int currentItemIndex = 0;
    private bool questActive = false;
    private bool questCompleted = false;
    private Coroutine hideDialogueCoroutine;

    public DestroyItemsQuest destroyItemsQuest; // Reference to the second quest script

    private List<string> introDialogues = new List<string>();
    private int currentIntroDialogueIndex = 0;

    private List<string> completeQuestDialogues = new List<string>();
    private int currentCompleteDialogueIndex = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (dialogueUI != null)
            dialogueUI.SetActive(false);

        if (interactPromptUI != null)
            interactPromptUI.SetActive(false);

        if (exclamationMark != null)
            exclamationMark.SetActive(true);

        introDialogues.Add("Hello! I have a quest for you. Are you ready to start?");
        introDialogues.Add("I need you to collect 3 brown grass in a specific order.");
        introDialogues.Add("You will be unable to get an item if it is not in order.");
        introDialogues.Add("The first brown grass would be near my house.");
        introDialogues.Add("The second brown grass is near the house to my right.");
        introDialogues.Add("And the last brown grass should be near the house to my left.");

        completeQuestDialogues.Add("Thank you for completing the quest!");
        completeQuestDialogues.Add("Mary is actually looking for you too.");
        completeQuestDialogues.Add("She's at the house to my right.");

        if (destroyItemsQuest != null)
            destroyItemsQuest.enabled = false; // Ensure the second quest is initially disabled
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
            ShowDialogue("You have collected all items! Return to Eugene.");
        }
        else
        {
            ShowDialogue($"Collected item {currentItemIndex}. Find the next one!");
        }
    }

    void CompleteQuest()
    {
        if (currentCompleteDialogueIndex < completeQuestDialogues.Count)
        {
            ShowDialogue(completeQuestDialogues[currentCompleteDialogueIndex]);
            currentCompleteDialogueIndex++;
        }
        else
        {
            questActive = false;
            questCompleted = true;

            if (exclamationMark != null)
            {
                exclamationMark.SetActive(false);
                exclamationMark2.SetActive(true);
            }

            // Activate the second quest
            if (destroyItemsQuest != null)
            {
                destroyItemsQuest.enabled = true;
            }
        }
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
            hideDialogueCoroutine = StartCoroutine(HideDialogueAfterDelay(5f));
        }
    }

    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (dialogueUI != null && dialogueUI.activeSelf)
            dialogueUI.SetActive(false);
    }
}
