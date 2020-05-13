using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using System.IO;
using System.Drawing;

namespace DungeonGen1
{
    class generator
    {
        Random lolXD = new Random();
        public mapTile[,] mapDungeon()
        {
            int width = lolXD.Next(5, 50);
            int height = lolXD.Next(5, 50);
            mapTile[,] toReturn = new mapTile[width, height];
            for (int x = 0; x < toReturn.GetLength(0); x++)
                for (int y = 0; y < toReturn.GetLength(1); y++)
                {
                    toReturn[x, y] = new mapTile();
                    toReturn[x, y].exists = true;
                    toReturn[x, y].contents = ' ';
                }
            return toReturn;
        }


        /// <summary>
        ///  This method creates a bitmap from a 2D array of maptiles
        /// </summary>
        /// <param name="dungeon">the dungeon you wish to draw</param>
        /// <param name="squareSize">the size of the square in pixels</param>
        /// <returns>A Bitmap</returns>
        public Bitmap Draw(mapTile[,] dungeon, int squareSize)
        {
            int originX = 5;
            int originY = 5;

            SKImageInfo todd = new SKImageInfo(dungeon.GetLength(1)*squareSize+50, dungeon.GetLength(0)*squareSize+50);
            SKSurface surface = SKSurface.Create(todd);
            SKCanvas canvas = surface.Canvas;
            SKPaint paint = new SKPaint { Color = SKColors.Black, StrokeWidth = 1, Style = SKPaintStyle.Stroke, TextSize = 16 };
            for (int i = 0; i < dungeon.GetLength(0); i++)
            {
                for (int k = 0; k < dungeon.GetLength(1); k++)
                {
                    if (dungeon[i, k].exists == true)
                    {
                        canvas.DrawRect(originX, originY, squareSize, squareSize, paint);
                        // text uses the origin at bottom left, so we must shift it down a square to make it into the square we want

                        canvas.DrawText(dungeon[i, k].contents.ToString(), originX, originY + squareSize, paint);
                    }
                    originX += squareSize;
                }
                originY += squareSize;
                originX = 5;
            }
            SKImage image = surface.Snapshot();
            SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
            MemoryStream m = new MemoryStream(data.ToArray());
            Bitmap bem = new Bitmap(m, false);

            return bem;

        }
    }
}
