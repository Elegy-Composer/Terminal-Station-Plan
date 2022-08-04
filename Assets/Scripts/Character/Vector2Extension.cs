using UnityEngine;

public static class Vector2Extension
{
    public static Vector3 ExtendToVector3(this Vector2 vector)
    {
        return new Vector3(vector.x, vector.y, 0);
    }
}
