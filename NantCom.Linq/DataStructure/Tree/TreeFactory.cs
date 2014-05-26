using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NantCom.DataStructure.Tree
{
    public class TreeFactory
    {
        /// <summary>
        /// Parse the tree in Project Euler's format
        /// return tuple of Root Node and Leaf Nodes (for solving problem)
        /// </summary>
        /// <param name="treeText"></param>
        /// <returns></returns>
        public static Tuple<TreeNode<int>, List<TreeNode<int>>> Parse(string input)
        {
            var row = 0;
            var lastRow = new List<Tree.TreeNode<int>>();
            Tree.TreeNode<int> root = null;
            foreach (var line in input.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var thisRow = new List<Tree.TreeNode<int>>();
                var numbers = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var number in numbers)
                {
                    Tree.TreeNode<int> node = new Tree.TreeNode<int>();
                    node.Value = int.Parse(number);
                    thisRow.Add(node);

                    if (root == null)
                    {
                        root = node;
                    }
                }

                // build tree structure
                for (int i = 0; i < lastRow.Count; i++)
                {
                    lastRow[i].Left = thisRow[i];
                    lastRow[i].Right = thisRow[i + 1];
                }

                lastRow = thisRow;

                row++;
            }

            #region Checking that we build tree correctly

            //var serialized = input.Replace("\r\n", "").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            //var allNumbers = (from number in serialized
            //               select int.Parse(number)).ToList();

            //var allFromTree = root.Discover().ToArray();
            //var check = allNumbers.Zip(allFromTree, (num, node) => num == node.Value).All(b => b);
            //Assert.IsTrue(check, "Some number missing");

            #endregion

            return new Tuple<TreeNode<int>, List<TreeNode<int>>>(root, lastRow);
        }

    }

}
