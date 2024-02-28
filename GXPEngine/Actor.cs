using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Actor : GameObject
    {
        public Sprite sprite;

        public void initializeSprite()
        {
            sprite.SetOrigin(sprite.width, sprite.height);
            sprite.SetXY(200,200);
        }
        public virtual void Update()
        {

        }
    }
}
