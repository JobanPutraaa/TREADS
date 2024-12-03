using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialogueUI;
    public float interactionRange = 5f;
    private Transform player;

    void Start()
    {
        if (dialogueUI != null)
            dialogueUI.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= interactionRange)
        {
            if (!dialogueUI.activeSelf)
            {
                dialogueUI.SetActive(true);
            }

            // Optionally update dialogue UI position to screen center
            dialogueUI.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        }
        else
        {
            if (dialogueUI.activeSelf)
            {
                dialogueUI.SetActive(false);
            }
        }
    }
}
