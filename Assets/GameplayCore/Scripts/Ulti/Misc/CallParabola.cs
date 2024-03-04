using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CallParabola
{
    public static IEnumerator MoveParabol(Transform movingGo, Vector3 start, Vector3 to, float arcHeight, float speed, Action callBack = null)
    {
        float distance = Vector3.Distance(start, to);

        float _stepScale = speed / distance;

        float _progress = 0;
        movingGo.position = start;
        while (movingGo.position.DistanceSqrt(to) > 0.01f)
        {
            _progress = Mathf.Min(_progress + Time.deltaTime * _stepScale, 1.0f);

            // Turn this 0-1 value into a parabola that goes from 0 to 1, then back to 0.
            float parabola = 1.0f - 4.0f * (_progress - 0.5f) * (_progress - 0.5f);

            // Travel in a straight line from our start position to the target.        
            Vector3 nextPos = Vector3.Lerp(start, to, _progress);

            // Then add a vertical arc in excess of this.
            nextPos.y += parabola * arcHeight;

            // Continue as before.
            movingGo.LookAt(nextPos, movingGo.forward);
            movingGo.position = nextPos;
            yield return null;
        }
        if (callBack != null)
        {
            callBack.Invoke();
        }
    }
}
