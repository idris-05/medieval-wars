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
    public static bool[,] BuildingCanHealAndSupplyThatUnit = {
    // CARAVAN | ARCHERS | CARAC | FIRESHIP | INFANTRY | TSHIP | SPIKEMAN | RCHALVARY | TREBUCHET | BANDIT | CATAPULTE | RAMSHIP | CHALVARY | Terrain    // --------------------------------------------------------------------------------------------------------------------------------------------
    { true , true , true , true , true  , true , true  , true  , true  , true , true  , true , true }, // Road
    { true , true , true , true , true  , true , true  , true  , true  , true , true  , true , true }, // Bridge
    { true , true , true , true , true  , true , true  , true , true , true , true , true , true }, // River
    { true , true , true , true  , true , true  , true , true , true , true, true , true  , true }, // Sea
    { true , true , true , true , true  , true  , true  , true  , true  , true , true  , true , true }, // Shoal
    { true , true , true , true  , true , true  , true , true , true , true, true , true  , true }, // Reef
    { true , true , true , true , true  , true , true  , true  , true  , true , true  , true , true }, // Plain
    { true , true , true , true , true  , true , true  , true  , true  , true , true  , true , true }, // Wood
    { true , true , true , true , true  , true , true  , true  , true  , true , true  , true , true }, // Village
    { true , true , true , true , true  , true , true  , true  , true  , true , true  , true , true }, // Barrack
    { true , true , true , true , true  , true , true  , true  , true  , true , true  , true , true }, // Stable
    { true , true , true , true  , true  , true  , true  , true  , true  , true , true  , true  , true }, // Port
    { true , true , true , true , true  , true , true  , true , true , true , true , true , true }, // Mountain
    { true , true , true , true , true  , true , true  , true  , true  , true , true  , true , true }  // Castle
};



}