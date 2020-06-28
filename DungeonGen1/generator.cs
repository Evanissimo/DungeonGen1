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
        List<Point> listOfEmptyCoords;
        Random lolXD = new Random();
        int bufferValue;
        List<dungeonRoom> unconnectedRooms;
        List<magicItems> allItems;
        List<magicItems> selectedItems;
        public generator()
        {
            unconnectedRooms = new List<dungeonRoom>();
            allItems = new List<magicItems>();
            selectedItems = new List<magicItems>();
        }
        public mapTile[,] testMap(int width, int height, int buffer)
        {
            //int width = lolXD.Next(5, 50);
            //int height = lolXD.Next(5, 50);
            bufferValue = buffer;
            listOfEmptyCoords = new List<Point>();
            mapTile[,] toReturn = new mapTile[width, height];
             
            for (int x = 0; x < toReturn.GetLength(0); x++)
            {
                for (int y = 0; y < toReturn.GetLength(1); y++)
                {
                    toReturn[x, y] = new mapTile();
                    toReturn[x, y].exists = 0;
                    toReturn[x, y].contents = ' ';
                    Point tooAdd = new Point(x, y);
                    // if statement right here test for buffer
                    if (x < width- 3 && y < height - 3 && x > 1 && y > 1) 
                    {
                        listOfEmptyCoords.Add(tooAdd);
                    }
                }
            }
            // toReturn[1, 9].contents = '7';
            // remove edges from the list of empty coords
            for(int x = toReturn.GetLength(0)- bufferValue; x<toReturn.GetLength(0); x++ )
            {
                for(int y = toReturn.GetLength(1) - bufferValue; y < toReturn.GetLength(1); y++)
                {
                    listOfEmptyCoords.Remove(new Point(x, y));
                }
            }
            // add edge indicator to bottom of grid
            for (int x = 0; x < toReturn.GetLength(0); x++)
                toReturn[x, toReturn.GetLength(1) - 1].exists = 2;
            for (int y = 0; y < toReturn.GetLength(1); y++)
                toReturn[toReturn.GetLength(0) - 1, y ].exists = 2;
            return toReturn;
            
        }

        public mapTile[,] planRooms(mapTile[,] map, int numberOfRooms)
        {
            
            // call roomCreator in aloop equal to numrooms.
            for(int i = 0; i<numberOfRooms; i++)
            {
                dungeonRoom tempRoom = new dungeonRoom();
                tempRoom = candidateCreator(map);
                // if temp room returns null or exception or whatever, then we need to either move to next room or stop creating rooms.
                // we may also need to only try to place smaller rooms.
                if (tempRoom.height != 0)
                {
                    map = placeARectangleRoom(map, tempRoom);
                }
            }
            // after room is validated, call placeARectangle room in same loop, passing validated room as argument
            return map;
        }
        /// <summary>
        /// This method creates a candidate room and calls the validator on it
        /// </summary>
        /// <param name="currMap"></param>
        /// <returns></returns>
        private dungeonRoom candidateCreator(mapTile [,] currMap)
        {
            // this allows us to understand the buffer values.
            int totalXLength = currMap.GetLength(0);
            int totalYLength = currMap.GetLength(1);
            // pick random origin point from list of origin points.
            Shuffle(listOfEmptyCoords);
            Point start =listOfEmptyCoords[0];
            // pick random width and height that does not push room through edge
            int widthRoom = lolXD.Next(bufferValue, 10);
            int heightRoom = lolXD.Next(bufferValue, 10);

           
            dungeonRoom toTry = new dungeonRoom(start.X, start.Y, widthRoom, heightRoom);

            dungeonRoom result = validator(toTry, currMap, 0);

            return result;
        }
        //gross clipping
        // 4 rooms is a's left edge to the left of b's right edge, if it is, is a's 
        private dungeonRoom validator(dungeonRoom toTry, mapTile [,] currMap, int levelsDeep)
        {
            if(toTry.upperleftY+ toTry.height>= currMap.GetLength(1) || toTry.upperleftX + toTry.width >= currMap.GetLength(0))
            {
                levelsDeep++;
                if (levelsDeep < listOfEmptyCoords.Count)
                {
                    toTry.upperleftX = listOfEmptyCoords[levelsDeep].X;
                    toTry.upperleftY = listOfEmptyCoords[levelsDeep].Y;
                    toTry = validator(toTry, currMap, levelsDeep);
                }
                else
                {
                    toTry.height = 0;
                    toTry.width = 0;
                    return toTry;
                }
            }


            if (unconnectedRooms.Any())
            {
                foreach (dungeonRoom d in unconnectedRooms)
                {
                    if(doTheyOverlap(toTry, d))
                    {
                        levelsDeep++;
                        if (levelsDeep < listOfEmptyCoords.Count)
                        {
                            toTry.upperleftX = listOfEmptyCoords[levelsDeep].X;
                            toTry.upperleftY = listOfEmptyCoords[levelsDeep].Y;
                            toTry = validator(toTry, currMap, levelsDeep);
                            return toTry;
                        }
                        else
                        {
                            toTry.height = 0;
                            toTry.width = 0;
                            return toTry;
                        }
                    }
                }
              
            }

            return toTry;
        }
        /// <summary>
        /// This method assumes the rectangle room has been determined to be a valid room not breaking into other rooms.
        /// </summary>
        /// <param name="currMap">The map as it is before drawing this room onto it</param>
        /// <param name="room">The dungeonRoom object to draw</param>
        /// <returns></returns>
        private mapTile[ , ]placeARectangleRoom(mapTile[,] currMap, dungeonRoom room)
        {
            // this point will be removed from the list of empty coordinates
            Point toRemove = new Point(0,0);
            // first runthrough makes adjacencies
            for(int y = room.upperleftY-1; y <= room.upperleftY + room.height + 1; y++)
            {
                for(int x = room.upperleftX-1; x <= room.upperleftX + room.width + 1; x++ )
                {
                    if (isInRange(x, y, currMap))
                    {
                        currMap[x, y].exists = 2;
                        toRemove.X = x;
                        toRemove.Y = y;
                        // all of the mapTiles in the room are now invalid room origin points
                        listOfEmptyCoords.Remove(toRemove);
                    }
                }
            }
            // second runthrough fills in inner part
            for (int y = room.upperleftY; y <= room.upperleftY + room.height; y++)
            {
                for (int x = room.upperleftX; x <= room.upperleftX + room.width; x++)
                {
                    if (isInRange(x,y,currMap))
                    {
                        currMap[x, y].exists = 1;
                    }
                }
            }
            unconnectedRooms.Add(room);
            return currMap;
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

            SKImageInfo todd = new SKImageInfo(dungeon.GetLength(0)*squareSize+50, dungeon.GetLength(1)*squareSize+50);
            SKSurface surface = SKSurface.Create(todd);
            SKCanvas canvas = surface.Canvas;
            SKPaint paint = new SKPaint { Color = SKColors.Black, StrokeWidth = 1, Style = SKPaintStyle.Stroke, TextSize = 16 };
            for (int i = 0; i < dungeon.GetLength(1); i++)
            {
                for (int k = 0; k < dungeon.GetLength(0); k++)
                {
                    if (dungeon[k, i].exists == 1)
                    {
                        canvas.DrawRect(originX, originY, squareSize, squareSize, paint);
                        // text uses the origin at bottom left, so we must shift it down a square to make it into the square we want

                        canvas.DrawText(dungeon[k, i].contents.ToString(), originX, originY + squareSize, paint);
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
        public void Shuffle<Point>( List<Point> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = lolXD.Next(n + 1);
                Point value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private bool isInRange(int x, int y, mapTile[,] map)
        {
            if (x < map.GetLength(0) && x > 0 && y < map.GetLength(1) && y > 0)
            {
                return true;
            }
            else return false;
        }

        public void MagicItemReader()
        {
            string[] lines = File.ReadAllLines("magicItems.txt");
            foreach(string s in lines)
            {
                magicItems toAdd = new magicItems(s);
                allItems.Add(toAdd);
            }
        }
        public void MagicItemSelector()
        {
            
        }
        private mapTile [,] roomConnector(dungeonRoom connectedRoom, dungeonRoom unconnectedRoom)
        {
            return null;
        }
        private bool doTheyOverlap(dungeonRoom candidateRoom, dungeonRoom existingRoom)
        {
            
            int CandidateLeftX = candidateRoom.upperleftX;
            int CandidateRightX = candidateRoom.upperleftX + candidateRoom.width;
            int CandidateTopY = candidateRoom.upperleftY;
            int CandidateBottomY = candidateRoom.upperleftY + candidateRoom.height;
            int existingLeftX = existingRoom.upperleftX;
            int existingRightX = existingRoom.upperleftX + existingRoom.width;
            int existingTopY = existingRoom.upperleftY;
            int existingBottomY = existingRoom.upperleftY + existingRoom.height;
            // a left side of one room is to the right of the right side of another.
            if (CandidateLeftX > 1+ existingRightX || existingLeftX> 1+ CandidateRightX )
            {
                return false;
            }
            // a bottom side of a is above a top side of b
            if(CandidateBottomY + 1 < existingTopY || existingBottomY + 1< CandidateTopY)
            {
                return false;
            }
            return true;
        }
    }
}
