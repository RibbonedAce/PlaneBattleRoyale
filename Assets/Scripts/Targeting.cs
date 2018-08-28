using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>How many units away the radar can see enemies</para>
    /// </summary>
    [SerializeField]
    private float range;

    /// <summary>
    /// <para>The tags of the objects to detect</para>
    /// </summary>
    [SerializeField]
    private string[] entityTags;

    /// <summary>
    /// <para>The tags of the objects to target</para>
    /// </summary>
    [SerializeField]
    private string[] targetTags;

    /// <summary>
    /// <para>The Reticle prefab for each target</para>
    /// </summary>
    [SerializeField]
    private GameObject reticle;

    /// <summary>
    /// <para>The Blip prefab for each non-target entity</para>
    /// </summary>
    [SerializeField]
    private GameObject[] entityBlips;

    /// <summary>
    /// <para>The Blip prefab for each target</para>
    /// </summary>
    [SerializeField]
    private GameObject[] targetBlips;

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
    /// <para>The maximum distance to lock on at</para>
    /// </summary>
    private static readonly float lockDistance = 80f;
    #endregion

    #region Properties
    /// <summary>
    /// <para>The list of non-target entities</para>
    /// </summary>
    public List<Entity> Entities { get; private set; }

    /// <summary>
    /// <para>The list of targets</para>
    /// </summary>
    public List<Target> Targets { get; private set; }

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
    public Target CurrentTarget { get; private set; }

    /// <summary>
    /// <para>The target currently being locked</para>
    /// </summary>
    public Transform TrackedTarget
    {
        get
        {
            if (CurrentTarget != null && Vector3.Distance(transform.position, CurrentTarget.transform.position) <= lockDistance)
            {
                return CurrentTarget.transform;
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
        Entities = new List<Entity>();
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
        // Matching reticles and blips to targets
        for (int i = 0; i < Targets.Count;)
        {
            if (Targets[i].transform == null)
            {
                if (CurrentTarget == Targets[i])
                {
                    CurrentTarget = null;
                }
                Targets.RemoveAt(i);
            }
            else
            {
                ++i;
            }
        }
        for (int i = 0; i < targetTags.Length; ++i)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(targetTags[i]))
            {
                if (!Target.ContainsTransform(Targets, g.transform))
                {
                    Blip b = null;
                    if (radar != null)
                    {
                        b = Instantiate(targetBlips[i], radar).GetComponent<Blip>();
                        b.Setup(g.transform, transform, range);
                    }

                    Reticle r = null;
                    if (canvas != null)
                    {
                        r = Instantiate(reticle, canvas).GetComponent<Reticle>();
                        r.Setup(g.transform, transform, lockDistance);
                    }

                    Targets.Add(new Target(g.transform, r, b));
                }
            }
        }

        // Updating the active reticle
        if (CurrentTarget == null)
        {
            GetClosestTarget();
        }

        // Matching blips to other entities
        for (int i = 0; i < Entities.Count;)
        {
            if (Entities[i].transform == null)
            {
                Entities.RemoveAt(i);
            }
            else
            {
                ++i;
            }
        }
        for (int i = 0; i < entityTags.Length; ++i)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(entityTags[i]))
            {
                if (!Entity.ContainsTransform(Entities, g.transform))
                {
                    Blip b = null;
                    if (radar != null)
                    {
                        b = Instantiate(entityBlips[i], radar).GetComponent<Blip>();
                        b.Setup(g.transform, transform, range);
                    }

                    Entities.Add(new Entity(g.transform, b));
                }
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
    public void GetClosestTarget()
    {
        Target result = CurrentTarget;
        float closestDistance = float.MaxValue;
        foreach (Target t in Targets)
        {
            float distance = Vector3.Angle(transform.forward, t.transform.position - transform.position);
            if (t != CurrentTarget && distance < closestDistance)
            {
                result = t;
                closestDistance = distance;
            }
        }
        if (CurrentTarget != null && CurrentTarget != result)
        {
            CurrentTarget.SetActive(false);
        }
        if (result != null && result != CurrentTarget)
        {
            result.SetActive(true);
        }
        CurrentTarget = result;
    }
	#endregion
	
	#region Coroutines
	
	#endregion
}
