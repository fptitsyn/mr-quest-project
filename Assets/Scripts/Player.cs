using System;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text manaText;
    
    [SerializeField] private float manaMultiplier = 1f;

    private const float ManaGeneration = 1f;
    private const float MaxMana = 200f;

    private float _mana;

    public float Mana
    {
        get => _mana;
        set => _mana = Mathf.Clamp(value, 0f, MaxMana);
    }

    private float _timer;
    private const float GenerationCooldown = 1f;
    private bool _canAdd = true;

    public static Player Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Mana = 50;
    }

    private void Update()
    {
        AddMana();
        manaText.text = Mathf.FloorToInt(_mana).ToString();
    }

    private void AddMana()
    {
        if (_canAdd)
        {
            Mana += ManaGeneration * manaMultiplier;
            _canAdd = false;
        }
        else
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _timer = GenerationCooldown;
                _canAdd = true;
            }
        }
    }
    
    private void SpendMana(float cost)
    {
        Mana -= cost;
    }
}