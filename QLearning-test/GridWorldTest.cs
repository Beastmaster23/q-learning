using Action = QLearning.Action;

namespace QLearning_test
{
    [TestClass]
    public class GridWorldTest
    {
        private GridWorld gridWorld;
        [TestInitialize]
        public void Setup()
        {
            (int, int) goal = (3, 3);
            (int, int) start = (0, 0);
            (int, int)[] holes = new (int, int)[0];
            gridWorld = new GridWorld(4, goal, start, holes);
        }

        [TestMethod]
        public void GetStateFromPositionTest()
        {
            int state = gridWorld.GetStateFromPosition(0, 0);
            Assert.IsTrue(state == 0);
            state = gridWorld.GetStateFromPosition(3, 3);
            Assert.IsTrue(state == 15);
        }

        [TestMethod]
        public void GetPositionFromStateTest()
        {
            gridWorld.Reset();
            (int x, int y) position = gridWorld.GetPositionFromState(0);
            Assert.IsTrue(position.x == 0 && position.y == 0);
            position = gridWorld.GetPositionFromState(15);
            Assert.IsTrue(position.x == 3 && position.y == 3);
        }

        [TestMethod]
        public void ResetTest()
        {
            gridWorld.Reset();
            (int x, int y) position = gridWorld.Reset();
            Assert.IsTrue(position.x == 0 && position.y == 0);
        }

        [TestMethod]
        public void StepTest()
        {
            gridWorld.Reset();
            (int x, int y) position = gridWorld.Reset();
            Assert.IsTrue(position.x == 0 && position.y == 0);
            (int x, int y) nextPos;
            double reward;
            bool isDone;
            (nextPos, reward, isDone) = gridWorld.Step(Action.Up);
            Assert.IsTrue(nextPos.x == 0 && nextPos.y == 0);
            Assert.IsTrue(reward == HyperParams.MoveReward);
            Assert.IsTrue(isDone == false);
            (nextPos, reward, isDone) = gridWorld.Step(Action.Right);
            Assert.IsTrue(nextPos.x == 1 && nextPos.y == 0);
            Assert.IsTrue(reward == HyperParams.MoveReward);
            Assert.IsTrue(isDone == false);

        }

        [TestMethod]
        public void IsDoneTest()
        {
            (int x, int y) position = gridWorld.Reset();
            (int x, int y) nextPos;
            double reward;
            bool isDone;
            (nextPos, reward, isDone) = gridWorld.Step(Action.Down);
            Assert.IsTrue(isDone == false);
            (nextPos, reward, isDone) = gridWorld.Step(Action.Down);
            Assert.IsTrue(isDone == false);
            (nextPos, reward, isDone) = gridWorld.Step(Action.Down);
            Assert.IsTrue(isDone == false);
            (nextPos, reward, isDone) = gridWorld.Step(Action.Down);
            Assert.IsTrue(isDone == false);
            (nextPos, reward, isDone) = gridWorld.Step(Action.Right);
            Assert.IsTrue(isDone == false);
            (nextPos, reward, isDone) = gridWorld.Step(Action.Right);
            Assert.IsTrue(isDone == false);
            (nextPos, reward, isDone) = gridWorld.Step(Action.Right);
            Assert.IsTrue(isDone == true);
        }
    }
}
