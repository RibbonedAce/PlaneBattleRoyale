using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerPlane : Movement
{
    #region Variables
    /// <summary>
    /// <para>The maximum health the plane can have</para>
    /// </summary>
    [SerializeField]
    protected int maxHealth;

    /// <summary>
    /// <para>The health of the plane</para>
    /// </summary>
    protected int health;

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
    protected GameObject missile;
    #endregion

    #region Properties
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
	}

    /// <summary>
    /// Use this for initialization
    /// </summary>
    protected override void Start() 
	{
        base.Start();
        Health = maxHealth;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected override void Update() 
	{
        base.Update();
        if (Input.GetButtonDown("Fire"))
        {
            FireMissile();
        }
	}

    /// <summary>
    /// Use this for physics-related changes
    /// </summary>
    protected override void FixedUpdate()
	{
        _rigidbody.MoveRotationBy(Quaternion.Euler(Vector3.right * Input.GetAxis("Pitch") * turnSpeed));
        _rigidbody.MoveRotationBy(Quaternion.Euler(-Vector3.forward * Input.GetAxis("Roll") * turnSpeed));
        _rigidbody.MoveRotationBy(Quaternion.Euler(Vector3.up * Input.GetAxis("Yaw") * turnSpeed));
        thrust = Mathf.Lerp(minThrust, maxThrust, (Input.GetAxis("Thrust") + 1) / 2);
        base.FixedUpdate();
    }

    /// <summary>
    /// Called when something collides with the Game Object
    /// </summary>
    /// <param name="collision"></param>
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
    /// Apply a pickup to the player's plane
    /// </summary>
    /// <param name="p">The pickup to apply</param>
    public void ApplyPickup(Pickup p)
    {
        Health += p.Health;
        maxThrust += p.Speed;
        turnSpeed += p.Turning;
        ammo += p.Ammo;
    }

    /// <summary>
    /// Fires a missile at the current target
    /// </summary>
    protected virtual void FireMissile()
    {
        GameObject g = Instantiate(missile, transform.position - transform.up, transform.rotation);
        g.GetComponent<Missile>().Target = Targeting.Instance.TrackedTarget;
    }
	#endregion
	
	#region Coroutines
	
	#endregion
}
