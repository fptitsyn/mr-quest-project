using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit;

namespace Spawn
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private InputActionReference spawnAction;
        [SerializeField] private InputActionReference switchAction;
        [SerializeField] private InputActionReference spawnOnKeyAction;
        [SerializeField] private XRRayInteractor xrRayInteractor;
        // [SerializeField] private PlaneClassification targetPlaneClassification;
        // [SerializeField] private GameObject objectPrefab;
    
        public static List<GameObject> Trees = new ();
        
        private void Spawn(InputAction.CallbackContext context)
        {
            if (xrRayInteractor.enabled && xrRayInteractor.TryGetCurrent3DRaycastHit(out var raycastHit, out _))
            {
                if (raycastHit.transform.TryGetComponent(out ARPlane arPlane) && arPlane.classification == SpawnObjectPicker.TargetPlaneClassification)
                {
                    AudioManager.Instance.PlaySfx("Spawn");
                    var hitPose = new Pose(raycastHit.point, Quaternion.LookRotation(raycastHit.normal));

                    var instantiate = Instantiate(SpawnObjectPicker.PickedObject, hitPose.position, hitPose.rotation);
                    // instantiate.name = "SpawnedObject";
                    
                    instantiate.AddComponent<ARAnchor>();
                    if (instantiate.CompareTag("Tree"))
                    {
                        Debug.Log("added tree");
                        // Trees.Add(instantiate);
                    }
                    return;
                }
                // if (raycastHit.transform.name == "SpawnedObject")
                // {
                //     Destroy(raycastHit.transform.gameObject);
                // }
            }
        }

        private void SpawnOnKey(InputAction.CallbackContext context)
        {
            AudioManager.Instance.PlaySfx("Spawn");
            GameObject player = GameObject.Find("SimulationCamera");
            var instantiate = Instantiate(SpawnObjectPicker.PickedObject, player.transform.position + Vector3.down * 3, Quaternion.identity);
            // instantiate.name = "SpawnedObject";
            if (instantiate.CompareTag("Tree"))
            {
                Debug.Log("added tree");
                // trees.Add(instantiate);
            }
            instantiate.AddComponent<ARAnchor>();
        }
        
        private void OnEnable()
        {
            #if UNITY_EDITOR
            spawnOnKeyAction.action.performed += SpawnOnKey;
            #endif
            spawnAction.action.Enable();
            spawnAction.action.performed += Spawn;
            switchAction.action.Enable();
        }

        private void OnDisable()
        {
            #if UNITY_EDITOR
            spawnOnKeyAction.action.performed += SpawnOnKey;
            #endif
            spawnAction.action.Disable();
            spawnAction.action.performed -= Spawn;
            switchAction.action.Disable();
        }
    }
}
