using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
        static int xp = 0;
        static int level = 1;

        static void Main(string[] args)
        {
            UnitTestHealthSystem();
            UnitTestXPSystem();

            level = 1;
            xp = 0;

            lives = 3; // resets lives due to healthsystem check setting them to 1

            ShowHUD();
            TakeDamage(10);

            ShowHUD();
            TakeDamage(125);

            ShowHUD();
            TakeDamage(75);
            
            ShowHUD();
            Revive();

            ShowHUD();
            Heal(25);

            ShowHUD();
            RegenerateShield(25);

            ShowHUD();
            TakeDamage(90);
            Revive();

            ShowHUD();
            TakeDamage(68);
            Revive();

            ShowHUD();
            TakeDamage(134);

            ShowHUD();
            Revive();

            ShowHUD();

            ShowHUD();
            Heal(-25);

            ShowHUD();
            TakeDamage(-25);

            ShowHUD();
            RegenerateShield(-25);

            ShowHUD();
            IncreaseXP(50);

            ShowHUD();
            IncreaseXP(50);

            ShowHUD();
            IncreaseXP(100);

            ShowHUD();
            IncreaseXP(149);

            ShowHUD();
            IncreaseXP(60);

            ShowHUD();
            Console.ReadKey();

        }

        static void ShowHUD()
        {
            HealthCheck();
            Console.WriteLine("Health: " + health + " / Health Status: " + status); 
            Console.WriteLine("Shield: " + shield);
            Console.WriteLine("Lives: " + lives);
            Console.WriteLine("Experience Points: " + xp);
            Console.WriteLine("Level: " + level);
            Console.WriteLine("");
        }

        static void Heal(int hp)
        {
            if (hp <= 0)
            {
                Error10("Healing");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Player has healed " + hp + " points");
                Console.WriteLine();
                health = health + hp;

                if (health >= 100)
                {
                    Console.WriteLine("Player is at full strength and cannot heal anymore");
                    Console.WriteLine();
                    health = 100;
                }
                Console.ResetColor();   
            }
        }

        static void RegenerateShield(int hp)
        {    
            if (hp <= 0)
            {
                Error10("Shield");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Shield has regenerated " + hp + " points");
                Console.WriteLine();
                shield = hp + shield;

                if (shield >= 100)
                {
                    Console.WriteLine("Shield is at full strength and cannot regenerate anymore");
                    Console.WriteLine();
                    shield = 100;
                }
                Console.ResetColor();
            }
        }

        static void TakeDamage(int damage)
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

                if (health < 0)
                {
                    health = 0;
                }
            }
        }

        static void Revive()
        {
            if (health == 0)
            {
                Console.ForegroundColor= ConsoleColor.Red;    
                Console.WriteLine("Player Died");
                Console.ResetColor();
                Console.WriteLine();

                health = 100;
                shield = 100;
                lives = lives - 1;

                if (lives <= 0)
                {
                    Console.ForegroundColor= ConsoleColor.Green;
                    Console.WriteLine("Player Reborn");
                    Console.ResetColor();   
                    lives = 3;
                    health = 100;
                    shield = 100;
                }
            }
        }

        static void Error10(string error)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            
            Console.WriteLine("Error: " + error + " points cannot be negative, they must be positive.");
            Console.ResetColor();
            Console.WriteLine();
        }

        static void HealthCheck()
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

        static void IncreaseXP(int amount)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            xp = xp + amount;
            Console.WriteLine("Player has gained " + amount + " xp");

            if (xp >= 100 && level == 1)
            {
                level++;
                xp = xp - 100;
                Console.WriteLine("Player has gained a level");
            }
            else if (xp >= 200 && level == 2)
            {
                level++;
                xp = xp - 200;
                Console.WriteLine("Player has gained a level");
            }
            else if (xp >= 300 && level == 3)
            {
                level++;
                xp = xp - 300;
                Console.WriteLine("Player has gained a level");
            }
            else if (xp >= 400 && level == 4)
            {
                level++;
                xp = xp - 400;
                Console.WriteLine("Player has gained a level");
            }
            else if (xp >= 500 && level == 5)
            {
                level++;
                xp = xp - 500;
                Console.WriteLine("Player has gained a level");
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        static void UnitTestHealthSystem()
        {
            Debug.WriteLine("Unit testing Health System started...");

            // TakeDamage()

            // TakeDamage() - only shield
            shield = 100;
            health = 100;
            lives = 3;
            TakeDamage(10);
            Debug.Assert(shield == 90);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // TakeDamage() - shield and health
            shield = 10;
            health = 100;
            lives = 3;
            TakeDamage(50);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 60);
            Debug.Assert(lives == 3);

            // TakeDamage() - only health
            shield = 0;
            health = 50;
            lives = 3;
            TakeDamage(10);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 40);
            Debug.Assert(lives == 3);

            // TakeDamage() - health and lives
            shield = 0;
            health = 10;
            lives = 3;
            TakeDamage(25);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 0);
            Debug.Assert(lives == 3);

            // TakeDamage() - shield, health, and lives
            shield = 5;
            health = 100;
            lives = 3;
            TakeDamage(110);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 0);
            Debug.Assert(lives == 3);

            // TakeDamage() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            TakeDamage(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // Heal()

            // Heal() - normal
            shield = 0;
            health = 90;
            lives = 3;
            Heal(5);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 95);
            Debug.Assert(lives == 3);

            // Heal() - already max health
            shield = 90;
            health = 100;
            lives = 3;
            Heal(5);
            Debug.Assert(shield == 90);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // Heal() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            Heal(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // RegenerateShield()

            // RegenerateShield() - normal
            shield = 50;
            health = 100;
            lives = 3;
            RegenerateShield(10);
            Debug.Assert(shield == 60);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // RegenerateShield() - already max shield
            shield = 100;
            health = 100;
            lives = 3;
            RegenerateShield(10);
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // RegenerateShield() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            RegenerateShield(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // Revive()

            // Revive()
            shield = 0;
            health = 0;
            lives = 2;
            Revive();
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 1);

            Debug.WriteLine("Unit testing Health System completed.");
            Console.Clear();
        }

        static void UnitTestXPSystem()
        {
            Debug.WriteLine("Unit testing XP / Level Up System started...");

            // IncreaseXP()

            // IncreaseXP() - no level up; remain at level 1
            xp = 0;
            level = 1;
            IncreaseXP(10);
            Debug.Assert(xp == 10);
            Debug.Assert(level == 1);

            // IncreaseXP() - level up to level 2 (costs 100 xp)
            xp = 0;
            level = 1;
            IncreaseXP(105);
            Debug.Assert(xp == 5);
            Debug.Assert(level == 2);

            // IncreaseXP() - level up to level 3 (costs 200 xp)
            xp = 0;
            level = 2;
            IncreaseXP(210);
            Debug.Assert(xp == 10);
            Debug.Assert(level == 3);

            // IncreaseXP() - level up to level 4 (costs 300 xp)
            xp = 0;
            level = 3;
            IncreaseXP(315);
            Debug.Assert(xp == 15);
            Debug.Assert(level == 4);

            // IncreaseXP() - level up to level 5 (costs 400 xp)
            xp = 0;
            level = 4;
            IncreaseXP(499);
            Debug.Assert(xp == 99);
            Debug.Assert(level == 5);

            Debug.WriteLine("Unit testing XP / Level Up System completed.");
            Console.Clear();
        }
    }
}
