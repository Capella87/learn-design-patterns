// Factory Method
// This code is a practice code to learn and apply factory method written in Java.
// To practice, I've implemented the simplified civilization system in Age of Empires II instead of code in <Head First Design Patterns>.

// To run this code, Java 17 or above is required.

// Enums for entities
class Entities {
    public static enum Units {
        Militia,
        Spearman,
        EagleScout,
        Condottiero,
        Huskarl
    }
}

// Resource requirements class
final class Requirements {
    private int food;
    private int wood;
    private int gold;
    private int stone;

    public Requirements(int food, int wood, int gold, int stone) {
        this.food = food;
        this.wood = wood;
        this.gold = gold;
        this.stone = stone;
    }

    public int getFood() {
        return food;
    }

    public int getWood() {
        return wood;
    }

    public int getGold() {
        return gold;
    }

    public int getStone() {
        return stone;
    }
}

// Entity interface
interface Entity {
    public String getName();
    public String getDescription();
    public int getHealth();
    public Requirements getRequirements();
}

// Building interface
interface Building extends Entity {
    public void demolish();
}

// Building abstract classes
abstract class MilitaryBuilding implements Building {
    String name;
    String description;
    int health;
    Requirements requirements;

    // Overridden methods from interfaces
    public String getName() {
        return "'" + name + " " + this.hashCode() + "'";
    }

    public String getDescription() {
        return description;
    }

    public int getHealth() {
        return health;
    }

    public void demolish() {
        System.out.println(this.getName() + " is demolished.");
    }

    public Requirements getRequirements() {
        return requirements;
    }

    // Methods
    public Unit requestUnit(Entities.Units target) {
        Unit unit = null;
        try {
            unit = deployUnit(target);
        }
        catch (ClassNotFoundException e) {
            System.err.println(e.getMessage());
        }
        finally {
            return unit;
        }
    }

    // Abstract methods
    protected abstract Unit deployUnit(Entities.Units target) throws ClassNotFoundException;
}

// Buildings
final class Barracks extends MilitaryBuilding {

    public Barracks() {
        name = "Barracks";
        description = "The Barracks is the first military building available for construction in Age of Empires II.\n" +
                "It is prerequisite to building the Archery Range and Stable.\n" +
                "It trains and improves infantry.";
        requirements = new Requirements(0, 175, 0, 0);
        health = 1500;
        System.out.println(this.getName() + " is constructed.");
    }

    // Factory method
    protected Unit deployUnit(Entities.Units target) throws ClassNotFoundException {
        Unit unit;
        switch (target) {
            case Militia -> unit = new Militia();
            case Spearman -> unit = new Spearman();
            default -> throw new ClassNotFoundException("Bad request.");
        }

        return unit;
    }
}

// Unit Interface
interface Unit extends Entity {
    public void die();
    public void move(int x, int y);
    public void attack(Entity target);
}

// Unit abstract classes
abstract class MilitaryUnit implements Unit {
    String name;
    String description;
    int health;
    Requirements requirements;

    public String getName() {
        return "'" + name + " " + this.hashCode() + "'";
    }

    public String getDescription() {
        return description;
    }

    public int getHealth() {
        return health;
    }

    public Requirements getRequirements() {
        return requirements;
    }

    public void die() {
        System.out.println(this.getName() + " is dead.");
    }

    public void move(int x, int y) {
        System.out.println(this.getName() + " moves to (" + x + ", " + y + ").");
    }

    public void attack(Entity target) {
        System.out.println(this.getName() +  " attacks " + target.getName() + '.');
    }
}

// Units
final class Militia extends MilitaryUnit {
    public Militia() {
        name = "Militia";
        description = "The Militia is an infantry unit in Age of Empires II that can be trained at the Barracks.\n" +
                "It is the first trainable military unit and the only one available to create in the Dark Age.";
        requirements = new Requirements(60, 20, 0, 0);
        health = 40;
        System.out.println(this.getName() + " is deployed.");
    }
}

final class Spearman extends MilitaryUnit {
    public Spearman() {
        name = "Spearman";
        description = "The Spearman is an infantry unit in Age of Empires II " +
                "that can be trained at the Barracks once the Feudal Age is reached.\n" +
                "They are a good early cavalry-counter, but are weak against virtually everything else.";
        requirements = new Requirements(35, 25, 0, 0);
        health = 45;
        System.out.println(this.getName() + " is deployed.");
    }
}

public class Factory {
    public static void main(String[] args) {
        var barrack1 = new Barracks();

        System.out.println(barrack1.getName());
        var militia1 = barrack1.requestUnit(Entities.Units.Militia);
        var spearman1 = barrack1.requestUnit(Entities.Units.Spearman);

        System.out.println(militia1.getDescription() + '\n');
        militia1.move(100, 100);
        militia1.attack(spearman1);
        spearman1.attack(militia1);

        barrack1.demolish();
    }
}