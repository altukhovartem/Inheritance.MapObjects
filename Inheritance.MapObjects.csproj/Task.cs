using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.MapObjects
{
    interface IOwner
    {
        int Owner { get; set; }
    }

    interface IArmy
    {
        Army Army { get; set; }
    }

    interface ITreasure
    {
        Treasure Treasure { get; set; }
    }

    public class Dwelling: IOwner
    {
        public int Owner { get; set; }
    }

    public class Mine: IOwner, IArmy, ITreasure
    {
        public int Owner { get; set; }
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }
    }

    public class Creeps: IArmy, ITreasure
    {
        public Army Army { get; set; }
        public Treasure Treasure { get; set; }
    }

    public class Wolfs: IArmy
    {
        public Army Army { get; set; }
    }

    public class ResourcePile: ITreasure
    {
        public Treasure Treasure { get; set; }
    }

    public static class Interaction
    {
        public static void Make(Player player, object mapObject)
        {
            if (mapObject is IOwner && !(mapObject is IArmy)&& !(mapObject is ITreasure))
            {
                ((IOwner)mapObject).Owner = player.Id;
                return;
            }
            if (mapObject is IArmy && mapObject is ITreasure && mapObject is IOwner)
            {
                if (player.CanBeat(((IArmy)mapObject).Army))
                {
                    ((IOwner)mapObject).Owner = player.Id;
                    player.Consume(((ITreasure)mapObject).Treasure);
                }
                else player.Die();
                return;
            }
            if (mapObject is IArmy && mapObject is ITreasure && !(mapObject is IOwner))
            {
                if (player.CanBeat(((IArmy)mapObject).Army))
                    player.Consume(((ITreasure)mapObject).Treasure);
                else
                    player.Die();
                return;
            }
            if (mapObject is ITreasure && !(mapObject is IOwner) && !(mapObject is IArmy))
            {
                player.Consume(((ITreasure)mapObject).Treasure);
                return;
            }
            if (mapObject is IArmy && !(mapObject is ITreasure) && !(mapObject is IOwner))
            {
                if (!player.CanBeat(((IArmy)mapObject).Army))
                    player.Die();
            }
        }
    }
}
