# Decorator pattern
#
# To run this code, Python 3.10 or above is required.

from typing import Protocol
from abc import abstractmethod


# Component class
class Beverage(Protocol):
    description = 'Untitled'

    @abstractmethod
    def cost(self) -> float:
        pass

    def get_description(self) -> str:
        return self.description


# Concrete components
class Espresso(Beverage):
    description = 'Espresso'

    def cost(self):
        return 1.99


class HouseBlend(Beverage):
    description = 'HouseBlend'

    def cost(self):
        return .89


class DarkRoast(Beverage):
    description = 'DarkRoast'

    def cost(self):
        return 1.79


class Decaf(Beverage):
    description = 'Decaf'

    def cost(self):
        return 1.99


# Decorator class
class CondimentDecorator(Beverage):
    beverage: Beverage

    @abstractmethod
    def get_description(self) -> str:
        pass


# Condiment classes
class Mocha(CondimentDecorator):
    def __init__(self, beverage: Beverage) -> None:
        self.beverage = beverage

    def get_description(self) -> str:
        return '{}, mocha'.format(self.beverage.get_description())

    def cost(self) -> float:
        return self.beverage.cost() + .20


class Soy(CondimentDecorator):
    def __init__(self, beverage: Beverage) -> None:
        self.beverage = beverage

    def get_description(self) -> str:
        return '{}, soy'.format(self.beverage.get_description())

    def cost(self) -> float:
        return self.beverage.cost() + .40


class Whip(CondimentDecorator):
    def __init__(self, beverage: Beverage) -> None:
        self.beverage = beverage

    def get_description(self) -> str:
        return '{}, whip'.format(self.beverage.get_description())

    def cost(self) -> float:
        return self.beverage.cost() + .10


class JavaChip(CondimentDecorator):
    def __init__(self, beverage: Beverage) -> None:
        self.beverage = beverage

    def get_description(self) -> str:
        return '{}, javachip'.format(self.beverage.get_description())

    def cost(self) -> float:
        return self.beverage.cost() + .20


# Main
beverage1 = HouseBlend()
print('{} ${:.2f}'.format(beverage1.get_description(), beverage1.cost()))

beverage2 = Espresso()
beverage2 = Soy(beverage2)
beverage2 = Whip(beverage2)
beverage2 = JavaChip(beverage2)
print('{} ${:.2f}'.format(beverage2.get_description(), beverage2.cost()))
