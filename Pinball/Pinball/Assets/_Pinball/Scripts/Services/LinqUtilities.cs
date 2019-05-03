using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LinqUtilities {

    /// <summary>
    /// Makes it possible to query a transforms children with linq
    /// example --- transform.Children().Where(x=>x.name.Contains("Patrol"))
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static IEnumerable<Transform> Children(this Transform t)
    {
        foreach (Transform c in t)
            yield return c;
    }
}
