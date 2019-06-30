using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentormateJSONAssignment
{
    class NBAPlayers
    {
        private string name;
        private int playingSince;
        private string position;
        private int rating;

        public string Name { get => name; set => name = value; }
        public int PlayingSince { get => playingSince; set => playingSince = value; }
        public string Position { get => position; set => position = value; }
        public int Rating { get => rating; set => rating = value; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", name, playingSince, position, rating);
        }

    }
}
