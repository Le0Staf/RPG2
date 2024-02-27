using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Items
{
    class Item
    {
        public string lootType;
        public string lootName;
        public int lootHP;
        public int lootDMG;
        public bool equiped;

        public Item(string LootType, string LootName, int LootHP, int LootDMG, bool Equiped)
        {
            lootType = LootType;
            lootName = LootName;
            lootHP = LootHP;
            lootDMG = LootDMG;
            equiped = Equiped;
        }
    }
}
