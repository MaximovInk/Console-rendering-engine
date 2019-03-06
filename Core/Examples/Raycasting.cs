using System;
using static MaximovInk.ConsoleGameEngine.Core.NativeMethods;

namespace MaximovInk.ConsoleGameEngine
{
    public class Raycasting : Engine
    {
        private short w = 24;
        private short h = 24;

        private float scaleX;
        private float scaleY;

        private float lastMousePosX = 0;

        private int[,] worldMap = new int[,]
{
  {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,2,2,2,2,2,0,0,0,0,3,0,3,0,3,0,0,0,1},
  {1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,3,0,0,0,3,0,0,0,1},
  {1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,2,2,0,2,2,0,0,0,0,3,0,3,0,3,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,4,0,4,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,4,0,0,0,0,5,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,4,0,4,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,4,0,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
  {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
};

        double posX = 22, posY = 12;  //x and y start position
        double dirX = -1, dirY = 0; //initial direction vector
        double planeX = 0, planeY = 0.66; //the 2d raycaster version of camera plane

        public Raycasting(short width = 840/2, short height = 254/2, string Title = "Console engine", short fontw = 4, short fonth = 4, bool showFps = true) : base(width, height, Title, fontw, fonth, showFps)
        {
        }

        protected override void OnDraw()
        {
            Clear();
            for (int x = 0; x < w; x++)
            {
                //calculate ray position and direction
                double cameraX = 2 * x / (double)w - 1; //x-coordinate in camera space
                double rayDirX = dirX + planeX * cameraX;
                double rayDirY = dirY + planeY * cameraX;
                //which box of the map we're in
                int mapX = (int)posX;
                int mapY = (int)posY;

                //length of ray from current position to next x or y-side
                double sideDistX;
                double sideDistY;

                //length of ray from one x or y-side to next x or y-side
                double deltaDistX = Math.Abs(1 / rayDirX);
                double deltaDistY = Math.Abs(1 / rayDirY);
                double perpWallDist;

                //what direction to step in x or y-direction (either +1 or -1)
                int stepX;
                int stepY;

                int hit = 0; //was there a wall hit?
                int side = 0; //was a NS or a EW wall hit?

                if (rayDirX < 0)
                {
                    stepX = -1;
                    sideDistX = (posX - mapX) * deltaDistX;
                }
                else
                {
                    stepX = 1;
                    sideDistX = (mapX + 1.0 - posX) * deltaDistX;
                }
                if (rayDirY < 0)
                {
                    stepY = -1;
                    sideDistY = (posY - mapY) * deltaDistY;
                }
                else
                {
                    stepY = 1;
                    sideDistY = (mapY + 1.0 - posY) * deltaDistY;
                }

                while (hit == 0)
                {
                    if (sideDistX < sideDistY)
                    {
                        sideDistX += deltaDistX;
                        mapX += stepX;
                        side = 0;
                    }
                    else
                    {
                        sideDistY += deltaDistY;
                        mapY += stepY;
                        side = 1;
                    }
                    
                    if (worldMap[mapX,mapY] > 0) hit = 1;
                }

                if (side == 0) perpWallDist = (mapX - posX + (1 - stepX) / 2) / rayDirX;
                else perpWallDist = (mapY - posY + (1 - stepY) / 2) / rayDirY;

                int lineHeight = (int)(h / perpWallDist);

                int drawStart = -lineHeight / 2 + h / 2;
                if (drawStart < 0) drawStart = 0;
                int drawEnd = lineHeight / 2 + h / 2;
                if (drawEnd >= h) drawEnd = h - 1;

                COLOR color;
                Character character = Character.Full;

                switch (worldMap[mapX,mapY])
                {
                    case 1: color = COLOR.FG_RED; break;
                    case 2: color = COLOR.FG_GREEN; break;
                    case 3: color = COLOR.FG_BLUE; break;
                    case 4: color = COLOR.FG_WHITE; break; 
                    default: color = COLOR.FG_YELLOW; break;
                }

                if (side == 1) { character = Character.Medium; }

                

                DrawLine((short)x, (short)drawStart, (short)x, (short)drawEnd, (short)character, (short)color);

            }

            Apply();
        }

        protected override void OnKey(KEY_EVENT_RECORD e)
        {
            double moveSpeed = 5.0 * elapsed;

            if (e.bKeyDown == true)
            {
                if (e.UnicodeChar == 'w')
                {
                    if (posX + dirX * moveSpeed > 0 && posX + dirX * moveSpeed < worldMap.GetLength(0))
                    {
                        if (worldMap[(int)(posX + dirX * moveSpeed), (int)posY] == 0) posX += dirX * moveSpeed;
                        if (worldMap[(int)(posX), (int)(posY + dirY * moveSpeed)] == 0) posY += dirY * moveSpeed;
                    }
                }
                if (e.UnicodeChar == 's')
                {
                    if (posX - dirX * moveSpeed > 0 && posX - dirX * moveSpeed < worldMap.GetLength(0))
                    {
                        if (worldMap[(int)(posX - dirX * moveSpeed), (int)posY] == 0) posX -= dirX * moveSpeed;
                        if (worldMap[(int)(posX), (int)(posY - dirY * moveSpeed)] == 0) posY -= dirY * moveSpeed;
                    }

                }
            }
        }

        protected override void OnMouse(MOUSE_EVENT_RECORD m)
        {
            double rotSpeed = 3.0 * elapsed;
            if (m.dwMousePosition.X != lastMousePosX )
            {
                float deltaX = m.dwMousePosition.X - lastMousePosX;
                lastMousePosX = m.dwMousePosition.X;
                if (deltaX > 0)
                {
                    double oldDirX = dirX;
                    dirX = dirX * Math.Cos(-rotSpeed) - dirY * Math.Sin(-rotSpeed);
                    dirY = oldDirX * Math.Sin(-rotSpeed) + dirY * Math.Cos(-rotSpeed);
                    double oldPlaneX = planeX;
                    planeX = planeX * Math.Cos(-rotSpeed) - planeY * Math.Sin(-rotSpeed);
                    planeY = oldPlaneX * Math.Sin(-rotSpeed) + planeY * Math.Cos(-rotSpeed);
                }
                else
                {
                    double oldDirX = dirX;
                    dirX = dirX * Math.Cos(rotSpeed) - dirY * Math.Sin(rotSpeed);
                    dirY = oldDirX * Math.Sin(rotSpeed) + dirY * Math.Cos(rotSpeed);
                    double oldPlaneX = planeX;
                    planeX = planeX * Math.Cos(rotSpeed) - planeY * Math.Sin(rotSpeed);
                    planeY = oldPlaneX * Math.Sin(rotSpeed) + planeY * Math.Cos(rotSpeed);
                }
            }

        }

        protected override void OnStart()
        {
            scaleX = Width / w;
            scaleY = Height / h;

            w *= (short)scaleX;
            h *= (short)scaleY;
        }
    }
}
