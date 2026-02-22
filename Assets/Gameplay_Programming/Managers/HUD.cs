using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] PlayerOverlayWidget overlayRef;
    PlayerOverlayWidget currentOverlay;

    public PlayerOverlayWidget Overlay => currentOverlay;

    private void Awake()
    {
        currentOverlay = Instantiate(overlayRef);
    }
}
