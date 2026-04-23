using UnityEngine;

public class Window1 : MonoBehaviour
{
    public Transform windowPivot;   // Assign WindowPivot in Inspector
    public float openAngle = 90f;
    public float animationDuration = 0.5f; 
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

        closedRotation = windowPivot.localRotation;
        // Rotating on the Y axis (0, openAngle, 0)
        openRotation = closedRotation * Quaternion.Euler(0, 0, openAngle);
    }

    public void ToggleWindow()
    {
        if (isAnimating) return; 

        isOpen = !isOpen;
        animationTime = 0f;
        isAnimating = true;

        AudioClip clipToPlay = isOpen ? openSound : closeSound;
        if (clipToPlay != null && audioSource != null)
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

            // Smoothstep
            t = t * t * (3f - 2f * t);

            if (isOpen)
                windowPivot.localRotation = Quaternion.Slerp(closedRotation, openRotation, t);
            else
                windowPivot.localRotation = Quaternion.Slerp(openRotation, closedRotation, t);

            if (t >= 1f)
                isAnimating = false;
        }
    }
}