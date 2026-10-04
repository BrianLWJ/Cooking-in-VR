using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
public class TeleportationActivator : MonoBehaviour
{
    public XRRayInteractor teleportInteractor;
    public InputActionProperty teleportActivatorAction;

    [Header("BUG FIX: Interacting w/ UI w/ ray still allows Tp even w/ the interaction group")]
    [Header("NO Tp during UI interaction)")]
    [Tooltip("Enabling NO Tp during UI interaction")]
    public bool noTpDuringUI;
    public XRRayInteractor rayInteractor;
    void Start()
    {
        teleportInteractor.gameObject.SetActive(false);

        teleportActivatorAction.action.performed += ctx =>
        {   
            //Extra Error code by Valem Tutorials to fix BUG
            if (rayInteractor && rayInteractor.IsOverUIGameObject() && noTpDuringUI)
            {
                return;
            }
            teleportInteractor.gameObject.SetActive(true);
        };
        if(noTpDuringUI) rayInteractor.uiHoverEntered.AddListener(x => DisableTeleportRay());
    }
    
    void Update()
    {
        if (teleportActivatorAction.action.WasReleasedThisFrame())
        {
            teleportInteractor.gameObject.SetActive(false);
        }
    }

    public void DisableTeleportRay()
    {
        teleportInteractor.gameObject.SetActive(false);
    }
}
