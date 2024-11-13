using Audio;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit;

namespace Spawn
{
    public class SpawnerMulticoloredObjects : MonoBehaviour
    {
        [SerializeField] private InputActionReference spawnAction;
        [SerializeField] private InputActionReference switchAction;
        [SerializeField] private XRRayInteractor xrRayInteractor;
        // [SerializeField] private PlaneClassification targetPlaneClassification;
        // [SerializeField] private GameObject objectPrefab;
    
        private void Spawn(InputAction.CallbackContext context)
        {
            if (xrRayInteractor.enabled && xrRayInteractor.TryGetCurrent3DRaycastHit(out var raycastHit, out _))
            {
            #if UNITY_EDITOR
                if (raycastHit.transform.TryGetComponent(out ARPlane arPlane) && arPlane.alignment == PlaneAlignment.HorizontalUp)
                {
                    AudioManager.Instance.PlaySfx("Spawn");
                    var hitPose = new Pose(raycastHit.point, Quaternion.LookRotation(raycastHit.normal));

                    var instantiate = Instantiate(SpawnObjectPicker.pickedObject, hitPose.position, hitPose.rotation);
                    // instantiate.name = "SpawnedObject";

                    instantiate.AddComponent<ARAnchor>();

                    return;
                }
            #else 
            if (raycastHit.transform.TryGetComponent(out ARPlane arPlane) && arPlane.classification == SpawnObjectPicker.targetPlaneClassification)
                {
                    AudioManager.Instance.PlaySfx("Spawn");
                    var hitPose = new Pose(raycastHit.point, Quaternion.LookRotation(raycastHit.normal));

                    var instantiate = Instantiate(SpawnObjectPicker.pickedObject, hitPose.position, hitPose.rotation);
                    // instantiate.name = "SpawnedObject";

                    instantiate.AddComponent<ARAnchor>();

                    return;
                }
            #endif
                // if (raycastHit.transform.name == "SpawnedObject")
                // {
                //     Destroy(raycastHit.transform.gameObject);
                // }
            }
        }
    
        private void OnEnable()
        {
            spawnAction.action.Enable();
            spawnAction.action.performed += Spawn;
            switchAction.action.Enable();
        }

        private void OnDisable()
        {
            spawnAction.action.Disable();
            spawnAction.action.performed -= Spawn;
            switchAction.action.Disable();
        }
    }
}
