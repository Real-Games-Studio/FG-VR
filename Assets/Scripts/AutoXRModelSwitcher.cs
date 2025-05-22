// using UnityEngine;
// using Oculus;

// public class AutoXRModelSwitcher : MonoBehaviour
// {
//     [Header("Controller Models")]
//     public GameObject leftControllerModel;
//     public GameObject rightControllerModel;

//     [Header("Hand Models")]
//     public GameObject leftHandModel;
//     public GameObject rightHandModel;

//     void Update()
//     {
//         // obtém a máscara de controllers conectados neste momento
//         var con = OVRInput.GetConnectedControllers();
//         bool handsConnected = (con & OVRInput.Controller.Hands) == OVRInput.Controller.Hands;
//         bool touchControllers = (con & OVRInput.Controller.LTouch) == OVRInput.Controller.LTouch
//                                  || (con & OVRInput.Controller.RTouch) == OVRInput.Controller.RTouch;

//         // Se houver **somente** mãos use hands, se houver **controllers** use controllers.
//         // Se ambos estiverem, você decide qual priorizar (aqui priorizamos controllers).
//         bool showHands = handsConnected && !touchControllers;

//         leftControllerModel.SetActive(!showHands);
//         rightControllerModel.SetActive(!showHands);

//         leftHandModel.SetActive(showHands);
//         rightHandModel.SetActive(showHands);
//     }
// }
