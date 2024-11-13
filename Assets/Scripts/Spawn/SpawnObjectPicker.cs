using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

namespace Spawn
{
    public class SpawnObjectPicker : MonoBehaviour
    {
        public static GameObject pickedObject;
        public static PlaneClassification targetPlaneClassification;
        [SerializeField] private InputActionReference setCharacterAction;
        [SerializeField] private InputActionReference setTreesAction;
        
        private void Start()
        {
            SetCharacter();
        }

        public void SetCharacter()
        {
            pickedObject = Resources.Load("Prefabs/Characters/Male1") as GameObject;
            targetPlaneClassification = PlaneClassification.Table;
        }
        
        public void SetTrees()
        {
            int r = Random.Range(1, 5);
            pickedObject = Resources.Load($"Prefabs/Trees/Tree{r}") as GameObject;
            targetPlaneClassification = PlaneClassification.Floor;
        }

        private void OnSetCharacter(InputAction.CallbackContext context)
        {
            pickedObject = Resources.Load("Prefabs/Characters/Male1") as GameObject;
        }
        
        private void OnSetTrees(InputAction.CallbackContext context)
        {
            int r = Random.Range(1, 5);
            pickedObject = Resources.Load($"Prefabs/Trees/Tree{r}") as GameObject;
        }
        
        private void OnEnable()
        {
            #if UNITY_EDITOR
                setCharacterAction.action.performed += OnSetCharacter;
                setTreesAction.action.performed += OnSetTrees;
            #endif
        }

        private void OnDisable()
        {
            #if UNITY_EDITOR
                setCharacterAction.action.performed -= OnSetCharacter;
                setTreesAction.action.performed -= OnSetTrees;
            #endif
        }
    }
}