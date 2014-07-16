using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSGLE.cs {
    class Cell {
        public Point realPoint{get;set;}
        public LandType lt { get; set; }
        public Cell(Point realPoint) {
            this.realPoint = realPoint;
            lt = LandType.CLEAR;
        }
        public void DrawCurrentCell(SpriteBatch sb) {

            sb.Draw(OSGLE.GetCurrentCellTexture(), new Rectangle(realPoint.X, realPoint.Y, 32, 32), Color.White);
        }
        public void Draw(SpriteBatch sb) {
            if (lt == LandType.GRASS) {
                sb.Draw(OSGLE.GetGrassTexture(), new Rectangle(realPoint.X, realPoint.Y, 32, 32), Color.White);
        
            }
            else if (lt == LandType.WALL) {
                sb.Draw(OSGLE.GetWallTexture(), new Rectangle(realPoint.X, realPoint.Y, 32, 32), Color.White);
        
            }
            else if (lt == LandType.CLEAR) {
                sb.Draw(OSGLE.GetDefualtTexture(), new Rectangle(realPoint.X, realPoint.Y, 32, 32), Color.White);
        
            }
        }
    }
}
