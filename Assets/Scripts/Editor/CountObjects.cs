using UnityEngine;
using UnityEngine.SceneManagement;

public class CountObjects
{
    [UnityEditor.MenuItem("My Functions/Count Objects")]
    public static void CountMyObjects()
    {
        var x = GameObject.FindObjectsByType<MeshRenderer>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
        Debug.Log($"The current count of MeshRenderer is {x.Length}");

    }
}
