using System.Collections;
using System.Collections.Generic;
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


    public void OnGrab()
    {
        if (_inputGripLeftController.action.ReadValue<float>() > .05f)
        {
            Vector3 leftHandVector = new Vector3(_playerLeftHand.transform.parent.position.x, _prefabLeftHand.transform.position.y, _playerLeftHand.transform.parent.position.z);
            _prefabLeftHand.transform.position = leftHandVector;
            _prefabLeftHand.transform.LookAt(transform);

            _prefabLeftHand.SetActive(true);
            _playerLeftHand.SetActive(false);
        }
        else
        {
            _prefabLeftHand.SetActive(false);
            _playerLeftHand.SetActive(true);
        }

        if (_inputGripRightController.action.ReadValue<float>() > .05f)
        {
            Vector3 rightHandVector = new Vector3(_playerRightHand.transform.parent.position.x, _prefabRightHand.transform.position.y, _playerRightHand.transform.parent.position.z);
            _prefabRightHand.transform.position = rightHandVector;
            _prefabRightHand.transform.LookAt(transform);

            _prefabRightHand.SetActive(true);
            _playerRightHand.SetActive(false);
        }
        else
        {
            _prefabRightHand.SetActive(false);
            _playerRightHand.SetActive(true);
        }
    }
}


