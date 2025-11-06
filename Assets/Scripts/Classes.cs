using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Pokemon
    {
        public string name { get; set; }
        public List<TypeSlot> types { get; set; }
        public Sprites sprites { get; set; }
        public List<MoveSlot> moves { get; set; }
    }

    // Sprites
    public class Sprites
    {
        public string front_default { get; set; }
        public string back_default { get; set; }
    }

    // Tipo do Player
    public class TypeSlot
    {
        public TypeInfo type { get; set; }
    }

    // Nome do tipo do player
    public class TypeInfo
    {
        public string name { get; set; }
    }

    // Abilidades do player
    public class MoveSlot
    {
        public MoveInfo move { get; set; }
    }

    public class MoveInfo
    {
        public string name { set; get; }
        public string url { set; get; }
    }

    // Tipo e pp do primeiro movimento do player
    public class MoveDetails
    {
        public string name { get; set; }
        public MoveType type { get; set; }
        public string pp { get; set; }
    }

    public class MoveType
    {
        public string name { get; set; }
        public string url { get; set; }
    }

}
