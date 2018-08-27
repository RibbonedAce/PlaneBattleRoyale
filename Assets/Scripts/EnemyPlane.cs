using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Targeting))]
public class EnemyPlane : Plane
{
    #region Variables
    /// <summary>
    /// <para>The delay between projectile shots</para>
    /// </summary>
    [SerializeField]
    protected float fireDelay;

    /// <summary>
    /// <para>The time in seconds since last firing</para>
    /// </summary>
    protected float lastFire;

    /// <summary>
    /// <para>The currently tracked target</para>
    /// </summary>
    protected Transform target;

    /// <summary>
    /// <para>The rigidbody component attached to the target</para>
    /// </summary>
    protected Rigidbody _tgtRigidbody;

    /// <summary>
    /// <para>How close to the target the plane can be before slowing down</para>
    /// </summary>
    protected readonly float minDistance = 5f;

    /// <summary>
    /// <para>How far way to the target the plane can be before speeding up</para>
    /// </summary>
    protected readonly float maxDistance = 80f;
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
        lastFire = fireDelay;
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

        // Decide when to fire
        lastFire = Mathf.Min(lastFire + Time.deltaTime, fireDelay);
        if (lastFire >= fireDelay && _targeting.TrackedTarget != null)
        {
            FireProjectile();
        }
        
        // Handle possible target switching
        if (_targeting.CurrentTarget != null && _targeting.CurrentTarget.transform != target)
        {
            target = _targeting.CurrentTarget.transform;
            _tgtRigidbody = target.GetComponent<Rigidbody>();
        }
    }

    /// <summary>
    /// Use this for physics-related changes
    /// </summary>
    protected override void FixedUpdate()
    {
        if (_tgtRigidbody != null)
        {
            Quaternion rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.LookRotation(_tgtRigidbody.position - _rigidbody.position), turnSpeed);
            _rigidbody.MoveRotation(rotation);
        }
        float currentThrust = GetThrust();
        thrust = Mathf.Lerp(minThrust, maxThrust, (currentThrust + 1) / 2);
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
    /// Fires a missile at the current target
    /// </summary>
    protected override void FireProjectile()
    {
        base.FireProjectile();
        lastFire = 0f;
    }

    protected virtual float GetThrust()
    {
        float distance = _tgtRigidbody == null ? minDistance + 1 : Vector3.Distance(_rigidbody.position, _tgtRigidbody.position);
        if (distance < minDistance)
        {
            return -1f;
        }
        else if (distance < maxDistance)
        {
            return 0f;
        }
        else
        {
            return 1f;
        }
    }
    #endregion

    #region Coroutines

    #endregion
}

