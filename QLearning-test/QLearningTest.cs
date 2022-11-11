

using Action = QLearning.Action;

namespace QLearning_test
{
    [TestClass]
    public class QLearningTest
    {
		private GridWorld gridWorld;
		private QLearningTrainer trainer;
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
		public void UpdateQTableTest(){
			(int start_x, int start_y) start = gridWorld.Reset();
			int startState = gridWorld.GetStateFromPosition(start.start_x, start.start_y);
			Action action = trainer.ChooseAction(startState);
			((int, int) nextPos, double reward, bool isDone) = gridWorld.Step(action);
			int nextState = gridWorld.GetStateFromPosition(nextPos.Item1, nextPos.Item2);
			Action nextAction = trainer.ChooseAction(nextState);
			trainer.UpdateQTable(startState, nextState, reward, action, nextAction);
			Assert.IsTrue(trainer.QTable[startState][(int)action] != 0);

		}

		[TestMethod]
		public void TrainTest()
		{
			trainer.Train(1000, 1000);
			Assert.IsTrue(trainer.TotalEpisodes == 1000);
			// Make sure the success rate is above 90% and correct calculated
			double successRate = (double)trainer.TotalGoals / trainer.TotalEpisodes;
			Assert.IsTrue(successRate > 0.9);
			Assert.IsTrue(trainer.SuccessRate == successRate);
			// Make sure the average reward is above 0.5 and correct calculated
			double averageReward = trainer.TotalReward / (double) trainer.TotalSteps;
			Assert.IsTrue(averageReward > 0.5);
			Assert.IsTrue(trainer.AverageReward == averageReward);
		}

		[TestMethod]
		public void TestTrainWithHoles()
		{
			(int, int) goal = (3, 3);
			(int, int) start = (0, 0);
			(int, int)[] holes = new (int, int)[]{	(1, 1),	};
			gridWorld = new GridWorld(4, goal, start, holes);
			trainer = new QLearningTrainer(gridWorld, HyperParams.LearningRate, HyperParams.DiscountFactor, HyperParams.ExplorationRate);
			trainer.Train(1000, 1000);
			Assert.IsTrue(trainer.TotalEpisodes == 1000);
			// Make sure the success rate is above 90% and correct calculated
			double successRate = (double)trainer.TotalGoals / trainer.TotalEpisodes;
			Assert.IsTrue(successRate > 0.9);
			Assert.IsTrue(trainer.SuccessRate == successRate);
			// Make sure the average reward is above 0.5 and correct calculated
			double averageReward = trainer.TotalReward / (double)trainer.TotalSteps;
			Assert.IsTrue(averageReward > 0.5);
			Assert.IsTrue(trainer.AverageReward == averageReward);
		}

		[TestMethod]
		public void TestTrain10()
		{
			trainer.Train(10, 32);
			Assert.IsTrue(trainer.TotalEpisodes == 10);
			// Make sure the success rate is above 90% and correct calculated
			double successRate = (double)trainer.TotalGoals / trainer.TotalEpisodes;
			Assert.IsTrue(successRate > 0.3);
			Assert.IsTrue(trainer.SuccessRate == successRate);
		}

		[TestMethod]
		public void TestTrainWithHoles100()
		{
			(int, int) goal = (3, 3);
			(int, int) start = (0, 0);
			(int, int)[] holes = new (int, int)[]{	(1, 1),	};
			gridWorld = new GridWorld(4, goal, start, holes);
			trainer = new QLearningTrainer(gridWorld, HyperParams.LearningRate, HyperParams.DiscountFactor, HyperParams.ExplorationRate);
			trainer.Train(100, 100);
			Assert.IsTrue(trainer.TotalEpisodes == 100);
			// Make sure the success rate is above 90% and correct calculated
			double successRate = (double)trainer.TotalGoals / trainer.TotalEpisodes;
			Assert.IsTrue(successRate > 0.5);
			Assert.IsTrue(trainer.SuccessRate == successRate);
			// Make sure the average reward is above 0.5 and correct calculated
			double averageReward = trainer.TotalReward / (double)trainer.TotalSteps;
			Assert.IsTrue(averageReward > 0.5);
			Assert.IsTrue(trainer.AverageReward == averageReward);
		}

		[TestMethod]
		public void TestTrainWithHoles1000()
		{
			(int, int) goal = (3, 3);
			(int, int) start = (0, 0);
			(int, int)[] holes = new (int, int)[]{	(1, 1),	};
			gridWorld = new GridWorld(4, goal, start, holes);
			trainer = new QLearningTrainer(gridWorld, HyperParams.LearningRate, HyperParams.DiscountFactor, HyperParams.ExplorationRate);
			trainer.Train(1000, 32);
			Assert.IsTrue(trainer.TotalEpisodes == 1000);
			// Make sure the success rate is above 90% and correct calculated
			double successRate = (double)trainer.TotalGoals / trainer.TotalEpisodes;
			Assert.IsTrue(successRate > 0.9);
			Assert.IsTrue(trainer.SuccessRate == successRate);
			// Make sure the average reward is above 0.5 and correct calculated
			double averageReward = trainer.TotalReward / (double)trainer.TotalSteps;
			Assert.IsTrue(averageReward > 0.5);
			Assert.IsTrue(trainer.AverageReward == averageReward);
		}
	}
}
