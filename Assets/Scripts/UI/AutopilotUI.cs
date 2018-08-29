using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutopilotUI : TextUI 
{
    #region Variables
    /// <summary>
    /// <para>The Plane to track the status of autopilot</para>
    /// </summary>
    [SerializeField]
    private Plane plane;
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
        _text.text = plane.AutoPilot ? "AUTOPILOT" : "";
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
