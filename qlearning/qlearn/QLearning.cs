

using System.Drawing;

namespace QLearning
{
	public class QLearningTrainer
	{
		private Random random = new Random();
		private GridWorld gridWorld;
		private double[][] qTable;
		private double[][] states;
		private List<double> rewards;
		private double learningRate;
		private double discountFactor;
		private double explorationRate;

		private int totalGoals;
		private int totalSteps;
		private int totalEpisodes;
		private double totalReward;
		
		
		public QLearningTrainer(GridWorld gridWorld, double learningRate, double discountFactor, double explorationRate = 0.1){
			this.gridWorld = gridWorld;
			this.learningRate = learningRate;
			this.discountFactor = discountFactor;
			this.explorationRate = explorationRate;
			rewards = new List<double>();
			int numStates = gridWorld.Size * gridWorld.Size;
			int numActions = Enum.GetNames(typeof(Action)).Length;
			qTable = new double[numStates][];
			states = new double[numStates][];
			for (int i = 0; i < numStates; i++){
				qTable[i] = new double[numActions];
				states[i] = new double[numActions];
			}
		}

		public Action ChooseAction(int state){
			// Choose a random action
			if (random.NextDouble() < explorationRate){
				return (Action)random.Next(0, 4);
			}
			// Choose the action with the highest Q-value
			else{
				double maxQ = double.MinValue;
				Action bestAction = Action.Up;
				foreach (Action action in Enum.GetValues(typeof(Action))){
					double q = qTable[state][(int)action];
					if (q > maxQ){
						maxQ = q;
						bestAction = action;
					}
				}
				return bestAction;
			}
		}

		public void UpdateQTable(int state, int nextState, double reward, Action action, Action nextAction){
			double prediction = qTable[state][(int)action];
			double target = reward + discountFactor * qTable[nextState][(int)nextAction];
			qTable[state][(int)action] += learningRate * (target - prediction);
		}

		public void Train(int episodes, int maxSteps){
			totalReward = 0;
			totalSteps = 0;
			totalGoals = 0;
			totalEpisodes = episodes;
			rewards.Clear();
			// Train the agent
			for (int i = 0; i < episodes; i++){
				// Reset the environment
				(int startX, int startY) = gridWorld.Reset();
				int state = gridWorld.GetStateFromPosition(startX, startY);
				Action action = ChooseAction(state);
				// Run the episode
				for (int j = 0; j < maxSteps; j++){
					// Get the next position
					((int nextX, int nextY), double reward, bool done) = gridWorld.Step(action);
					int nextState = gridWorld.GetStateFromPosition(nextX, nextY);
					// Choose the next action
					Action nextAction = ChooseAction(nextState);
					// Update the Q-table
					UpdateQTable(state, nextState, reward, action, nextAction);
					// Update the state and action
					state = nextState;
					action = nextAction;
					totalReward += reward;
					rewards.Add(reward);
					totalSteps++;
					// Check if the episode is done
					if (done){
						// Check if we have reached the goal
						if (gridWorld.IsGoal){
							totalGoals++;
						}
						break;
					}
					
				}
			}
			PlotRewards();
		}

		public void PrintQTable(){
			for (int i = 0; i < qTable.Length; i++){
				Console.Write($"State {i}: ");
				for (int j = 0; j < qTable[i].Length; j++){
					Console.Write($"{qTable[i][j]:0.00} ");
				}
				Console.WriteLine();
			}
		}

		public void SaveQTable(string path){
			using (StreamWriter writer = new StreamWriter(path)){
				for (int i = 0; i < qTable.Length; i++){
					writer.Write($"State {i}: ");
					for (int j = 0; j < qTable[i].Length; j++){
						writer.Write($"{qTable[i][j]:0.00} ");
					}
					writer.WriteLine();
				}
			}
		}

		public void PlotRewards(){
			// Plot the rewards
			var plt = new ScottPlot.Plot(600, 400);
			plt.AddSignal(rewards.ToArray(), sampleRate:1000, label:"Rewards", color:Color.Red);
			plt.Title("Rewards");
			plt.SaveFig("rewards.png");
		}

		public void SaveRewards(string v)
		{
			using (StreamWriter writer = new StreamWriter(v)){
				for (int i = 0; i < rewards.Count; i++){
					writer.WriteLine(rewards[i]);
				}
			}
		}

		// Getters and Setters
		public double[][] QTable{get{return qTable;}}
		public double[][] States{get{return states;}}
		public List<double> Rewards{get{return rewards;}}
		public double TotalReward{get{return totalReward;}}
		public int TotalSteps{get{return totalSteps;}}
		public int TotalGoals{get{return totalGoals;}}
		public int TotalEpisodes{get{return totalEpisodes;}}
		public double AverageReward{get{return totalReward / totalSteps;}}
		public double SuccessRate{get{return (double)totalGoals / totalEpisodes;}}
		
	}
}