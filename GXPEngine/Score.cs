using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Score: IEquatable<Score>, IComparable<Score>
    {
        public int score;
        public string name;

        public Score(int score, string name)
        {
            this.name = name;
            this.score = score;
        }

        public string writeScore()
        {
            return score + " -  " + name;
        }
        public string saveScore()
        {
            return score + "|" + name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Score objAsPart = obj as Score;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public int SortByNameAscending(string name1, string name2)
        {

            return name1.CompareTo(name2);
        }

        // Default comparer for Part type.
        public int CompareTo(Score comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart == null)
                return 1;

            else
                return this.score.CompareTo(comparePart.score);
        }
        public override int GetHashCode()
        {
            return score;
        }
        public bool Equals(Score other)
        {
            if (other == null) return false;
            return (this.score.Equals(other.score));
        }
    }
}
