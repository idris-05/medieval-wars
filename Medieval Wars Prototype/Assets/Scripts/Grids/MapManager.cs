using UnityEngine;
using System.IO;

public class MapManager : MonoBehaviour
{
    // Votre tableau représentant la carte
    int[,] mapData = new int[,]
{
    // Ligne 1
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 },
    // Ligne 2
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0 ,0  },
    // Ligne 3
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    // Ligne 4
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 },
    // Ligne 5
    { 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 },
    // Ligne 6
    { 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0 ,0 },
    // Ligne 7
    {0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    // Ligne 8
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    // Ligne 9
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    // Ligne 10
    {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
};


   /* void Start()
    {
        // Chemin du fichier où vous voulez sauvegarder la carte
        string filePath = Application.dataPath + "/maps/map.txt";

        // Écriture des données de la carte dans le fichier
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < mapData.GetLength(0); i++)
            {
                for (int j = 0; j < mapData.GetLength(1); j++)
                {
                    writer.Write(mapData[i, j]);

                    // Ajouter une virgule pour séparer les valeurs
                    if (j < mapData.GetLength(1) - 1)
                        writer.Write(",");
                }
                // Nouvelle ligne après chaque ligne de la carte
                writer.WriteLine();
            }
        }

        Debug.Log("Map saved to: " + filePath);
    } */
}
