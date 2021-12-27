using System.Collections.Generic;
using GameManagerData.data;

namespace GameManagerData
{
    public class PlayableLoadData
    {
        public List<PlayableData> Playables = new List<PlayableData>();

        public void AddFurnitureData(PlayableData data)
        {
            Playables.Add(data);
        }
    }
}