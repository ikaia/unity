using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    public Transform characterA; // Assign the first character in the Inspector
    public Transform characterB; // Assign the second character in the Inspector

    private List<string> distanceData = new List<string>(); // To store the distance data over time
    private string filePath;

  void Start()
    {
        // Create a unique file path in the user's file explorer
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        filePath = Path.Combine(Application.dataPath, $"DistanceData_{timestamp}.csv");
        
        // Add CSV header
        distanceData.Add("Time,Distance");
    }

    void Update()
    {
        if (characterA != null && characterB != null)
        {
            // Calculate the distance between the characters
            float distance = Vector3.Distance(characterA.position, characterB.position);

            // Record the time and distance
            string record = Time.time.ToString("F2") + "," + distance.ToString("F2");
            distanceData.Add(record);
        }
    }

    void OnApplicationQuit()
    {
        // Write the distance data to a CSV file
        File.WriteAllLines(filePath, distanceData);

        Debug.Log("Distance data saved to: " + filePath);
    }
}
