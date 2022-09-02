# Strategy pattern

# To run this code, Python 3.10 or above is required.

from abc import abstractclassmethod
from asyncio import protocols
from typing import Protocol

# Feature abstract classes
class Fly(Protocol):
    @abstractclassmethod
    def fly(self) -> None:
        pass

class Quack(Protocol):
    @abstractclassmethod
    def quack(self) -> None:
        pass

# Feature classes
class FlyWithWings(Fly):
    def fly(self):
        print('I am flying!')

class NormalQuack(Quack):
    def quack(self):
        print('Quack!')

# Main component abstract class
class Duck(Protocol):
    flybehavior: Fly
    quackbehavior: Quack

    @abstractclassmethod
    def perform_fly(self) -> None:
        pass

    @abstractclassmethod
    def perform_quack(self) -> None:
        pass

    @abstractclassmethod
    def display(self) -> None:
        pass

    def swim(self):
        print('All ducks can float on water. Even the fake one does!')

# Main component classes
class MallaradDuck(Duck):
    flybehavior = FlyWithWings()
    quackbehavior = NormalQuack()

    def perform_fly(self):
        self.flybehavior.fly()
    
    def perform_quack(self):
        self.quackbehavior.quack()
    
    def display(self):
        print('I\'m a mallard duck.')


# Main
duck = MallaradDuck()
duck.perform_fly()
duck.perform_quack()
duck.display()
duck.swim()
