using UnityEngine;

public class ProximityUI : MonoBehaviour
{
    public GameObject uiElement;

    public string playerTag = "Player";

    public float triggerRadius = 5f;

    private SphereCollider triggerCollider;

    void Start()
    {
        // Ensure the UI starts as inactive
        if (uiElement != null)
            uiElement.SetActive(false);

        // Add a SphereCollider if not already present
        triggerCollider = gameObject.AddComponent<SphereCollider>();
        triggerCollider.isTrigger = true;
        triggerCollider.radius = triggerRadius;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            ShowUI();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            HideUI();
        }
    }

    private void ShowUI()
    {
        if (uiElement != null)
            uiElement.SetActive(true);
    }

    private void HideUI()
    {
        if (uiElement != null)
            uiElement.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the trigger radius in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }
}
