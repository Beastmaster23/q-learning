using System;

namespace QLearning{

	public class Maze
	{
		private Random random;
		private int width;
		private int height;
		private Cell[,] cells;

		public Maze(int width, int height){
			random = new Random();
			this.width = width;
			this.height = height;
			cells = new Cell[width, height];
			for (int x = 0; x < width; x++){
				for (int y = 0; y < height; y++){
					cells[x, y] = new Cell(x, y);
				}
			}
		}

		public void Generate(){
			// Create a list of all cells
			List<Cell> unvisited = new List<Cell>();
			for (int x = 0; x < width; x++){
				for (int y = 0; y < height; y++){
					unvisited.Add(cells[x, y]);
				}
			}

			// Choose the initial cell, mark it as visited and push it to the stack
			Cell current = cells[0, 0];
			unvisited.Remove(current);

			// While there are unvisited cells
			while (unvisited.Count > 0){
				// If the current cell has any neighbours which have not been visited
				List<Cell> neighbours = GetNeighbours(current);
				if (neighbours.Count > 0){
					// Choose randomly one of the unvisited neighbours
					int index = random.Next(0, neighbours.Count);
					Cell next = neighbours[index];
					// Remove the wall between the current cell and the chosen cell
					RemoveWall(current, next);
					// Push the current cell to the stack
					// Make the chosen cell the current cell and mark it as visited
					current = next;
					unvisited.Remove(current);
				}
				else{
					// Pop a cell from the stack
					// Make it the current cell
				}
			}
		}

		public List<Cell> GetNeighbours(Cell cell){
			List<Cell> neighbours = new List<Cell>();
			if (cell.X > 0)
			{
				neighbours.Add(cells[cell.X - 1, cell.Y]);
			}

			if (cell.X < width - 1)
			{
				neighbours.Add(cells[cell.X + 1, cell.Y]);
			}

			if (cell.Y > 0)
			{
				neighbours.Add(cells[cell.X, cell.Y - 1]);
			}

			if (cell.Y < height - 1)
			{
				neighbours.Add(cells[cell.X, cell.Y + 1]);
			}

			return neighbours;
		}

		private void RemoveWall(Cell current, Cell next){
			if (current.X == next.X){
				if (current.Y > next.Y){
					current.Walls[0] = false;
					next.Walls[2] = false;
				}
				else{
					current.Walls[2] = false;
					next.Walls[0] = false;
				}
			}
			else{
				if (current.X > next.X){
					current.Walls[3] = false;
					next.Walls[1] = false;
				}
				else{
					current.Walls[1] = false;
					next.Walls[3] = false;
				}
			}
		}

		public ObjectType[][] ConvertToGrid(){
			ObjectType[][] result = new ObjectType[width][];
			for (int x = 0; x < width; x++){
				result[x] = new ObjectType[height];
				for (int y = 0; y < height; y++){
					result[x][y] = cells[x, y].Type;
				}
			}

			return result;
		}
		public Cell this[int x, int y]{
			get { return cells[x, y]; }
			set { cells[x, y] = value; }
		}
	}

	public class Cell{
		private int x;
		private int y;
		private ObjectType type;
		private bool[] walls;
		public Cell(int x, int y){
			this.x = x;
			this.y = y;
			this.type = ObjectType.Empty;
			walls = new bool[4];
		}

		// gettters and setters
		public int X{
			get { return x; }
			set { x = value; }
		}

		public int Y{
			get { return y; }
			set { y = value; }
		}

		public ObjectType Type{
			get { return type; }
			set { type = value; }
		}

		public bool[] Walls{
			get { return walls; }
			set { walls = value; }
		}
	}
}