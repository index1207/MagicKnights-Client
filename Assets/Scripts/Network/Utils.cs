using MagicKnights.Api.Packet;
using UnityEngine;

public class Utils
{
    public static FVector3 Convert(Vector3 v) => new() { X = v.x, Y = v.y, Z = v.z };
    public static Vector3 Convert(FVector3 v) => new() { x = v.X, y = v.Y, z = v.Z };
}
