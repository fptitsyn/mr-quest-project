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
        public static List<GameObject> Characters = new ();

        private void Spawn(InputAction.CallbackContext context)
        {
            if (SpawnObjectPicker.ObjectManaCost > Player.Instance.Mana)
            {
                return;
            }

            if (xrRayInteractor.enabled && xrRayInteractor.TryGetCurrent3DRaycastHit(out var raycastHit, out _))
            {
                if (raycastHit.transform.TryGetComponent(out ARPlane arPlane))
                {
                    AudioManager.Instance.PlaySfx("Spawn");
                    var hitPose = new Pose(raycastHit.point, Quaternion.LookRotation(raycastHit.normal));

                    var instantiate = Instantiate(SpawnObjectPicker.PickedObject, hitPose.position, Quaternion.identity);
                    // instantiate.name = "SpawnedObject";
                    
                    // instantiate.AddComponent<ARAnchor>();
                    if (instantiate.CompareTag("Tree"))
                    {
                        // Debug.Log("added tree");
                        instantiate.AddComponent<ARAnchor>();
                    }
                    else // characters
                    {
                        Characters.Add(instantiate);
                    }
                    
                    Player.Instance.Mana -= SpawnObjectPicker.ObjectManaCost;
                }
            }
        }

        private void SpawnOnKey(InputAction.CallbackContext context)
        {
            if (SpawnObjectPicker.ObjectManaCost > Player.Instance.Mana)
            {
                return;
            }
            
            AudioManager.Instance.PlaySfx("Spawn");
            GameObject player = GameObject.Find("SimulationCamera");
            var instantiate = Instantiate(SpawnObjectPicker.PickedObject, player.transform.position + Vector3.down * 3, Quaternion.identity);
            // instantiate.name = "SpawnedObject";
            if (instantiate.CompareTag("Tree"))
            {
                // Debug.Log("added tree");
            }
            instantiate.AddComponent<ARAnchor>();
            Player.Instance.Mana -= SpawnObjectPicker.ObjectManaCost;
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
