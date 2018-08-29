using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUI : TextUI 
{
    #region Variables
    /// <summary>
    /// <para>The Rigidbody to monitor speed</para>
    /// </summary>
    [SerializeField]
    private Rigidbody _rigidbody;
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
        _text.text = string.Format("Speed:\n{0:0.0}", _rigidbody.velocity.magnitude);
    }

    /// <summary>
    /// Use this for physics-related changes
    /// </summary>
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        _text.text = _rigidbody.useGravity ? "STALLING" : "";
    }
    #endregion

    #region Methods

    #endregion

    #region Coroutines

    #endregion
}
