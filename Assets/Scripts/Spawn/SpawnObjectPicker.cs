using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

namespace Spawn
{
    public class SpawnObjectPicker : MonoBehaviour
    {
        [SerializeField] private InputActionAsset actions;
        
        public static GameObject PickedObject;
        public static PlaneClassification TargetPlaneClassification;
        
        private InputAction _setCharacterAction;
        private InputAction _setTreesAction;
        
        private void Start()
        {
            SetCharacter();
            
            _setCharacterAction = actions.FindAction("Set Character", true);
            _setTreesAction = actions.FindAction("Set Trees", true);
        }

        public void SetCharacter()
        {
            PickedObject = Resources.Load("Prefabs/Characters/Male1") as GameObject;
            TargetPlaneClassification = PlaneClassification.Table;
            Debug.Log("character");
        }
        
        public void SetTrees()
        {
            int r = Random.Range(1, 5);
            PickedObject = Resources.Load($"Prefabs/Trees/Tree{r}") as GameObject;
            TargetPlaneClassification = PlaneClassification.Floor;
            Debug.Log("tree");
        }

        // private void OnSetCharacter(InputAction.CallbackContext context)
        // {
        //     pickedObject = Resources.Load("Prefabs/Characters/Male1") as GameObject;
        // }
        //
        // private void OnSetTrees(InputAction.CallbackContext context)
        // {
        //     int r = Random.Range(1, 5);
        //     pickedObject = Resources.Load($"Prefabs/Trees/Tree{r}") as GameObject;
        // }
        
        // private void OnEnable()
        // {
        //     #if UNITY_EDITOR
        //         setCharacterAction.action.triggered += OnSetCharacter;
        //         setTreesAction.action.performed += OnSetTrees;
        //     #endif
        // }
        //
        // private void OnDisable()
        // {
        //     #if UNITY_EDITOR
        //         setCharacterAction.action.performed -= OnSetCharacter;
        //         setTreesAction.action.performed -= OnSetTrees;
        //     #endif
        // }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            if (_setCharacterAction.triggered)
            {
                SetCharacter();
            }

            if (_setTreesAction.triggered)
            {
                SetTrees();
            }
        }
    }
}