using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace healthSystem_adrianDorey
{
    internal class Program
    {
        // declarations
        static int health = 100;
        static string status;
        static int lives = 3;
        static int shield = 100;
        static int remainder;

        static void Main(string[] args)
        {
            showHUD();

            takeDamage(10);

            showHUD();           

            Console.WriteLine("Continue");
            Console.ReadKey();
        }

        static void showHUD()
        {
            //cleanConsole();
            healthCheck();
            Console.WriteLine("Health: " + health + " / Health Status: " + status); 
            Console.WriteLine("Shield: " + shield);
            Console.WriteLine("Lives: " + lives);
            Console.WriteLine("");
        }

        static void cleanConsole()
        {
            Console.Clear();
        }
        
        static void heal(int hp)
        {
            Console.WriteLine();

            if (health >= 100)
            {
                health = 100;
            }
            else if (hp <= 0)
            {
                Error10("Healing");
            }
            else
                health = health + hp;
        }

        static void healthCheck()
        {
            if (health <= 0)
            {
                status = "Player Died";
                health = 0;
                lives = lives - 1;
            }
            else if ((health > 0) && (health <= 15))
            {
                status = "Imminent Danger";
            }
            else if ((health > 15) && (health <= 50))
            {
                status = "Badly Hurt";
            }
            else if ((health > 50 && health <= 75))
            {
                status = "Hurt";
            }
            else if ((health > 75 && health < 100))
            {
                status = "Healthy";
            }
            else if (health == 100)
            {
                status = "Perfectly Healthy";
            }

            else if (lives <= 0)
            {
                Console.WriteLine("Game Over");
                lives = 0;
            }

        }

        static void regenerateShield(int hp)
        {
            if (shield >= 100)
            {
                shield = 100;
            }
            if (hp <= 0)
            {
                Error10("shield");
            }

        }

        static void Error10(string error)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (error == "Shield" ||  error == "Healing")
            {
                Console.WriteLine("Error: " + error + " points cannot be negative");
            }
            else
                Console.WriteLine("Error: " + error + " points cannot be positive");
        }

        static void takeDamage(int damage)
        {

            if (damage <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Error10("Damage");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("Player has taken " + damage + " damage");
                Console.WriteLine();
                Console.ResetColor();
                if (shield >= damage)
                {
                    shield -= damage;
                }
                else
                {
                    remainder = damage - shield;
                    shield = 0;
                    health -= remainder;
                }
               

            }
            
        }

    }
}
