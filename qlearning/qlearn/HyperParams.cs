

namespace QLearning
{
	

	public class HyperParams
	{
		public static double LearningRate = 0.1;
		public static double DiscountFactor = 0.9;
		public static double ExplorationRate = 0.1;
		public static int Episodes = 1000;
		public static int MaxSteps = 100;

		public static double MoveReward = -0.1;
		public static double GoalReward = 10;
		public static double HoleReward = -1;

	}
}