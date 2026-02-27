using UnityEngine;
using UnityEngine.UI;

public class Hole : MonoBehaviour
{
    public Text holeText; // Texto UI

    private void Start()
    {
        if (holeText != null)
        {
            holeText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("HOYO!");

            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            // Centrar bola en el hoyo
            other.transform.position = transform.position;

            // Mostrar texto
            if (holeText != null)
            {
                holeText.gameObject.SetActive(true);
                holeText.text = "¡HOYO!";
            }
        }
    }
}