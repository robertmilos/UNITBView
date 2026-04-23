// Door.cs (Smooth version)
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorPivot;   // Assign DoorPivot in Inspector
    public float openAngle = 90f;
    public float animationDuration = 0.5f;  // Seconds
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private float animationTime = 0f;
    private bool isAnimating = false;
    private AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        closedRotation = doorPivot.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
    }

    public void ToggleDoor()
    {
        if (isAnimating) return;  // Prevent spamming click during animation

        isOpen = !isOpen;
        animationTime = 0f;
        isAnimating = true;

        AudioClip clipToPlay = isOpen ? openSound : closeSound;
        if (clipToPlay != null)
        {
            audioSource.PlayOneShot(clipToPlay, audioSource.volume);
        }
    }

    void Update()
    {
        if (isAnimating)
        {
            animationTime += Time.deltaTime;
            float t = Mathf.Clamp01(animationTime / animationDuration);

            // Smoothstep makes motion smoother
            t = t * t * (3f - 2f * t);

            if (isOpen)
                doorPivot.localRotation = Quaternion.Slerp(closedRotation, openRotation, t);
            else
                doorPivot.localRotation = Quaternion.Slerp(openRotation, closedRotation, t);

            if (t >= 1f)
                isAnimating = false;
        }
    }
}
