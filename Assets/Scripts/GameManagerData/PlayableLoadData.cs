using System.Collections.Generic;
using GameManagerData.data;

namespace GameManagerData
{
    public class PlayableLoadData
    {
        //Klase satur ielādētos datus par spēlējamajiem objektiem
        public List<PlayableData> Playables = new List<PlayableData>();

        public void AddPlayableData(PlayableData data)
        {
            Playables.Add(data);
        }
    }
}