using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;

namespace DefaultNamespace
{
    public class SelectMainPlane : MonoBehaviour
    {
        [SerializeField] private XRRayInteractor rayInteractor;
        [SerializeField] private InputActionAsset actions;
        
        private InputAction _selectPlaneAction;
        
        public ARPlane selectedPlane;

        private void Start()
        {
            _selectPlaneAction = actions.FindAction("Select Plane"); 
        }

        public void SelectPlane()
        {
            if (rayInteractor.enabled && rayInteractor.TryGetCurrent3DRaycastHit(out var hit, out _))
            {
                if (hit.transform.TryGetComponent<ARPlane>(out var plane))
                {
                    selectedPlane = plane;
                    selectedPlane.AddComponent<ARAnchor>();
                    selectedPlane.GetComponent<MeshRenderer>().material.color = Color.red;
                    Debug.Log("Selected plane " + selectedPlane.name);
                }
            }
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (_selectPlaneAction.triggered)
            {
                Debug.Log("triggered");
                SelectPlane();
            }
        }
    }
}