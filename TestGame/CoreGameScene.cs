using System;
using System.IO;
using Puffin.Core;
using Puffin.Core.Ecs;
using Puffin.Core.Tiles;

namespace MyGame
{
    public class CoreGameScene : Scene
    {
        private const int MOVE_VELOCITY = 200;
        private DateTime start = DateTime.Now;

        public CoreGameScene()
        {
            var tileMap = new TileMap(30, 17, Path.Combine("Content", "dungeon.png"), 32, 32);
            tileMap.Define("Floor", 0, 0);
            tileMap.Define("Wall", 1, 0, true);

            for (var y = 0; y < 17; y++) {
                for (var x = 0; x < 30; x++) {
                    if (x == 0 || y == 0 || x == 29 || y == 16) {
                        tileMap[x, y] = "Wall";
                    } else {
                        tileMap[x, y] = "Floor";
                    }
                }
            }

            this.Add(tileMap);

            var player = new Entity().Colour(0xFFFFFF, 32, 32)
                .Move(850, 48)
                .FourWayMovement(100)
                .Collide(32, 32, true);
            
            float total = 0;
            player.OnUpdate((elapsed) => {
                total += elapsed;
                if (total >= 1000) {
                    total = 0;
                    player.Colour(0xFF0000, 32, 32);
                } else if (total >= 500) {
                    player.Colour(0x0000FF, 64, 64);
                }
            });
            
            this.Add(player);
                

            this.Add(new Entity().Colour(0xFF0000, 128, 64).Move(100, 100).Collide(128, 64));
            this.Add(new Entity().Colour(0x884400, 128, 64).Move(150, 200).Collide(128, 64));

            this.Add(new Entity().Colour(0x0088FF, 32, 32).Move(50, 400).Collide(32, 32, true).Velocity(MOVE_VELOCITY, -MOVE_VELOCITY));
        }
    }
}