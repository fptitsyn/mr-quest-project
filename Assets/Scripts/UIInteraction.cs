using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.OpenXR.Features.Meta;

public class UIInteraction : MonoBehaviour
{
    [SerializeField] private Button spawnCapsuleButton;
    [SerializeField] private Button resetSceneButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private GameObject capsulePrefab;
    

    private void Awake()
    {
        spawnCapsuleButton.onClick.AddListener(SpawnCapsule);
        resetSceneButton.onClick.AddListener(ResetScene);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void SpawnCapsule()
    {
        Instantiate(capsulePrefab, transform.position - transform.forward * 0.5f, Quaternion.identity);
    }
    
    private void ResetScene()
    {
        ARSession arSession = FindAnyObjectByType<ARSession>();
        bool result = (arSession.subsystem as MetaOpenXRSessionSubsystem)?.TryRequestSceneCapture() ?? false;
        Debug.Log($"Запрос на захват сцены Meta OpenXR завершен с результатом: {result}");
    }
    
    private void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
