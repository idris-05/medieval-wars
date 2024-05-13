using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    public GameObject spotlightPrefab;
    public Light2D globalLight;
    public float dayNightDuration ;
    public float startTime;
    public Gradient gradient;

    // Struct to store light variation data
    public struct LightVariation
    {
        public float baseIntensity;
        public float baseRadius;
        public float timeMultiplier;
        public float intensityVariation;
        public float radiusVariation;
        public float timeVariation;
    }

    // Dictionary to store light variation for each spotlight
    public Dictionary<GameObject, LightVariation> lightVariations = new Dictionary<GameObject, LightVariation>();

    private void Start()
    {
        globalLight = FindAnyObjectByType<Light2D>();
        startTime = Time.time;
        lightVariations.Clear();

       AddLightsToBuildings();
    }

    private void Update()
    {
        // Loop throught each light and variation pair
        foreach(var lightAndVariation in lightVariations)
        {
            GameObject spotLight = lightAndVariation.Key;
            LightVariation variation = lightAndVariation.Value;

            float timeOffset = Time.time * variation.timeMultiplier;

            // Calculate oscillating values based on the stored base values

            float currentIntensity = variation.baseIntensity + Mathf.Sin(timeOffset) * variation.intensityVariation;

            float currentRadius = variation.baseRadius + Mathf.Cos(timeOffset) * variation.radiusVariation;

            spotLight.GetComponent<Light2D>().intensity = currentIntensity;
            spotLight.GetComponent<Light2D>().pointLightInnerRadius = currentRadius;
        }

        VaryGlobalLight();
    }

    // Create a spotLight at given position with random variations
   
    public void InstantiateSpotLight(Vector3 position)
    {
            GameObject newLight = Instantiate(spotlightPrefab,position, Quaternion.identity);

            newLight.transform.parent = FindAnyObjectByType<LightManager>().transform; // Set Parent

            // Generate random variations for the light

            LightVariation variation = new()
            {
                baseIntensity = Mathf.Clamp(Random.value, 0.7f, 1.2f),
                baseRadius = Mathf.Clamp(Random.value, 0.6f, 1.0f),
                intensityVariation = Mathf.Clamp(Random.value, 0.2f, 0.45f),
                radiusVariation = Mathf.Clamp(Random.value, 0.07f, 0.1f),
                timeMultiplier = Mathf.Clamp(Random.value, 0.6f, 0.8f),
            };

            // Add Light and its variation to the dictionary

            lightVariations.Add(newLight, variation);
    }

    public void AddLightsToBuildings()
    {
        foreach(Building building in FindObjectsOfType<Building>() )
        {
            InstantiateSpotLight(building.transform.position);
        }
    }

    // Day and night logic

    public void VaryGlobalLight()
    {
        float timeElapsed = Time.time - startTime;
        float percentage = Mathf.Sin(timeElapsed / dayNightDuration * Mathf.PI * 2) * 0.5f + 0.5f;
        percentage = Mathf.Clamp01(percentage);

        globalLight.color = gradient.Evaluate(percentage);
    }

}
