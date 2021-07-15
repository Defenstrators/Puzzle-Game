using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioChangeTrigger : MonoBehaviour
{
    [Header("Dependencies")]
    public musicManager musicManager;

    [Header("Attributes")]
    [Min(0), Tooltip("The index to switch to when its trigger is activated."), InspectorName("Song Index")]
    public int songIndex;

    [Tooltip("What tags does the gameobject need to be able to trigger the Maestro.")]
    public string[] enabledTags;

    private void Awake()
    {
        if (musicManager) {
            // If we didn't attach the musicManager...
            Debug.LogError("Failed to initialise audio trigger. Reason: Maestro was not attached.");

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // If we've told Maestro to show the triggers, show them. Otherwise, don't.
        GetComponent<MeshRenderer>().enabled = musicManager.shouldShowTriggersInGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ValidateGameobjTag(other.gameObject.tag)) {
            // If the tag is in the list of enabled tags,
            musicManager.SwitchSong(songIndex);
        }
    }

    private bool ValidateGameobjTag(string tagToValidate)
    {
        // Check through each tag to see if it matches.
        foreach (string validTag in enabledTags) {
            if (validTag == tagToValidate) {
                // If it does, return true.
                return true;
            }
        }
        // Otherwise, if nothing in the list works, return false.
        return false;
    }
}
