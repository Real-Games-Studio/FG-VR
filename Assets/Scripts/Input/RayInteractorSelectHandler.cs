using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class InteractorActionHandler : MonoBehaviour
{
    [Header("Referência ao NearFarInteractor")]
    [SerializeField] private NearFarInteractor rayInteractor;

    [Header("Raycast Settings")]
    [SerializeField, Tooltip("Distância máxima do ray")]
    private float maxRayDistance = 10f;
    [SerializeField, Tooltip("Layers a considerar no raycast")]
    private LayerMask raycastMask = ~0;

    [Header("Input Actions")]
    [SerializeField, Tooltip("Action que representa o botão de pressionar")]
    private InputActionReference pressAction;
    [SerializeField, Tooltip("Action que representa o botão de soltar")]
    private InputActionReference releaseAction;

    // Guarda o último ISelectable que recebeu o OnSelect, pra chamar OnDeselect depois
    private ISelectable _currentSelectable;

    private void Awake()
    {
        if (rayInteractor == null)
            rayInteractor = GetComponent<NearFarInteractor>();
    }

    private void OnEnable()
    {
        // Inscreve nos eventos do Input System
        pressAction.action.started += OnPress;   // ou .performed, conforme sua Action
        releaseAction.action.canceled += OnRelease;
        pressAction.action.Enable();
        releaseAction.action.Enable();
    }

    private void OnDisable()
    {
        pressAction.action.started -= OnPress;
        releaseAction.action.canceled -= OnRelease;
        pressAction.action.Disable();
        releaseAction.action.Disable();
    }

    // Quando o botão configurado for pressionado
    private void OnPress(InputAction.CallbackContext ctx)
    {
        // Origem e direção do seu NearFarInteractor
        Vector3 origin = rayInteractor.transform.position;
        Vector3 direction = rayInteractor.transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxRayDistance, raycastMask))
        {
            // Se achou um ISelectable, chama OnSelect e guarda referência
            if (hit.collider.TryGetComponent<ISelectable>(out var sel))
            {
                _currentSelectable = sel;
                sel.OnSelect();
            }
        }
    }

    // Quando o mesmo botão for solto
    private void OnRelease(InputAction.CallbackContext ctx)
    {
        if (_currentSelectable != null)
        {
            _currentSelectable.OnDeselect();
            _currentSelectable = null;
        }
    }
}
