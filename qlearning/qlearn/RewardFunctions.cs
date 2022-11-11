

namespace QLearning
{
public interface RewardFunction
	{
		public static MazeReward MazeReward = new MazeReward();
		double GetReward(GridWorld gridWorld, int state, int action);
	}

	public class MazeReward: RewardFunction
	{
		public double GetReward(GridWorld gridWorld, int state, int action){
			(int x, int y) = gridWorld.GetPositionFromState(state);
			double move_reward = HyperParams.MoveReward;
			double goal_reward = HyperParams.GoalReward;
			double hole_reward = HyperParams.HoleReward;
			// Check if the agent is at the goal
			if (x == gridWorld.Goal.Item1 && y == gridWorld.Goal.Item2){
				return goal_reward;
			}
			// Check if the agent is in a hole
			foreach ((int hole_x, int hole_y) hole in gridWorld.Holes){
				if (x == hole.hole_x && y == hole.hole_y){
					return hole_reward;
				}
			}
			return move_reward;
		}

		
	}

	public interface PolicyFunction
	{
		int GetAction(int state);
	}
}