using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using System;

public class HandAnimation : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private int index;
    List<InputFeatureUsage> inputFeatures = new List<InputFeatureUsage>();


    // Update is called once per frame

    void Update()
    {

        InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        leftController.TryGetFeatureUsages(inputFeatures);

        //Avoid the exception of wrong object type comparassion. (because inputfeatures list return bool and Vector2)
        if (inputFeatures[index].type == typeof(bool))
            if (leftController.TryGetFeatureValue(inputFeatures[index].As<bool>(), out bool thumb))
                text.text = $"{index} ThumbTouch: " + thumb;
    }
}
