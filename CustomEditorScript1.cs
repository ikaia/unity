using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class CustomEditorWindow : EditorWindow
{
    public AudioClip Narration1Clip;
    public AudioClip Narration2Clip;
    public AudioClip Narration3Clip;
    public AudioClip Narration4Clip;
    public AudioClip Narration5Clip;
    public AudioClip Narration6Clip;
    public AudioClip Narration7Clip;
    public AudioClip Narration8Clip;
    public AudioClip Narration9Clip;
    public AudioClip Narration10Clip;
    public AudioClip Narration11Clip;
    
    public AudioClip Avatar1Clip;
    public AudioClip Avatar2Clip;
    public AudioClip Avatar3Clip;
    public AudioClip Avatar4Clip;
    public AudioClip Avatar5Clip;
    public AudioClip Avatar6Clip;
    public AudioSource audioSource;
    
    public GameObject human_v2;
	private Animator human_v2_Controller;

   // Carousel variables
    private int currentPage = 0;
    private int totalButtons = 11;
    private int buttonsPerPage = 3;
	
    private Vector2 scrollPosition = Vector2.zero;
	
	 
    public GameObject virtualNavMesh;  // The virtual nav mesh object
    private Vector3 originalPosition;  // Store the original position of the nav mesh
    public NavMeshAgent navMeshAgent; // NavMeshAgent for controlling movement

	
    [MenuItem("Window/Custom Editor Window")]
    public static void ShowWindow()
    { 
        GetWindow<CustomEditorWindow>("Custom Editor");
    }

    public void OnEnable()
    {
        if (audioSource == null)
        {
            human_v2 = GameObject.Find("Human_v2");
            audioSource = human_v2.GetComponent<AudioSource>();
        }
		// Ensure the Animator is also found and assigned
        if (human_v2 != null)
        {
            human_v2_Controller = human_v2.GetComponent<Animator>();
        }
	
    }

    public void OnDisable()
    {
        if (audioSource != null)
        {
            // Optionally destroy the audio source if needed
        }
    }

    public void OnGUI()
    {
		 
        // Create a custom style for the title
        GUIStyle titleStyle = new GUIStyle(EditorStyles.boldLabel)
        {
            fontSize = 18, // Set a larger font size
            fontStyle = FontStyle.Bold, // Ensure the font is bold
            alignment = TextAnchor.MiddleCenter // Optional: Center the text
        };

        // Use the custom style for the title
        GUILayout.Label("The Control Panel", titleStyle);

        GUILayout.Label("Audio Clips", EditorStyles.boldLabel);

        
        
        GUILayout.BeginVertical();

        Avatar1Clip = (AudioClip)EditorGUILayout.ObjectField("Avatar#1 Clip", Avatar1Clip, typeof(AudioClip), false);
        Avatar2Clip = (AudioClip)EditorGUILayout.ObjectField("Avatar#2 Clip", Avatar2Clip, typeof(AudioClip), false);
        Avatar3Clip = (AudioClip)EditorGUILayout.ObjectField("Avatar#3 Clip", Avatar3Clip, typeof(AudioClip), false);
        Avatar4Clip = (AudioClip)EditorGUILayout.ObjectField("Avatar#4 Clip", Avatar4Clip, typeof(AudioClip), false);
        Avatar5Clip = (AudioClip)EditorGUILayout.ObjectField("Avatar#5 Clip", Avatar5Clip, typeof(AudioClip), false);
        Avatar6Clip = (AudioClip)EditorGUILayout.ObjectField("Avatar#6 Clip", Avatar6Clip, typeof(AudioClip), false);

        Narration1Clip = (AudioClip)EditorGUILayout.ObjectField("Narration#1 Clip", Narration1Clip, typeof(AudioClip), false);
        Narration2Clip = (AudioClip)EditorGUILayout.ObjectField("Narration#2 Clip", Narration2Clip, typeof(AudioClip), false);
        Narration3Clip = (AudioClip)EditorGUILayout.ObjectField("Narration#3 Clip", Narration3Clip, typeof(AudioClip), false);
        Narration4Clip = (AudioClip)EditorGUILayout.ObjectField("Narration#4 Clip", Narration4Clip, typeof(AudioClip), false);
        Narration5Clip = (AudioClip)EditorGUILayout.ObjectField("Narration#5 Clip", Narration5Clip, typeof(AudioClip), false);
        Narration6Clip = (AudioClip)EditorGUILayout.ObjectField("Narration#6 Clip", Narration6Clip, typeof(AudioClip), false);
        
        Narration7Clip = (AudioClip)EditorGUILayout.ObjectField("Narration#7 Clip", Narration7Clip, typeof(AudioClip), false);
        Narration8Clip = (AudioClip)EditorGUILayout.ObjectField("Narration#8 Clip", Narration8Clip, typeof(AudioClip), false);
        Narration9Clip = (AudioClip)EditorGUILayout.ObjectField("Narration#9 Clip", Narration9Clip, typeof(AudioClip), false);
        Narration10Clip = (AudioClip)EditorGUILayout.ObjectField("Narration#10 Clip", Narration10Clip, typeof(AudioClip), false);
        Narration11Clip = (AudioClip)EditorGUILayout.ObjectField("Narration#11 Clip", Narration11Clip, typeof(AudioClip), false);

    

        GUILayout.EndVertical();
		    // Call the method to display the carousel of steps
        DisplayCarousel();
       
		GUILayout.Label("----- Steps #1 -----", EditorStyles.boldLabel);
         GUILayout.BeginHorizontal(); // Start a horizontal layout for avatars and actions

        // Avatar Buttons in a Vertical Column
        GUILayout.BeginVertical();
		 GUILayout.BeginHorizontal(); // Start a horizontal layout for avatars and actions
		if (GUILayout.Button("Enters", GUILayout.Width(50)))
        {
            StartWomanFollow();
        }
		if (GUILayout.Button("Exits", GUILayout.Width(50)))
        {
            SwitchTarget();
        }
		 GUILayout.EndHorizontal(); // Start a horizontal layout for avatars and actions
		
        if (GUILayout.Button("Avatar#1", GUILayout.Width(100)))
        {
            PlayAudio(Avatar1Clip);
        }
        if (GUILayout.Button("Avatar#2", GUILayout.Width(100)))
        {
            PlayAudio(Avatar2Clip);
        }
        if (GUILayout.Button("Avatar#3", GUILayout.Width(100)))
        {
            PlayAudio(Avatar3Clip);
        }
        if (GUILayout.Button("Avatar#4", GUILayout.Width(100)))
        {
            PlayAudio(Avatar4Clip);
        }
        if (GUILayout.Button("Avatar#5", GUILayout.Width(100)))
        {
            PlayAudio(Avatar5Clip);
        }
        if (GUILayout.Button("Avatar#6", GUILayout.Width(100)))
        {
            PlayAudio(Avatar6Clip);
        }
        GUILayout.EndVertical();
		
		
        GUILayout.BeginVertical();
		 // Handshake, Hug, and Reset Buttons in a Vertical Column Beside Avatars
		 //GUILayout.Label("----- Conditions #1 -----", EditorStyles.boldLabel);
        if (GUILayout.Button("Handshake", GUILayout.Width(75)))
        {
			
            TriggerHandshakeAnimation();
			//StartHandshake();
            Debug.Log("Button Handshake Clicked");
        }
        if (GUILayout.Button("Hug", GUILayout.Width(75)))
        {
			TriggerHugAnimation();
            Debug.Log("Button Hug Clicked");
        }
        if (GUILayout.Button("Reset", GUILayout.Width(75)))
        {
            StopWomanFollow();
            Debug.Log("Reset button clicked");
        }
          GUILayout.EndVertical();

        GUILayout.EndHorizontal(); // End the horizontal layout

        GUILayout.Space(20);
        
		GUILayout.Label("----- Environment -----", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Day", GUILayout.Width(50)))
        {
            Debug.Log("Day Clicked");
        }
        if (GUILayout.Button("Night", GUILayout.Width(50)))
        {
            Debug.Log("Night Clicked");
        }
        GUILayout.EndHorizontal();
    }


private void DisplayCarousel()
{
    // Calculate total pages
    int totalPages = Mathf.CeilToInt((float)totalButtons / buttonsPerPage);
    
    // Display the current page number and total pages
    GUILayout.Label($"Page {currentPage + 1}/{totalPages}", EditorStyles.boldLabel);


    // Calculate which button to display on the current page
    int startIndex = currentPage * buttonsPerPage;
    int endIndex = Mathf.Min(startIndex + buttonsPerPage, totalButtons);

    // Display the button for the current page
    for (int i = startIndex; i < endIndex; i++)
    {
        if (GUILayout.Button($"Narration #{i + 1}", GUILayout.Height(30)))
        {
            Debug.Log($"Button for Narration {i + 1} clicked.");
            PlayStepClip(i); // Play the corresponding audio clip for this button
        }
    }

    // Carousel Navigation Section
    GUILayout.BeginHorizontal();
    GUILayout.FlexibleSpace();

    // Navigate to the previous page
    if (GUILayout.Button("<", GUILayout.Width(30)))
    {
        currentPage = Mathf.Max(0, currentPage - 1); // Prevent going to negative page
    }

    GUILayout.Space(20);

    // Navigate to the next page
    if (GUILayout.Button(">", GUILayout.Width(30)))
    {
        currentPage = Mathf.Min(totalPages - 1, currentPage + 1); // Prevent going beyond the last page
    }

    GUILayout.FlexibleSpace();
    GUILayout.EndHorizontal();
}

private void PlayStepClip(int index)
{
    AudioClip clip = index switch
    {
        0 => Narration1Clip,
        1 => Narration2Clip,
        2 => Narration3Clip,
        3 => Narration4Clip,
        4 => Narration5Clip,
        5 => Narration6Clip,
        6 => Narration7Clip,
        7 => Narration8Clip,
        8 => Narration9Clip,
        9 => Narration10Clip,
        10 => Narration11Clip,  // Added the 11th clip
        _ => null
    };

    PlayAudio(clip);
}


    private void PlayAudio(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
	 public void SwitchTarget()
    {
        WomanFollow womanFollow = FindObjectOfType<WomanFollow>();
        if (womanFollow != null)
        {
            womanFollow.SwitchTarget();
			human_v2_Controller = human_v2.GetComponent<Animator>();
          // Set the isWalking trigger to start walking animation
            human_v2_Controller.SetTrigger("isWalking");
			StartWomanFollow();
            Debug.Log("Switch Target started.");
        }
        else
        {
            Debug.LogWarning("WomanFollow component not found in the scene.");
        }
	}
     public void StartWomanFollow()
    {
        WomanFollow womanFollow = FindObjectOfType<WomanFollow>();
        if (womanFollow != null)
        {
            womanFollow.StartFollowing();
			human_v2_Controller = human_v2.GetComponent<Animator>();
          // Set the isWalking trigger to start walking animation
            human_v2_Controller.SetTrigger("isWalking");
            Debug.Log("Woman started following.");
        }
        else
        {
            Debug.LogWarning("WomanFollow component not found in the scene.");
        }
    }

    public void StopWomanFollow()
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

    public void TriggerHandshakeAnimation()
    {
		WomanFollow womanFollow = FindObjectOfType<WomanFollow>();
        if (womanFollow != null)
        {
			human_v2_Controller = human_v2.GetComponent<Animator>();
           // Set the isReachOut trigger to start animation
            human_v2_Controller.SetTrigger("isReachOut");
            Debug.Log("Woman started reached out Hand.");
        }
        else
        {
            Debug.LogWarning("WomanFollow component not found in the scene.");
        }
    }
	public void TriggerHugAnimation()
    {
		WomanFollow womanFollow = FindObjectOfType<WomanFollow>();
        if (womanFollow != null)
        {
			human_v2_Controller = human_v2.GetComponent<Animator>();
           // Set the isReachOut trigger to start animation
            human_v2_Controller.SetTrigger("isHug");
            Debug.Log("Woman started to Hug.");
        }
        else
        {
            Debug.LogWarning("WomanFollow component not found in the scene.");
        }
    }
	}
	
/*	 // Method to start the handshake animation
    void StartHandshake()
    {
        // Store the original position of the nav mesh
        originalPosition = virtualNavMesh.transform.position;

        // Trigger the handshake animation
        human_v2_Controller.SetTrigger("Handshake");

        // Move the nav mesh closer by 0.2 units
        MoveNavMeshCloser();
    }

    // Method to move the nav mesh closer by 0.2 units
    void MoveNavMeshCloser()
    {
        // Calculate the new position by moving the nav mesh closer
        Vector3 newPosition = virtualNavMesh.transform.position + virtualNavMesh.transform.forward * 0.2f;

        // Move the nav mesh using NavMeshAgent
        navMeshAgent.Move(newPosition - navMeshAgent.transform.position);

        // Optionally, you could smooth this movement over time using a coroutine for a more natural effect.
    }

    // Method to reset the nav mesh to its original position
    void ResetNavMeshPosition()
    {
        // Move the nav mesh back to the original position
        navMeshAgent.Move(originalPosition - navMeshAgent.transform.position);
    }

    // Update method to track the completion of the handshake animation
    void Update()
    {
        // If the animation state is 'Handshake' and it's finished, reset the nav mesh position
        if (human_v2_Controller.GetCurrentAnimatorStateInfo(0).IsName("Handshake") &&
            human_v2_Controller.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            ResetNavMeshPosition();
        }
    }*/

