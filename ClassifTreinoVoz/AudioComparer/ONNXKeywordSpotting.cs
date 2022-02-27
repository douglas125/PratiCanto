using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace AudioComparer
{
    /// <summary>Keyword spotting using ONNX model</summary>
    public class ONNXKeywordSpotting
    {
        /// <summary>Inference session</summary>
        public InferenceSession session;
        /// <summary>Constructor</summary>
        public ONNXKeywordSpotting(string model_path)
        {
            this.session = new InferenceSession(model_path);
            List<float> vals = new List<float> { 1, 2, 3, 4 };
            this.Predict(vals);
        }
        public List<float> Predict(List<float> audio_sample)
        {
            DenseTensor<float> t = new DenseTensor<float>(new[] { 1, audio_sample.Count});
            for (int i = 0; i < audio_sample.Count; i++)
                t[0, i] = audio_sample[i];

            // var inputTensor = new DenseTensor<float>(new float[] { 10 }, new int[] { 1, 1 });

            // Create input data for session.
            var input = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor<float>("input", t) };

            // Run session and send input data in to get inference output. Call ToList then get the Last item. Then use the AsEnumerable extension method to return the Value result as an Enumerable of NamedOnnxValue.
            var results = session.Run(input);
            IEnumerable<float> outs = results.First().AsEnumerable<float>();

            List<float> list_outs = outs.ToList<float>();

            return list_outs;
        }
    }
}
