using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    public AudioClip[] audioClipOptions;
    public int currentClipIndex;
    public int audioClipCount = 0;

    [Header("Debug Items")]
    public bool shouldShowTriggersInGame = false;

    private void Awake()
    {
        // Make sure we actually have options.
        audioClipCount = audioClipOptions.Length;
        if (audioClipCount == 0) {
            // If we don't have any options,
            Debug.LogWarning("No audio clips have been found. Maestro won\'t work without audio clips!");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchSong(int indexToSwitch)
    {
        if (audioClipOptions[currentClipIndex] && audioClipOptions[indexToSwitch]) {
            // Make sure both clips exist before trying to switch...

        }
    }
}
