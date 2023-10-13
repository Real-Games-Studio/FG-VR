using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class HandAnimation : MonoBehaviour
{

    public static InputFeatureUsage<bool> thumbTouch;
    // Start is called before the first frame update
    public TextMeshProUGUI text;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        List<InputFeatureUsage> inputFeatures = new List<InputFeatureUsage>();
        leftController.TryGetFeatureUsages(inputFeatures);
        bool thumb;
        leftController.TryGetFeatureValue(inputFeatures[11].As<bool>(), out thumb);
        text.text = "11 ThumbTouch: " + thumb;
    }
}
