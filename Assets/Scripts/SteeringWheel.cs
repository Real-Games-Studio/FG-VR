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

    [Header("Steering Wheel")]
    [SerializeField] private GameObject _centerSteeringWheel;

    [Range(0, 3)]
    [SerializeField] private float _radius;

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
            Vector3 direction = (_playerLeftHand.transform.position - _centerSteeringWheel.transform.localPosition).normalized;
            Debug.Log(direction);
            _prefabLeftHand.transform.localPosition = _centerSteeringWheel.transform.localPosition + direction * _radius;

            // _prefabLeftHand.transform.localPosition = new Vector3(-.11f, 0, .026f);

            _prefabLeftHand.SetActive(true);
            _playerLeftHand.SetActive(false);
        }
        else
        {
            _prefabLeftHand.SetActive(false);
            _playerLeftHand.SetActive(true);
        }

        // if (_inputGripRightController.action.ReadValue<float>() == 1)
        // {


        //     _prefabRightHand.SetActive(true);
        //     _playerRightHand.SetActive(false);
        // }
        // else
        // {
        //     _prefabRightHand.SetActive(false);
        //     _playerRightHand.SetActive(true);
        // }
    }

    public void HoldingSteerWheel(bool state)
    {
        _isHolding = state;
    }
}



