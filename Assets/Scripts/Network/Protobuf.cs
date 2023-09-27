using Packet;
using UnityEngine;

public class Protobuf
{
    public static FVector3 MakeVector(Vector3 v)
    {
        return new FVector3 {X = v.x, Y = v.y, Z = v.z};
    }
}
