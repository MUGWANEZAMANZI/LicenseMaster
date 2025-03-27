using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using UnityEngine;

public class Finance : MonoBehaviour
{
    public static int Cash = 5000; // Changed from string to int

    public static Dictionary<string, int> punishments = new Dictionary<string, int>
    {
        {"Speeding", 100},
        {"Collision", 10},
        {"OneWay", 30},
        {"Stop", 10},
        {"OffRoad", 5},
        {"TrafficLight", 20}
    };

    public static Dictionary<string, int> awards = new Dictionary<string, int>
    {
        {"parking1", 200},
        {"parking2", 300},
        {"parking3", 400},
        {"parking4", 500},
        {"parking5", 500}
    };

    // Method to apply penalty and return the deducted amount
    public static int Violation(string violationType)
    {
        // Check if violation exists in punishments dictionary
        if (punishments.TryGetValue(violationType, out int penalty))
        {
            Cash -= penalty; // Deducts penalty from Cash
            Log($"Violation: {violationType}, Penalty: {penalty}, New Cash: {Cash}");
            return penalty; // Return the penalty amount
        }
        else
        {
            Debug.LogWarning($"Violation type '{violationType}' not found!");
            return -1; // Return -1 if the violation type doesn't exist
        }
    }

    // Method to apply reward for achievements
    public static void Achieved(string achievement)
    {
        // Check if achievement exists in awards dictionary
        if (awards.TryGetValue(achievement, out int reward))
        {
            Cash += reward; // Add reward to Cash
            Debug.Log($"Achievement: {achievement}, Reward: {reward}, New Cash: {Cash}");
        }
        else
        {
            Debug.LogWarning($"Achievement '{achievement}' not found!");
        }
    }

    // Helper method to log information to the console
    private static void Log(string message)
    {
        Debug.Log(message);
    }
}

