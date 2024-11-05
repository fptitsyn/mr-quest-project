using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class DebugMode : MonoBehaviour
{
    [SerializeField]
    private InputActionReference toggleSurfaceRenderingAction; // Действие ввода для переключения визуализации

    [SerializeField] private bool isVisualisedOnStart; // Начальное состояние визуализации

    [SerializeField] private Canvas debugPanel;
    
    private bool _isVisualised; // Текущее состояние визуализации
    private ARPlaneManager _planeManager; // Компонент для управления AR-плоскостями

    private void Awake()
    {
        // Инициализация компонентов
        _planeManager = GetComponent<ARPlaneManager>();
        _isVisualised = isVisualisedOnStart; // Устанавливаем начальное состояние визуализации
    }

    public void OnEnable()
    {
        // Подписка на действие ввода для переключения визуализации
        toggleSurfaceRenderingAction.action.started += OnToggleSurfaceRendering;

        // Подписка на события изменения AR-плоскостей и границ
        _planeManager.planesChanged += OnPlanesChanged;

        // Обновление начальной визуализации
        PlaneUpdateVisualisation();
    }

    public void OnDisable()
    {
        // Отписка от действия ввода
        toggleSurfaceRenderingAction.action.started -= OnToggleSurfaceRendering;

        // Отписка от событий изменения AR-плоскостей и границ
        _planeManager.planesChanged -= OnPlanesChanged;
    }

    // Метод-обработчик для изменения состояния AR-плоскостей
    private void OnPlanesChanged(ARPlanesChangedEventArgs arPlanesChangedEventArgs) => PlaneUpdateVisualisation();

    // Метод-обработчик для переключения визуализации при срабатывании действия ввода
    private void OnToggleSurfaceRendering(InputAction.CallbackContext obj)
    {
        _isVisualised = !_isVisualised; // Переключение состояния визуализации
        debugPanel.enabled = _isVisualised;
        PlaneUpdateVisualisation();
    }

    // Обновление визуализации AR-плоскостей
    private void PlaneUpdateVisualisation()
    {
        foreach (var arPlane in _planeManager.trackables)
        {
            if (arPlane.TryGetComponent(out ARPlaneColorizer arPlaneColorizer))
            {
                arPlaneColorizer.IsVisualised = _isVisualised; // Устанавливаем состояние визуализации
            }
        }
    }
}