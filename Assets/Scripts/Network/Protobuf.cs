using MagicKnights.Api.Packet;
using UnityEngine;

public class Utils
{
    public static FVector3 MakeVector(Vector3 v) => new FVector3 { X = v.x, Y = v.y, Z = v.z };
}
