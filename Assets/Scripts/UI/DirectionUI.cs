using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionUI : TextUI 
{
    #region Variables
    /// <summary>
    /// <para>The transform to measure direction</para>
    /// </summary>
    [SerializeField]
    private Transform source;
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
        _text.text = string.Format("{0:000}", (Vector3.SignedAngle(Vector3.forward, Vector3.ProjectOnPlane(source.forward, Vector3.up), Vector3.up) + 360) % 360);
    }

    /// <summary>
    /// Use this for physics-related changes
    /// </summary>
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
	#endregion
	
	#region Methods
	
	#endregion
	
	#region Coroutines
	
	#endregion
}
