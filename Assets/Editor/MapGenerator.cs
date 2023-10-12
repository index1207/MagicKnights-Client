using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    #if UNITY_EDITOR
    [MenuItem("Tools/Generate Mapdata")]
    private static void GenerateMapdata()
    {
        StringBuilder builder = new StringBuilder();
        GameObject map = Resources.Load<GameObject>("Prefabs/Map/Map");
        Tilemap collision = Util.FindChild<Tilemap>(map, "Collision", true);

        var cellBounds = collision.cellBounds;
        Vector2Int min = new Vector2Int(cellBounds.xMin, cellBounds.yMin);
        Vector2Int max = new Vector2Int(cellBounds.xMax, cellBounds.yMax);

        builder.Append(min.x + " " + min.y);
        builder.AppendLine();
        builder.Append(max.x + " " + max.y);
        builder.AppendLine();
        
        for (int y = max.y; y >= min.y; --y)
        {
            for (int x = min.x; x <= max.x; ++x)
            {
                builder.Append(collision.GetTile(new Vector3Int(x, y)) ? "1" : "0");
            }
            builder.AppendLine();
        }

        string filename = "Mapdata.txt";
        using (var writer = File.CreateText($"Assets/Resources/Map/{filename}"))
        {
            writer.Write(builder);
        }
        using (var writer = File.CreateText($"../server/data/map/{filename}"))
        {
            writer.Write(builder);
        }
    }
    #endif
}
