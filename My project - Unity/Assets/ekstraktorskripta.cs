using UnityEngine;
using UnityEngine.InputSystem;

public class ekstraktorskripta : MonoBehaviour
{
    public Camera ARcamera;
    public AudioSource audioSource;
    private InputAction tapAction;

    void Awake()
    {
        tapAction = new InputAction(
            type: InputActionType.Button,
            binding: "<Pointer>/press"
        );
    }

    void OnEnable()
    {
        tapAction.Enable();
        tapAction.performed += OnTap;
    }

    void OnDisable()
    {
        tapAction.performed -= OnTap;
        tapAction.Disable();
    }

    void OnTap(InputAction.CallbackContext ctx)
    {
        Vector2 pos = Pointer.current != null ? Pointer.current.position.ReadValue() : Vector2.zero;
        Ray ray = (ARcamera != null ? ARcamera : Camera.main).ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Transform root = hit.collider.transform;
            while (root != null && !root.CompareTag("Ekstraktor"))
                root = root.parent;

            if (root != null)
            {
                if (audioSource != null)
                {
                    if (audioSource.isPlaying)
                        audioSource.Stop();
                    audioSource.Play();
                }
                return;
            }
        }

        // Tap was not on Ekstraktor -> stop audio if playing
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
    }
}