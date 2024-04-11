﻿using Backrooms.Common;
using Backrooms.World.Generation;
using Godot;

namespace Backrooms.World;

public partial class Chunk : Node3D {
    public ChunkModel ChunkModel { get; set; }

    public Vector2 ChunkPosition { get; set; }

    public Room [,] Rooms;

    public PackedScene RoomScene { get; set; }

    public void SetupRooms(ChunkModel model) {
        for(int x = 0; x < Rooms.GetLength(0); x++) {
            for(int y = 0; y < Rooms.GetLength(1); y++) {
                var localPos = new Vector2 (x, y);
                var worldPos = new Vector2 (ChunkPosition.X * Constants.ChunkWidth + x, ChunkPosition.Y * Constants.ChunkHeight + y);

                var room = RoomScene.Instantiate () as Room;

                room.LocalPosition = localPos;
                room.WorldPosition = worldPos;
                room.Position = new Vector3 (x * Constants.RoomWidth, 0, y * Constants.RoomHeight);
                room.SetupRoom (model [x, y]);

                Rooms [x, y] = room;

                AddChild (room);
            }
        }
    }

    public void ToggleRoomWalls (int x, int y, bool north = false, bool south = false, bool east = false, bool west = false) => Rooms [x, y].SetWalls (north, south, east, west);

    public void ToggleRoomCorners (int x, int y, bool northEast = false, bool northWest = false, bool southEast = false, bool southWest = false) => Rooms [x, y].SetWalls (northEast, northWest, southEast, southWest);
}
