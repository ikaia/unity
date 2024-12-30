/*using System.Collections.Generic;
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
}*/
/*using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DistanceTracker : MonoBehaviour //This integar recording distance data ONLY
{
    public Transform characterA; // Assign the first character in the Inspector
    public Transform characterB; // Assign the second character in the Inspector

    private List<string> distanceData = new List<string>(); // To store the distance data over time
    private string filePath;

    private int elapsedSeconds = 0; // Track time in whole seconds
    private float timer = 0f; // Internal timer to count seconds

    void Start()
    {
        // Create a unique file path in the user's file explorer
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        filePath = Path.Combine(Application.dataPath, $"DistanceData_{timestamp}.csv");
        
        // Add CSV header
        distanceData.Add("Time (s),Distance");
    }

    void Update()
    {
        if (characterA != null && characterB != null)
        {
            // Update the timer
            timer += Time.deltaTime;

            // Check if a whole second has passed
            if (timer >= 1f)
            {
                timer -= 1f; // Subtract 1 second from the timer
                elapsedSeconds++;

                // Calculate the distance between the characters
                float distance = Vector3.Distance(characterA.position, characterB.position);

                // Record the time and distance
                string record = elapsedSeconds + "," + distance.ToString("F2");
                distanceData.Add(record);
            }
        }
    }

    void OnApplicationQuit()
    {
        // Write the distance data to a CSV file
        File.WriteAllLines(filePath, distanceData);

        Debug.Log("Distance data saved to: " + filePath);
    }
}*/
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CSV : MonoBehaviour
{
    public Transform characterA; // Assign the first character in the Inspector
    public Transform characterB; // Assign the second character in the Inspector
    public XRController xrController; // Reference to the XR Controller

    private List<string> dataRecords = new List<string>(); // To store distance and grip hold data
    private string filePath;

    private int elapsedSeconds = 0; // Track time in whole seconds
    private float timer = 0f; // Internal timer to count seconds

    private bool isGripPressed = false; // Track if the grip button is pressed
    private float holdStartTime = 0f; // Time when the grip button was first pressed

    void Start()
    {
        // Create a unique file path in the user's file explorer
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        filePath = Path.Combine(Application.dataPath, $"DistanceGripData_{timestamp}.csv");

        // Add CSV header
        dataRecords.Add("Time (s),Distance (m),Grip Hold Duration (s)");
    }

    void Update()
    {
        if (characterA != null && characterB != null)
        {
            // Update the timer for distance tracking
            timer += Time.deltaTime;

            // Check if a whole second has passed
            if (timer >= 1f)
            {
                timer -= 1f; // Subtract 1 second from the timer
                elapsedSeconds++;

                // Calculate the distance between the characters
                float distance = Vector3.Distance(characterA.position, characterB.position);

                // Get the current grip hold duration if the button is still pressed
                float currentHoldDuration = isGripPressed ? Time.time - holdStartTime : 0f;

                // Record the time, distance, and grip hold duration
                string record = $"{elapsedSeconds},{distance:F2},{currentHoldDuration:F0}";
                dataRecords.Add(record);

                Debug.Log($"Time: {elapsedSeconds}s, Distance: {distance:F2}m, Grip Hold: {currentHoldDuration:F0}s");
            }
        }

        // Check grip button state
        if (xrController.inputDevice.IsPressed(InputHelpers.Button.Grip, out bool isPressed))
        {
            if (isPressed && !isGripPressed)
            {
                // Grip button pressed for the first time
                isGripPressed = true;
                holdStartTime = Time.time; // Record the start time
            }
            else if (!isPressed && isGripPressed)
            {
                // Grip button released
                isGripPressed = false;
                float holdDuration = Time.time - holdStartTime; // Calculate total hold duration

                Debug.Log($"Grip held for {holdDuration:F0} seconds.");
            }
        }
    }

    void OnApplicationQuit()
    {
        // Write the data to a CSV file
        File.WriteAllLines(filePath, dataRecords);

        Debug.Log("Distance and grip hold data saved to: " + filePath);
    }
}

