using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

namespace Spawn
{
    public class SpawnObjectPicker : MonoBehaviour
    {
        public static GameObject pickedObject;
        public static PlaneClassification targetPlaneClassification;

        private void Start()
        {
            SetHuman();
        }

        public void SetHuman()
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
    }
}