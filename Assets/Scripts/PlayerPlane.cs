using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Targeting))]
public class PlayerPlane : Plane
{
    #region Variables
    
    #endregion

    #region Properties
    /// <summary>
    /// <para>The instance to reference</para>
    /// </summary>
    public static PlayerPlane Instance { get; private set; }
    #endregion

    #region Events
    /// <summary>
    /// Awake is called before start
    /// </summary>
    protected override void Awake()
	{
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        base.Awake();
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
        if (Input.GetButtonDown("Fire"))
        {
            FireProjectile();
        }
        if (Input.GetButton("Switch"))
        {
            _targeting.GetClosestTarget();
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
    /// <param name="collision">The collision that occurred</param>
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
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
	#endregion
	
	#region Coroutines
	
	#endregion
}
