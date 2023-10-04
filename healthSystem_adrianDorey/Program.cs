using System;
using System.Collections.Generic;
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



        static void Main(string[] args)
        {
            showHUD();

            takeDamage(-20);
           
            Console.WriteLine("Continue");
            Console.ReadKey();
        }

        static void showHUD()
        {
            healthCheck();
            Console.WriteLine("Health: " + health + " / Health Status: " + status); 
            Console.WriteLine("Shield: " + shield);
            Console.WriteLine("Lives: " + lives);
            Console.WriteLine("");
        }

        // modifies health / based on parameter input(input describes how much player is healed)
        // range checking / error checking
        static void heal(int hp)
        {
            Console.WriteLine();

            health = health + hp;

            if (health >= 100)
            {
                health = 100;
            }
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


        // modifies shield /based on parameter input(input describes how much shield should regenerate)
        // range checking / error checking
        static void regenerateShield(int hp)
        {

        }

        // modifies health, shield & lives
        static void takeDamage(int damage)
        {
            if (damage <= 0)
            {
                Console.WriteLine("Error: Player cannot take negative value of damage. Must be positive value.");
                return;
            }
            else if(shield < damage)
            {

            }
        }

    }
}
