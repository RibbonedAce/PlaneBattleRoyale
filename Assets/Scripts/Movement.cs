using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Movement : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>The units to travel in per second</para>
    /// </summary>
    [SerializeField]
    protected float thrust;

    /// <summary>
    /// <para>The magnitude of speed lost per FixedUpdate call</para>
    /// </summary>
    [SerializeField]
    protected float drag;

    /// <summary>
    /// <para>The minimum speed to go before stalling</para>
    /// </summary>
    [SerializeField]
    protected float stallSpeed;

    /// <summary>
    /// <para>The Rigidbody component attached</para>
    /// </summary>
    protected Rigidbody _rigidbody;
	#endregion
	
	#region Properties
	
	#endregion
	
	#region Events
	/// <summary>
    /// Awake is called before start
    /// </summary>
	protected virtual void Awake()
	{
        _rigidbody = GetComponent<Rigidbody>();
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

	}

    /// <summary>
    /// Use this for physics-related changes
    /// </summary>
    protected virtual void FixedUpdate()
	{
        _rigidbody.velocity += transform.forward * thrust;
        _rigidbody.velocity *= (1 - drag);
        if (!_rigidbody.useGravity && _rigidbody.velocity.magnitude <= stallSpeed)
        {
            _rigidbody.useGravity = true;
        }
        else if (_rigidbody.useGravity && _rigidbody.velocity.magnitude > stallSpeed)
        {
            _rigidbody.useGravity = false;
        }
    }

    /// <summary>
    /// Called when something collides with the Game Object
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
	#endregion
	
	#region Methods
	
	#endregion
	
	#region Coroutines
	
	#endregion
}