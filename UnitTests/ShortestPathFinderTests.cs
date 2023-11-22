using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Collections.Generic;

namespace ShortestPath_UnitTest
{
    [TestClass]
    public class ShortestPathFinderTests
    {
        [TestMethod]
        public void FindShortestPath_ShouldReturnCorrectPathAndDistance()
        {
            // Arrange
            List<Nodes> graphNodes = new List<Nodes>
        {
            new Nodes("A"),
            new Nodes("B"),
            new Nodes("C"),
            new Nodes("D"),
            new Nodes("E"),
        };

            graphNodes[0].Neighbors.Add(graphNodes[1], 2);
            graphNodes[0].Neighbors.Add(graphNodes[2], 4);
            graphNodes[1].Neighbors.Add(graphNodes[2], 1);
            graphNodes[1].Neighbors.Add(graphNodes[3], 7);
            graphNodes[2].Neighbors.Add(graphNodes[4], 3);
            graphNodes[3].Neighbors.Add(graphNodes[4], 1);

            ShortestPathFinder pathFinder = new ShortestPathFinder();

            // Act
            ShortestPathData result = pathFinder.ShortestPath("A", "D", graphNodes);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new List<string> { "A", "B", "C", "D" }, result.NodeNames);
            Assert.AreEqual(4, result.Distance);
        }
    }
}
