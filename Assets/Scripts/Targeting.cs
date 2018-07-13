using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>Where the distances of the targets are measured from</para>
    /// </summary>
    [SerializeField]
    private Transform source;

    /// <summary>
    /// <para>How many units away the radar can see enemies</para>
    /// </summary>
    [SerializeField]
    private float range;

    /// <summary>
    /// <para>The tag of the objects to target</para>
    /// </summary>
    [SerializeField]
    private string targetTag;

    /// <summary>
    /// <para>The Reticle prefab for each target</para>
    /// </summary>
    [SerializeField]
    private GameObject reticle;

    /// <summary>
    /// <para>The Blip prefab for each target</para>
    /// </summary>
    [SerializeField]
    private GameObject blip;

    /// <summary>
    /// <para>The canvas to put the Reticles on</para>
    /// </summary>
    [SerializeField]
    private Transform canvas;

    /// <summary>
    /// <para>The radar to put the Blips on</para>
    /// </summary>
    [SerializeField]
    private Transform radar;

    /// <summary>
    /// <para>The currently tracked target</para>
    /// </summary>
    private Target currentTarget;

	#endregion
	
	#region Properties
    /// <summary>
    /// <para>The instance to reference</para>
    /// </summary>
	public static Targeting Instance { get; private set; }

    /// <summary>
    /// <para>The list of targets</para>
    /// </summary>
    public List<Target> Targets { get; private set; }

    /// <summary>
    /// <para>Where the distances of the targets are measured from</para>
    /// </summary>
    public Transform Source
    {
        get
        {
            return source;
        }
    }

    /// <summary>
    /// <para>How many units away the radar can see enemies</para>
    /// </summary>
    public float Range
    {
        get
        {
            return range;
        }
    }

    /// <summary>
    /// <para>The target currently being tracked</para>
    /// </summary>
    public Transform TrackedTarget
    {
        get
        {
            if (currentTarget != null && currentTarget.reticle.Locked)
            {
                return currentTarget.transform;
            }
            else
            {
                return null;
            }
        }
    }
	#endregion
	
	#region Events
	/// <summary>
    /// Awake is called before start
    /// </summary>
	private void Awake()
	{
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of Targeting.");
            enabled = false;
        }
        Targets = new List<Target>();
	}
	
	/// <summary>
    /// Use this for initialization
    /// </summary>
	private void Start() 
	{
		
	}
	
	/// <summary>
    /// Update is called once per frame
    /// </summary>
	private void Update()
    { 
        // Matching reticles to objects
        for (int i = 0; i < Targets.Count;)
        {
            if (Targets[i].transform == null)
            {
                if (currentTarget == Targets[i])
                {
                    currentTarget = null;
                }
                Targets.RemoveAt(i);
            }
            else
            {
                ++i;
            }
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag(targetTag))
        {
            if (!Target.ContainsTransform(Targets, g.transform))
            {
                Reticle r = Instantiate(reticle, canvas).GetComponent<Reticle>();
                r.Target = g.transform;
                Blip b = Instantiate(blip, radar).GetComponent<Blip>();
                b.Target = g.transform;
                Targets.Add(new Target(g.transform, r, b));
            }
        }

        // Updating the active reticle
        if (currentTarget == null || Input.GetButtonDown("Switch"))
        {
            if (currentTarget != null)
            {
                currentTarget.SetActive(false);
            }
            currentTarget = GetClosestTarget();
            if (currentTarget != null)
            {
                currentTarget.SetActive(true);
            }
        }
	}
	
	/// <summary>
    /// Use this for physics-related changes
    /// </summary>
	private void FixedUpdate()
	{
		
	}
	#endregion
	
	#region Methods
    /// <summary>
    /// Finds the reticle closest to the center of the screen
    /// </summary>
    /// <returns>The reticle whose average anchors are closest to (0.5, 0.5)</returns>
    private Target GetClosestTarget()
    {
        Target result = null;
        float closestDistance = float.MaxValue;
        foreach (Target t in Targets)
        {
            RectTransform rt = t.reticle.GetComponent<RectTransform>();
            Vector2 pos = Vector2.Lerp(rt.anchorMin, rt.anchorMax, 0.5f);
            float distance = Vector2.Distance(pos, Vector2.one * 0.5f);
            if (t != currentTarget && distance < closestDistance)
            {
                result = t;
                closestDistance = distance;
            }
        }
        return result;
    }
	#endregion
	
	#region Coroutines
	
	#endregion
}
