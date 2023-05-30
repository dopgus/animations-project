using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public static class SkullUtil
{
#if UNITY_EDITOR
    [UnityEditor.MenuItem("Ebac/Test %g")]
    public static void Test()
    {
        Debug.Log("Test");
    }
#endif

    public static SphereCollider AddTrigger(Transform parent, float radius = 1)
    {
        GameObject _trigger = new GameObject("Trigger", typeof(SphereCollider));
        _trigger.transform.SetParent(parent);
        _trigger.transform.localPosition = Vector3.zero;
        _trigger.transform.localScale = Vector3.one;
        _trigger.layer = LayerMask.NameToLayer("Triggers");

        SphereCollider _triggerCollider = _trigger.GetComponent<SphereCollider>();
        _triggerCollider.radius = radius;
        _triggerCollider.isTrigger = true;

        return _triggerCollider;
    }

    public static void Scale(this Transform t, float size = 1.2f)
    {
        t.localScale = Vector3.one * size;
    }

    #region RANDOM STUFF

    public static T GetRandom<T>(this T[] array)
    {
        if (array.Length == 0)
            return default(T);

        return array[Random.Range(0, array.Length)];
    }

    public static T GetRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static T GetRandomButNotSame<T>(this List<T> list, T unique)
    {
        if (list.Count == 1)
            return unique;

        int randomIdex = Random.Range(0, list.Count);
        return list[randomIdex];
    }
    #endregion

    public static void DelayedCallback(MonoBehaviour mono, float delay, Action callback)
    {
        mono.StartCoroutine(CoroutineDelayedCallback(delay, callback));
    }

    private static IEnumerator CoroutineDelayedCallback(float delay, Action callback)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }

    public static void Scale(List<GameObject> objsToScale, float duration, float delayInEach = 0f)
    {
        for (int i = 0; i < objsToScale.Count; i++)
        {
            objsToScale[i].transform.DOScale(1, duration).SetDelay(delayInEach * i);
        }
    }

    public static string FormatTimeInMinutesAndSecons(float time)
    {
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = (time % 60).ToString("00");

        return string.Format("{0} : {1}", minutes, seconds);
    }

    #region STRINGS
    public static string StringToColor(string s, string color = "yellow")
    {
        return "<colo=" + color + ">" + s + "</color";
    }
    #endregion
}
