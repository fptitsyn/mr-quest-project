using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace DefaultNamespace
{
    public class ARPlaneDetector : MonoBehaviour
    {
        [SerializeField] private ARPlaneManager arPlaneManager;
        
        private void OnEnable()
        {
            arPlaneManager.planesChanged += OnPlanesChanged;
        }

        private void OnDisable()
        {
            arPlaneManager.planesChanged -= OnPlanesChanged;
        }

        public void OnPlanesChanged(ARPlanesChangedEventArgs changes)
        {
            foreach (var plane in changes.added)
            {
                Debug.Log(plane.alignment + " added");
            }

            foreach (var plane in changes.updated)
            {
                Debug.Log(plane.alignment + " updated");
            }

            foreach (var plane in changes.removed)
            {
                Debug.Log(plane.alignment + " removed");
            }
        }
    }
}