using System;
using System.Collections.Generic;
using System.Linq;

namespace CAB201_Assesment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = new ObstacleAvoidanceSystem();
            system.UserInterface();
        }
    }

    /// <summary>
    /// The main class for the Theatron-o-Tron 900
    /// </summary>
    /// Task 1 Implementing the User Interface
    public class ObstacleAvoidanceSystem
    {
        private List<Obstacle> obstacles = new List<Obstacle>();
        private List<(int X, int Y)> obstacleCoordinates = new List<(int X, int Y)>();

        /// <summary>
        /// Implementing the User interface
        /// It reads the user input and analyze it is valid or not 
        /// </summary>
        /// <exception cref="ArgumentException">It throws the error message when inccorect number arugments is given by user </exception>
        /// <exception cref="FormatException">Displays error message when input is not valid integers</exception>
        /// <exception cref="NotSupportedException">Display error message when unsupported argument is entered </exception>
        public void UserInterface()
        {
            // Display welcome message and commands for the user 
            Console.WriteLine("Welcome to the Threat-o-tron 9000 Obstacle Avoidance System.");
            Help();

            while (true)
            {
                Console.Write("\nEnter command:\n ");
                string input = Console.ReadLine()?.Trim();

                // Splits the input 
                string[] parts = input.Split(' ');
                // Checking different inputs of the user and displays the message accordingly
                // firstly check the initial word of the input
                switch (parts[0])
                {
                    case "add":
                        try
                        {
                            // if the length is less than 2 it will throw an message 
                            if (parts.Length < 2)
                            {
                                throw new ArgumentException("incorrect number of arguments.");
                            }

                            // if the first word is add and then check for the second word 
                            switch (parts[1])
                            {
                                // if the second word is guard and then check requirements like valid coordinates. 
                                case "guard":
                                    // Checks the number of arguments
                                    if (parts.Length != 4)
                                    {
                                        throw new ArgumentException("Incorrect number of arguments.");
                                    }

                                    // Checks the input coordinates are valid integers
                                    if (!int.TryParse(parts[2], out int guardX) ||
                                        !int.TryParse(parts[3], out int guardY))
                                    {
                                        throw new FormatException("Coordinates are not valid integers.");
                                    }

                                    AddGuard(guardX, guardY);
                                    break;
                                // if the second word is fence and then check for the conditions of fence like length, orientation and valid coordinates  
                                case "fence":
                                    // Checks the number of arguments 
                                    if (parts.Length != 6)
                                    {
                                        throw new ArgumentException("Incorrect number of arguments.");
                                    }

                                    // Checks the input coordinates are valid 
                                    if (!int.TryParse(parts[2], out int fenceX) ||
                                        !int.TryParse(parts[3], out int fenceY) ||
                                        !int.TryParse(parts[5], out int length))
                                    {
                                        throw new FormatException("Coordinates  are not valid integers");
                                    }

                                    // Checks the directions are valid or not
                                    if (!(parts[4] == "east" || parts[4] == "west" || parts[4] == "north" ||
                                          parts[4] == "south"))
                                    {
                                        throw new FormatException("Orientation must be 'east' or 'north'.");
                                    }

                                    // Checks the length is a valid integer greater than 0
                                    if (length < 1)
                                    {
                                        throw new FormatException("Length must be a valid integer greater than 0.");
                                    }

                                    AddFence(fenceX, fenceY, parts[4], length);
                                    break;
                                // if the second word is sensor and then check for valid inputs by the user like coordiantes and radius
                                case "sensor":
                                    //Checks the number of arguments is correct or not
                                    if (parts.Length != 5)
                                    {
                                        throw new ArgumentException("Incorrect number of arguments.");
                                    }

                                    // Checking the coordinates are valid integers or not 
                                    if (!int.TryParse(parts[2], out int sensorX) ||
                                        !int.TryParse(parts[3], out int sensorY) ||
                                        !double.TryParse(parts[4], out double range) || range <= 0)
                                    {
                                        throw new FormatException("Coordinates are not valid integers.");
                                    }

                                    AddSensor(sensorX, sensorY, range);
                                    break;
                                // if the second word is camera and then check for its requirements like coordinates and  direction.
                                case "camera":
                                    // Checks the number of arguments 
                                    if (parts.Length != 5)
                                    {
                                        throw new ArgumentException("Incorrect number of arguments.");
                                    }

                                    // Checks the integers are volid or not
                                    if (!int.TryParse(parts[2], out int cameraX) ||
                                        !int.TryParse(parts[3], out int cameraY))
                                    {
                                        throw new ArgumentException("Coordinates are not valid integers.");
                                    }

                                    //Checks the directions are valid or not
                                    if (!(parts[4] == "east" || parts[4] == "west" || parts[4] == "north" ||
                                          parts[4] == "south"))

                                    {
                                        throw new FormatException(
                                            "Direction must be 'north', 'south', 'east' or 'west'.");
                                    }

                                    AddCamera(cameraX, cameraY, parts[4]);
                                    break;
                                default:
                                    throw new NotSupportedException(
                                        "Invalid add command. Type 'help' for a list of commands.");
                            }
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (NotSupportedException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;
                    // if the first word is check it will and it validates the input like the coordinates is integers or not.        
                    case "check":
                        try
                        {
                            // Checks the number of arguments is correct or not.
                            if (parts.Length != 3)
                            {
                                throw new ArgumentException(" Incorrect number of arguments.");
                            }

                            // Checking coordinates are valid integers or not.
                            if (!int.TryParse(parts[1], out int checkX) || !int.TryParse(parts[2], out int checkY))
                            {
                                throw new FormatException("Coordinates are not valid integers.");
                            }

                            CheckLocation(checkX, checkY);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An unexpected error occurred: " + ex.Message);
                        }

                        break;
                    //if the the first word is exit and it displays a message and stops the program.
                    case "exit":
                        Console.WriteLine("\nThank you for using Threat-o-tron 9000.");
                        return;
                    // if the first word is map and then it checks for the requirements of map command 
                    case "map":
                        try
                        {
                            // Check for the correct number of arguments.
                            if (parts.Length != 5)
                            {
                                throw new ArgumentException("Incorrect number of arguments.");
                            }

                            // Check the coordinates are valid or not.
                            if (!int.TryParse(parts[1], out int mapX) || !int.TryParse(parts[2], out int mapY))
                            {
                                throw new FormatException("Coordinates are not valid integers.");
                            }

                            // Check the height and width are valid positive integers
                            if (!int.TryParse(parts[3], out int mapWidth) ||
                                !int.TryParse(parts[4], out int mapHeight) || mapWidth <= 0 || mapHeight <= 0)
                            {
                                throw new FormatException("Width and height must be valid positive integers.");
                            }

                            ObstacleMap(mapX, mapY, mapWidth, mapHeight);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An unexpected error occurred: " + ex.Message);
                        }

                        break;
                    // If the first word is path and check the requirements of path command 
                    case "path":
                        try
                        {
                            //check for the number of arguments is correct.
                            if (parts.Length != 5)
                            {
                                throw new ArgumentException("Incorrect number of arguments.");
                            }

                            // Check for the agent coordinates are valid or not.
                            if (!int.TryParse(parts[1], out int agentX) || !int.TryParse(parts[2], out int agentY))
                            {
                                throw new FormatException("Agent coordinates are not valid integers.");
                            }

                            // Check the coordinates of goal are valid or not
                            if (!int.TryParse(parts[3], out int objectX) || !int.TryParse(parts[4], out int objectY))
                            {
                                throw new FormatException("Objective coordinates are not valid integers.");
                            }

                            // Check the coordinates of agent and goal is same or not
                            if (agentX == objectX && agentY == objectY)
                            {
                                Console.WriteLine("Agent, you are already at the objective.");
                            }
                            //checks the location of the mission objective is obstructed by an obstacle.
                            else if (obstacleCoordinates.Contains((objectX, objectY)))
                            {
                                Console.WriteLine("The objective is blocked by an obstacle and cannot be reached.");
                            }
                            else
                            {
                                Path(agentX, agentY, objectX, objectY);
                            }
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An unexpected error occurred: " + ex.Message);
                        }

                        break;
                    // if the first word is check and displays thehelp message.
                    case "help":
                        Help();
                        break;
                    // if the command is invalid and it displays invalid message.
                    default:
                        Console.WriteLine("Invalid option: " + input + "\nType 'help' to see a list of commands.");
                        break;

                }
            }
        }
        /// <summary>
        /// It displays the help messages in the user interface
        /// </summary>
        private void Help()
        {
            Console.WriteLine("Valid commands are:");
            Console.WriteLine("add guard <x> <y>: registers a guard obstacle");
            Console.WriteLine(
                "add fence <x> <y> <orientation> <length>: registers a fence obstacle. Orientation must be 'east' or 'north'.");
            Console.WriteLine("add sensor <x> <y> <radius>: registers a sensor obstacle");
            Console.WriteLine(
                "add camera <x> <y> <direction>: registers a camera obstacle. Direction must be 'north', 'south', 'east' or 'west'.");
            Console.WriteLine("check <x> <y>: checks whether a location and its surroundings are safe");
            Console.WriteLine("map <x> <y> <width> <height>: draws a text-based map of registered obstacles");
            Console.WriteLine("path <agent x> <agent y> <objective x> <objective y>: finds a path free of obstacles");
            Console.WriteLine("help: displays this help message");
            Console.WriteLine("exit: closes this program");
        }
// Task 2 implementing the obstacles 
        /// <summary>
        /// Abstract class fro obstacle(Inherited by all other obstacles)
        /// </summary>
        public abstract class Obstacle
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
       //Class for a guard obstacle
        private class GuardObstacle : Obstacle
        {
           
        }
        //Class for fence obstacle 
        private class FenceObstacle : Obstacle
        {
            public string? orientation { get; set; }// direction of the fence 
            public int Length { get; set; }// length of the fence 
        }
        //class for Sensor obstacle
        private class SensorObstacle : Obstacle
        {
            public double Range { get; set; }
        /// <summary>
        /// Determines is a coordinate within the range .
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
            public bool IsWithinRange(int x, int y)
            {
                double distance = CalculateDistance(X, Y, x, y);
                return distance <= Range;
            }
            /// <summary>
            /// Caluculate the distance two points(Uses pythagorean formula for that )
            /// </summary>
            /// <param name="x1"></param>
            /// <param name="y1"></param>
            /// <param name="x2"></param>
            /// <param name="y2"></param>
            /// <returns>returns the distance between two points</returns>
            private double CalculateDistance(int x1, int y1, int x2, int y2)
            {
                int dx = x2 - x1;
                int dy = y2 - y1;
                return Math.Sqrt(dx * dx + dy * dy);
            }
            /// <summary>
            /// A method to store the covered values 
            /// </summary>
            /// <returns>Return the coordinates covered by Sensor</returns>

            public List<(int X, int Y)> GetCoveredCoordinates()
            {
                //list of covered coordinates and finding the max/min range of x and y covered by the sensor 
                List<(int X, int Y)> coveredCoordinates = new List<(int X, int Y)>();
                int minX = (int)Math.Floor(X - Range);
                int maxX = (int)Math.Ceiling(X + Range);
                int minY = (int)Math.Floor(Y - Range);
                int maxY = (int)Math.Ceiling(Y + Range);
                // Loop through each x and y value within the range
                for (int i = minX; i <= maxX; i++)
                {
                    for (int j = minY; j <= maxY; j++)
                    {
                        if (IsWithinRange(i, j))
                        {
                            // If the coordinate is within range, add it to the list of covered coordinates
                            coveredCoordinates.Add((i, j));
                        }
                    }
                }

                return coveredCoordinates;
            }
        }
        /// <summary>
        /// Class for representing the camera
        /// </summary>
        private class CameraObstacle : Obstacle
        {
            public string? Direction { get; set; }//Direction of camera facing 
            private const int DefaultGridSize = 40;// default size of the grid 
            /// <summary>
            /// Method for  getting the coordinates covered by camera 
            /// </summary>
            /// <param name="gridSize"></param>
            /// <returns>it returns the covered coordinates </returns>
            public List<(int X, int Y)> GetCoveredCoordinates(int gridSize = DefaultGridSize)
            {
                List<(int X, int Y)> coveredCoordinates = new List<(int X, int Y)>();
               //Finding the coverage based on direction of the camera and add tat in to a list 
                switch (Direction.ToLower())
                {
                    case "north":
                        for (int i = 1; i < gridSize; i++)// iterate according to the grid size
                        {
                            for (int j = -i; j <= i; j++)
                            {
                                coveredCoordinates.Add((X + j, Y + i));
                            }
                        }

                        break;
                    case "south":
                        for (int i = 1; i < gridSize; i++)
                        {
                            for (int j = -i; j <= i; j++)
                            {
                                coveredCoordinates.Add((X + j, Y - i));
                            }
                        }

                        break;
                    case "east":
                        for (int i = 1; i < gridSize; i++)
                        {
                            for (int j = -i; j <= i; j++)
                            {
                                coveredCoordinates.Add((X + i, Y + j));
                            }
                        }

                        break;
                    case "west":
                        for (int i = 1; i < gridSize; i++)
                        {
                            for (int j = -i; j <= i; j++)
                            {
                                coveredCoordinates.Add((X - i, Y + j));
                            }
                        }

                        break;
                }

                return coveredCoordinates;
            }
        }

        /// <summary>
        /// It adds the guard into the obstacle map
        /// </summary>
        /// <param name="x">The x coordinate of guard's position</param>
        /// <param name="y">The y cooridnate of guard's position</param>
        private void AddGuard(int x, int y)
        {
            obstacles.Add(new GuardObstacle { X = x, Y = y });
            obstacleCoordinates.Add((x, y));// Add the guard obstacle coordinate to the list
            // Print the message 
            Console.WriteLine("Successfully added guard obstacle.");
        }

        /// <summary>
        /// This adds the fence into the obstacle map.
        /// </summary>
        /// <param name="x">It represents the x coordinate of the fence starting position</param>
        /// <param name="y">It represents the y coordinate of the fence starting position</param>
        /// <param name="orientation">This shows the direction of the fence </param>
        /// <param name="length">It represents the length of the fence </param>
        private void AddFence(int x, int y, string orientation, int length)
        {
            // Iterate according to the length of the fence 
            for (int i = 0; i < length; i++)
            {
                switch (orientation)
                {
                    // if the orientation is east it will increase the x coordinate by 1 unit and add the value to obstacle coordinates
                    case "east":
                        obstacles.Add(
                            new FenceObstacle { X = x + i, Y = y, orientation = orientation, Length = length });
                        obstacleCoordinates.Add((x + i, y));
                        break;
                    // if the orientation is west it will decrease the x coordinate by 1 unit and add the value to obstacle coordinates
                    case "west":
                        obstacles.Add(
                            new FenceObstacle { X = x - i, Y = y, orientation = orientation, Length = length });
                        obstacleCoordinates.Add((x - i, y));
                        break;
                    // if the orientation is north it will increase the y coordinate by 1 unit and add the value to obstacle coordinates
                    case "north":
                        obstacles.Add(
                            new FenceObstacle { X = x, Y = y + i, orientation = orientation, Length = length });
                        obstacleCoordinates.Add((x, y + i));
                        break;
                    // if the orientation is south it will decrease the x coordinate by 1 unit and add the value to obstacle coordinates
                    case "south":
                        obstacles.Add(
                            new FenceObstacle { X = x, Y = y - i, orientation = orientation, Length = length });
                        obstacleCoordinates.Add((x, y - i));
                        break;
                }
            }
            // Print the message if the input parameters are correct
            Console.WriteLine("Successfully added fence obstacle.");
        }

        /// <summary>
        /// This adds the sensor into obstacle map
        /// </summary>
        /// <param name="x">The x coordinate of sensor's position</param>
        /// <param name="y">The y coordinate of sensor's position </param>
        /// <param name="range">The range is the area covered by the sensor</param>
        private void AddSensor(int x, int y, double range)
        {
            SensorObstacle sensor = new SensorObstacle { X = x, Y = y, Range = range };
            obstacles.Add(sensor);
            obstacleCoordinates.Add((x, y)); // Add the sensor's position to the obstacleCoordinates list
            // Adding the coordinates of area covered by the sensor into a list
            foreach (var coord in sensor.GetCoveredCoordinates())
            {
                obstacleCoordinates.Add(coord);
            }
            //Print the message if the input parameters are correct
            Console.WriteLine("Successfully added sensor obstacle.");
        }

        /// <summary>
        /// This add the camera in the obstacle map
        /// </summary>
        /// <param name="x">The x coordinate of camera's position</param>
        /// <param name="y">The y coordinate of camera's position</param>
        /// <param name="direction">It is the direction is where camera is facing </param>
        private void AddCamera(int x, int y, string direction)
        {
            var camera = new CameraObstacle { X = x, Y = y, Direction = direction };
            obstacles.Add(camera);
            obstacleCoordinates.Add((x, y)); // Add the camera position to the obstacleCoordinates list
            // Add the coordinates of area the camera covered
            foreach (var coord in camera.GetCoveredCoordinates())
            {
                obstacleCoordinates.Add(coord);
            }
            // Print the message if the input parameters are correct
            Console.WriteLine("Successfully added camera obstacle");
        }

       

        /// <summary>
        /// It helps to find the safe direction the around the agent where there is no obstacles 
        /// </summary>
        /// <param name="x">The x coordinates of agent location </param>
        /// <param name="y">The y coordinates of agent location</param>
        private void CheckLocation(int x, int y)
        {
            //Task 3 finding the safe directions for the agent
            // Creating an list to store the safe directions
            List<string> safeDirections = new List<string>();
            //Define the directions and their corresponding coordinates
            var directions = new Dictionary<string, (int X, int Y)>
            {
                { "North", (x, y + 1) }, // if it is north,it add 1 to the y coordinate 
                { "East", (x + 1, y) }, //if it is north,it add 1 to the x coordinate
                { "South", (x, y - 1) }, //if it is north,it substract 1 to the y coordinate
                { "West", (x - 1, y) } //if it is north,it substract 1 to the x coordinate
            };
            // Loop through each direction
            foreach (var direction in directions)
            {
                // Intialize each direction is safe 
                bool isSafe = true;
                // Loop through the list of obstacle coordinates 
                foreach (var obstacleCoord in obstacleCoordinates)
                {
                    // If any direction coordinates in Obsactles it will break 
                    if (obstacleCoord.X == direction.Value.X && obstacleCoord.Y == direction.Value.Y)
                    {
                        isSafe = false;
                        break;
                    }
                }

                // if the direction is safe it will add that direction to safe directions 
                if (isSafe)
                {
                    safeDirections.Add(direction.Key);
                }
            }

            // Checking the safe direction list 
            if (safeDirections.Count > 0)
            {
                //If the list contains any directions and it will print the direction
                Console.WriteLine("\nYou can safely take any of the following directions:");
                foreach (var direction in safeDirections)
                {
                    Console.WriteLine(direction);
                }
            }
            // if there is no directions in safe direction, so agent can't move and prints message for that
            else
            {
                Console.WriteLine("\nYou cannot safely move in any direction. Abort mission.");
            }
        }

        /// <summary>
        /// It display a map of current obstacles in the system 
        /// </summary>
        /// <param name="mapX">The x coordinate of bottom left positon(south west) of the map </param>
        /// <param name="mapY">The y coordinate of bettom left position(south west) of the map </param>
        /// <param name="mapWidth">It is width of the map </param>
        /// <param name="mapHeight">It is height of the map</param>
        //Task 5 Displaying the map 
        private void ObstacleMap(int mapX, int mapY, int mapWidth, int mapHeight)
        { 
            // Print the message of the for displaying the map
            Console.WriteLine("\nHere is a map of obstacles in the selected region:");
            // Iterating through each row of the map
            for (int y = mapY + mapHeight - 1; y >= mapY; y--)
            {
                // Loop through each column of the map
                for (int x = mapX; x < mapX + mapWidth; x++)
                {
                    bool isObstacle = false;
                    // Loop through each obstacle in the list of obstacles
                    foreach (var obstacle in obstacles)
                    {
                        // Check if any obstacle is preset in current position
                        if (obstacle.X == x && obstacle.Y == y)
                        {
                            //Check the obstacle is guard 
                            if (obstacle is GuardObstacle)
                            {
                                Console.Write("G");//Then Write "G" for representing the guard in msp.
                            }
                            //Check the obstacle is fence
                            else if (obstacle is FenceObstacle)
                            {
                                Console.Write("F");// Then write "F" for representing the fence in the map.
                            }
                            //Check the obstacle is Sensor
                            else if (obstacle is SensorObstacle)// Then write "S" for representing the Sensor in the map.
                            {
                                Console.Write("S");
                            }
                            //Check the obstacle is camera
                            else if (obstacle is CameraObstacle)
                            {
                                Console.Write("C");// Then write "C" for representing the camera in the map.
                            }

                            isObstacle = true;
                            break;
                        }
                        //Checking if the coordinates is covered by camera
                        else if (obstacle is CameraObstacle camera && camera.GetCoveredCoordinates().Contains((x, y)))
                        {
                            Console.Write("C");// Then write "C" for representing the camera in the map.
                            isObstacle = true;
                            break;
                        }
                        //Checking if the coordinates is covered by sensor
                        else if (obstacle is SensorObstacle sensor && sensor.GetCoveredCoordinates().Contains((x, y)))
                        {
                            Console.Write("S");// Then write "S" for representing the Sensor in the map.
                            isObstacle = true;
                            break;
                        }
                    }
                    //If there no obstacles then print dot(.) in that position
                    if (!isObstacle)
                    {
                        Console.Write(".");
                    }
                }

                Console.WriteLine();
            }
        }
        /// <summary>
        /// It shows the path of agent to the goal without obstructing the obstacle using BFS algorithm
        /// </summary>
        /// <param name="agentX">The X coordinate of agent position </param>
        /// <param name="agentY">The Y coordinate of agent position</param>
        /// <param name="objectiveX">The X coordinate of goal</param>
        /// <param name="objectiveY">The Y coordinate of goal</param>
        //Task 6 finding the safe path for agent to reach the goal
        private void Path(int agentX, int agentY, int objectiveX, int objectiveY)
        { 
            // define the directions
            var directions = new List<(int X, int Y, string Dir)>
            {
                (0, 1, "north"),
                (1, 0, "east"),
                (0, -1, "south"),
                (-1, 0, "west")
            };
            // Initialize a queue to store the coordinates need to be visited 
            var queue = new Queue<(int X, int Y, List<(string Dir, int Distance)>)>();
            // Initialize list store the visited coordinates 
            var visited = new HashSet<(int X, int Y)>();
            // adding the starting position to the queue(agent location)
            queue.Enqueue((agentX, agentY, new List<(string Dir, int Distance)>()));
            // adding the current location to the visited list
            visited.Add((agentX, agentY));
            // starting a while loop to find the safe path
            while (queue.Count > 0)
            {
                var (currentX, currentY, currentPath) = queue.Dequeue();
                //Checking all the directions
                foreach (var (dx, dy, direction) in directions)
                {
                    //finding the next position according to the current position
                    int nextX = currentX + dx;
                    int nextY = currentY + dy;
                    //checking if the next coordinates is the goal
                    if (nextX == objectiveX && nextY == objectiveY)
                    {
                        var finalPath = new List<(string Dir, int Distance)>(currentPath);
                        if (finalPath.Count > 0 && finalPath.Last().Dir == direction)
                        {
                            finalPath[finalPath.Count - 1] = (direction, finalPath.Last().Distance + 1);
                        }
                        else
                        {
                            finalPath.Add((direction, 1));
                        }
                        // Print the path to goal
                        Console.WriteLine("\nThe following path will take you to the objective:");
                        foreach (var (dir, dist) in finalPath)
                        { 
                            // Singular 
                            if (dist == 1)
                            {
                                Console.WriteLine($"Head {dir} for {dist} klick.");
                            }
                            // plural
                            else
                            {
                                Console.WriteLine($"Head {dir} for {dist} klicks.");
                            }

                        }

                        return;
                    }
                    // check if the current position is already visited and  obsactles in that position by checking obstacle coordinate list
                    if (!visited.Contains((nextX, nextY)) && !obstacleCoordinates.Contains((nextX, nextY)))
                    {
                        var newPath = new List<(string Dir, int Distance)>(currentPath);
                        if (newPath.Count > 0 && newPath.Last().Dir == direction)
                        {
                            newPath[newPath.Count - 1] = (direction, newPath.Last().Distance + 1);
                        }
                        else
                        {
                            newPath.Add((direction, 1));
                        }

                        queue.Enqueue((nextX, nextY, newPath));
                        visited.Add((nextX, nextY));
                    }
                }
            }
            //Display message if there is no safe path to goal
            Console.WriteLine("There is no safe path to the objective.");
        }

    }
}