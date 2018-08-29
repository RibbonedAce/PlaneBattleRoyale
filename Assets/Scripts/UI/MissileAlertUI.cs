using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAlertUI : TextUI 
{
    #region Variables
    /// <summary>
    /// <para>The transform to track missile targeting</para>
    /// </summary>
    [SerializeField]
    private Transform source;

    /// <summary>
    /// <para>A missile that is attacking the source</para>
    /// </summary>
    private Missile currentMissile;
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
        if (currentMissile == null)
        {
            foreach (Missile m in Missile.Missiles)
            {
                if (m.Target == source)
                {
                    currentMissile = m;
                    break;
                }
            }
        }
        _text.text = currentMissile != null ? "MISSILE ALERT" : "";
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
