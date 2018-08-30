using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Missile : Movement
{
    #region Variables
    /// <summary>
    /// <para>The missiles in the game right now</para>
    /// </summary>
    private static List<Missile> missiles;

    /// <summary>
    /// <para>How many times per second to make a 360 degree turn</para>
    /// </summary>
    [SerializeField]
    private float lifeSpan;

    /// <summary>
    /// <para>How many times per second to make a 360 degree turn</para>
    /// </summary>
    [SerializeField]
    private float turnSpeed;

    /// <summary>
    /// <para>How much damage the missile does</para>
    /// </summary>
    [SerializeField]
    private int damage;
    #endregion

    #region Properties
    /// <summary>
    /// <para>The target to go towards</para>
    /// </summary>
    public Transform Target { get; set; }
    #endregion

    #region Properties
    /// <summary>
    /// <para>The missiles in the game right now</para>
    /// </summary>
    public static List<Missile> Missiles
    {
        get
        {
            if (missiles == null)
            {
                missiles = new List<Missile>();
            }
            return missiles;
        }
        private set
        {
            missiles = value;
        }
    }

    /// <summary>
    /// <para>How much damage the missile does</para>
    /// </summary>
    public int Damage
    {
        get
        {
            return damage;
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
        missiles.Add(this);
        Destroy(gameObject, lifeSpan);
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
        base.Update();
	}

    /// <summary>
    /// Use this for physics-related changes
    /// </summary>
    protected override void FixedUpdate()
	{
        if (Target != null)
        {
            _rigidbody.MoveRotation(Utils.SlerpFixedDistance(transform.rotation, Quaternion.LookRotation(Target.position - transform.position), turnSpeed * 360 * Time.fixedDeltaTime));
        }
        base.FixedUpdate();
	}

    /// <summary>
    /// Called when something collides with the Game Object
    /// </summary>
    /// <param name="collision">The collision that occurred</param>
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    /// <summary>
    /// Called when the script is destroyed
    /// </summary>
    protected virtual void OnDestroy()
    {
        missiles.Remove(this);
    }
    #endregion

    #region Methods

    #endregion

    #region Coroutines

    #endregion
}
