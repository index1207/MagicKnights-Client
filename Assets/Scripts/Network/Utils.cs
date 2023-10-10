using MagicKnights.Api.Packet;
using UnityEngine;

public class Utils
{
    public static FVector3 Convert(Vector3 v) => new FVector3 { X = v.x, Y = v.y, Z = v.z };
    public static Vector3 Convert(FVector3 v) => new Vector3 { x = v.X, y = v.Y, z = v.Z };
}
