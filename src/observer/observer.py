from typing import Protocol
from abc import abstractmethod
from typing_extensions import Self

class Observer(Protocol):

    def update(self: Self) -> None:
        raise NotImplementedError()

class Observable(Protocol):
    observers: list[Observer]
    
    @abstractmethod
    def register(self: Self, obs: Observer) -> None:
        self.observers.append(obs)
    
    @abstractmethod
    def remove(self: Self, obs: Observer) -> None:
        self.observers.remove(obs)
    
    @abstractmethod
    def notify(self: Self):
        for i in self.observers:
            i.update()

class WeatherData(Observable):

    temperature = None
    humidity = None
    pressure = None

    def __init__(self):
        self.observers = []
    
    def register(self, obs):
        self.observers.append(obs)

    def remove(self, obs):
        self.observers.remove(obs)
    
    def notify(self):
        for i in self.observers:
            i.update()

    def set_data(self: Self, temp: float, hum: float, pres: float):
        self.temperature = temp
        self.humidity = hum
        self.pressure = pres
        self.notify()


class WeatherStation(Observer):
    
    temperature = None
    humidity = None

    def __init__(self, weatherdata):
        self.weatherdata = weatherdata
        self.weatherdata.register(self)

    def update(self):
        self.temperature = self.weatherdata.temperature
        self.humidity = self.weatherdata.humidity
        self.display()
    
    def display(self):
        print('Temperature: ', self.temperature)
        print('Humidity: ', self.humidity, end='\n\n')


data = WeatherData()

subscriber = WeatherStation(weatherdata= data)
data.set_data(temp=30.6, hum=80, pres=1013.3)
data.set_data(temp=29.8, hum=85, pres=1014.2)
