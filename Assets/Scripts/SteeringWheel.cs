using UnityEngine;
using UnityEngine.InputSystem;

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
            
            _prefabLeftHand.transform.position = _playerLeftHand.transform.parent.position;

            _prefabLeftHand.SetActive(true);
            _playerLeftHand.SetActive(false);
        }
        else
        {
            _prefabLeftHand.SetActive(false);
            _playerLeftHand.SetActive(true);
        }

        if (_inputGripRightController.action.ReadValue<float>() == 1)
        {
            _prefabRightHand.transform.position = _playerRightHand.transform.parent.position;

            _prefabRightHand.SetActive(true);
            _playerRightHand.SetActive(false);
        }
        else
        {
            _prefabRightHand.SetActive(false);
            _playerRightHand.SetActive(true);
        }
    }

    public void HoldingSteerWheel(bool state)
    {
        _isHolding = state;
    }
}



