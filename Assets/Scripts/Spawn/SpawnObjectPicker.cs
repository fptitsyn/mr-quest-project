using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

namespace Spawn
{
    public class SpawnObjectPicker : MonoBehaviour
    {
        [SerializeField] private InputActionAsset actions;

        [SerializeField] private Button characterButton;
        [SerializeField] private Button treeButton;
        
        public static GameObject PickedObject;
        public static PlaneClassification TargetPlaneClassification;
        public static float ObjectManaCost;
        
        private InputAction _setCharacterAction;
        private InputAction _setTreesAction;
        
        private const float TreeCost = 5;
        private const float CharacterCost = 25;
        
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
            ObjectManaCost = CharacterCost;
            treeButton.image.color = Color.white;
            characterButton.image.color = Color.gray;
        }
        
        public void SetTrees()
        {
            int r = Random.Range(1, 5);
            PickedObject = Resources.Load($"Prefabs/Trees/Tree{r}") as GameObject;
            TargetPlaneClassification = PlaneClassification.Floor;
            ObjectManaCost = TreeCost;
            treeButton.image.color = Color.grey;
            characterButton.image.color = Color.white;
        }

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