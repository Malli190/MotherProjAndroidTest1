using Game.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.MapSystem.Models
{
    public class RegionChunk : Chunk // Участок
    {
        public string name { get; set; }
        public List<Chunk> chunks;
        public RegionChunk()
        {
            chunks = new List<Chunk>();

        }
        public RegionChunk(Location size)
        {
            chunks = new List<Chunk>();

            SetSize(size);
        }
        public void Init()
        {
            SetSize(new Location(10, 10));
            GenerateChunks();
        }
        void GenerateChunks()
        {
            int cID = 0;
            
            chunks.Clear();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Chunk chunk = new Chunk()
                    {
                        id = cID,
                        position = new Location(x, y),
                    };
                    chunks.Add(chunk);

                    cID += 1;
                }
            }
        }
    }
}
