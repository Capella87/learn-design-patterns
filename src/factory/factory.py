# Factory method
#
# To run this code. Python 3.10 or above is required.

from typing import Protocol
from abc import abstractmethod
from enum import Enum


# Resource requirements class
class Requirements:
    def __init__(self, food, wood, gold, stone):
        self.food = food
        self.wood = wood
        self.gold = gold
        self.stone = stone


# Units enum class
class GameUnits(Enum):
    MILITIA = 1
    SPEARMAN = 2
    EAGLESCOUT = 3
    CONDOTTIERO = 4
    HUSKARL = 5


# Entity abstract class
class Entity(Protocol):
    description: str
    requirements: Requirements
    health: int

    def get_name(self) -> str:
        return '\'{} {}\''.format(self.name, hash(self))


# Entity descendant abstract classes
class Building(Entity):
    @abstractmethod
    def demolish(self) -> None:
        pass


class Unit(Entity):
    @abstractmethod
    def die(self) -> None:
        pass

    @abstractmethod
    def move(self, x: int, y: int) -> None:
        pass

    @abstractmethod
    def attack(self, target: Entity) -> None:
        pass


# Building descendant abstract classes
class MilitaryBuilding(Building):
    def demolish(self) -> None:
        print('{} is demolished.'.format(self.get_name()))
        return

    def request_unit(self, target: GameUnits) -> Unit:
        try:
            return self.deploy(target)
        except ValueError as err:
            print(err)

    @abstractmethod
    def deploy(self, target: GameUnits) -> Unit:
        pass


# Building classes
class Barracks(MilitaryBuilding):
    def __init__(self) -> None:
        self.name = "Barracks"
        self.description = '''The Barracks is the first military building available for construction in Age of Empires II.
It is prerequisite to building the Archery Range and Stable.
It trains and improves infantry.'''
        self.health = 1500
        self.requirements = Requirements(0, 175, 0, 0)

        print('{} is built.'.format(self.get_name()))

    # Factory method
    def deploy(self, target: GameUnits) -> Unit:
        match target:
            case GameUnits.MILITIA:
                return Militia()
            case GameUnits.SPEARMAN:
                return Spearman()
            case _:
                raise ValueError('{} - Invalid request.'.format(target))


# Unit descendant abstract classes
class MilitaryUnit(Unit):
    def move(self, x, y) -> None:
        print('{} moves to ({}, {}).'.format(self.get_name(), x, y))

    def die(self) -> None:
        print('{} is dead.'.format(self.get_name()))

    def attack(self, target) -> None:
        print('{} attacks {}.'.format(self.get_name(), target.get_name()))


# Unit classes
class Militia(MilitaryUnit):
    def __init__(self) -> None:
        self.description = '''The Militia is an infantry unit in Age of Empires II that can be trained at the Barracks.
It is the first trainable military unit and the only one available to create in the Dark Age.'''
        self.name = 'Militia'
        self.health = 40
        self.requirements = Requirements(60, 20, 0, 0)

        print('{} is deployed.'.format(self.get_name()))


class Spearman(MilitaryUnit):
    def __init__(self) -> None:
        self.description = '''The Spearman is an infantry unit in Age of Empires II that can be trained at the Barracks once the Feudal Age is reached.
They are a good early cavalry-counter, but are weak against virtually everything else.'''
        self.name = 'Spearman'
        self.health = 45
        self.requirements = Requirements(35, 25, 0, 0)

        print('{} is deployed.'.format(self.get_name()))


# Main
barracks = Barracks()
print(barracks.get_name())

militia1 = barracks.request_unit(GameUnits.MILITIA)
spearman1 = barracks.request_unit(GameUnits.SPEARMAN)

print(militia1.description)
print()
militia1.move(100, 100)
militia1.attack(spearman1)
spearman1.attack(militia1)

barracks.demolish()
