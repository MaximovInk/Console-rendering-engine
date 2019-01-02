using System;

namespace MaximovInk.ConsoleGameEngine
{
    public class Program
    {
        [STAThread]
        private static void Main()
        {
            bool select = false;

            char Char;
            Engine e = new Engine();

            while (select == false)
            {
                Console.WriteLine("Hello, this is examples.. Write 1 - raycasting ,2 - image loading,3 - primitives to play");

                Char = Console.ReadLine()[0];

                switch (Char)
                {
                    case '1':
                        e = new Raycasting();
                        select = true;
                        break;
                    case '2':
                        e = new ImageConverting();
                        select = true;
                        break;
                    case '3':
                        e = new Primitives();
                        select = true;
                        break;
                    default:
                        break;
                }
            }
            
            e.StartLoop();
        }

    }
}
