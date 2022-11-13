

using Action = QLearning.Action;

namespace QLearning_test
{
    [TestClass]
    public class QLearningTest
    {
		private GridWorld gridWorld;
		private QLearningTrainer trainer;

		public QLearningTest()
		{
			(int, int) goal = (3, 3);
			(int, int) start = (0, 0);
			(int, int)[] holes = new (int, int)[]{	(1, 1),	};
			
			gridWorld = new GridWorld(4, goal, start, holes);
			trainer = new QLearningTrainer(gridWorld, 0.1, 0.9, 0.1);
		}
		[TestInitialize]
		public void Setup(){
			(int, int) goal = (3, 3);
			(int, int) start = (0, 0);
			(int, int)[] holes = new (int, int)[]{	(1, 1),	};
			
			gridWorld = new GridWorld(4, goal, start, holes);
			trainer = new QLearningTrainer(gridWorld, HyperParams.LearningRate, HyperParams.DiscountFactor, HyperParams.ExplorationRate);
		}
        [TestMethod]
        public void ChooseActionTest()
		{
			(int start_x, int start_y) start = gridWorld.Reset();
			int startState = gridWorld.GetStateFromPosition(start.start_x, start.start_y);
			Action action = trainer.ChooseAction(startState);
			Assert.IsTrue(action == Action.Up || action == Action.Down || action == Action.Left || action == Action.Right);

		}

		[TestMethod]
		public void TrainTest()
		{
			double highestSuccessRate = 0;
			double highestSuccessRateEpisode = 0;

			for (int i = 0; i < HyperParams.Episodes; i++)
			{
				(int start_x, int start_y) start = gridWorld.Reset();
				int startState = gridWorld.GetStateFromPosition(start.start_x, start.start_y);
				int steps = 0;
				int success = 0;
				while (steps < HyperParams.MaxSteps)
				{
					Action action = trainer.ChooseAction(startState);
					((int, int) nextPos, double reward, bool isDone) = gridWorld.Step(action);
					int nextState = gridWorld.GetStateFromPosition(nextPos.Item1, nextPos.Item2);
					Action nextAction = trainer.ChooseAction(nextState);
					trainer.UpdateQTable(startState, nextState, reward, action, nextAction);
					startState = nextState;
					steps++;
					if (isDone)
					{
						success++;
						break;
					}
				}
				double successRate = (double)success / (double)steps;
				if (successRate > highestSuccessRate)
				{
					highestSuccessRate = successRate;
					highestSuccessRateEpisode = i;
				}
			}
			Console.WriteLine($"Highest success rate: {highestSuccessRate} at episode {highestSuccessRateEpisode}");
			Assert.IsTrue(highestSuccessRate > 0.4);

		}

		[TestMethod]
		public void TestTrainWithHoles()
		{
			(int, int) goal = (3, 3);
			(int, int) start = (0, 0);
			(int, int)[] holes = new (int, int)[]{	(1, 1),	};
			gridWorld = new GridWorld(4, goal, start, holes);
			trainer = new QLearningTrainer(gridWorld, HyperParams.LearningRate, HyperParams.DiscountFactor, HyperParams.ExplorationRate);
			
			double highestSuccessRate = 0;
			double highestAverageReward = 0;

			for (int i = 0; i < 10; i++)
			{
				trainer.Train(1000, 32);
				if (trainer.SuccessRate > highestSuccessRate)
				{
					highestSuccessRate = trainer.SuccessRate;
				}
				if (trainer.AverageReward > highestAverageReward)
				{
					highestAverageReward = trainer.AverageReward;
				}
				// Print the results
				Console.WriteLine("Episode: " + trainer.TotalEpisodes + " Success Rate: " + trainer.SuccessRate + " Average Reward: " + trainer.AverageReward);
			}
			Assert.IsTrue(highestSuccessRate > 0.9);
			Assert.IsTrue(highestAverageReward > 0.05);
		}

