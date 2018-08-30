using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>The tags of the objects to target</para>
    /// </summary>
    [SerializeField]
    protected string[] targetTags;

    /// <summary>
    /// <para>The maximum distance to lock on at</para>
    /// </summary>
    protected static readonly float lockDistance = 80f;
    #endregion

    #region Properties
    /// <summary>
    /// <para>The list of targets</para>
    /// </summary>
    public List<Target> Targets { get; protected set; }

    /// <summary>
    /// <para>The target currently being tracked</para>
    /// </summary>
    public Target CurrentTarget { get; protected set; }

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
    protected virtual void Awake()
	{
        Targets = new List<Target>();
	}

    /// <summary>
    /// Use this for initialization
    /// </summary>
    protected virtual void Start() 
	{
		
	}

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected virtual void Update()
    { 
        // Finding targets
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
                    Targets.Add(new Target(g.transform, null, null));
                }
            }
        }

        // Updating the active target
        if (CurrentTarget == null)
        {
            GetClosestTarget();
        }
	}

    /// <summary>
    /// Use this for physics-related changes
    /// </summary>
    protected virtual void FixedUpdate()
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
