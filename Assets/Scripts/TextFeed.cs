using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFeed : MonoBehaviour 
{
    #region Variables
    /// <summary>
    /// <para>How many seconds each entry lasts</para>
    /// </summary>
    [SerializeField]
    private float expireTime;

    /// <summary>
    /// <para>The currently tracked objects</para>
    /// </summary>
    private List<GameObject> currentObjects;
    #endregion

    #region Properties
    public static TextFeed Instance { get; private set; }
    #endregion

    #region Events
    /// <summary>
    /// Awake is called before start
    /// </summary>
    private void Awake()
	{
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        currentObjects = new List<GameObject>();
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
		for (int i = 0; i < transform.childCount; ++i)
        {
            if (!currentObjects.Contains(transform.GetChild(i).gameObject))
            {
                AddObject(transform.GetChild(i).gameObject);
            }
        }
        foreach (GameObject g in currentObjects)
        {
            if (g.GetComponent<TextEntry>().Lifetime > expireTime)
            {
                RemoveTopObject();
            }
        }
	}
	
	/// <summary>
    /// Use this for physics-related changes
    /// </summary>
	private void FixedUpdate()
	{
		
	}
	#endregion
	
	#region Methods
    /// <summary>
    /// Add a text object to the list
    /// </summary>
    /// <param name="obj">The object to add</param>
	public void AddObject(GameObject obj)
    {
        foreach (GameObject g in currentObjects)
        {
            RectTransform rt = g.GetComponent<RectTransform>();
            rt.SetAnchors(new Vector2(rt.anchorMin.x, rt.anchorMin.y + 0.1f), new Vector2(rt.anchorMax.x, rt.anchorMax.y + 0.1f));
        }
        currentObjects.Add(obj);
        obj.GetComponent<RectTransform>().SetAnchors(new Vector2(0f, 0f), new Vector2(1f, 0.1f));
    }

    /// <summary>
    /// Remove the topmost text object from the list
    /// </summary>
	public void RemoveTopObject()
    {
        Destroy(currentObjects[0]);
        currentObjects.RemoveAt(0);
    }
    #endregion

    #region Coroutines

    #endregion
}
