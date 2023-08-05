// Decorator class

// To run this code, .NET 5 or later is required.

// Component abstract class
public abstract class Beverage
{
    public virtual string Description { get; } = "Untitled";

    public abstract double Cost();
}

// Concrete components
public class Espresso : Beverage
{
    public override string Description { get; } = "Espresso";

    public override double Cost() => 1.99;
}

public class HouseBlend : Beverage
{
    public override string Description { get; } = "HouseBlend";

    public override double Cost() => .89;
}

public class DarkRoast : Beverage
{
    public override string Description { get; } = "DarkRoast";

    public override double Cost() => 1.79;
}

public class Decaf : Beverage
{
    public override string Description { get; } = "Decaf";

    public override double Cost() => 1.99;
}

// Decorator class
public abstract class CondimentDecorator : Beverage
{
    protected Beverage beverage;
}

// Concrete decorators
public sealed class Mocha : CondimentDecorator
{
    public override string Description { get { return beverage.Description + ", mocha"; } }

    public Mocha(Beverage beverage)
    {
        this.beverage = beverage;
    }

    public override double Cost() => beverage.Cost() + .20;
}

public sealed class Soy : CondimentDecorator
{
    public override string Description { get { return beverage.Description + ", soy"; } }

    public Soy(Beverage beverage)
    {
        this.beverage = beverage;
    }

    public override double Cost() => beverage.Cost() + .40;
}

public sealed class Whip : CondimentDecorator
{
    public override string Description {  get { return beverage.Description + ", whip"; } }

    public Whip(Beverage beverage)
    {
        this.beverage = beverage;
    }

    public override double Cost() => beverage.Cost() + .10;
}

public sealed class JavaChip : CondimentDecorator
{
    public override string Description { get { return beverage.Description + ", javachip"; } }

    public JavaChip(Beverage beverage)
    {
        this.beverage = beverage;
    }

    public override double Cost() => beverage.Cost() + .20;
}

public static class Program
{
    public static void Main(string[] args)
    {
        Beverage beverage = new Decaf();
        Console.WriteLine($"{beverage.Description} ${beverage.Cost()}");

        // Use decorator
        Beverage beverage2 = new DarkRoast();
        beverage2 = new Soy(beverage2);
        beverage2 = new Mocha(beverage2);
        beverage2 = new Mocha(beverage2);
        beverage2 = new Whip(beverage2);
        beverage2 = new JavaChip(beverage2);
        Console.WriteLine($"{beverage2.Description} ${beverage2.Cost()}");
    }
}
