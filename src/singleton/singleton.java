// Singleton pattern

// To run this code, Java 17 or above is required.

class Singleton {
    private static Singleton singleton;

    private Singleton() {
        System.out.println("The singleton object is constructed.");
    }

    public static Singleton getInstance() {
        if (singleton == null) {
            singleton = new Singleton();
        }
        else {
            System.out.println("The singleton object is already generated.");
        }

        return singleton;
    }
}

public class Main {
    public static void main(String[] args) {
        var s1 = Singleton.getInstance();
        var s2 = Singleton.getInstance();
    }
}