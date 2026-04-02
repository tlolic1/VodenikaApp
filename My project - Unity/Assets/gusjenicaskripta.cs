using UnityEngine;
using UnityEngine.InputSystem;

public class gusjenicaskripta : MonoBehaviour
{
    public Camera ARcamera;
    public GameObject slikaGusjenice;
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
            while (root != null && !root.CompareTag("Gusjenica"))
                root = root.parent;

            if (root != null)
            {
                if (slikaGusjenice != null)
                    slikaGusjenice.SetActive(true);
                return;
            }
        }

        // If we get here, the tap was NOT on a Gusjenica -> hide the image
        if (slikaGusjenice != null && slikaGusjenice.activeSelf)
            slikaGusjenice.SetActive(false);
    }
}
