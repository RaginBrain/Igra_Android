using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Igra_Android
{
	public class Jostick:Sprite
	{
		private float m_udaljenost;
		 Point centar;


		public Jostick (Texture2D t, Rectangle r,float ud)
		{
			rectangle = r;
			texture = t;
			m_udaljenost = ud;
			centar = r.Center;
		}


		public void Update(TouchCollection touchCollection)
		{
			float faktorX;
			float faktorY;
			float udaljenostX;
			float udaljenostY;
			float uk_udaljenost;
			foreach (TouchLocation tl in touchCollection) {

				if ((tl.State == TouchLocationState.Pressed)
				    || (tl.State == TouchLocationState.Moved)) 
				{
					udaljenostX = Math.Abs (centar.X - tl.Position.X);
					udaljenostY = Math.Abs (centar.Y - tl.Position.X);
					uk_udaljenost = (float)Math.Pow ((double)(udaljenostX * udaljenostX + udaljenostY * udaljenostY), (1 / 2));
					faktorX = udaljenostX / uk_udaljenost;
					faktorY = udaljenostY / uk_udaljenost;

					if (uk_udaljenost <= m_udaljenost) {
						if (tl.Position.X > centar.X)
							rectangle.X = (int)udaljenostX;
						else
							rectangle.X = (int)(-udaljenostX);

						if (tl.Position.Y > centar.Y)
							rectangle.Y = (int)udaljenostY;
						else
							rectangle.Y = (int)(-udaljenostY);
					} 
					else {
						rectangle.X = (int)(faktorX * m_udaljenost);
						rectangle.Y = (int)(faktorY * m_udaljenost);
					}
				}
			}
		}
			

	}
}

