using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        static int remainder = 0;

        static void Main(string[] args)
        {
            showHUD();
            takeDamage(125);
            continueButton();

            showHUD();
            takeDamage(75);
            Revive();
            continueButton();

            showHUD();
            takeDamage(95);
            Revive();
            continueButton();

            showHUD();
            takeDamage(60);
            Revive();
            continueButton();

            showHUD();
            takeDamage(184);
            Revive();
            continueButton();

            showHUD();
            takeDamage(83);
            Revive();
            continueButton();

            showHUD();
            takeDamage(128);
            Revive();
            continueButton();

            showHUD();
            Console.ReadKey();


            
        }

        static void showHUD()
        {
            cleanConsole();
            healthCheck();
            Console.WriteLine("Health: " + health + " / Health Status: " + status); 
            Console.WriteLine("Shield: " + shield);
            Console.WriteLine("Lives: " + lives);
            Console.WriteLine("");
        }

        static void continueButton()
        {
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
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

        static void takeDamage(int damage)
        {
            if (damage <= 0)
            {
                Error10("Damage");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
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

        static void Revive()
        {
            if (health == 0)
            {
                Console.WriteLine("Player Died");
                Console.WriteLine();
                shield = 100;
                health = 100;

                if (lives > 0)
                    lives = lives-1;
            }
            else if (lives == 0)
            {
                showHUD();
                continueButton();
                cleanConsole();
                shield = 100;
                health = 100;
                lives = 3;
            }
        }

        static void Error10(string error)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            
            Console.WriteLine("Error: " + error + " points cannot be negative, they must be positive.");
            Console.ResetColor();
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
    }
}
