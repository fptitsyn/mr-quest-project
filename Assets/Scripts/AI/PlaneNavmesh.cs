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

        // private List<ARPlane> _planes = new List<ARPlane>();
        
        private void Start()
        {
            // XRPlaneSubsystem subsystem = arPlaneManager.subsystem;
            // if (subsystem != null)
            // {
            //     Debug.Log("Planes are working (evidently they are not)");
            //     Debug.Log("requested " + subsystem.requestedPlaneDetectionMode);
            //     Debug.Log("current " + subsystem.currentPlaneDetectionMode);
            //     subsystem.requestedPlaneDetectionMode = PlaneDetectionMode.Horizontal;
            //     Debug.Log("requested " + subsystem.requestedPlaneDetectionMode);
            //     Debug.Log("current " + subsystem.currentPlaneDetectionMode);
            //     Debug.Log("ar plane manager enabled" + arPlaneManager.enabled);
            // }
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
            foreach (var plane in changes.added)
            {
                if (plane.alignment == PlaneAlignment.HorizontalUp)
                {
                    // if (_planes.Count < 20)
                    // {
                        Debug.Log("plane added");
                        // _planes.Add(plane);
                        Debug.Log("horizontalup plane added");
                        plane.gameObject.layer = LayerMask.NameToLayer("HorizontalUp");
                        plane.GetComponent<NavMeshSurface>().enabled = true;
                        NavMeshSurface surface = plane.GetComponent<NavMeshSurface>();
                        surface.BuildNavMesh();
                    // }
                }
            }

            foreach (var plane in changes.updated)
            {
                if (plane.alignment == PlaneAlignment.HorizontalUp)
                {
                    // if (!_planes.Contains(plane))
                    // {
                    //     return;
                    // }
                    Debug.Log("horizontalup plane updated");
                    // plane.gameObject.layer = LayerMask.NameToLayer("HorizontalUp");
                    NavMeshSurface surface = plane.GetComponent<NavMeshSurface>();
                    surface.BuildNavMesh();
                }
            }

            // foreach (var plane in changes.removed)
            // {
            //     // Maybe don't actually do anything
            //     if (plane.alignment == PlaneAlignment.HorizontalUp)
            //     {
            //         // if (_planes.Contains(plane))
            //         // {
            //         //     _planes.Remove(plane);
            //             Debug.Log("horizontalup plane removed");
            //             NavMeshSurface surface = plane.GetComponent<NavMeshSurface>();
            //             if (surface.enabled)
            //             {
            //                 surface.RemoveData();
            //             }
            //         // }
            //     }
            // }
        }
    }
}