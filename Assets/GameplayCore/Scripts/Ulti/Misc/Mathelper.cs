using System.Collections;
using UnityEngine;

public static class Mathelper
{
    public static float Distance(Vector3 a, Vector3 b)
    {
        Vector3 vector;
        float distanceSquared;

        vector.x = a.x - b.x;
        vector.y = a.y - b.y;
        vector.z = a.z - b.z;

        distanceSquared = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;

        return (float)System.Math.Sqrt(distanceSquared);
    }

    public static float Distance(Vector2 a, Vector2 b)
    {
        Vector3 vector;
        float distanceSquared;

        vector.x = a.x - b.x;
        vector.y = a.y - b.y;

        distanceSquared = vector.x * vector.x + vector.y * vector.y;

        return (float)System.Math.Sqrt(distanceSquared);
    }

    public static float Distance(float x, float y, float z, float a, float b, float c)
    {
        Vector3 vector;
        float distanceSquared;

        vector.x = x - a;
        vector.y = y - b;
        vector.z = z - c;

        distanceSquared = vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;

        return (float)System.Math.Sqrt(distanceSquared);
    }

    public static float Distance(float x, float y, float a, float b)
    {
        Vector3 vector;
        float distanceSquared;

        vector.x = x - a;
        vector.y = y - b;

        distanceSquared = vector.x * vector.x + vector.y * vector.y;

        return (float)System.Math.Sqrt(distanceSquared);
    }
}
