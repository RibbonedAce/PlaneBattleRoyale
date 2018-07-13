using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target 
{
    #region Variables
    /// <summary>
    /// <para>The Transform that the target tracks</para>
    /// </summary>
    public Transform transform;

    /// <summary>
    /// <para>The Reticle on screen that tracks the target</para>
    /// </summary>
    public Reticle reticle;

    /// <summary>
    /// <para>The Blip on radar that tracks the target</para>
    /// </summary>
    public Blip blip;
    #endregion

    #region Properties

    #endregion

    #region Constructors
    /// <summary>
    /// The constructor filling in the transform and its given reticle and blip
    /// </summary>
    /// <param name="t">The transform to track</param>
    /// <param name="r">The reticle to use</param>
    /// <param name="b">The blip to use</param>
    public Target(Transform t, Reticle r, Blip b)
    {
        transform = t;
        reticle = r;
        blip = b;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Set the activeness of the reticle and the blip
    /// </summary>
    /// <param name="active">What to set the activeness to</param>
    public void SetActive(bool active)
    {
        reticle.Active = active;
        blip.Active = active;
    }

    /// <summary>
    /// <para>Searches for a Transform targetted in a list</para>
    /// </summary>
    /// <param name="list">The Collection of Reticles</param>
    /// <param name="t">The Transform to look for</param>
    /// <returns>Whether the Collection has a Target on the Transform</returns>
    public static bool ContainsTransform(ICollection<Target> list, Transform t)
    {
        foreach (Target tgt in list)
        {
            if (tgt.transform == t)
            {
                return true;
            }
        }
        return false;
    }
    #endregion

    #region Coroutines

    #endregion
}
