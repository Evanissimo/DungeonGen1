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
        private Bitmap bmp;
        private int tileSize;
        private mapTile[,] dungeonFinal;

        public Form1()
        {
            InitializeComponent();
        }

        private void generate_Click(object sender, EventArgs e)
        {

            dunGen = new generator();
            
            dungeonFinal = dunGen.testMap(20,20, 3);
            dungeonFinal = dunGen.planRooms(dungeonFinal, 5);
            Bitmap finalBitmap = dunGen.Draw(dungeonFinal, 20);
            pictureBox1.Image = finalBitmap;
        }


    }
}
