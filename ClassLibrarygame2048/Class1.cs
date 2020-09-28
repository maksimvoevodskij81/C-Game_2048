using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrarygame2048
{
    public class Model
    {
        static Random random = new Random();
        Map map;
       public static bool isGameOver;
        bool moved;
       public static int colectScore { get; set; }
        public static int bestScore { get; set; }

        public int size { get { return map.size; } }

        public Model(int size)
        {
            map = new Map(size);

        }

        public void Start()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    map.SetMap(x, y, 0);
                }
            }
            AddNuber2or4();
            AddNuber2or4();
        }
        void AddNuber2or4()
        {
            if (isGameOver) return;
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(0, map.size);
                int y = random.Next(0, map.size);
                if (map.GetPositionMap(x, y) == 0)
                {
                    map.SetMap(x, y, random.Next(1, 3) * 2);
                    return;
                }
            }

        }
        void Move(int x, int y, int sx, int sy)//метод перемещения 
        {
            if (map.GetPositionMap(x, y) > 0)
            {
                while (map.GetPositionMap(x + sx, y + sy) == 0)//если кнопка пустая переносим число и устанавливаем 
                {
                    map.SetMap(x + sx, y + sy, map.GetPositionMap(x, y));
                    map.SetMap(x, y, 0);//устанавливаем 0 в то место откуда сместились
                    x += sx;//смещаемся в лево
                    y += sy;
                    moved = true;
                }
            }

        }
        void JoinNamber(int x, int y, int sx, int sy)
        {
            if (map.GetPositionMap(x, y) > 0)
            {
                if (map.GetPositionMap(x + sx, y + sy) == map.GetPositionMap(x, y))
                {
                   
                    map.SetMap(x + sx, y + sy, map.GetPositionMap(x, y) * 2);
                    if(colectScore< map.GetPositionMap(x, y) * 2)
                    colectScore = map.GetPositionMap(x, y) * 2;
                    while (map.GetPositionMap(x - sx, y - sy) > 0)
                    {
                        map.SetMap(x, y, map.GetPositionMap(x - sx, y - sy));
                        x -= sx;
                        y -= sy;

                    }
                    map.SetMap(x, y, 0);
                    moved = true;
                }

            }
        }

        public void Left()
        {

            moved = false;
            for (int y = 0; y < size; y++)
            {
                for (int x = 1; x < size; x++)
                {
                    Move(x, y, -1, 0);

                }
            }
            for (int y = 0; y < size; y++)
            {
                for (int x = 1; x < size; x++)
                {
                    JoinNamber(x, y, -1, 0);

                }
            }

            if (moved) AddNuber2or4();
        }

        public void Right()
        {
            moved = false;
            for (int y = 0; y < size; y++)
            {
                for (int x = size - 2; x >= 0; x--)
                {
                    Move(x, y, +1, 0);

                }
            }
            for (int y = 0; y < size; y++)
            {
                for (int x = size - 2; x >= 0; x--)
                {
                    JoinNamber(x, y, +1, 0);

                }
            }
            if (moved) AddNuber2or4();
        }

        public void Up()
        {
            moved = false;
            for (int x = 0; x < size; x++)
            {
                for (int y = 1; y < size; y++)
                {
                    Move(x, y, 0, -1);

                }
            }
            for (int x = 0; x < size; x++)
            {
                for (int y = 1; y < size; y++)
                {
                    JoinNamber(x, y, 0, -1);

                }
            }
            if (moved) AddNuber2or4();
        }

        public void Down()
        {
            moved = false;
            for (int x = 0; x < size; x++)
            {
                for (int y = size - 2; y >= 0; y--)
                {
                    Move(x, y, 0, +1);

                }
            }
            for (int x = 0; x < size; x++)
            {
                for (int y = size - 2; y >= 0; y--)
                {
                    JoinNamber(x, y, 0, +1);

                }
            }
            if (moved) AddNuber2or4();
        }

        public int GetMap(int x, int y)
        {
            return map.GetPositionMap(x, y);
        }

        public bool IsGameOver()
        {
            if (isGameOver)
                return true;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (map.GetPositionMap(x, y) == 0)
                    {
                        return false;
                    }
                }
            }
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (map.GetPositionMap(x + 1, y) == map.GetPositionMap(x, y) || map.GetPositionMap(x, y) == map.GetPositionMap(x, y + 1))
                    {
                        return false;
                    }
                }
            }
            isGameOver = true;
            bestScore = bestScore >= colectScore ? bestScore : colectScore;
            return isGameOver;
        }
    }
}
