public interface IFly
{
    public void Fly();
}

public class FlyWithWings : IFly
{
    public void Fly()
    {
        Console.WriteLine("I'm flying!");
    }
}

public class FlyNoWay : IFly
{
    public void Fly()
    {
        Console.WriteLine("I cannot fly.");
    }
}

public class FlyRocketPowered : IFly
{
    public void Fly()
    {
        Console.WriteLine("I'm flying with rocket power!");
    }
}

public interface IQuack
{
    public void Quack();
}

public class NormalQuack : IQuack
{
    public void Quack()
    {
        Console.WriteLine("Quack!");
    }
}

public class Squeak : IQuack
{
    public void Quack()
    {
        Console.WriteLine("Squeak!");
    }
}

public class MuteQueak : IQuack
{
    public void Quack()
    {
        Console.WriteLine("Quiet!");
    }
}

public abstract class Duck
{
    public IQuack QuackBehavior { get; set; }
    public IFly FlyBehavior { get; set; }

    public abstract void Display();
    
    public void PerformFly()
    {
        FlyBehavior.Fly();
    }

    public void PerformQuack()
    {
        QuackBehavior.Quack();
    }

    public void Swim()
    {
        Console.WriteLine("All ducks can float on water. Even the fake one does.");
    }
}

public class MallardDuck : Duck
{
    public MallardDuck()
    {
        QuackBehavior = new NormalQuack();
        FlyBehavior = new FlyWithWings();
    }

    public override void Display()
    {
        Console.WriteLine("I'm a mallard duck.");
    }
}

public class ModelDuck : Duck
{
    public ModelDuck()
    {
        FlyBehavior = new FlyNoWay();
        QuackBehavior = new NormalQuack();
    }

    public override void Display()
    {
        Console.WriteLine("I'm a mock duck.");
    }
}


public static class Program
{
    public static void Main(string[] args)
    {
        Duck mallard = new MallardDuck();
        mallard.PerformQuack();
        mallard.PerformFly();


        Duck model = new ModelDuck();
        model.PerformFly();
        model.FlyBehavior = new FlyRocketPowered();
        model.PerformFly();
    }
}