using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkiaSharp;
using System.IO;

namespace DungeonGen1
{
    public partial class Form1 : Form
    {
        private generator dunGen; 
        public Form1()
        {
            InitializeComponent();
        }

        private void generate_Click(object sender, EventArgs e)
        {
            dunGen = new generator();

        }

        public void drawDungeon(bool[,] dungeon, int squareSize)
        {
            squareSize = 20;
            int originX = 0;
            int originY = 0;
            SKImageInfo todd = new SKImageInfo(100, 100);
            SKSurface surface = SKSurface.Create(todd);
            SKCanvas canvas = surface.Canvas;
            SKPaint paint = new SKPaint { Color = SKColors.Black, StrokeWidth = 1, Style = SKPaintStyle.Stroke, TextSize = 16 };
            for (int i = 0; i < dungeon.GetLength(0); i++)
            {
                for (int k = 0; k < dungeon.GetLength(1); k++)
                {
                    if (dungeon[i, k] == true)
                    {
                        canvas.DrawRect(originX, originY, squareSize, squareSize, paint);
                        // text uses the origin at bottom left, so we must shift it down a square to make it into the square we want
                        canvas.DrawText("A", originX, originY + squareSize, paint);
                    }
                    originX += squareSize;
                }
                originY += squareSize;
                originX = 0;
            }
            SKImage image = surface.Snapshot();
            SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
            MemoryStream m = new MemoryStream(data.ToArray());
            Bitmap bem = new Bitmap(m, false);
            
        }
    }
}
