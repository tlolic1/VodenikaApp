using UnityEngine;
using UnityEngine.InputSystem;

public class meksikoskripta : MonoBehaviour
{
    public Camera ARcamera;
    public GameObject tekst;
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
            while (root != null && !root.CompareTag("Meksiko"))
                root = root.parent;

            if (root != null)
            {
                if (tekst != null)
                    tekst.SetActive(true);
                return;
            }
        }

        // Tap was not on a Meksiko -> hide tekst if visible
        if (tekst != null && tekst.activeSelf)
            tekst.SetActive(false);
    }
}
