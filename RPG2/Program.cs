using System;
using Enemies;
using Items;
using System.Security.Cryptography.X509Certificates;

namespace RPG2
{
    class Program
    {
        static void Main()
        {
            //Positions
            var x = 0;
            var y = 0;
            var i = 0;
            string movementInput = "w";

            List<string> beenIn = new List<string>();
            List<int> beenInInt = new List<int>();

            //Player
            var playerHP = 100;
            var playerDMG = 20;

            var currentPlayerHP = playerHP;
            var playerAttackDMG = 0;

            //Enemies
            Enemy slime = new("slime", 50, 10);
            Enemy zombie = new("zombie", 100, 15);

            //Items
            Item woodenSword = new("weapon", "wooden sword", 0, 10, false);
            Item ironHelmet = new("helmet", "iron helmet", 50, 0, false);

            var currentEnemyHP = 0;
            var currentEnemyDMG = 0;
            var currentEnemyAttackDMG = 0;

            List<Enemy> EnemyList = new List<Enemy>
            {
                slime,
                zombie
            };

            List<Item> ItemList = new List<Item>
            {
                woodenSword,
                ironHelmet
            };

            void Move()
            {
                Console.Clear();
                var y_pos = y * 5;
                var d = 0;

                for (i = -12; i < 13; i++)
                {
                    d++;
                    if (i != y_pos + x)
                    {
                        if (beenInInt.Contains(i) == true)
                        {
                            Console.Write(" e");
                        }
                        else
                        {
                            Console.Write(" x");
                        }
                    }
                    else if (i == y_pos + x)
                    {
                        Console.Write(" o");
                    }

                    if (d == 5)
                    {
                        Console.WriteLine("");
                        d = 0;
                    }
                }

                UpdateBeenIn();

                Console.WriteLine("   W");
                Console.WriteLine("A  S  D");
                Console.WriteLine("Enter Direction");
                movementInput = Console.ReadLine();

                if (movementInput == "d")
                {
                    Console.Clear();
                    if (x >= -2 && x != 2)
                    {
                        x++;
                    }

                    Move();
                }
                if (movementInput == "a")
                {
                    Console.Clear();
                    if (x >= -1 && x != -2)
                    {
                        x--;
                    }
                    Console.WriteLine(x);

                    Move();
                }
                if (movementInput == "s")
                {
                    Console.Clear();
                    if (y >= -2 && y != 2)
                    {
                      y++;
                    }

                    Move();
                }
                if (movementInput == "w")
                {
                    Console.Clear();
                    if (y <= 2 && y != -2)
                    {
                      y--;
                    }

                    Move();
                }
                else
                {
                    Console.Clear();
                    Move();
                }

                void UpdateBeenIn()
                {
                    Console.WriteLine("Your coordinates are: " + x + "," + y);
                    Console.WriteLine("x = undiscovered  e = discovered  o = player");

                    if (beenIn.Contains(x.ToString() + y.ToString()) != false)
                    {
                        Console.WriteLine("You've already been here");
                    }
                    else if (x + y != 0)
                    {
                        if (new Random().Next(0, 2) == 0)
                        {
                            Encounter();
                        }
                        else
                        {
                            Treasure();
                        }
                    }
                    beenIn.Add(x.ToString() + y.ToString());
                    beenInInt.Add(x + (y * 5));
                }

                string encounterChoice;
                string treasureChoice;
                int enemy;
                int loot;

                void Treasure()
                {
                    loot = new Random().Next(0, 2);
                    Console.Clear();
                    Console.WriteLine("You found a " + ItemList[loot].lootName);
                    Console.WriteLine("Stats: " + ItemList[loot].lootHP + "HP " + ItemList[loot].lootDMG + "DMG");
                    Console.WriteLine("1. Equip");
                    Console.WriteLine("2. Leave");
                    treasureChoice = Console.ReadLine();

                    for (i = 0; i != 1;)
                    {
                        if (treasureChoice == "1")
                        {
                            Console.Clear();
                            if (ItemList[loot].equiped == false)
                            {
                                Console.WriteLine("You equiped a " + ItemList[loot].lootName);
                                playerDMG = playerDMG + ItemList[loot].lootDMG;
                                playerHP = playerHP + ItemList[loot].lootHP;
                                Thread.Sleep(2000);
                                ItemList[loot].equiped = true;
                                i++;
                            }
                            else
                            {
                                Console.WriteLine("You've already had a " + ItemList[loot].lootName + " Equiped");
                                Thread.Sleep(1000);
                                i++;
                            }

                        }
                        else if (treasureChoice == "2")
                        {
                            Console.Clear();
                            Console.WriteLine("You left");
                            Thread.Sleep(1000);
                            i++;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Not a choice");
                        }
                    }
                    beenIn.Add(x.ToString() + y.ToString());
                    beenInInt.Add(x + (y * 10));
                    Move();

                }

                void Encounter()
                {
                    currentPlayerHP = playerHP;
                    enemy = new Random().Next(0, 2);

                    currentEnemyHP = EnemyList[enemy].enemyHP;
                    currentEnemyDMG = EnemyList[enemy].enemyDMG;

                    Fight();

                    void Fight()
                    {
                        Console.Clear();
                        Console.WriteLine("You've encountered a " + EnemyList[enemy].enemyName);
                        Console.WriteLine("1. Fight");
                        Console.WriteLine("2. Surrender");

                        encounterChoice = Console.ReadLine();

                        if (encounterChoice == "1")
                        {
                            Console.Clear();

                            while (currentPlayerHP > 0 && currentEnemyHP > 0)
                            {
                                Console.WriteLine("Player: " + currentPlayerHP + "                   " + EnemyList[enemy].enemyName + ": " + currentEnemyHP);
                                Console.WriteLine("1. Attack");
                                Console.WriteLine("2. Don't Attack");

                                encounterChoice = Console.ReadLine();

                                if (encounterChoice == "1")
                                {
                                    playerAttackDMG = new Random().Next((playerDMG - 5), (playerDMG + 5));
                                    currentEnemyHP = currentEnemyHP - playerAttackDMG;
                                    Console.WriteLine("You did " + playerAttackDMG + " DMG");
                                    Thread.Sleep(1000);

                                    if (currentEnemyHP > 0)
                                    {
                                        currentEnemyAttackDMG = new Random().Next((currentEnemyDMG - 5), (playerDMG + 5));
                                        currentPlayerHP = currentPlayerHP - currentEnemyAttackDMG;
                                        Console.WriteLine(EnemyList[enemy].enemyName + " did " + currentEnemyAttackDMG + " DMG");
                                        Thread.Sleep(1000);
                                    }
                                    else
                                    {
                                        Console.WriteLine("You WIN");
                                        currentPlayerHP = playerHP;
                                        Thread.Sleep(1000);
                                    }
                                }
                                else if (encounterChoice == "2")
                                {
                                    Console.WriteLine("You did 0 DMG");
                                    Thread.Sleep(1000);

                                    if (currentEnemyHP > 0)
                                    {
                                        currentEnemyAttackDMG = new Random().Next((currentEnemyDMG - 5), (playerDMG + 5));
                                        currentPlayerHP = currentPlayerHP - currentEnemyAttackDMG;
                                        Console.WriteLine(EnemyList[enemy].enemyName + " did " + currentEnemyAttackDMG + " DMG");
                                        Thread.Sleep(1000);
                                        if (currentPlayerHP <= 0)
                                        {
                                            Console.WriteLine("You Died");

                                            Thread.Sleep(1000);
                                            Console.Clear();
                                            Console.WriteLine("Game Over");
                                            Environment.Exit(0);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("You WIN");
                                        Thread.Sleep(1000);
                                    }
                                }
                                Console.Clear();
                            }
                        }
                        else if (encounterChoice == "2")
                        {
                            Console.Clear();
                            Console.WriteLine("Game Over");
                            Environment.Exit(0);
                        }
                        else
                        {
                            Fight();
                        }
                        beenIn.Add(x.ToString() + y.ToString());
                        beenInInt.Add(x + (y * 10));
                        Move();
                    }

                }
            }
            Move();
        }
    }
}



