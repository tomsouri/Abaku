﻿using System;

using CommonTypes;

namespace InteractiveConsoleTests
{

    class Program
    {

        static void OpManaging()
        {
            OperationManagingTest.UniversalFactorsFormulaIdentifierInteractiveTest();
            OperationManagingTest.FormulaIdentifierInteractiveTest();
        }
        static void BoardManaging()
        {
            BoardManagingTest.BoardMgerTest();
        }
        static void Validation()
        {
            ValidatorTest.Test();
        }
        static void Evaluation()
        {
            EvaluatorTest.Test();
        }
        static void Optimization()
        {
            OptimizerTest.Test();
            //OptimizerTest.TestSequenceGenerator();
        }
        public static void BoardControl()
        {
            BoardControllerTest.Test();
        }
        static void Main(string[] args)
        {
            //OpManaging();
            //BoardManaging();
            //Validation();
            //Evaluation();
            //Optimization();
            BoardControl();
        }
    }
}
