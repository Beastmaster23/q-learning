



namespace QLearning{

	public class QAgent:Agent
	{
		private double[][] qTable;

		public QAgent(int x, int y, int numStates):base(x, y)
		{
			int numActions = Enum.GetNames(typeof(Action)).Length;
			qTable = new double[numStates][];
			for (int i = 0; i < numStates; i++){
				qTable[i] = new double[numActions];
			}
		}

		public QAgent(int x, int y, double[][] qTable):base(x, y)
		{
			this.qTable = qTable;
		}

		public override Action ChooseAction(int state){
			// Choose the action with the highest Q-value
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
}