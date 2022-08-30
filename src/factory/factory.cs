// Factory Method
// This code is a practice code to learn and apply factory method written in C#.
// To practice, I've implemented the simplified civilization system in Age of Empires II instead of code in <Head First Design Patterns>.

// To run this code, .NET 5 or above is required. I highly recommend to use .NET 6 or above.

namespace AgeOfEmpiresII
{
    // Enums for entities
    public static class Entities
    {
        public enum MilitaryUnits
        {
            Militia,
            Spearman,
            EagleScout,
            Condottiero,
            Huskarl,
        };
    }

    // Resource requirements to deploy military or construct a building.
    public struct Requirements
    {
        public int Food { get; set; } = 0;
        public int Wood { get; set; } = 0;
        public int Gold { get; set; } = 0;
        public int Stone { get; set; } = 0;

        public Requirements(int food, int wood, int gold, int stone)
        {
            Food = food;
            Wood = wood;
            Gold = gold;
            Stone = stone;
        }
    }

    // Entity interface
    public interface IEntity
    {
        public string Name { init; get; }
        public string Description { init; get; }
        public int Health { get; set; }
        public ref Requirements GetRequirements();
    }

    // Building interface
    public interface IBuilding : IEntity, IDisposable
    {
        public void Demolish();
    }
    
    // Building abstract classes
    public abstract class MilitaryBuilding : IBuilding
    {
        protected string _name;
        protected Requirements _requirements;

        public string Name
        {
            init => _name = value;
            get => $"'{_name} {this.GetHashCode()}'";
        }
        public string Description { init; get; }
        public int Health { get; set; }

        public ref Requirements GetRequirements() => ref _requirements;
        
        public virtual void Demolish()
        {
            Console.WriteLine($"{Name} is demolished.");
            Dispose();
        }
        
        public void Dispose() => GC.SuppressFinalize(this);
        ~MilitaryBuilding() => Dispose();
        
        public IUnit RequestUnit(Entities.MilitaryUnits target)
        {
            IUnit unit = null;
            try
            {
                unit = DeployUnit(target);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            return unit;
        }

        // Factory method
        protected abstract IUnit DeployUnit(Entities.MilitaryUnits target);
    }

    // Building classes
    sealed class Barracks : MilitaryBuilding
    {
        public Barracks()
        {
            Name = "Barracks";
            Description = "The Barracks is the first military building available for construction in Age of Empires II.\n" +
                          "It is prerequisite to building the Archery Range and Stable.\n" +
                          "It trains and improves infantry.";
            _requirements = new Requirements(0, 175, 0, 0);
            Health = 1500;
            Console.WriteLine($"{Name} is constructed.");
        }

        protected override IUnit DeployUnit(Entities.MilitaryUnits target) => target switch
        {
            Entities.MilitaryUnits.Militia => new Militia(),
            Entities.MilitaryUnits.Spearman => new Spearman(),
            _ => throw new ArgumentException("Wrong request."),
        };
    }
    
    // Unit interface
    public interface IUnit : IEntity
    {
        public void Die();
        public void Move(int x, int y);
        public void Attack(IEntity target);
    }

    // Unit abstract classes
    public abstract class MilitaryUnit : IUnit, IDisposable
    {
        protected string _name;
        protected Requirements _requirements;
        public string Name
        {
            init => _name = value;
            get => $"'{_name} {this.GetHashCode()}'";
        }
        
        public string Description { init; get; }
        public int Health { get; set; }

        public ref Requirements GetRequirements() => ref _requirements;

        public virtual void Die()
        {
            Console.WriteLine($"{Name} is dead.");
            Dispose();
        }

        public virtual void Move(int x, int y) => Console.WriteLine($"{Name} moves to ({x}, {y}).");
        public virtual void Attack(IEntity target) => Console.WriteLine($"{Name} attacks {target.Name}.");

        public void Dispose() => GC.SuppressFinalize(this);
        ~MilitaryUnit() => Dispose();
    }

    // Unit classes
    public sealed class Militia : MilitaryUnit
    {
        public Militia()
        {
            Name = "Militia";
            Description = "The Militia is an infantry unit in Age of Empires II that can be trained at the Barracks.\n" +
                          "It is the first trainable military unit and the only one available to create in the Dark Age.";
            _requirements = new Requirements(60, 20, 0, 0);
            Health = 40;
            Console.WriteLine($"{Name} is deployed.");
        }
    }

    public sealed class Spearman : MilitaryUnit
    {
        public Spearman()
        {
            Name = "Spearman";
            Description = "The Spearman is an infantry unit in Age of Empires II " +
                          "that can be trained at the Barracks once the Feudal Age is reached.\n" +
                          "They are a good early cavalry-counter, but are weak against virtually everything else.";
            _requirements = new Requirements(35, 25, 0, 0);
            Health = 45;
            Console.WriteLine($"{Name} is deployed.");
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            var barrack1 = new Barracks();

            Console.WriteLine(barrack1.Name);
            var militia1 = barrack1.RequestUnit(Entities.MilitaryUnits.Militia);
            var spearman1 = barrack1.RequestUnit(Entities.MilitaryUnits.Spearman);

            Console.WriteLine(militia1.Description);
            Console.WriteLine();
            militia1.Move(100, 100);
            militia1.Attack(spearman1);
            militia1.Attack(militia1);
            
            barrack1.Demolish();
        }
    }
}
