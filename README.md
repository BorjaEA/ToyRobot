# Toy Robot Simulator

A simple console-based simulation of a toy robot moving around on a grid. This project lets you place a robot, move it, rotate it, and even place obstacles (walls) on the board. Perfect for experimenting with command parsing, grid navigation, and edge wrapping logic.

---

## Features

- **Place a Robot** anywhere on the board with a specific facing (North, South, East, West).  
- **Move the Robot** forward in the direction it is facing.  
- **Turn Left/Right** to change the robot's facing.  
- **Place Walls** to block the robot’s path.  
- **Edge Wrapping**: The robot wraps around when it moves off the edge of the board.  
- **Command Sequences**: Execute multiple commands in order.  
- **REPORT** command shows the robot’s current position and facing.

---

## Commands

| Command           | Description |
|------------------|-------------|
| `PLACE_ROBOT X,Y,FACING` | Place the robot at column `X`, row `Y`, facing `FACING`. Example: `PLACE_ROBOT 1,1,NORTH` |
| `PLACE_WALL X,Y` | Place a wall at column `X`, row `Y`. The robot cannot move into walls. |
| `MOVE`           | Move the robot one unit forward in the current facing direction. |
| `LEFT`           | Turn the robot 90° to the left. |
| `RIGHT`          | Turn the robot 90° to the right. |
| `REPORT`         | Print the robot's current position and facing to the console. |

> The board coordinates start at `(1,1)` in the bottom-left corner.

---

## How to Run

Clone the repository, build the project, and run the console app with:

```bash
git clone <repository-url>
cd ToyRobot
dotnet build
dotnet run --project src/ToyRobot.ConsoleApp

Then type commands directly in the console, for example:
PLACE_ROBOT 1,1,NORTH
MOVE
REPORT

Example Session
> PLACE_ROBOT 2,3,EAST
> MOVE
> MOVE
> LEFT
> MOVE
> REPORT
3,5,NORTH

Edge Wrapping and Walls

If the robot moves beyond the grid’s edge, it wraps around to the opposite side.

The robot cannot move into a wall. Attempting to do so will ignore the move.

Testing

Unit tests are provided for both the domain and application layers:

Domain Tests: Board logic, movement, edge wrapping, wall placement, etc.

Application Tests: Command execution, robot service, sequences of commands.

Run all tests using:

dotnet test
