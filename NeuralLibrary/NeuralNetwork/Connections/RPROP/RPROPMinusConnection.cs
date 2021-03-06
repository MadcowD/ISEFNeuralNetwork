﻿using System;

namespace NeuralLibrary.NeuralNetwork.Connections.RPROP
{
    /// <summary>
    /// The RPROP Minus algorithm
    /// </summary>
    public class RPROPMinusConnection : Connection
    {
        public RPROPMinusConnection(Neuron anteriorNeuron, Neuron posteriorNeuron)
            : base(anteriorNeuron, posteriorNeuron)
        { }

        #region Parameters

        private const double stepMin = 0.0000016;
        private const double stepMax = 50;
        private const double stepInitial = 0.1;
        private const double stepIncrease = 1.2;
        private const double stepDecrease = 0.5;

        #endregion Parameters

        #region Fields

        private double lastGradient = 0;
        private double lastStep = 0;
        private double deltaWeight = 0;
        private double step = stepInitial;

        #endregion Fields

        /// <summary>
        /// Updates the weight using the RPROP- algorithm
        /// http://www.inf.fu-berlin.de/lehre/WS06/Musterererkennung/Paper/rprop.pdf
        /// </summary>
        /// <param name="learningParameters"></param>
        protected override void UpdateWeight(params double[] learningParameters)
        {
            if (lastGradient * Gradient > 0)
            {
                step = Math.Min(lastStep * stepIncrease, stepMax);
                deltaWeight = -Math.Sign(Gradient) * step;
                Weight += deltaWeight;
                lastGradient = Gradient;
            }
            else if (lastGradient * Gradient < 0)
            {
                step = Math.Max(lastStep * stepDecrease, stepMin);
                lastGradient = 0;
            }
            else if (lastGradient * Gradient == 0)
            {
                deltaWeight = -Math.Sign(Gradient) * step;
                Weight += deltaWeight;
                lastGradient = Gradient;
            }
            lastStep = step;
        }

        /// <summary>
        /// The learning parameter count.
        /// </summary>
        protected override uint LearningParameterCount
        {
            get { return 0; }
        }
    }
}