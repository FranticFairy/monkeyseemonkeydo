using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class WaterfallObject : GameObject
    {
        public bool obstacle = false;
        public bool fromBelow = false;
        public int vine;
        public int height;

        public WaterfallObject(bool obstacle, bool fromBelow, int vine, int height) 
        { 
            this.obstacle = obstacle; this.fromBelow = fromBelow; this.vine = vine; this.height = height;
        }

        public string getType()
        {
            if(obstacle &&  fromBelow) { return "D"; }
            else if(obstacle && !fromBelow) { return "O"; }
            else { return "F"; }
        }
    }
}
