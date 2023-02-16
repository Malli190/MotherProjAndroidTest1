using Game.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.MapSystem.Models
{
    public class AreaChunk : Chunk // Регион
    {
        public string name { get; set; }
        
        public List<RegionChunk> chunks;
        public AreaChunk()
        {
            chunks = new List<RegionChunk>();
        }
        public AreaChunk(Location size)
        {
            chunks = new List<RegionChunk>();

            SetSize(size);
        }
        public void Init()
        {
            SetSize(new Location(2, 2));
            GenerateChunks();
        }
        public void GenerateChunks()
        {
            int cID = 0;

            chunks.Clear();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    RegionChunk chunk = new RegionChunk()
                    {
                        id = cID,
                        name = StaticMethods.GetRandomMapName(3, 2),
                        position = new Location(x, y),
                    };
                    chunk.Init();
                    chunks.Add(chunk);

                    cID += 1;
                }
            }
        }
        public RegionChunk GetChunkByPosition(Location pos)
        {
            RegionChunk result = null;
            for (int i = 0; i < chunks.Count; i++)
            {
                if (chunks[i].position.x == pos.x &&
                    chunks[i].position.y == pos.y)
                {
                    result = chunks[i];
                    break;
                }
            }
            return result;
        }
    }
}
