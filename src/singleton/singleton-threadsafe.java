// Singleton pattern

// To run this code, Java 17 or above is required.

// Singleton with Double-checked Locking in Java
class Singleton {
    private volatile static Singleton singleton;

    private Singleton() {
        System.out.println("The singleton object is constructed.");
    }

    public static Singleton getInstance() {
        if (singleton == null) {
            synchronized (Singleton.class) {
                if (singleton == null) {
                    singleton = new Singleton();
                }
            }
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