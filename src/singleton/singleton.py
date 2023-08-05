# Singleton with threading
#
# To run this code. Python 3.10 or above is required.

from threading import Lock, Thread
from typing import Any


# We're using Metaclass to customize the class object behavior
class SingletonMeta(type):
    """_summary_
    Thread-safe Singleton
    :param type: _description_
    :type type: _type_
    """

    _instances = {}

    _lock: Lock = Lock()
    

    # Create a new singleton object or receive its 'pointer'
    def __call__(cls, *args: Any, **kwargs: Any) -> Any:
        with cls._lock:
            if cls not in cls._instances:
                instance = super().__call__(*args, **kwargs)
                cls._instances[cls] = instance
        
        return cls._instances.get(cls)
    

class Singleton(metaclass=SingletonMeta):

    value: str = None

    def __init__(self, value: str) -> None:
        self.value = value
    
    
def singleton_create(value: str) -> None:
    obj = Singleton(value)
    print(obj.value)


if __name__ == "__main__":

    first_process = Thread(target=singleton_create, args=('Fallout 4', ))
    second_process = Thread(target=singleton_create, args=('Fallout: New Vegas', ))

    first_process.start()
    second_process.start()
