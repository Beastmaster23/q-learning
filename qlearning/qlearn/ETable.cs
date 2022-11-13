

namespace QLearning{


	public class ETable
	{
		private Dictionary<int, Dictionary<Action, double>> eTable;
		private int numStates;

		public ETable(int numStates){
			this.numStates = numStates;
			eTable = new Dictionary<int, Dictionary<Action, double>>();
			for (int i = 0; i < numStates; i++){
				eTable[i] = new Dictionary<Action, double>();
				foreach (Action action in Enum.GetValues(typeof(Action))){
					eTable[i][action] = 0;
				}
			}
		}

		public void Clear(){
			for (int i = 0; i < numStates; i++){
				foreach (Action action in Enum.GetValues(typeof(Action))){
					eTable[i][action] = 0;
				}
			}
		}

		public void Update(int state, Action action, double value){
			eTable[state][action] = value;
		}

		public double Get(int state, Action action){
			return eTable[state][action];
		}

		public double this[int state, Action action]{
			get { return eTable[state][action]; }
			set { eTable[state][action] = value; }
		}
		
	}
}