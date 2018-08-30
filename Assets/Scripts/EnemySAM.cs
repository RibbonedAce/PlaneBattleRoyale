using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Targeting))]
public class EnemySAM : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>The maximum health the plane can have</para>
    /// </summary>
    [SerializeField]
    private int maxHealth;

    /// <summary>
    /// <para>How quickly the plane can turn</para>
    /// </summary>
    [SerializeField]
    private float turnSpeed;

    /// <summary>
    /// <para>The delay between projectile shots</para>
    /// </summary>
    [SerializeField]
    private float fireDelay;

    /// <summary>
    /// <para>The missile object to fire</para>
    /// </summary>
    [SerializeField]
    private GameObject projectile;

    /// <summary>
    /// <para>The health of the plane</para>
    /// </summary>
    private int health;

    /// <summary>
    /// <para>The time in seconds since last firing</para>
    /// </summary>
    private float lastFire;

    /// <summary>
    /// <para>The currently tracked target</para>
    /// </summary>
    private Transform target;

    /// <summary>
    /// <para>The Rigidbody component attached</para>
    /// </summary>
    private Rigidbody _rigidbody;

    /// <summary>
    /// <para>The Targeting component attached</para>
    /// </summary>
    private Targeting _targeting;

    /// <summary>
    /// <para>The rigidbody component attached to the target</para>
    /// </summary>
    private Rigidbody _tgtRigidbody;
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
    private void Awake()
	{
        _rigidbody = GetComponent<Rigidbody>();
        _targeting = GetComponent<Targeting>();
        Health = maxHealth;
        lastFire = fireDelay;
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
	private void FixedUpdate()
	{
        if (_tgtRigidbody != null)
        {
            _rigidbody.MoveRotation(Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.LookRotation(_tgtRigidbody.position - _rigidbody.position), turnSpeed));
        }
    }

    /// <summary>
    /// Called when something collides with the Game Object
    /// </summary>
    /// <param name="collision">The collision that occurred</param>
    private void OnCollisionEnter(Collision collision)
    {
        Missile m = collision.collider.GetComponent<Missile>();
        if (m != null)
        {
            Health -= m.Damage;
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Fires a missile at the current target
    /// </summary>
    private void FireProjectile()
    {
        GameObject g = Instantiate(projectile, transform.position + transform.up, transform.rotation);
        g.GetComponent<Missile>().Target = _targeting.TrackedTarget;
        lastFire = 0f;
    }
    #endregion

    #region Coroutines

    #endregion
}