		[TestMethod]
		public void TestTrain10()
		{
			double highestSuccessRate = 0;
			double highestAverageReward = 0;
			for (int i = 0; i < 10; i++)
			{
				trainer.Train(1000, 32);
				double successRate = (double)trainer.TotalGoals / trainer.TotalEpisodes;
				double averageReward = trainer.TotalReward / (double)trainer.TotalSteps;
				if (successRate > highestSuccessRate)
				{
					highestSuccessRate = successRate;
				}
				if (averageReward > highestAverageReward)
				{
					highestAverageReward = averageReward;
				}
				// Print the results
				Console.WriteLine("Episode: " + trainer.TotalEpisodes + " Success Rate: " + successRate + " Average Reward: " + averageReward);
			}
			Console.WriteLine("Highest Success Rate: " + highestSuccessRate + " Highest Average Reward: " + highestAverageReward);
			Assert.IsTrue(highestSuccessRate > 0.9);
			Assert.IsTrue(highestAverageReward > 0.05);

		}

		[TestMethod]
		public void TestTrainWithHoles100()
		{
			(int, int) goal = (3, 3);
			(int, int) start = (0, 0);
			(int, int)[] holes = new (int, int)[]{	(1, 1),	};
			gridWorld = new GridWorld(4, goal, start, holes);
			trainer = new QLearningTrainer(gridWorld, HyperParams.LearningRate, HyperParams.DiscountFactor, HyperParams.ExplorationRate);
			
			double highestSuccessRate = 0;
			double highestAverageReward = 0;

			for (int i = 0; i < 10; i++)
			{
				trainer.Train(1000, 32);
				Assert.IsTrue(trainer.TotalEpisodes == 1000);
				double successRate = (double)trainer.TotalGoals / trainer.TotalEpisodes;
				Assert.IsTrue(trainer.SuccessRate == successRate);
				double averageReward = trainer.TotalReward / (double)trainer.TotalSteps;
				Assert.IsTrue(trainer.AverageReward == averageReward);

				if (successRate > highestSuccessRate)
				{
					highestSuccessRate = successRate;
				}
				if (averageReward > highestAverageReward)
				{
					highestAverageReward = averageReward;
				}

				// Print the results
				Console.WriteLine($"Success rate: {successRate}");
				Console.WriteLine($"Average reward: {averageReward}");
			}
			Console.WriteLine($"Highest success rate: {highestSuccessRate}");
			Console.WriteLine($"Highest average reward: {highestAverageReward}");

			Assert.IsTrue(highestSuccessRate > 0.9);
			Assert.IsTrue(highestAverageReward > 0.05);
		}

		[TestMethod]
		public void TestTrainWithHoles1000()
		{
			(int, int) goal = (3, 3);
			(int, int) start = (0, 0);
			(int, int)[] holes = new (int, int)[]{	(1, 1),	};
			gridWorld = new GridWorld(4, goal, start, holes);
			trainer = new QLearningTrainer(gridWorld, HyperParams.LearningRate, HyperParams.DiscountFactor, HyperParams.ExplorationRate);
			double highestSuccessRate = 0;
			double highestAverageReward = 0;
			for(int i = 0; i < 10; i++)
			{
				trainer.Train(1000, 1000);
				Assert.IsTrue(trainer.TotalEpisodes == 1000);
				double successRate = (double)trainer.TotalGoals / trainer.TotalEpisodes;
				if (successRate > highestSuccessRate)
					highestSuccessRate = successRate;
				double averageReward = trainer.TotalReward / (double)trainer.TotalSteps;
				if (averageReward > highestAverageReward)
					highestAverageReward = averageReward;
				// Print out the results
				Console.WriteLine($"Success rate: {successRate}");
				Console.WriteLine($"Average reward: {averageReward}");
				Assert.IsTrue(successRate == trainer.SuccessRate);
				Assert.IsTrue(averageReward == trainer.AverageReward);
			}
			
			Console.WriteLine($"Highest success rate: {highestSuccessRate}");
			Console.WriteLine($"Highest average reward: {highestAverageReward}");
			Assert.IsTrue(highestSuccessRate > 0.9);
			Assert.IsTrue(highestAverageReward > 0.07);
		}
	}
}
