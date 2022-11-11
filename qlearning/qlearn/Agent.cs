

namespace QLearning{

	public class Agent
	{
		protected int x;
		protected int y;
		

		public Agent()
		{
			x = 0;
			y = 0;
			
		}

		public Agent(int x, int y)
		{
			this.x = x;
			this.y = y;
			
		}

		public void UpdatePosition(Action action)
		{
			switch(action){
				case Action.Up:
					y = y - 1;
					break;
				case Action.Down:
					y = y + 1;
					break;
				case Action.Left:
					x = x - 1;
					break;
				case Action.Right:
					x = x + 1;
					break;
			}
		}

		public virtual Action ChooseAction(int state){
			// Choose a random action
			Array values = Enum.GetValues(typeof(Action));
			Random random = new Random();
			Action randomAction = (Action)values.GetValue(random.Next(values.Length));
			return randomAction;
		}

		// Getters and Setters
		public int X{get{return x;} set{x = value;}}
		public int Y{get{return y;} set{y = value;}}
	}
}