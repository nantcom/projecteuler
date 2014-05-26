using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NantCom;
using NantCom.Collections;

namespace UnitTestProject1
{
    /// <summary>
    /// Summary description for UtilityTest
    /// </summary>
    [TestClass]
    public class UtilityTest
    {
        [TestMethod]
        public void PriorityQueue()
        {
            PriorityQueue<int> pq = new PriorityQueue<int>(LinqExt.TakeMore);
            pq.Enqueue(1, 1);
            pq.Enqueue(9, 9);
            pq.Enqueue(2, 2);
            pq.Enqueue(5, 5);

            Assert.IsTrue(pq.First() == 9, "Wrong Priority");
            Assert.IsTrue(pq.Dequeue() == 9, "Wrong Priority");
            Assert.IsTrue(pq.Dequeue() == 5, "Wrong Priority");

            pq.ChangePriority(1, 29);
            Assert.IsTrue(pq.First() == 1, "Wrong Priority, 1 should be first priority now with priority 29");

            pq.Enqueue(3, 2); //add item with same priority
            pq.Enqueue(4, 2); //add item with same priority

            Assert.IsTrue(pq.Last() == 4, "Wrong Priority, 4 should be last");

            pq.Enqueue(30, 30); // mix case
            Assert.IsTrue(pq.First() == 30, "Wrong Priority, 30 should be first priority");

            pq.Enqueue(31, 30); // mix case
            Assert.IsTrue(pq.First() == 30, "Wrong Priority, 30 should still be first priority");

            pq.Enqueue(5, 2); //add item with same priority
            pq.Enqueue(6, 2); //add item with same priority

            Assert.IsTrue(pq.Last() == 6, "Wrong Priority, 6 should be last");

        }
    }
}
