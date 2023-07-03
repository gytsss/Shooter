# Space Shooting Game
Welcome to the Space Shooter game! In this action-packed shooter game, your mission is to eliminate all the enemies before time runs out. Utilizing various design patterns such as the Factory Method, Finite State Machine (FSM), and SOLID principles, this game offers an engaging and challenging gameplay experience.

Game Objective
Your objective is to navigate through the space station and destroy enemy drones. You'll need to strategically shoot down enemy drone while avoiding their lasers. The game ends when either all enemies are defeated or the timer runs out.

Features
Intense Space Shooter Gameplay: Engage in thrilling space battles with various enemy types and challenging obstacles.
Time Pressure: Race against the clock as you strive to defeat all enemies before time expires.
Dynamic Enemy Behavior: Experience dynamic enemy AI that adapts and reacts to your actions, providing an immersive and challenging gameplay experience.
Visual Effects: Enjoy stunning visual effects and explosions that intensify the excitement of the space battles.

Design Patterns Implemented
#### Factory Method
The Factory Method design pattern is used to create different types of enemy spaceships dynamically. By encapsulating the object creation process within a factory class, we achieve flexibility and extensibility, allowing for easy addition of new enemy ship types in the future.

#### Finite State Machine (FSM)
The Finite State Machine pattern is employed to manage the behavior of the enemy spaceships. Each spaceship has different states, such as "Idle," "Attacking," and "Evading," and transitions between these states based on certain conditions. This pattern provides a structured approach to modeling and managing complex enemy behaviors.

#### SOLID Principles
Throughout the development of this game, the SOLID principles have been adhered to:
Single Responsibility Principle: Each class has a single responsibility, promoting maintainability and readability.
Open/Closed Principle: The game components are designed to be easily extensible without requiring modifications to existing code.
Liskov Substitution Principle: Derived classes have been designed to be substitutable for their base classes, allowing for polymorphism and flexible usage.
Interface Segregation Principle: Interfaces have been defined to be specific and focused on the needs of the implementing classes, preventing unnecessary dependencies.
Dependency Inversion Principle: The game components depend on abstractions rather than concrete implementations, promoting loose coupling and ease of testing.

Installation
Clone this repository.
Open the project in Unity3D.
Build and run the game on your desired platform.

Controls
W, A, S, D or Arrow keys: Move.
Left Mouse Button: Fire.
E: Pick up.
G: Drop.
Spacebar: jump.
Esc: Pause the game and access the menu.

Credits
Developed by [GodoyTobias]
