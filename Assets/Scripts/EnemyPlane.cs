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
    /// <para>The number of seconds the enemy can chase before needing to level out</para>
    /// </summary>
    [SerializeField]
    protected float stamina;
    
    /// <summary>
    /// <para>The delay between projectile shots</para>
    /// </summary>
    [SerializeField]
    protected float fireDelay;

    /// <summary>
    /// <para>The number of seconds since a chase started</para>
    /// </summary>
    protected float timeChasing;

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

    /// <summary>
    /// <para>The number of seconds needed to recover</para>
    /// </summary>
    protected readonly float recoverTime = 5f;
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
        timeChasing = stamina;
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

        // Manage chase
        timeChasing -= Time.deltaTime;
        if (timeChasing <= 0)
        {
            StartCoroutine(Recover());
        }
    }

    /// <summary>
    /// Use this for physics-related changes
    /// </summary>
    protected override void FixedUpdate()
    {
        if (!AutoPilot && _tgtRigidbody != null)
        {
            _rigidbody.MoveRotation(Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.LookRotation(_tgtRigidbody.position - _rigidbody.position), turnSpeed));
        }
        thrust = Mathf.Lerp(minThrust, maxThrust, (GetThrust() + 1) / 2);
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

    /// <summary>
    /// Finds the thrust needed based on distance to target
    /// </summary>
    /// <returns>The thrust level that the enemy will use</returns>
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
    /// <summary>
    /// Recover from chasing the target
    /// </summary>
    /// <returns>The time to recover</returns>
    protected virtual IEnumerator Recover()
    {
        AutoPilot = true;
        yield return new WaitForSeconds(recoverTime);
        timeChasing = stamina;
        AutoPilot = false;
    }
    #endregion
}

