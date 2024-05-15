using System;
using UnityEngine;

public class BuildingsUtil : MonoBehaviour
{
    // barrack :
    // archers , infantries , spike man , bandit , catapult , trebuchet 

    // stable : 
    // calvary , R_calvary , caravan

    // dock :
    // RamShip , Tship , carrack , fireship

    // !! 5essn@ matrice fiha chkon (bulding) li y9der chkon (l unit)
    // had l matrice mr7ch tb9a haka , ndiro fiha ghir l buildings , na7iw les terrains

    public static bool[,] BuildingCanHealAndSupplyThatUnit =
  {
    // CARAVAN|ARCHERS|CARRACK|FIRESHIP|INFA |TSHIP |SPEAR | RCAVALRY | BANDIT | CATAPULTE | CAVALRY | Terrain
    // ------------------------------------------------------------   -----------------------------------------------------------------------------
    { false,  true,    false,  false,  true , false, true , false,    true ,    true ,      false }, // Barrack
    { false,  false,   true ,  true ,  false, true , false, false,    false,    false,      false }, // Port
    { true ,  false,   false,  false,  false, false, false, true ,    false,    false,      true  }, // Stable
    { false,  true,    false,  false,  true , false, true , false,    true ,    true ,      false }, // Castle
    { false,  true,    false,  false,  true , false, true , false,    true ,    true ,      false }, // Village
    { false,  false,   false,  false,  false, false, false, false,    false,    false,      false }, // Road
    { false,  false,   false,  false,  false, false, false, false,    false,    false,      false }, // Bridge
    { false,  false,   false,  false,  false, false, false, false,    false,    false,      false }, // River
    { false,  false,   false,  false,  false, false, false, false,    false,    false,      false }, // Sea
    { false,  false,   false,  false,  false, false, false, false,    false,    false,      false }, // Shoal
    { false,  false,   false,  false,  false, false, false, false,    false,    false,      false }, // Reef
    { false,  false,   false,  false,  false, false, false, false,    false,    false,      false }, // Plain
    { false,  false,   false,  false,  false, false, false, false,    false,    false,      false }, // Wood
    { false,  false,   false,  false,  false, false, false, false,    false,    false,      false }  // Mountain
};





}