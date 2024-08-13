using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers
{

}

public static class AnimatorExtensions
{
    public static void ResetAllAnimatorTriggers(this Animator animator)
    {
        foreach (var trigger in animator.parameters)
        {
            if (trigger.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(trigger.name);
            }
        }
    }
    public static void SetTrigger(this Animator animator, string name, bool resetAllTriggers)
    {
        if (resetAllTriggers)
        {
            animator.ResetAllAnimatorTriggers();
        }
        animator.SetTrigger(name);
    }
}



public static class GameObjectExtensions
{
    public static void SetRecursiveLayer(this GameObject o, int layer)
    {
        SetInternalLayer(o.transform, layer);
    }

    private static void SetInternalLayer(Transform t, int layer)
    {
        t.gameObject.layer = layer;

        foreach (Transform o in t)
        {
            SetInternalLayer(o, layer);
        }
    }
}


public static class TransformExtensions
{
    public static void DestroyAllChildren(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        transform.DetachChildren();
    }

    public static void DestroyAllChildrenImmediate(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        transform.DetachChildren();
    }

    public static T GetClosestObject<T>(this List<T> allObject, Vector3 position) where T : Component
    {
        var closestDistance = float.MaxValue;
        T closestObject = null;
        foreach (var current in allObject)
        {
            var currentDistance = Vector3.Distance(current.transform.position, position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                closestObject = current;
            }
        }

        return closestObject;
    }

    public static Bounds GetLocalBoundsForObject(this Transform t)
    {
        var b = new Bounds(Vector3.zero, Vector3.zero);
        RecurseEncapsulate(t, ref b);
        return b;

        void RecurseEncapsulate(Transform child, ref Bounds bounds)
        {
            var mesh = child.GetComponent<MeshFilter>();
            if (mesh)
            {
                var lsBounds = mesh.sharedMesh.bounds;
                var wsMin = child.TransformPoint(lsBounds.center - lsBounds.extents);
                var wsMax = child.TransformPoint(lsBounds.center + lsBounds.extents);
                bounds.Encapsulate(t.InverseTransformPoint(wsMin));
                bounds.Encapsulate(t.InverseTransformPoint(wsMax));
            }
            foreach (Transform grandChild in child.transform)
            {
                RecurseEncapsulate(grandChild, ref bounds);
            }
        }
    }

}

public static class VectorExtensions
{
    public static float DistanceXZ(this Vector3 _source, Vector3 _target)
    {
        return Vector2.Distance(new Vector2(_source.x, _source.z), new Vector2(_target.x, _target.z));
    }
}

public static class ListExtensions
{
    public static T GetRandom<T>(this List<T> myList)
    {
        return myList[UnityEngine.Random.Range(0, myList.Count)];
    }

    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}