using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utils
{
    #region Methods
    /// <summary>
    /// Move the position by the given amount
    /// </summary>
    /// <param name="r">The rigidbody to use</param>
    /// <param name="move">The Vector3 to move the position by</param>
    public static void MovePositionBy(this Rigidbody r, Vector3 move)
    {
        r.MovePosition(r.position + move);
    }

    /// <summary>
    /// Move the rotation by the given amount
    /// </summary>
    /// <param name="r">The rigidbody to use</param>
    /// <param name="move">The Quaternion to move the rotation by</param>
    public static void MoveRotationBy(this Rigidbody r, Quaternion move)
    {
        r.MoveRotation(r.rotation * move);
    }

    /// <summary>
    /// Set the anchors of a Rect Transform while also keeping offets at zero
    /// </summary>
    /// <param name="r"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public static void SetAnchors(this RectTransform r, Vector2 min, Vector2 max)
    {
        r.anchorMin = min;
        r.anchorMax = max;
        r.anchoredPosition = Vector3.zero;
        r.sizeDelta = Vector3.zero;
    }

    /// <summary>
    /// Interpolates two Quaternions at a fixed angle from the first
    /// </summary>
    /// <param name="q1">The Quaternion to measure the angle from</param>
    /// <param name="q2">The Quaternion to turn towards</param>
    /// <param name="angle">The maximum angle change from the first Quaternion</param>
    /// <returns>The new angle; returns the second Quaternion if the angle is smaller than the given one</returns>
    public static Quaternion SlerpFixedDistance(Quaternion q1, Quaternion q2, float angle)
    {
        return Quaternion.Slerp(q1, q2, angle / Quaternion.Angle(q1, q2));
    }

    /// <summary>
    /// Takes the world to viewport point of a position, but returns (-1, -1) if not in front
    /// </summary>
    /// <param name="pos">The position in the world to convert</param>
    /// <returns>The viewport point on screen</returns>
    public static Vector2 FrontWorldtoViewportPoint(Vector3 pos)
    {
        if (Vector3.Angle(Camera.main.transform.forward, pos - Camera.main.transform.position) < 90f)
        {
            return Camera.main.WorldToViewportPoint(pos);
        }
        else
        {
            return Vector2.one * -1;
        }
    }

    /// <summary>
    /// Makes a horizontal vector from a given degree rotation on the y-axis and magnitude
    /// </summary>
    /// <param name="angle">The angle of the rotation in degrees</param>
    /// <param name="magnitude">The magnitude of the vector</param>
    /// <returns>The vector from the rotated angle</returns>
    public static Vector2 VectorFromAngle(float angle, float magnitude)
    {
        Vector2 result = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle));
        return result * magnitude;
    }
    #endregion

    #region Coroutines

    #endregion
}
