namespace AgeOfEmpiresII
{
    public static class Entity
    {
        public enum MilitaryUnit
        {
            Militia,
            Spearman,
            EagleScout,
            Condottiero,
            Huskarl,
        };

        public enum Technology
        {
            Toolworking,
            Metalworking,
        }
    }

    // Buildings
    public interface IBuilding
    {
        public string Name { init; get; }
        public string Description { init; get; }
        public ref RequirementResource GetRequirements();
    }

    public abstract class MilitaryBuilding : IBuilding
    {
        public string Name { init; get; }
        public string Description { init; get; }
        protected RequirementResource _reqSource;

        public ref RequirementResource GetRequirements()
        {
            return ref _reqSource;
        }

        protected abstract IUnit DeployUnit(Entity.MilitaryUnit e);
        // protected abstract IResearch Research(Enum e);

        public abstract IUnit RequestNewUnit(Entity.MilitaryUnit e);
        // public abstract IResearch RequestResearch(Enum e);
    }

    public sealed class Barracks : MilitaryBuilding
    {
        public Barracks()
        {
            Name = "Barracks";
            Description = "Test description.";
            _reqSource = new RequirementResource(0, 175, 0, 0);
        }

        // Factory method. Used pattern matching in C#.
        public override IUnit RequestNewUnit(Entity.MilitaryUnit e) =>
            e switch
            {
                Entity.MilitaryUnit.Militia => new Militia(),
                Entity.MilitaryUnit.Spearman => new Spearman(),
                _ => throw new KeyNotFoundException("Wrong unit.")
            };

        protected override IUnit DeployUnit(Entity.MilitaryUnit e)
        {
            throw new NotImplementedException();
        }
    }

    // Resource requirements to deploy military or construct a building.
    public struct RequirementResource
    {
        public int Food { get; set; } = 0;
        public int Wood { get; set; } = 0;
        public int Gold { get; set; } = 0;
        public int Stone { get; set; } = 0;
        
        public RequirementResource(int food, int wood, int gold, int stone)
        {
            Food = food;
            Wood = wood;
            Gold = gold;
            Stone = stone;
        }
    }

    // Units
    public interface IUnit
    {
        public string Name { init; get; }
        public string Description { init; get; }
        public ref RequirementResource GetRequirements();
    }

    public abstract class MilitaryUnit : IUnit
    {
        public string Name { init; get; }
        public string Description { init; get; }
        protected RequirementResource _reqSource;

        public ref RequirementResource GetRequirements()
        {
            return ref _reqSource;
        }

        public abstract void Death();
        public abstract void Attack();
        public abstract void Defense();
        public abstract void Move(string unitEntityName);
    }

    public class Militia : MilitaryUnit
    {
        public Militia()
        {
            Name = "Militia";
            Description = "The Militia is an infantry unit in Age of Empires II that can be trained at the Barracks.\n" +
                "It is the first trainable military unit and the only one available to create in the Dark Age. ";
            _reqSource = new RequirementResource(60, 20, 0, 0);
        }

        public override void Attack()
        {
            Console.WriteLine("Attack!");
        }

        public override void Death()
        {
        }

        public override void Defense()
        {
        }

        public override void Move(string unitEntityName)
        {
            Console.WriteLine($"{unitEntityName} is moved to there.");
        }
    }

    public class Spearman : MilitaryUnit
    {
        public Spearman()
        {
            Name = "Spearman";
            Description = "The Spearman is an infantry unit in Age of Empires II " +
                "that can be trained at the Barracks once the Feudal Age is reached.\n" +
                "They are a good early cavalry-counter, but are weak against virtually everything else.";
            _reqSource = new RequirementResource(35, 25, 0, 0);
        }

        public override void Attack()
        {
            Console.WriteLine("Attack!");
        }

        public override void Death()
        {
        }

        public override void Defense()
        {
        }

        public override void Move(string unitEntityName)
        {
            Console.WriteLine($"{unitEntityName} is moved to there.");
        }
    }

    public static class Program
    {
        public static void Main(string[] args)
        {
            var firstBarrack = new Barracks();

            Console.WriteLine(firstBarrack.Name);
            var militia1 = (MilitaryUnit)firstBarrack.RequestNewUnit(Entity.MilitaryUnit.Militia);
            var militia2 = (MilitaryUnit)firstBarrack.RequestNewUnit(Entity.MilitaryUnit.Militia);
            militia1.Attack();
            militia2.Move(nameof(militia2));
            militia1.Attack();
        }
    }
}
