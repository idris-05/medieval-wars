using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceUtil : MonoBehaviour
{
    public Sprite[] numbersFromZeroToTenSpritesForHealth;

    public DamageIcon damageIconPrefab;

    public GameObject UnitHealthIconPrefab;

    [SerializeField] public GameObject CellhighlightLines;

    [SerializeField] public GameObject CellhighlightHolder;

    [SerializeField] public List<GameObject> GlowLinesWhenHighlightedPrefabs;

    public List<GameObject> GlowLinesThatExistOnTheScene;
}
