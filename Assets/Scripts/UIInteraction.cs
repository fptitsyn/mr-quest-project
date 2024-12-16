using Audio;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.OpenXR.Features.Meta;

public class UIInteraction : MonoBehaviour
{
    [SerializeField] private Button resetSceneButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitSettingsButton;

    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject settingsUI;
    
    [SerializeField] private GameObject capsulePrefab;

    private void Awake()
    {
        resetSceneButton.onClick.AddListener(ResetScene);
        exitButton.onClick.AddListener(ExitGame);
        settingsButton.onClick.AddListener(ToggleSettings);
        exitSettingsButton.onClick.AddListener(ToggleSettings);
    }

    private void Start()
    {
        AudioManager.Instance.ShuffleMusic();
    }
    
    private void ResetScene()
    {
        AudioManager.Instance.PlaySfx("Click");
        ARSession arSession = FindAnyObjectByType<ARSession>();
        bool result = (arSession.subsystem as MetaOpenXRSessionSubsystem)?.TryRequestSceneCapture() ?? false;
        Debug.Log($"Запрос на захват сцены Meta OpenXR завершен с результатом: {result}");
    }

    private void ToggleSettings()
    {
        AudioManager.Instance.PlaySfx("Click");
        mainUI.SetActive(!mainUI.activeSelf);
        settingsUI.SetActive(!settingsUI.activeSelf);
    }
    
    private void ExitGame()
    {
        AudioManager.Instance.PlaySfx("Click");
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
