interface FlyBehavior {
    public void fly();
}

class FlyWithWings implements FlyBehavior {
    public void fly() {
        System.out.println("I can fly!");
    }
}

class FlyNoWay implements FlyBehavior {
    public void fly() {
        System.out.println("I cannot fly.");
    }
}

class FlyRocketPowered implements FlyBehavior {
    public void fly() {
        System.out.println("I'm flying with rocket power!");
    }
}

interface QuackBehavior {
    public void quack();
}

class NormalQuack implements QuackBehavior {
    public void quack() {
        System.out.println("Quack!");
    }
}

class Squeak implements QuackBehavior {
    public void quack() {
        System.out.println("Squeak!");
    }
}

class MuteQuack implements QuackBehavior {
    public void quack() {
        System.out.println("Quiet!");
    }
}

abstract class Duck {
    FlyBehavior flyBehavior;
    QuackBehavior quackBehavior;

    public void performFly() {
        flyBehavior.fly();
    }

    public void performQuack() {
        quackBehavior.quack();
    }

    public void swim() {
        System.out.println("All ducks can float on water. Even the fake one does.");
    }

    public void setFlyBehavior(FlyBehavior flyBehavior) {
        this.flyBehavior = flyBehavior;
    }

    public void setQuackBehavior(QuackBehavior quackBehavior) {
        this.quackBehavior = quackBehavior;
    }

    public abstract void display();
}

class MallardDuck extends Duck {
    public MallardDuck() {
        flyBehavior = new FlyWithWings();
        quackBehavior = new NormalQuack();
    }

    @Override
    public void display() {
        System.out.println("I'm a mallard duck");
    }
}

class ModelDuck extends Duck {
    public ModelDuck() {
        flyBehavior = new FlyNoWay();
        quackBehavior = new NormalQuack();
    }

    @Override
    public void display() {
        System.out.println("I'm a model duck.");
    }
}

public class Main {
    public static void main(String[] args) {
        Duck mallard = new MallardDuck();
        mallard.performQuack();
        mallard.performFly();

        Duck model = new ModelDuck();
        model.performFly();
        model.setFlyBehavior(new FlyRocketPowered());
        model.performFly();
    }
}
