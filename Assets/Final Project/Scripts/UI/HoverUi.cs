using UnityEngine;
using UnityEngine.EventSystems;

public class HoverUi : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] public AudioClip hoverClip;
    [SerializeField] public AudioClip pressClip;
    public float cooldown = 0.05f;
    public float pressCooldown = 0.1f;

    private float lastPlayTime;
    private float lastPressTime;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverClip == null || Time.time - lastPlayTime < cooldown)
            return;

        AudioManager.Instance.PlaySfx(1, hoverClip, 1);

        lastPlayTime = Time.time;
    }

    public void OnPress()
    {
        if (hoverClip == null || Time.time - lastPressTime < pressCooldown)
            return;

        AudioManager.Instance.PlaySfx(1, pressClip, 1);

        lastPressTime = Time.time;
    }
}
