using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class UIEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private FontStyles HOVERED_FONT = FontStyles.Bold | FontStyles.UpperCase;
    private FontStyles DEFAULT_FONT = FontStyles.Bold | FontStyles.LowerCase;
    public TextMeshProUGUI buttonText;

    public AudioSource HoverSound;
    public AudioSource ClickSound;
    void Start()
    {
        if (buttonText == null)
            Debug.LogError("TextMeshProUGUI not found: ", this);
        if (ClickSound != null)
        {
            ClickSound.playOnAwake = true;
            ClickSound.loop = false;
            ClickSound.time = 0.10f;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
            buttonText.fontStyle = HOVERED_FONT;
        if (HoverSound != null)
        {
            HoverSound.Stop();
            HoverSound.time = 0f;
            HoverSound.Play();
        }
        else
        {
            Debug.LogWarning("HoverSound is not assigned.");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
            buttonText.fontStyle = DEFAULT_FONT;
    }
    public void OnClick()
    {
        // This method can be used to add a click effect, such as playing a sound or animation
        if (EventSystem.current.currentSelectedGameObject == gameObject)
            EventSystem.current.SetSelectedGameObject(null);

        if (ClickSound != null)
        {
            ClickSound.Stop();
            ClickSound.time = 0f;
            ClickSound.Play();
        }
        else
        {
            Debug.LogWarning("ClickSound is not assigned.");
        }
    }
}