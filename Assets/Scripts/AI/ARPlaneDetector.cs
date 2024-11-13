using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace AI
{
    public class ARPlaneDetector : MonoBehaviour
    {
        [SerializeField] private ARPlaneManager arPlaneManager;
        private GameObject _player;

        private void Start()
        {
            _player = GameObject.Find("SimulationCamera");
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
                    plane.gameObject.layer = LayerMask.NameToLayer("HorizontalUp");
                    plane.GetComponent<NavMeshSurface>().enabled = true;
                }
            }

            foreach (var plane in changes.updated)
            {
                if (plane.alignment == PlaneAlignment.HorizontalUp)
                {
                    plane.gameObject.layer = LayerMask.NameToLayer("HorizontalUp");
                    NavMeshSurface surface = plane.GetComponent<NavMeshSurface>();
                    surface.BuildNavMesh();
                    Debug.Log("navmesh updated");
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
                        Debug.Log("navmesh removed");
                    }
                }
            }
        }

        // private IEnumerator UpdatePlaneNavMeshData(ARPlane plane)
        // {
        //     NavMeshSurface surface = plane.GetComponent<NavMeshSurface>();
        //     NavMeshData navMeshData = new NavMeshData
        //     {
        //         position = plane.transform.position,
        //         rotation = plane.transform.rotation
        //     };
        //     AsyncOperation operation = surface.UpdateNavMesh(navMeshData);
        //     while (!operation.isDone)
        //     {
        //         yield return null;
        //     }        
        //     Debug.Log("navmesh updated");
        // }
        //
        // private IEnumerator NavMeshOutOfDateCoroutine(Vector3 playerPosition, float navigationMeshRadius, bool rebuildAll)
        // {
        //     // Get the list of all "sources" around us.  This is basically little gridded subsquares
        //     // of our terrains.
        //     List<NavMeshBuildSource> buildSources = new List<NavMeshBuildSource>();
        //
        //     // Set up a boundary area for the build sources collector to look at;
        //     Bounds patchBounds = new Bounds(playerPosition,
        //         new Vector3(navigationMeshRadius, navigationMeshRadius, navigationMeshRadius));
        //
        //     // This actually collects the potential surfaces.
        //     NavMeshBuilder.CollectSources(
        //         patchBounds,
        //         1 << LayerMask.NameToLayer("HorizontalUp"),
        //         NavMeshCollectGeometry.RenderMeshes,
        //         0,
        //         new List<NavMeshBuildMarkup>(),
        //         buildSources);
        //
        //     yield return null;
        //
        //     // Build some empty NavMeshData objects
        //     int numAgentTypes = NavMesh.GetSettingsCount();
        //     NavMeshData[] meshData = new NavMeshData[numAgentTypes];
        //
        //     for (int agentIndex = 0; agentIndex < numAgentTypes; agentIndex++)
        //     {
        //         // Get the settings for each of our agent "sizes" (humanoid, giant humanoid)
        //         NavMeshBuildSettings bSettings = NavMesh.GetSettingsByIndex(agentIndex);
        //
        //         // If there are any issues with the agent, print them out as a warning.
        //         #if DEBUG
        //             foreach (string s in bSettings.ValidationReport(patchBounds))
        //             {
        //                 Debug.LogWarning($"BuildSettings Report: {NavMesh.GetSettingsNameFromID(bSettings.agentTypeID)} : {s}");
        //             }
        //         #endif
        //         // Make empty mesh data object.
        //         meshData[agentIndex] = new NavMeshData();
        //
        //         AsyncOperation buildOp = NavMeshBuilder.UpdateNavMeshDataAsync(meshData[agentIndex], bSettings, buildSources, patchBounds);
        //
        //         while (!buildOp.isDone) yield return null;
        //     }
        //
        //     if (rebuildAll)
        //     {
        //         NavMesh.RemoveAllNavMeshData();
        //     }
        //
        //     for (int nmd = 0; nmd < meshData.Length; nmd++)
        //     {
        //         NavMesh.AddNavMeshData(meshData[nmd]);
        //     }
        //
        //     yield return null;
        // }
    }
}