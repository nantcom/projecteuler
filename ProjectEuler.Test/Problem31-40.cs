using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Text;
using System.Dynamic;
using NantCom;
using NantCom.DataStructure.Tree;

namespace ProjectEuler.Test
{

    [TestClass]
    public class ProblemsOneToTen
    {

        [TestMethod]
        [TestCategory("31-40")]
        public void Problem31()
        {
            /*In England the currency is made up of pound, £, and pence, p, and there are eight coins in general circulation:

            1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).

            It is possible to make £2 in the following way:

            1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p

            How many different ways can £2 be made using any number of coins?
            */
            var target = 200;
            var allPossible =
                              from twoPound in 0.To(target / 200)
                              from onePound in 0.To((target - twoPound * 200) / 100)
                              from fiftyP in 0.To((target - onePound * 100) / 50) // if one pound is used, only 2 fifty p can be used
                              from twentyP in 0.To((target - fiftyP * 50) / 20)
                              from tenP in 0.To((target - twentyP * 20) / 10)
                              from fiveP in 0.To((target - tenP * 10) / 5)
                              from twoP in 0.To((target - fiveP * 5) / 2)
                              from oneP in 0.To((target - twoP * 2) / 1)
                              let total =
                                    oneP * 1 +
                                    twoP * 2 +
                                    fiveP * 5 +
                                    tenP * 10 +
                                    twentyP * 20 +
                                    fiftyP * 50 +
                                    onePound * 100 +
                                    twoPound * 200
                              where total == 200
                              let print = string.Format("2Px{0},1Px{1},50px{2},20px{3},10px{4},5px{5},2px{6},1px{7}", twoPound, onePound, fiftyP, twentyP, tenP, fiveP, twoP, oneP).Print()
                             
                              select 1;


            var totalWays = allPossible.Sum();

        }
    }
}
