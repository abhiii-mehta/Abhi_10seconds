using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonClickSound : MonoBehaviour
{
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject clicked = EventSystem.current.currentSelectedGameObject;
            if (clicked != null && clicked.GetComponent<Button>())
            {
                audioSource.PlayOneShot(clickSound);
            }
        }
    }
}
