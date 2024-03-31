using UnityEngine;
using System.IO;

public class  extractormap : MonoBehaviour
{
    // Votre tableau représentant la carte
    public int[,] mapData;

    void Start()
    {
        // Chemin du fichier où la carte est sauvegardée
        string filePath = Application.dataPath + "/maps/map.txt";

        // Charger la carte depuis le fichier
        LoadMapFromFile(filePath);

        // Afficher la carte dans la console à titre de vérification
        Debug.Log("hi");
        PrintMap();
    }

    void LoadMapFromFile(string filePath)
    {

        // Vérifier si le fichier existe
        if (File.Exists(filePath))
        {
            // Lire les lignes du fichier
            string[] lines = File.ReadAllLines(filePath);

            // Initialiser le tableau en fonction du nombre de lignes et d'éléments par ligne
            mapData = new int[lines.Length, lines[0].Split(',').Length];

            // Remplir le tableau avec les données du fichier
            for (int i = 0; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                for (int j = 0; j < values.Length; j++)
                {
                    int.TryParse(values[j], out mapData[i, j]);
                }
            }
        }
        else
        {
            Debug.LogError("Map file not found: " + filePath);
        }
    }

    void PrintMap()
    {
        // Afficher la carte dans la console à titre de vérification
        if (mapData != null)
        {
            for (int i = 0; i < mapData.GetLength(0); i++)
            {
                for (int j = 0; j < mapData.GetLength(1); j++)
                {
                    Debug.Log("[" + i + "," + j + "]: " + mapData[i, j]);
                }
            }
        }
        else
        {
            Debug.LogError("Map data is null.");
        }
    }
}
