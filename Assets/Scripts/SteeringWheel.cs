using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Content.Interaction;

public class SteeringWheel : MonoBehaviour
{
    [Header("Controller Inputs")]
    [SerializeField] private InputActionReference _inputGripLeftController;
    [SerializeField] private InputActionReference _inputGripRightController;

    [Header("Player hands")]
    [SerializeField] private GameObject _prefabLeftHand;
    [SerializeField] private GameObject _prefabRightHand;

    [Header("Fixed steering wheel hands")]
    [SerializeField] private GameObject _playerLeftHand;
    [SerializeField] private GameObject _playerRightHand;
    [SerializeField] private bool _isHolding;


    public void OnGrab()
    {
        if (_inputGripLeftController.action.ReadValue<float>() == 1)
        {

            Vector3 leftHandVector = new Vector3(_playerLeftHand.transform.parent.position.x, _prefabLeftHand.transform.position.y, _playerLeftHand.transform.parent.position.z);
            _prefabLeftHand.transform.position = leftHandVector;
            _prefabLeftHand.transform.LookAt(transform);

            _prefabLeftHand.SetActive(true);
            _playerLeftHand.SetActive(false);

            Debug.Log("left on");
        }
        else
        {
            _prefabLeftHand.SetActive(false);
            _playerLeftHand.SetActive(true);

            Debug.Log("left off");
        }

        if (_inputGripRightController.action.ReadValue<float>() == 1)
        {
            Vector3 rightHandVector = new Vector3(_playerRightHand.transform.parent.position.x, _prefabRightHand.transform.position.y, _playerRightHand.transform.parent.position.z);
            _prefabRightHand.transform.position = rightHandVector;
            _prefabRightHand.transform.LookAt(transform);

            _prefabRightHand.SetActive(true);
            _playerRightHand.SetActive(false);

            Debug.Log("right on");
        }
        else
        {
            _prefabRightHand.SetActive(false);
            _playerRightHand.SetActive(true);

            Debug.Log("right off");
        }
    }

    public void HoldingSteerWheel(bool state)
    {
        _isHolding = state;
    }
}



