using System.Collections.Generic;
using GameManagerData.data;

namespace GameManagerData
{
    public class FurnitureLoadData
    {
        public List<FurnitureData> Furniture = new List<FurnitureData>();

        public void AddFurnitureData(FurnitureData data)
        {
            Furniture.Add(data);
        }
    }
}