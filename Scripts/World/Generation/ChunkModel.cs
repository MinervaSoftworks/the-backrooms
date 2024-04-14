using Godot;
using System.Collections.Generic;

namespace Backrooms.World.Generation;

public class ChunkModel {
    public Vector2 Coordinates { get; }

    public Vector2 Dimensions { get; }

    private Dictionary<Vector2, RoomModel> _rooms = new Dictionary<Vector2, RoomModel> ();

    public ChunkModel (Vector2 cords, Vector2 dims) {
        Coordinates = cords;
        Dimensions = dims;
        SetupRooms ();
    }

    private void SetupRooms () {
        for (int x = 0; x < Dimensions.X; x++) {
            for (int y = 0; y < Dimensions.Y; y++) {
                var cords = new Vector2 (x, y);

                _rooms.Add (cords, new RoomModel (cords));
            }
        }
    }

    public RoomModel this [Vector2 vec] {
        get {
            return _rooms [vec];
        }
        set {
            _rooms [vec] = value;
        }
    }

    public IEnumerable<RoomModel> Neighbors (Vector2 of) {
        var neighbors = new List<RoomModel> ();

        var cords = WorldUtility.GetNeighbors (of, _rooms.Keys);

        foreach (var cord in cords) {
            neighbors.Add (_rooms [cord]);
        }

        return neighbors;
    }
}