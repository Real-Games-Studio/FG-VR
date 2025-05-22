using System.Collections.Generic;
using Unity.XR.Oculus;
using UnityEngine;
using UnityEngine.XR;

public enum ControllerHand { Left, Right }

[RequireComponent(typeof(Animator))]
public class HandAnimation : MonoBehaviour
{
    public const float INPUT_RATE_CHANGE = 20f;

    [SerializeField] private ControllerHand hand = ControllerHand.Right;

    private XRNode xrNode;
    private InputDevice device;
    private Animator animator;

    // valores suavizados
    private float gripValue = 0f;
    private float triggerValue = 0f;
    private float thumbBlend = 0f;
    private float pointBlend = 0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        // define de qual nó XR (esquerdo/direito) vamos ler
        xrNode = (hand == ControllerHand.Left)
            ? XRNode.LeftHand
            : XRNode.RightHand;
    }

    void Update()
    {
        // (re)busca o dispositivo se o anterior não estiver válido
        if (!device.isValid)
            InitializeDevice();

        if (!device.isValid)
            return;  // ainda não há controller, sai

        // 1) Grip (float 0..1)
        if (device.TryGetFeatureValue(CommonUsages.grip, out float g))
            gripValue = g;
        animator.SetFloat("Grip", gripValue);

        // 2) Trigger (float 0..1)
        if (device.TryGetFeatureValue(CommonUsages.trigger, out float t))
            triggerValue = t;
        animator.SetFloat("Trigger", triggerValue);

        // 3) Touch de polegar e dedo indicador (bool)
        bool isThumbTouch = false;
        bool isIndexTouch = false;
        device.TryGetFeatureValue(OculusUsages.thumbTouch, out isThumbTouch);
        device.TryGetFeatureValue(OculusUsages.indexTouch, out isIndexTouch);

        thumbBlend = SmoothBlend(isThumbTouch, thumbBlend);
        pointBlend = SmoothBlend(isIndexTouch, pointBlend);

        animator.SetLayerWeight(1, thumbBlend);
        animator.SetLayerWeight(2, pointBlend);
    }

    private void InitializeDevice()
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(xrNode, devices);
        if (devices.Count > 0)
            device = devices[0];
    }

    private float SmoothBlend(bool pressed, float current)
    {
        float delta = Time.deltaTime * INPUT_RATE_CHANGE;
        return Mathf.Clamp01(current + (pressed ? delta : -delta));
    }
}
