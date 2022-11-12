
namespace QLearning
{
	class Program
	{
		public static void Main(){
			// GridWorld:
			// 1. n x n grid = n x n states
			// 2. 4 actions: up, down, left, right
			// 3. 4 states: 0, 1, 2, 3
			// 4. 3 rewards: 0.1 for moving , 1 for reaching goal, -1 for hitting wall, 0 for all other states

			// Create a new GridWorld
			(int start_x, int start_y) start = (0, 0);
			(int goal_x, int goal_y) goal = (15, 15);
			(int hole1_x, int hole1_y) hole1 = (1, 1);
			GridWorld gridWorld = new GridWorld(16, goal, start, new (int, int)[]{hole1});
			gridWorld.RandomGoal();
			gridWorld.RandomHoles(1);
			// Create QLearningTrainer
			QLearningTrainer trainer = new QLearningTrainer(gridWorld, HyperParams.LearningRate, HyperParams.DiscountFactor, HyperParams.ExplorationRate);
			// Train the agent
			trainer.Train(HyperParams.Episodes, HyperParams.MaxSteps);
			// Save rewards to file
			trainer.SaveRewards("rewards.csv");
			trainer.SaveQTable("qtable.txt");
			// Test the agent
			QAgent agent = new QAgent(start.start_x, start.start_y, trainer.QTable);
			gridWorld.Agent = agent;
			gridWorld.Reset();
			gridWorld.Play(200);
		}
	}
}
