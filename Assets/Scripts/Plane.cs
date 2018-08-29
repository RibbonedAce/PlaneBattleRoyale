using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Targeting))]
public class Plane : Movement
{
    #region Variables
    /// <summary>
    /// <para>The maximum health the plane can have</para>
    /// </summary>
    [SerializeField]
    protected int maxHealth;

    /// <summary>
    /// <para>The minimum thrust on the plane</para>
    /// </summary>
    [SerializeField]
    protected float minThrust;

    /// <summary>
    /// <para>The maximum thrust on the plane</para>
    /// </summary>
    [SerializeField]
    protected float maxThrust;

    /// <summary>
    /// <para>How quickly the plane can turn</para>
    /// </summary>
    [SerializeField]
    protected float turnSpeed;

    /// <summary>
    /// <para>The amount of ammo</para>
    /// </summary>
    [SerializeField]
    protected int ammo;

    /// <summary>
    /// <para>The missile object to fire</para>
    /// </summary>
    [SerializeField]
    protected GameObject projectile;

    /// <summary>
    /// <para>The health of the plane</para>
    /// </summary>
    protected int health;

    /// <summary>
    /// <para>The Targeting component attached</para>
    /// </summary>
    protected Targeting _targeting;
    #endregion

    #region Properties
    /// <summary>
    /// <para>Whether the plane is on autopilot</para>
    /// </summary>
    public bool AutoPilot { get; protected set; }

    /// <summary>
    /// <para>The maximum health the plane can have</para>
    /// </summary>
    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    /// <summary>
    /// <para>The amount of ammo</para>
    /// </summary>
    public int Ammo
    {
        get
        {
            return ammo;
        }
    }

    /// <summary>
    /// <para>The health of the plane</para>
    /// </summary>
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
            if (health <= 0)
            {
                Destroy(gameObject);
            }
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
        _targeting = GetComponent<Targeting>();
        Health = maxHealth / 2;
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
        if (AutoPilot)
        {
            _rigidbody.MoveRotation(Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, Vector3.up)), turnSpeed));
            thrust = 0.5f;
        }
        base.FixedUpdate();
    }

    /// <summary>
    /// Called when something collides with the Game Object
    /// </summary>
    /// <param name="collision">The collision that occurred</param>
    protected override void OnCollisionEnter(Collision collision)
    {
        Missile m = collision.collider.GetComponent<Missile>();
        if (m != null)
        {
            Health -= m.Damage;
        }
        else
        {
            base.OnCollisionEnter(collision);
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Fires a missile at the current target
    /// </summary>
    protected virtual void FireProjectile()
    {
        if (ammo > 0)
        {
            --ammo;
            GameObject g = Instantiate(projectile, transform.position - transform.up, transform.rotation);
            g.GetComponent<Missile>().Target = _targeting.TrackedTarget;
        }
    }
    #endregion

    #region Coroutines

    #endregion
}

