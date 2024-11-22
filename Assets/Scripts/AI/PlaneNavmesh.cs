using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace AI
{
    public class PlaneNavmesh : MonoBehaviour
    {
        [SerializeField] private ARPlaneManager arPlaneManager;

        private void Start()
        {
            XRPlaneSubsystem subsystem = arPlaneManager.subsystem;
            if (subsystem != null)
            {
                Debug.Log("Planes are working (evidently they are not)");
                Debug.Log(subsystem.currentPlaneDetectionMode);
                subsystem.requestedPlaneDetectionMode = PlaneDetectionMode.Horizontal;
                Debug.Log(subsystem.currentPlaneDetectionMode);
                Debug.Log(arPlaneManager.enabled);
            }
        }

        private void OnEnable()
        {
            arPlaneManager.planesChanged += OnPlanesChanged;
        }

        private void OnDisable()
        {
            arPlaneManager.planesChanged -= OnPlanesChanged;
        }

        private void OnPlanesChanged(ARPlanesChangedEventArgs changes)
        {
            Debug.Log("plane added");
            foreach (var plane in changes.added)
            {
                if (plane.alignment == PlaneAlignment.HorizontalUp)
                {
                    Debug.Log("horizontalup plane added");
                    plane.gameObject.layer = LayerMask.NameToLayer("HorizontalUp");
                    plane.GetComponent<NavMeshSurface>().enabled = true;
                    NavMeshSurface surface = plane.GetComponent<NavMeshSurface>();
                    surface.BuildNavMesh();
                }
            }

            foreach (var plane in changes.updated)
            {
                Debug.Log("plane updated");
                if (plane.alignment == PlaneAlignment.HorizontalUp)
                {
                    Debug.Log("horizontalup plane updated");
                    plane.gameObject.layer = LayerMask.NameToLayer("HorizontalUp");
                    NavMeshSurface surface = plane.GetComponent<NavMeshSurface>();
                    surface.BuildNavMesh();
                }
            }

            foreach (var plane in changes.removed)
            {
                if (plane.alignment == PlaneAlignment.HorizontalUp)
                {
                    NavMeshSurface surface = plane.GetComponent<NavMeshSurface>();
                    if (surface.enabled)
                    {
                        surface.RemoveData();
                    }
                }
            }
        }
    }
}