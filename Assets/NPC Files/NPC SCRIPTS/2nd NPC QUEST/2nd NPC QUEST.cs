using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyItemsQuest : MonoBehaviour
{
    public GameObject dialogueUI;       // Dialogue UI
    public GameObject interactPromptUI; // Interaction prompt UI
    public GameObject[] itemsToDestroy; // The items to destroy in order
    public GameObject questGiver;       // The NPC giving the quest
    public GameObject exclamationMark;  // Exclamation mark above the NPC
    public float questGiverInteractionRange = 2f; // Range for interacting with the quest giver
    private Transform player;           // Player reference
    private int currentItemIndex = 0;   // Index of the item to destroy
    private bool questActive = false;   // Is the quest active?
    private bool questCompleted = false; // Is the quest completed?
    private Coroutine hideDialogueCoroutine; // Reference to the active coroutine

    private List<string> introDialogues = new List<string>(); // List to hold the introductory dialogues
    private int currentIntroDialogueIndex = 0; // Index to track which intro dialogue is currently being displayed

    private List<string> completeQuestDialogues = new List<string>(); // List to hold the completion dialogues
    private int currentCompleteDialogueIndex = 0; // Index to track which completion dialogue is currently being displayed

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (dialogueUI != null)
            dialogueUI.SetActive(false);

        if (interactPromptUI != null)
            interactPromptUI.SetActive(false);

        if (exclamationMark != null)
            exclamationMark.SetActive(true);

        // Initialize the introductory dialogues
        introDialogues.Add("Hello! I have another quest for you. Are you ready?");
        introDialogues.Add("I need you to destroy the 3 leafless trees in a specific order.");
        introDialogues.Add("Make sure to destroy them in order, or it won't be destroyed.");
        introDialogues.Add("The first target is near my house.");
        introDialogues.Add("The second target is the one most near John's house");
        introDialogues.Add("And the last, of course it'll be the last one standing.");

        // Initialize the completion dialogues
        completeQuestDialogues.Add("You destroyed all the targets! Good Job!");
        completeQuestDialogues.Add("Thanks for helping out!");
        completeQuestDialogues.Add("John seems to be looking for you next.");
    }

    void Update()
    {
        // Handle interaction with the quest giver
        float distanceToNPC = Vector3.Distance(player.position, questGiver.transform.position);
        if (distanceToNPC <= questGiverInteractionRange && !questCompleted)
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
                    ShowDialogue(introDialogues[currentIntroDialogueIndex]);
                    currentIntroDialogueIndex++;
                }
                else if (!questActive && currentIntroDialogueIndex >= introDialogues.Count)
                {
                    StartQuest();
                }
                else if (questActive && currentItemIndex == itemsToDestroy.Length)
                {
                    CompleteQuest();
                }
                else if (questActive)
                {
                    ShowDialogue("You still need to destroy the remaining targets!");
                }
            }
        }
        else if (interactPromptUI != null)
        {
            interactPromptUI.SetActive(false);
        }

        // Handle item destruction
        if (questActive && currentItemIndex < itemsToDestroy.Length)
        {
            GameObject currentItem = itemsToDestroy[currentItemIndex];
            if (currentItem != null)
            {
                DestroyableItem destroyable = currentItem.GetComponent<DestroyableItem>();
                if (destroyable != null)
                {
                    float distanceToItem = Vector3.Distance(player.position, currentItem.transform.position);

                    if (distanceToItem <= destroyable.interactionRange && Input.GetMouseButtonDown(0)) // Left mouse button
                    {
                        DestroyTarget(currentItem);
                    }
                }
            }
        }
    }

    void StartQuest()
    {
        questActive = true;
        ShowDialogue("Go and destroy the targets in order!");
    }

    void DestroyTarget(GameObject target)
    {
        target.SetActive(false); // Simulate destruction
        currentItemIndex++;

        if (currentItemIndex < itemsToDestroy.Length)
        {
            ShowDialogue($"Destroyed target {currentItemIndex}. Move to the next one!");
        }
        else
        {
            ShowDialogue("You have destroyed all targets! Return to Mary.");
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

            if (hideDialogueCoroutine != null)
            {
                StopCoroutine(hideDialogueCoroutine);
            }

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