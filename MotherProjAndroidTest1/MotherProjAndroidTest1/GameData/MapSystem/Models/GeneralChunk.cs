using Game.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.MapSystem.Models
{
    public class GeneralChunk : Chunk // Общий
    {
        public string name { get; set; }
        
        public List<AreaChunk> chunks;
        
        public int regionWidth;
        public int regionHeight;
        
        public GeneralChunk()
        {
            name = "-";
            chunks = new List<AreaChunk>();

            //Init();
        }
        public GeneralChunk(Location size)
        {
            name = "-";
            chunks = new List<AreaChunk>();

            SetSize(size);
        }
        void Init()
        {
            SetSize(new Location(2, 2));
            GenerateChunks();
        }
        public void SetRegionSize(Location size)
        {
            regionWidth = (int)size.x;
            regionHeight = (int)size.y;
        }
        public void GenerateChunks()
        {
            int cID = 0;

            chunks.Clear();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    AreaChunk chunk = new AreaChunk(new Location(regionWidth, regionHeight))
                    {
                        id = cID,
                        name = StaticMethods.GetRandomMapName(4, 2),
                        position = new Location(x, y),
                    };
                    chunk.GenerateChunks();
                    chunks.Add(chunk);

                    cID += 1;
                }
            }
        }
        public AreaChunk GetChunkByPosition(Location pos)
        {
            AreaChunk result = null;
            for(int i = 0; i < chunks.Count; i++)
            {
                if(chunks[i].position.x == pos.x &&
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
