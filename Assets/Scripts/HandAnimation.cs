using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using System;
using System.Xml.Linq;

enum Controller
{
    LeftHand,
    RightHand
}

public class HandAnimation : MonoBehaviour
{
    public const float INPUT_RATE_CHANGE = 15.0f;
    private float pointBlend, thumbBlend;
    private Animator myAnimator;
    private List<InputFeatureUsage> inputFeatures = new List<InputFeatureUsage>();
    private Dictionary<Controller, XRNode> controllerType = new Dictionary<Controller, XRNode>
    {
        {Controller.LeftHand , XRNode.LeftHand },
        {Controller.RightHand , XRNode.RightHand }
    };
    [SerializeField] private Controller sideController;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnimStates();
    }



    private void SetGripAnim(InputDevice controller)
    {
        controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out float value);
        myAnimator.SetFloat("Grip", value);
    }

    private void UpdateAnimStates()
    {
        InputDevice vrController = InputDevices.GetDeviceAtXRNode(controllerType[sideController]);
        vrController.TryGetFeatureUsages(inputFeatures);

        // Grip
        SetGripAnim(vrController);

        //Avoid the exception of wrong object type comparassion. (because inputfeatures list return bool and Vector2)
        if (inputFeatures[5].type == typeof(bool))
        {

            // Thumbs up
            bool thumb;
            if (vrController.TryGetFeatureValue(inputFeatures[5].As<bool>(), out thumb) || vrController.TryGetFeatureValue(inputFeatures[7].As<bool>(), out thumb) || vrController.TryGetFeatureValue(inputFeatures[12].As<bool>(), out thumb) || vrController.TryGetFeatureValue(inputFeatures[13].As<bool>(), out thumb))
            {
                thumbBlend = InputValueRateChange(!thumb, thumbBlend);
                myAnimator.SetLayerWeight(1, thumbBlend);
                Debug.Log("teste");
            }

            // if (vrController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryTouch, out bool a)) { }            

            // Point
            if (vrController.TryGetFeatureValue(inputFeatures[10].As<bool>(), out bool point) && point) // "&& point" to call only when pressed
            {
                pointBlend = InputValueRateChange(!point, pointBlend);
                myAnimator.SetLayerWeight(2, pointBlend);
                Debug.Log("teste 2");
            }
        }
    }

    private float InputValueRateChange(bool isDown, float value)
    {
        float rateDelta = Time.deltaTime * INPUT_RATE_CHANGE;
        float sign = isDown ? 1.0f : -1.0f;
        return Mathf.Clamp01(value + rateDelta * sign);
    }


    // void Update()
    // {

    //     InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    //     leftController.TryGetFeatureUsages(inputFeatures);

    //     //Avoid the exception of wrong object type comparassion. (because inputfeatures list return bool and Vector2)
    //     if (inputFeatures[index].type == typeof(bool))
    //     {
    //         leftController.TryGetFeatureValue(inputFeatures[index].As<bool>(), out bool thumb);
    //         text.text = $"{index} ThumbTouch: " + thumb;
    //     }
    // }
}
