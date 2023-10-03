using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healthSystem_adrianDorey
{
    internal class Program
    {
        // declarations
        static int health = 100;
        static int lives = 3;



        static void Main(string[] args)
        {


        }

        static void showHUD()
        {
            Console.WriteLine("Health: " + " / Health Status: ");
            healthCheck();
            Console.WriteLine("Lives: ");
            Console.WriteLine("Shield: ");
            Console.WriteLine("");
        }

        static void heal(int hp)
        {

        }

        static void healthCheck()
        {
            Console.WriteLine();
        }



        static void regenerateShield(int hp)
        {

        }

        // modifies health, shield & lives
        static void takeDamage(int damage)
        {

        }

    }
}
