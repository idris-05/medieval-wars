using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UserInterfaceUtil : MonoBehaviour
{

    private static UserInterfaceUtil instance;
    public static UserInterfaceUtil Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<UserInterfaceUtil>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("UserInterfaceUtil");
                    instance = obj.AddComponent<UserInterfaceUtil>();
                }
            }
            return instance;
        }
    }

    public Sprite[] numbersFromZeroToTenSpritesForHealth;

    public DamageIcon damageIconPrefab;

    public UnitHealthIcon UnitHealthIconPrefab;

    [SerializeField] public GameObject CellhighlightLines;

    [SerializeField] public GameObject CellhighlightHolder;

    [SerializeField] public List<GameObject> GlowLinesWhenHighlightedPrefabs;

    public List<GameObject> GlowLinesThatExistOnTheScene;

    public GameObject RedDaggerPrefab;

    public List<Sprite> FlagSprites;

    public GameObject FlagPrefab;

    public GameObject FlagHolder;

    public GameObject CaptureEffect;

    public GameObject BuySpawnEffect;

    public GameObject RecieveSupplyIconPrefab;

    public GameObject RecieveHealEffectPrefab;

    public SupplyLackApple SupplyLackApplePrefab;

    // 0 : white
    // 1 : blue
    // 2 : red


    // public GameObject HighlightLightPrefab;

    // public Light2D HighlightLight;
}
