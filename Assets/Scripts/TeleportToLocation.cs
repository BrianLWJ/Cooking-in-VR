using UnityEngine;
using Unity.XR.CoreUtils;

public class TeleportToLocation : MonoBehaviour
{
    public XROrigin xrOrigin;
    public Transform targetLocation;
    public Transform targetMenuLocation;
    public AudioManager audioManager;

    public void Teleport()
    {
        xrOrigin.MoveCameraToWorldLocation(targetLocation.position);
        audioManager.PlayMusic(audioManager.kitchenBGM);

        // Optional: also match facing direction
        Vector3 forward = targetLocation.forward;
        forward.y = 0f;
        if (forward.sqrMagnitude > 0.001f)
        {
            xrOrigin.transform.rotation = Quaternion.LookRotation(forward);
        }
    }

    public void TeleportMenu()
    {
        xrOrigin.MoveCameraToWorldLocation(targetMenuLocation.position);
        audioManager.PlayMusic(audioManager.menuBGM);

        // Optional: also match facing direction
        Vector3 forward = targetMenuLocation.forward;
        forward.y = 0f;
        if (forward.sqrMagnitude > 0.001f)
        {
            xrOrigin.transform.rotation = Quaternion.LookRotation(forward);
        }
    }
}
