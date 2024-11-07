using UnityEditor;
using UnityEngine;

public class CustomEditorWindow : EditorWindow
{
    private AudioClip narrationStartClip;
    private AudioClip narrationEndClip;
    private AudioClip sync1Clip;
    private AudioClip sync2Clip;
    private AudioClip sync3Clip;
    private AudioClip sync4Clip;
    private AudioSource audioSource;

    [MenuItem("Window/Custom Editor Window")]
    public static void ShowWindow()
    {
        GetWindow<CustomEditorWindow>("Custom Editor");
    }

    private void OnEnable()
    {
        if (audioSource == null)
        {
            GameObject audioObject = new GameObject("EditorAudioSource");
            audioSource = audioObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioObject.hideFlags = HideFlags.HideAndDontSave;
        }
    }

    private void OnDisable()
    {
        if (audioSource != null)
        {
            DestroyImmediate(audioSource.gameObject);
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Audio Clips", EditorStyles.boldLabel);

        narrationStartClip = (AudioClip)EditorGUILayout.ObjectField("Narration Start Clip", narrationStartClip, typeof(AudioClip), false);
        narrationEndClip = (AudioClip)EditorGUILayout.ObjectField("Narration End Clip", narrationEndClip, typeof(AudioClip), false);

        sync1Clip = (AudioClip)EditorGUILayout.ObjectField("Sync1 Clip", sync1Clip, typeof(AudioClip), false);
        sync2Clip = (AudioClip)EditorGUILayout.ObjectField("Sync2 Clip", sync2Clip, typeof(AudioClip), false);
        sync3Clip = (AudioClip)EditorGUILayout.ObjectField("Sync3 Clip", sync3Clip, typeof(AudioClip), false);
        sync4Clip = (AudioClip)EditorGUILayout.ObjectField("Sync4 Clip", sync4Clip, typeof(AudioClip), false);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Narration : Start"))
        {
            PlayAudio(narrationStartClip);
        }
        if (GUILayout.Button("Narration : End"))
        {
            PlayAudio(narrationEndClip);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Sync1"))
        {
            PlayAudio(sync1Clip);
            StartWomanFollow();
        }
        if (GUILayout.Button("Sync2"))
        {
            PlayAudio(sync2Clip);
        }
        if (GUILayout.Button("Sync3"))
        {
            PlayAudio(sync3Clip);
        }
        if (GUILayout.Button("Sync4"))
        {
            PlayAudio(sync4Clip);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Handshake"))
        {
            Debug.Log("Button Handshake Clicked");
        }
        if (GUILayout.Button("Hug"))
        {
            Debug.Log("Button Hug Clicked");
        }
        if (GUILayout.Button("Reset"))
        {
            StopWomanFollow();
            Debug.Log("Reset button clicked");
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    private void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or clip is missing.");
        }
    }

    private void StartWomanFollow()
    {
        WomanFollow womanFollow = FindObjectOfType<WomanFollow>();
        if (womanFollow != null)
        {
            womanFollow.StartFollowing();
            Debug.Log("Woman started following.");
        }
        else
        {
            Debug.LogWarning("WomanFollow component not found in the scene.");
        }
    }

    private void StopWomanFollow()
    {
        WomanFollow womanFollow = FindObjectOfType<WomanFollow>();
        if (womanFollow != null)
        {
            womanFollow.StopFollowing();
            Debug.Log("Woman stopped following.");
        }
        else
        {
            Debug.LogWarning("WomanFollow component not found in the scene.");
        }
    }
}
