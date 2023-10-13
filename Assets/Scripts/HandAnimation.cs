using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class HandAnimation : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private int index;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        List<InputFeatureUsage> inputFeatures = new List<InputFeatureUsage>();
        leftController.TryGetFeatureUsages(inputFeatures);
        // leftController.TryGetFeatureValue(inputFeatures[index].As<bool>(), out bool thumb);
        bool thumb;
        if (leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryTouch, out thumb))
        {
            if (text)
            {
                text.text = $"{index} ThumbTouch: " + thumb;
                Debug.Log("entrei");
            }
        }
    }
}
