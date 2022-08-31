abstract class Beverage {
    String description = "Untitled";

    public String getDescription() {
        return description;
    }

    public abstract double cost();
}

// Decorators
abstract class CondimentDecorator extends Beverage {
    Beverage beverage;

    public abstract String getDescription();
}

// Concrete Decorators
class Mocha extends CondimentDecorator {
    public Mocha(Beverage beverage) {
        this.beverage = beverage;
    }

    // Override cost and calls parent component method.
    public String getDescription() {
        return beverage.getDescription() + ", mocha";
    }

    public double cost() {
        return beverage.cost() + .20;
    }
}

class Soy extends CondimentDecorator {
    public Soy(Beverage beverage) {
        this.beverage = beverage;
    }

    public String getDescription() {
        return beverage.getDescription() + ", soy";
    }

    public double cost() {
        return beverage.cost() + .40;
    }
}

class Whip extends CondimentDecorator {
    public Whip(Beverage beverage) {
        this.beverage = beverage;
    }

    public String getDescription() {
        return beverage.getDescription() + ", whip";
    }

    public double cost() {
        return beverage.cost() + .10;
    }
}


// Concrete Components
class Espresso extends Beverage {
    public Espresso() {
        description = "Espresso";
    }

    public double cost() {
        return 1.99;
    }
}

class HouseBlend extends Beverage {
    public HouseBlend() {
        description = "HouseBlend";
    }

    public double cost() {
        return .89;
    }
}

class DarkRoast extends Beverage {
    public DarkRoast() {
        description = "DarkRoast";
    }

    public double cost() {
        return 1.79;
    }
}

class Decaf extends Beverage {
    public Decaf() {
        description = "Decaf";
    }

    public double cost() {
        return 1.99;
    }
}

public class LearnJava {
    public static void main(String[] args) {
        Beverage beverage = new Espresso();
        System.out.println(beverage.getDescription() + " $" + beverage.cost());

        Beverage beverage2 = new DarkRoast();
        beverage2 = new Mocha(beverage2);
        beverage2 = new Mocha(beverage2);
        beverage2 = new Whip(beverage2);
        System.out.println(beverage2.getDescription() + " $" + beverage2.cost());

        Beverage beverage3 = new HouseBlend();
        beverage3 = new Soy(beverage3);
        beverage3 = new Mocha(beverage3);
        beverage3 = new Whip(beverage3);
        System.out.println(beverage3.getDescription() + " $" + beverage3.cost());
    }
}