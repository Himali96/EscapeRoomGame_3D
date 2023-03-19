using UnityEngine;

public class MouseInteractionsControler : MonoBehaviour
{
    Camera cam;
    public LayerMask mask;

    void Start()
    {
        cam = GetComponent<Camera>();
        MiniGameLoader.Instance.onRoomViewChange += OnRoomViewChange;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
            {
                IInteractuableObject interactuableObject = hit.collider.GetComponent<IInteractuableObject>();
                interactuableObject?.OnClick();
            }
        }
    }

    void OnRoomViewChange(bool _isVisible)
    {
        enabled = _isVisible;
    }
}