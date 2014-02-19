﻿using NeuralLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NumericalExperiment
{
    class TrainingData : DataSet
    {
        public override void Load()
        {
            using (StreamReader sr = new StreamReader(@"\..\..\..\..\DATASET\training.dat"))
            {
                while (true)
                {
                    String line = sr.ReadLine();

                    if (line == null)
                        break;

                    double[] inputs = new double[9];
                    string[] s = line.Split(',');

                    for (int i = 1; i < 10; i++)
                    {
                        inputs[i - 1] = double.Parse(s[i]);
                    }

                    double[] desired = new double[1] { 0 };

                    //Switch inputs
                    if (double.Parse(s[s.Length - 1]) == 2)
                        desired[0] = 0;
                    else
                        desired[0] = 1;

                    this.Add(new DataPoint(inputs, desired));
                }
            }
        }
    }
}
