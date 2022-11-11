using System.Drawing;

namespace QLearning
{
    public class GridWorld
    {
        private Random random = new Random();
        private Agent agent;
        private int size;
        private (int x, int y) goal;
        private (int x, int y) start;
        private (int, int)[] holes;
        private ObjectType[][] grid;
        private bool isDead;
        private bool isGoal;
        private RewardFunction rewardFunction;

        public GridWorld(int size)
        {
            this.size = size;
            grid = new ObjectType[size][];
            for (int i = 0; i < size; i++)
            {
                grid[i] = new ObjectType[size];
            }

            goal = (size - 1, size - 1);
            start = (0, 0);
            holes = new (int, int)[0];
            CreateGrid();
            agent = new Agent(start.x, start.y);
            rewardFunction = RewardFunction.MazeReward;
        }

        public GridWorld(int size, (int, int) goal, (int, int) start, (int, int)[] holes){
            this.size = size;
            this.goal = goal;
            this.start = start;
            this.holes = holes;
            CreateGrid();
            agent = new Agent(start.Item1, start.Item2);
            rewardFunction = RewardFunction.MazeReward;
        }

        public void CreateGrid(){
            grid = new ObjectType[size][];
            for (int i = 0; i < size; i++){
                grid[i] = new ObjectType[size];
                for (int j = 0; j < size; j++){
                    grid[i][j] = ObjectType.Empty;
                }
            }
            grid[goal.Item1][goal.Item2] = ObjectType.Goal;
            foreach ((int, int) hole in holes){
                grid[hole.Item1][hole.Item2] = ObjectType.Wall;
            }
        }

        public (int, int) Reset(){
            agent = new Agent(start.Item1, start.Item2);
            isDead = false;
            isGoal = false;
            CreateGrid();
            return (agent.X, agent.Y);
        }

        public ((int, int), double, bool) Step(Action action){
            int current_state = GetStateFromPosition(agent.X, agent.Y);
            // Get the next position
            (int, int) nextPosition = GetNextPosition(agent.X, agent.Y, action);
            // Get the reward
            double reward = HyperParams.MoveReward;
            
            // Check if the agent has reached the goal or fallen into a hole
            bool done = false;
            if (nextPosition == goal){
                done = true;
                reward = HyperParams.GoalReward;
                isGoal = true;
            }
            foreach ((int, int) hole in holes){
                if (nextPosition == hole){
                    done = true;
                    reward = HyperParams.HoleReward;
                    isDead = true;
                    break;
                }
            }
            // Update the agent's position if we moved
            if (nextPosition != (agent.X, agent.Y)){
                agent.UpdatePosition(action);
            }
            return (nextPosition, reward, done);
        }

        public void Play(int maxSteps){
            bool done = false;
            int current_state = GetStateFromPosition(agent.X, agent.Y);
            Action action = agent.ChooseAction(current_state);
            //Render();
            for (int i = 0; i < maxSteps; i++){
                ((int, int), double, bool) result = Step(action);
                int next_state = GetStateFromPosition(result.Item1.Item1, result.Item1.Item2);
                action = agent.ChooseAction(next_state);
                done = result.Item3;
                //Console.WriteLine($"\nChosen action: {action}");
                //Render();

                if (done){
                    // Check if the agent has reached the goal or fallen into a hole
                    if (result.Item1 == goal){
                        Console.WriteLine("Reached the goal!");
                    }
                    else{
                        Console.WriteLine("Fell into a hole!");
                    }
                    break;
                }
            }
            //Render();
        }
        public (int, int) RandomPositionSafe(ObjectType type){
            int x = random.Next(0, size);
            int y = random.Next(0, size);
            while (grid[x][y] == type){
                x = random.Next(0, size);
                y = random.Next(0, size);
            }
            return (x, y);
        }
        public void RandomGoal(){
            goal = RandomPositionSafe(ObjectType.Empty);
            grid[goal.Item1][goal.Item2] = ObjectType.Goal;
        }
        public void RandomHoles(int numHoles){
            holes = new (int, int)[numHoles];
            for (int i = 0; i < numHoles; i++){
                holes[i] = RandomPositionSafe(ObjectType.Empty);
                grid[holes[i].Item1][holes[i].Item2] = ObjectType.Wall;
            }
        }
        public void Render(){
            for (int i = 0; i < size; i++){
                for (int j = 0; j < size; j++){
                    if (agent.X == i && agent.Y == j){
                        Console.Write("A ");
                    }
                    else{
                        Console.Write($"{grid[i][j]} ");
                    }
                }
                Console.WriteLine();
            }
        }
        private (int, int) GetNextPosition(int x, int y, Action action){
            int next_x = x;
            int next_y = y;
            switch (action){
                case Action.Up:
                    next_y = Math.Max(y - 1, 0);
                    break;
                case Action.Down:
                    next_y = Math.Min(y + 1, size - 1);
                    break;
                case Action.Left:
                    next_x = Math.Max(x - 1, 0);
                    break;
                case Action.Right:
                    next_x = Math.Min(x + 1, size - 1);
                    break;
            }

            return (next_x, next_y);
        }

        // Helper functions
        public int GetStateFromPosition(int x, int y){
            return x + y * size;
        }

        public (int, int) GetPositionFromState(int state){
            int x = state % size;
            int y = state / size;
            return (x, y);
        }
    
        // Getters and setters
        public int Size { get => size; set => size = value; }
        public (int, int) Goal { get => goal; set => goal = value; }
        public (int, int) Start { get => start; set => start = value; }
        public (int, int)[] Holes { get => holes; set => holes = value; }
        public Agent Agent { get => agent; set => agent = value; }
        public RewardFunction RewardFunction { get => rewardFunction; set => rewardFunction = value; }
        public ObjectType[][] Grid { get => grid; set => grid = value; }
        public bool IsDead { get => isDead;}
        public bool IsGoal { get => isGoal;}
        public bool IsGameEnd { get => IsDead|| isGoal;}
    }

    public enum Action
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum ObjectType
    {
        Empty,
        Wall,
        Goal,
        Agent,
        Start
    }
}