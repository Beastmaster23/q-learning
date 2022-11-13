

namespace QLearning
{
	

	public class HyperParams
	{
		public static double LearningRate = 0.1;
		public static double DiscountFactor = 0.9;
		public static double ExplorationRate = 0.3;
		public static int Episodes = 1000;
		public static int MaxSteps = 32;

		public static double MoveReward = 0;
		public static double GoalReward = 1;
		public static double HoleReward = -10;

	}
}