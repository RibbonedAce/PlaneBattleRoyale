using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : Targeting 
{
    #region Variables
    /// <summary>
    /// <para>How many units away the radar can see enemies</para>
    /// </summary>
    [SerializeField]
    protected float range;

    /// <summary>
    /// <para>The tags of the objects to detect</para>
    /// </summary>
    [SerializeField]
    protected string[] entityTags;

    /// <summary>
    /// <para>The Reticle prefab for each target</para>
    /// </summary>
    [SerializeField]
    protected GameObject reticle;

    /// <summary>
    /// <para>The Blip prefab for each target</para>
    /// </summary>
    [SerializeField]
    protected GameObject[] targetBlips;

    /// <summary>
    /// <para>The Blip prefab for each non-target entity</para>
    /// </summary>
    [SerializeField]
    protected GameObject[] entityBlips;

    /// <summary>
    /// <para>The canvas to put the Reticles on</para>
    /// </summary>
    [SerializeField]
    protected Transform canvas;

    /// <summary>
    /// <para>The radar to put the Blips on</para>
    /// </summary>
    [SerializeField]
    protected Transform radar;
    #endregion

    #region Properties
    /// <summary>
    /// <para>The list of non-target entities</para>
    /// </summary>
    public List<Entity> Entities { get; protected set; }

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
    #endregion

    #region Events
    /// <summary>
    /// Awake is called before start
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Entities = new List<Entity>();
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected override void Update()
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
                    Blip b = Instantiate(targetBlips[i], radar).GetComponent<Blip>();
                    b.Setup(g.transform, transform, range);

                    Reticle r = Instantiate(reticle, canvas).GetComponent<Reticle>();
                    r.Setup(g.transform, transform, lockDistance);

                    Targets.Add(new Target(g.transform, r, b));
                }
            }
        }

        // Updating the active target
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
                    Blip b = Instantiate(entityBlips[i], radar).GetComponent<Blip>();
                    b.Setup(g.transform, transform, range);

                    Entities.Add(new Entity(g.transform, b));
                }
            }
        }
    }
    #endregion

    #region Methods

    #endregion

    #region Coroutines

    #endregion
}
