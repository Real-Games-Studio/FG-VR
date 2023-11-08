using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;


public class CarController : MonoBehaviour
{
   enum wheelSides { LeftUp, RightUp, DownLeft, DownRight }
   private const string VERTICAL = "Vertical";
   private const float STEERINGWHEEL_VALUE_ADJUST = .5f;
   private float horizontalInput;
   private float verticalInput;
   private float currentSteerAngle;
   private float currentbreakForce;
   private bool isBreaking;

   [SerializeField] private XRKnob xRKnobWheelScript;

   [Header("Engine vars")]
   [SerializeField] private float motorForce;
   [SerializeField] private float breakForce;
   [SerializeField] private float maxSteeringAngle;
   [SerializeField] private WheelCollider[] wheelColliders;
   [SerializeField] private Transform[] wheelsTransforms;
   [SerializeField] private bool setWheels;

   [SerializeField] private InputActionReference _inputTriggerLeftController;

   private void Update()
   {
      GetInput();
      HandleMotor();
      HandleSteering();
      UpdateWheels();
   }

   private void OnValidate()
   {
      if (setWheels) StartWheels();
   }

   private void StartWheels()
   {
      for (int i = 0; i < wheelsTransforms.Length; i++)
      {
         wheelsTransforms[i].name = "Wheel " + (wheelSides)i;

         Vector3 pos;
         Quaternion rot;
         wheelColliders[i].GetWorldPose(out pos, out rot);
         wheelsTransforms[i].rotation = rot;
         wheelsTransforms[i].position = pos;
      }

      setWheels = false;
   }

   private void GetInput()
   {
      horizontalInput = xRKnobWheelScript.value - STEERINGWHEEL_VALUE_ADJUST;
      verticalInput = Mathf.Clamp(_inputTriggerLeftController.action.ReadValue<float>(), 0, 1);
      isBreaking = Input.GetKey(KeyCode.Space);
   }

   private void HandleMotor()
   {
      for (int i = 2; i < 4; i++)
         wheelColliders[i].motorTorque = verticalInput * motorForce;


      currentbreakForce = isBreaking ? breakForce : 0f;

      ApplyBreaking();
   }

   private void ApplyBreaking()
   {
      for (int i = 0; i < wheelColliders.Length; i++)
         wheelColliders[i].brakeTorque = currentbreakForce;
   }

   private void HandleSteering()
   {
      currentSteerAngle = maxSteeringAngle * horizontalInput;

      for (int i = 0; i < 2; i++)
         wheelColliders[i].steerAngle = currentSteerAngle;
   }

   private void UpdateWheels()
   {
      for (int i = 0; i < wheelsTransforms.Length; i++)
         UpdateSingleWheel(wheelColliders[i], wheelsTransforms[i]);
   }


   private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
   {
      Vector3 pos;
      Quaternion rot;
      wheelCollider.GetWorldPose(out pos, out rot);
      wheelTransform.rotation = rot;
      wheelTransform.position = pos;
   }
}
