using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerPlane : Movement 
{
    #region Variables
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
    /// <para>The missile object to fire</para>
    /// </summary>
    [SerializeField]
    protected GameObject missile;
    #endregion

    #region Properties

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
        _rigidbody.MoveRotationBy(Quaternion.Euler(Vector3.right * Input.GetAxis("Pitch")));
        _rigidbody.MoveRotationBy(Quaternion.Euler(-Vector3.forward * Input.GetAxis("Roll")));
        _rigidbody.MoveRotationBy(Quaternion.Euler(Vector3.up * Input.GetAxis("Yaw")));
        thrust = Mathf.Lerp(minThrust, maxThrust, (Input.GetAxis("Thrust") + 1) / 2);
        base.FixedUpdate();
    }

    /// <summary>
    /// Called when something collides with the Game Object
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
    #endregion

    #region Methods
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
