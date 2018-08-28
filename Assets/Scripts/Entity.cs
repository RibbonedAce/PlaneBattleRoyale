using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    #region Variables
    /// <summary>
    /// <para>The Transform that the target tracks</para>
    /// </summary>
    public Transform transform;

    /// <summary>
    /// <para>The Blip on radar that tracks the target</para>
    /// </summary>
    public Blip blip;
    #endregion

    #region Properties

    #endregion

    #region Constructors
    /// <summary>
    /// The constructor filling in the transform and its given blip
    /// </summary>
    /// <param name="t">The transform to track</param>
    /// <param name="b">The blip to use</param>
    public Entity(Transform t, Blip b)
    {
        transform = t;
        blip = b;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Set the activeness of the blip
    /// </summary>
    /// <param name="active">What to set the activeness to</param>
    public virtual void SetActive(bool active)
    {
        if (blip != null)
        {
            blip.Active = active;
        }
    }

    /// <summary>
    /// <para>Searches for a Transform tracked in a list</para>
    /// </summary>
    /// <param name="list">The Collection of Reticles</param>
    /// <param name="e">The Transform to look for</param>
    /// <returns>Whether the Collection has an Entity on the Transform</returns>
    public static bool ContainsTransform(ICollection<Entity> list, Transform e)
    {
        foreach (Entity ent in list)
        {
            if (ent.transform == e)
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
