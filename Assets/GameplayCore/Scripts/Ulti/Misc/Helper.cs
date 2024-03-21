using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helper
{
    public static Camera _camera;
    private static Vector3 RandomVec;
    public static Camera Camera
    {
        get
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }

            return _camera;
        }
    }


    private static List<int> listSort = new List<int>();

    public static int GetSort(int min)
    {
        int x = 0;
        for (int i = 0; i < listSort.Count - 1; i++)
        {
            if (listSort[0] > min)
            {
                x = min;
                listSort.Add(x);
                listSort.Sort();
                break;
            }
            if (listSort[i] + 1 >= min && listSort[i] + 1 != listSort[i + 1])
            {
                x = listSort[i] + 1;
                listSort.Add(x);
                listSort.Sort();
                break;
            }
        }
        if (x == 0)
        {
            if (listSort.Count == 0)
            {
                x = min;
            }
            else
            {
                x = listSort[listSort.Count - 1] + 1 >= min ? listSort[listSort.Count - 1] + 1 : min;
            }

            listSort.Add(x);
        }
        return x;
    }
    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    public static Vector3 GetRandomVecter()
    {
        RandomVec.y = UnityEngine.Random.Range(180, 370);
        return RandomVec;
    }
    public static void listReset() { listSort.Clear(); }

    private static Dictionary<float, WaitForSeconds> waitDictionary = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWait(float time)
    {
        if (waitDictionary.TryGetValue(time, out var wait))
        {
            return wait;
        }

        waitDictionary[time] = new WaitForSeconds(time);
        return waitDictionary[time];
    }

    private static PointerEventData _eventDataCurrentPos;
    private static List<RaycastResult> _result;

    public static bool IsOverUi()
    {
        _eventDataCurrentPos = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        _result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurrentPos, _result);
        return _result.Count > 0;
    }

    public static T GetEnumValue<T>(int _value) where T : Enum
    {
        T[] values = (T[])Enum.GetValues(typeof(T));

        if (_value < 0)
        {
            _value = values.Length - 1;
        }
        T resul = values[0];
        foreach (T value in values)
        {
            int intValue = Convert.ToInt32(value);
            if (intValue == _value)
            {
                resul = value;
            }
        }
        return resul;
    }

    public static string ConvertNumberToStringUlong(ulong numb)
    {
        ulong Billion = 1000000000;
        ulong Million = 1000000;
        ulong Thousand = 1000;

        string result = "";

        if (numb > Billion * Billion * 10)
        {
            numb = numb / (Billion * Billion);
            result = numb + "BB";
        }
        else if (numb > Million * Billion * 10)
        {
            numb = numb / (Billion * Million);
            result = numb + "MB";
        }
        else if (numb > Thousand * Billion * 10)
        {
            numb = numb / (Billion * Thousand);
            result = numb + "KB";
        }
        else if (numb > Billion * 10)
        {
            numb = numb / Billion;
            result = numb + "B";
        }
        else if (numb > Million * 10)
        {
            numb = numb / Million;
            result = numb + "M";
        }
        else if (numb > Thousand * 10)
        {
            numb = numb / Thousand;
            result = numb + "K";
        }
        else
        {
            result = numb.ToString();
        }
        return result;
    }

    public static string ConvertNumberToString(int numb)
    {
        ulong Thousand = 1000;

        string result = "";

        if (numb > 1000000 * 10)
        {
            numb /= 1000000;
            result = numb + "M";
        }
        else if (numb > 1000 * 10)
        {
            numb /= 1000;
            result = numb + "K";
        }
        else
        {
            result = numb.ToString();
        }
        return result;
    }

    public static ulong[] stringToUlongArray(string input)
    {
        input = input.Replace(",", "");
        List<ulong> _reward = new List<ulong>();
        string[] alls = input.Split(' ');
        foreach (var item in alls)
        {
            ulong _out = 0;
            if (ulong.TryParse(item, out _out))
            {
                _reward.Add(_out);
            }
        }

        return _reward.ToArray();
    }
    public static int[] stringToIntArray(string input)
    {
        input = input.Replace(",", "");
        List<int> _reward = new List<int>();
        string[] alls = input.Split(' ');
        foreach (var item in alls)
        {
            int _out = -1;
            if (int.TryParse(item, out _out))
            {
                _reward.Add(_out);
            }
        }

        return _reward.ToArray();
    }

    public static float[] stringToFloatArray(string input)
    {
        input = input.Replace(",", ".");
        List<float> _reward = new List<float>();
        string[] alls = input.Split(' ');
        foreach (var item in alls)
        {
            float _out = -1;
            if (float.TryParse(item, out _out))
            {
                _reward.Add(_out);
            }
        }

        return _reward.ToArray();
    }


    public static bool DifferentDate(string _oldTime)
    {
        var tempOff = Convert.ToInt64(_oldTime);
        var oldTime = DateTime.FromBinary(tempOff);
        var current = DateTime.Now;
        if (oldTime.Day != current.Day || oldTime.Month != current.Month || oldTime.Year != current.Year)
        {
            return true;
        }
        return false;
    }


    public static float DifferentTimeNowSecond(string _oldTime)
    {
        var tempOff = Convert.ToInt64(_oldTime);
        var oldTime = DateTime.FromBinary(tempOff);
        var current = DateTime.Now;
        var diff = current.Subtract(oldTime);
        return (float)diff.TotalSeconds;
    }

    public static float DifferentTimeNowMinute(string _oldTime)
    {
        var tempOff = Convert.ToInt64(_oldTime);
        var oldTime = DateTime.FromBinary(tempOff);
        var current = DateTime.Now;
        var diff = current.Subtract(oldTime);
        return (float)diff.TotalMinutes;
    }

    public static float DifferentTimeNowDate(string _oldTime)
    {
        var tempOff = Convert.ToInt64(_oldTime);
        var oldTime = DateTime.FromBinary(tempOff);
        var current = DateTime.Now;
        var diff = current.Subtract(oldTime);
        return (float)diff.TotalDays;
    }


    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var result);
        return result;
    }


    public static float DistanceSqrt(this Vector3 vec, Vector3 tar)
    {
        return (tar.x - vec.x) * (tar.x - vec.x) + (tar.y - vec.y) * (tar.y - vec.y) + (tar.z - vec.z) * (tar.z - vec.z);
    }

    // Check 2 đường thẳng giao nhau
    public static bool AreLinesIntersecting(Vector3 p1, Vector3 p2, Vector3 q1, Vector3 q2)
    {
        // Tính toán các hệ số
        float denominator = (q2.z - q1.z) * (p2.x - p1.x) - (q2.x - q1.x) * (p2.z - p1.z);
        float numerator1 = (q2.x - q1.x) * (p1.z - q1.z) - (q2.z - q1.z) * (p1.x - q1.x);
        float numerator2 = (p2.x - p1.x) * (p1.z - q1.z) - (p2.z - p1.z) * (p1.x - q1.x);

        // Kiểm tra xem hai đoạn thẳng có giao nhau không
        if (denominator == 0)
        {
            return numerator1 == 0 && numerator2 == 0;
        }

        // Kiểm tra xem điểm giao nhau nằm trong khoảng của cả hai đoạn thẳng
        float r = numerator1 / denominator;
        float s = numerator2 / denominator;

        return r >= 0 && r <= 1 && s >= 0 && s <= 1;
    }

    public static bool CheckIntersection(Vector3 previousPos, Vector3 currentPos, Vector3[] linePositions)
    {
        for (int i = 0; i < linePositions.Length - 1; i++)
        {
            Vector3 lineStart = linePositions[i];
            Vector3 lineEnd = linePositions[i + 1];

            if (AreLinesIntersecting(previousPos, currentPos, lineStart, lineEnd))
            {
                return true;
            }
        }

        return false;
    }

    public static Vector3 CustomNormalize(this Vector3 v)
    {
        double m = System.Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        Debug.Log(m);

        if (m > 9.99999974737875E-06)
        {
            float fm = (float)m;
            v.x /= fm;
            v.y /= fm;
            v.z /= fm;
            return v;
        }
        else
        {
            return Vector3.zero;
        }
    }


    public static bool CustomOutNormalize(this Vector3 v, Vector3 tar, out Vector3 _result)
    {
        double m = System.Math.Sqrt((tar.x - v.x) * (tar.x - v.x) + (tar.y - v.y) * (tar.y - v.y) + (tar.z - v.z) * (tar.z - v.z));
        _result = Vector3.zero;
        if (m > 0.4f)
        {
            float fm = (float)m;
            v.x = (tar.x - v.x) / fm;
            v.y = (tar.y - v.y) / fm;
            v.z = (tar.z - v.z) / fm;
            _result = v;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static int GetVector(int x, int y)
    {
        return x * 8 + y;
    }
    public static int GetVector(Vector2Int vec)
    {
        return vec.x * 8 + vec.y;
    }
}
