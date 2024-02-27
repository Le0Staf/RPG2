using System;

namespace Enemies
{
    class Enemy
    {
        public string enemyName;
        public int enemyHP;
        public int enemyDMG;

        public Enemy(string EnemyName, int EnemyHP, int EnemyDMG)
        {
            enemyName = EnemyName;
            enemyHP = EnemyHP;
            enemyDMG = EnemyDMG;
        }
    }
}
