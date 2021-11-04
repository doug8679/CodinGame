using System;
using System.Collections.Generic;
using System.Text;

namespace CodeVsZombies
{
    public class GameSimulator
    {
        private Player _player;
        private List<Human> _humans;
        private List<Zombie> _zombies;

        public GameSimulator()
        {
            _player = new Player();
            _humans = new List<Human>();
            _zombies = new List<Zombie>();
        }

        internal void SetPlayer()
        {
            var inputs = Console.ReadLine().Split(' ');
            _player.X = int.Parse(inputs[0]);
            _player.Y = int.Parse(inputs[1]);
        }

        internal void PlaceZombies()
        {
            int zombieCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < zombieCount; i++)
            {
                var inputs = Console.ReadLine().Split(' ');
                _zombies.Add(new Zombie {
                    Id = int.Parse(inputs[0]),
                    X = int.Parse(inputs[1]),
                    Y = int.Parse(inputs[2]),
                    NextX = int.Parse(inputs[3]),
                    NextY = int.Parse(inputs[4])
                });
            }
        }

        internal void MakeMove()
        {
            Console.WriteLine($"0 0");
        }

        internal void PlaceHumans()
        {
            int humanCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < humanCount; i++)
            {
                var inputs = Console.ReadLine().Split(' ');
                _humans.Add(new Human {
                    Id = int.Parse(inputs[0]),
                    X = int.Parse(inputs[1]),
                    Y = int.Parse(inputs[2])
                });
            }
        }
    }

    public abstract class GameObject
    {
        public int X {  get; set; }
        public int Y {  get; set; }
    }

    public abstract class NonPlayerObject : GameObject
    {
        public int Id {  get; set; }
    }

    public class Human : NonPlayerObject
    {

    }

    public class Zombie : NonPlayerObject
    {
        public int NextX {  get; set; }
        public int NextY { get; set; }
    }
}
