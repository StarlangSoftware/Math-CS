using System;
using System.Collections.Generic;
using System.Linq;

namespace Math;

public class Tensor
{
    /**
     * <summary>A class representing a multidimensional tensor that supports basic operations and broadcasting.</summary>
     */

    private readonly int[] _shape;
    private readonly int[] _strides;
    private readonly List<double> _data;
    
    /**
     * <summary>Initializes the tensor with given data and shape.</summary>
     *
     * <param name="data">  Nested lists representing the tensor data.</param>
     * <param name="shape"> The shape of the tensor. If null, the shape is inferred from the data.</param>
     */
    public Tensor(List<List<List<double>>> data, int[] shape) {
        if (shape == null) {
            _shape = InferShape(data);
        } else {
            _shape = shape;
        }
        _data = Flatten(data);
        var totalElements = _data.Count;
        if (ComputeNumElements(_shape) != totalElements) {
            throw new ArgumentException("Shape does not match the number of elements in data.");
        }
        _strides = ComputeStrides(_shape);
    }
    
    /**
     * <summary>Initializes the tensor with given data and shape.</summary>
     *
     * <param name="data">  Nested lists representing the tensor data.</param>
     * <param name="shape"> The shape of the tensor. If null, the shape is inferred from the data.</param>
     */
    public Tensor(List<List<double>> data, int[] shape) {
        if (shape == null) {
            _shape = InferShape(data);
        } else {
            _shape = shape;
        }
        _data = Flatten(data);
        var totalElements = _data.Count;
        if (ComputeNumElements(_shape) != totalElements) {
            throw new ArgumentException("Shape does not match the number of elements in data.");
        }
        _strides = ComputeStrides(_shape);
    }

    /**
    * <summary>Initializes the tensor with given data and shape.</summary>
    *
    * <param name="data">  Nested lists representing the tensor data.</param>
    * <param name="shape"> The shape of the tensor. If null, the shape is inferred from the data.</param>
    */
    public Tensor(List<double> data, int[] shape) {
        if (shape == null) {
            _shape = InferShape(data);
        } else {
            _shape = shape;
        }
        _data = Flatten(data);
        var totalElements = _data.Count;
        if (ComputeNumElements(_shape) != totalElements) {
            throw new ArgumentException("Shape does not match the number of elements in data.");
        }
        _strides = ComputeStrides(_shape);
    }

    public Tensor(List<double> data) :  this(data, null) {
    }
    
    public Tensor(List<List<double>> data) :  this(data, null) {
    }

    public Tensor(List<List<List<double>>> data) :  this(data, null) {
    }

    /**
    * <summary>Infers the shape of the tensor from nested lists.</summary>
    *
    * <param name="data"> Nested lists representing the tensor data.</param>
    * <returns> Array representing the shape.</returns>
    */
    private int[] InferShape(List<List<List<double>>> data) {
        object firstElement = data[0];
        var firstList = (List<List<double>>) firstElement;
        var restOfShape = InferShape(firstList);
        var shape = new int[restOfShape.Length + 1];
        shape[0] = data.Count;
        Array.Copy(restOfShape, 0, shape, 1, restOfShape.Length);
        return shape;
    }
    
    /**
    * <summary>Infers the shape of the tensor from nested lists.</summary>
    *
    * <param name="data"> Nested lists representing the tensor data.</param>
    * <returns> Array representing the shape.</returns>
    */
    private int[] InferShape(List<List<double>> data) {
        object firstElement = data[0];
        var firstList = (List<double>) firstElement;
        var restOfShape = InferShape(firstList);
        var shape = new int[restOfShape.Length + 1];
        shape[0] = data.Count;
        Array.Copy(restOfShape, 0, shape, 1, restOfShape.Length);
        return shape;
    }
    
    /**
     * <summary>Infers the shape of the tensor from nested lists.</summary>
     *
     * <param name="data"> Nested lists representing the tensor data.</param>
     * <returns> Array representing the shape.</returns>
     */
    private int[] InferShape(List<double> data) {
        if (data.Count == 0) {
            return [0];
        }
        return [data.Count];
    }

    /**
     * <summary>Concatenates two tensors into a one.</summary>
     *
     * <param name="tensor"> 2nd tensor for concatenation.</param>
     * <param name="dimension"> to concatenate.</param>
     * <returns> Concatenated {@link Tensor}.</returns>
     */
    public Tensor Concat(Tensor tensor, int dimension) {
        if (dimension >= _shape.Length) {
            throw new ArgumentException("Dimension out of bounds.");
        }
        if (_shape.Length != tensor._shape.Length) {
            throw new ArgumentException("Dimensions length do not match.");
        }
        var startIndex = 1;
        var endIndex1 = 1;
        var endIndex2 = 1;
        for (var i = 0; i < _shape.Length; i++) {
            if (i != dimension && _shape[i] != tensor._shape[i]) {
                throw new ArgumentException("Dimensions do not match.");
            }
            if (i >= dimension) {
                endIndex1 *= _shape[i];
                endIndex2 *= tensor._shape[i];
            } else {
                startIndex *= _shape[i];
            }
        }
        var newShape = new int[_shape.Length];
        for (var i = 0; i < _shape.Length; i++) {
            if (i == dimension) {
                newShape[i] = _shape[i] + tensor._shape[i];
            } else {
                newShape[i] = _shape[i];
            }
        }
        var newList = new List<double>();
        for (var i = 0; i < startIndex; i++) {
            for (var j = 0; j < endIndex1; j++) {
                newList.Add(_data[i * endIndex1 + j]);
            }
            for (int j = 0; j < endIndex2; j++) {
                newList.Add(tensor._data[i * endIndex2 + j]);
            }
        }
        return new Tensor(newList, newShape);
    }

    /**
     * <summary>Returns the sub-{@link Tensor} taking the given dimensions.</summary>
     *
     * <returns> a sub-{@link Tensor}.</returns>
     */
    public Tensor Get(int[] dimensions) {
        if (dimensions.Length >= _shape.Length) {
            throw new ArgumentException("Dimensions exceeds or same as the tensor's dimension.");
        }
        for (var j = 0; j < dimensions.Length; j++) {
            if (dimensions[j] >= _shape[j]) {
                throw new IndexOutOfRangeException("There is a dimension length exceed the tensor's dimension length.");
            }
        }
        var newShape = new int[_shape.Length - dimensions.Length];
        Array.Copy(_shape, dimensions.Length, newShape, 0, newShape.Length);
        int i = 0, start = 0, end = _data.Count;
        do {
            var parts = (end - start) / _shape[i];
            start += parts * dimensions[i];
            end = start + parts;
            i++;
        } while (i < dimensions.Length);
        return new Tensor(_data.GetRange(start, end - start),  newShape);
    }

    /**
     * <summary>Flattens nested lists into a single list.</summary>
     *
     * <param name="data"> Nested lists representing the tensor data.</param>
     * <returns> Flattened list of tensor elements.</returns>
     */
    private List<double> Flatten(List<List<List<double>>> data) {
        var flattenedList = new List<double>();
        foreach (var item in data) {
            flattenedList.AddRange(Flatten(item));
        }
        return flattenedList;
    }
    
    /**
     * <summary>Flattens nested lists into a single list.</summary>
     *
     * <param name="data"> Nested lists representing the tensor data.</param>
     * <returns> Flattened list of tensor elements.</returns>
     */
    private List<double> Flatten(List<List<double>> data) {
        var flattenedList = new List<double>();
        foreach (var item in data) {
            flattenedList.AddRange(Flatten(item));
        }
        return flattenedList;
    }
    
    /**
     * <summary>Flattens nested lists into a single list.</summary>
     *
     * <param name="data"> Nested lists representing the tensor data.</param>
     * <returns> Flattened list of tensor elements.</returns>
     */
    private List<double> Flatten(List<double> data) {
        var flattenedList = new List<double>();
        foreach (var item in data) {
            flattenedList.Add(item);
        }
        return flattenedList;
    }

    /**
     * <summary>Computes the strides for each dimension based on the shape.</summary>
     *
     * <param name="shape"> Array representing the tensor shape.</param>
     * <returns> Array representing the strides.</returns>
     */
    private int[] ComputeStrides(int[] shape) {
        var strides = new int[shape.Length];
        var product = 1;
        for (var i = shape.Length - 1; i >= 0; i--) {
            strides[i] = product;
            product *= shape[i];
        }
        return strides;
    }

    /**
     * <summary>Computes the total number of elements in the tensor based on its shape.</summary>
     *
     * <param name="shape"> Array representing the tensor shape.</param>
     * <returns> Total number of elements.</returns>
     */
    private int ComputeNumElements(int[] shape) {
        var product = 1;
        foreach (var dim in shape) {
            product *= dim;
        }
        return product;
    }

    /**
     * <summary>Validates that indices are within the valid range for each dimension.</summary>
     * <param name="indices"> Array of indices specifying the position.</param>
     */
    private void ValidateIndices(int[] indices) {
        if (indices.Length != _shape.Length) {
            throw new IndexOutOfRangeException("Expected " + _shape.Length + " indices but got " + indices.Length + ".");
        }
        for (var i = 0; i < indices.Length; i++) {
            if (!(0 <= indices[i] && indices[i] < _shape[i])) {
                throw new IndexOutOfRangeException("Index is out of bounds for shape ");
            }
        }
    }

    /**
     * <summary>Retrieves the value at the given indices.</summary>
     *
     * <param name="indices"> Array of indices specifying the position.</param>
     * <returns> Value at the specified position.</returns>
     */
    public double GetValue(int[] indices) {
        ValidateIndices(indices); // Ensure indices are valid
        var flatIndex = 0;
        for (var i = 0; i < indices.Length; i++) {
            flatIndex += indices[i] * _strides[i];
        }
        return _data[flatIndex];
    }

    /**
     * <summary>Sets the value at the given indices.</summary>
     *
     * <param name="indices"> Array of indices specifying the position.</param>
     * <param name="value">   Value to set at the specified position.</param>
     */
    public void SetValue(int[] indices, double value) {
        ValidateIndices(indices); // Ensure indices are valid
        var flatIndex = 0;
        for (var i = 0; i < indices.Length; i++) {
            flatIndex += indices[i] * _strides[i];
        }
        _data[flatIndex] = value;
    }

    /**
     * <summary>Reshapes the tensor to the specified new shape.</summary>
     *
     * <param name="newShape"> Array representing the new shape.</param>
     * <returns> New tensor with the specified shape.</returns>
     */
    public Tensor Reshape(int[] newShape) {
        if (ComputeNumElements(newShape) != ComputeNumElements(_shape)) {
            throw new ArgumentException("Total number of elements must remain the same.");
        }
        return new Tensor(_data, newShape);
    }

    /**
     * <summary>Transposes the tensor according to the specified axes.</summary>
     *
     * <param name="axes"> Array representing the order of axes. If null, reverses the axes.</param>
     * <returns> New tensor with transposed axes.</returns>
     */
    public Tensor Transpose(int[] axes) {
        if (axes == null) {
            axes = new int[_shape.Length];
            for (var i = 0; i < _shape.Length; i++) {
                axes[i] = _shape.Length - 1 - i;
            }
        }
        var newShape = new int[_shape.Length];
        for (var i = 0; i < axes.Length; i++) {
            newShape[i] = _shape[axes[i]];
        }
        var flattenedData = TransposeFlattenedData(axes, newShape);
        return new Tensor(flattenedData, newShape);
    }

    /**
     * <summary>Rearranges the flattened data for transposition.</summary>
     *
     * <param name="axes">     Array representing the order of axes.</param>
     * <param name="newShape"> Array representing the new shape.</param>
     * <returns> Flattened list of transposed data.</returns>
     */
    private List<double> TransposeFlattenedData(int[] axes, int[] newShape) {
        var newStrides = ComputeStrides(newShape);
        var flattenedData = new List<double>();
        var numElements = ComputeNumElements(newShape);
        for (var i = 0; i < numElements; i++) {
            var newIndices = UnflattenIndex(i, newStrides);
            var originalIndices = new int[_shape.Length];
            for (var dim = 0; dim < _shape.Length; dim++) {
                var originalDimIndex = -1;
                for (var j = 0; j < axes.Length; j++) {
                    if (axes[j] == dim) {
                        originalDimIndex = j;
                        break;
                    }
                }
                if (originalDimIndex != -1) {
                    originalIndices[dim] = newIndices[originalDimIndex];
                }
            }
            flattenedData.Add(GetValue(originalIndices));
        }
        return flattenedData;
    }

    /**
     * <summary>Converts a flat index to multi-dimensional indices based on strides.</summary>
     *
     * <param name="flatIndex"> The flat index to convert.</param>
     * <param name="strides">   Array representing the strides.</param>
     * <returns> Array of multi-dimensional indices.</returns>
     */
    private int[] UnflattenIndex(int flatIndex, int[] strides) {
        var indices = new int[strides.Length];
        for (var i = 0; i < strides.Length; i++) {
            indices[i] = flatIndex / strides[i];
            flatIndex %= strides[i];
        }
        return indices;
    }

    /**
     * <summary>Determines the broadcasted shape of two tensors.</summary>
     *
     * <param name="shape1"> Array representing the first tensor shape.</param>
     * <param name="shape2"> Array representing the second tensor shape.</param>
     * <returns> Array representing the broadcasted shape.</returns>
     */
    private int[] BroadcastShape(int[] shape1, int[] shape2) {
        // Reverse both shapes to align from the right
        var reversedShape1 = shape1.ToArray().Reverse().ToArray();
        var reversedShape2 = shape2.ToArray().Reverse().ToArray();
        var resultShape = new List<int>();
        
        // Compare dimensions from the right
        var maxLength = System.Math.Max(shape1.Length, shape2.Length);
        for (var i = 0; i < maxLength; i++) {
            var dim1 = (i < reversedShape1.Length) ? reversedShape1[i] : 1;
            var dim2 = (i < reversedShape2.Length) ? reversedShape2[i] : 1;
            
            if (dim1 == dim2) {
                resultShape.Add(dim1);
            } else if (dim1 == 1 || dim2 == 1) {
                resultShape.Add(System.Math.Max(dim1, dim2));
            } else {
                throw new ArgumentException("Shapes are not broadcastable");
            }
        }
        
        // Add remaining dimensions from shape1
        for (var i = reversedShape2.Length; i < reversedShape1.Length; i++) {
            resultShape.Add(reversedShape1[i]);
        }
        
        // Add remaining dimensions from shape2
        for (var i = reversedShape1.Length; i < reversedShape2.Length; i++) {
            resultShape.Add(reversedShape2[i]);
        }
        
        // Reverse back to get the final shape
        resultShape.Reverse();
        
        return resultShape.ToArray();
    }

    /**
     * <summary>Broadcasts the tensor to the specified target shape.</summary>
     *
     * <param name="targetShape"> Array representing the target shape.</param>
     * <returns> New tensor with the target shape.</returns>
     */
    public Tensor BroadcastTo(int[] targetShape) {
        var diff = targetShape.Length - _shape.Length;
        var expandedShape = new int[targetShape.Length];
        for (var i = 0; i < diff; i++) {
            expandedShape[i] = 1;
        }
        Array.Copy(_shape, 0, expandedShape, diff, _shape.Length);

        for (var i = 0; i < targetShape.Length; i++) {
            if (!(expandedShape[i] == targetShape[i] || expandedShape[i] == 1)) {
                throw new ArgumentException("Cannot broadcast shape ");
            }
        }
        var newData = new List<double>();
        var numElements = ComputeNumElements(targetShape);
        var targetStrides = ComputeStrides(targetShape);
        for (var i = 0; i < numElements; i++) {
            var indices = UnflattenIndex(i, targetStrides);
            var originalIndices = new int[indices.Length];
            for (var j = 0; j < indices.Length; j++) {
                originalIndices[j] = (expandedShape[j] > 1) ? indices[j] : 0;
            }
            newData.Add(GetValue(originalIndices));
        }
        return new Tensor(newData, targetShape);
    }

    /**
     * <summary>Adds two tensors element-wise with broadcasting.</summary>
     *
     * <param name="other"> The other tensor to add.</param>
     * <returns> New tensor with the result of the addition.</returns>
     */
    public Tensor Add(Tensor other) {
        var broadcastShape = BroadcastShape(_shape, other._shape);
        var tensor1 = this.BroadcastTo(broadcastShape);
        var tensor2 = other.BroadcastTo(broadcastShape);
        var resultData = new List<double>();
        var numElements = ComputeNumElements(broadcastShape);
        for (var i = 0; i < numElements; i++) {
            resultData.Add(tensor1._data[i] + tensor2._data[i]);
        }
        return new Tensor(resultData, broadcastShape);
    }

    /**
     * <summary>Subtracts one tensor from another element-wise with broadcasting.</summary>
     *
     * <param name="other"> The other tensor to subtract.</param>
     * <returns> New tensor with the result of the subtraction.</returns>
     */
    public Tensor Subtract(Tensor other) {
        var broadcastShape = BroadcastShape(_shape, other._shape);
        var tensor1 = this.BroadcastTo(broadcastShape);
        var tensor2 = other.BroadcastTo(broadcastShape);
        var resultData = new List<double>();
        var numElements = ComputeNumElements(broadcastShape);
        for (var i = 0; i < numElements; i++) {
            resultData.Add(tensor1._data[i] - tensor2._data[i]);
        }
        return new Tensor(resultData, broadcastShape);
    }

    /**
     * <summary>Multiplies two tensors element-wise with broadcasting.</summary>
     *
     * <param name="other"> The other tensor to multiply.</param>
     * <returns> New tensor with the result of the multiplication.</returns>
     */
    public Tensor HadamardProduct(Tensor other) {
        var broadcastShape = BroadcastShape(_shape, other._shape);
        var tensor1 = this.BroadcastTo(broadcastShape);
        var tensor2 = other.BroadcastTo(broadcastShape);
        var resultData = new List<double>();
        var numElements = ComputeNumElements(broadcastShape);
        for (var i = 0; i < numElements; i++) {
            resultData.Add(tensor1._data[i] * tensor2._data[i]);
        }
        return new Tensor(resultData, broadcastShape);
    }

    /**
     * <summary>Performs matrix multiplication (batched if necessary).
     * For tensors of shape (..., M, K) and (..., K, N), returns (..., M, N).</summary>
     *
     * <param name="other"> Tensor with shape compatible for matrix multiplication.</param>
     * <returns> Tensor resulting from matrix multiplication.</returns>
     */
    public Tensor Multiply(Tensor other) {
        if (_shape.Length < 2 || other._shape.Length < 2 || _shape[_shape.Length - 1] != other._shape[other._shape.Length - 2]) {
            throw new ArgumentException("Shapes are not aligned for multiplication.");
        }

        var batchShape = new int[_shape.Length - 2];
        Array.Copy(_shape, 0, batchShape, 0, _shape.Length - 2);
        var m = _shape[^2];
        var k1 = _shape[^1];
        var k2 = other._shape[^2];
        var n = other._shape[^1];

        if (k1 != k2) {
            throw new ArgumentException("Inner dimensions must match for matrix multiplication.");
        }

        // Broadcasting batch shape if necessary
        var otherBatchShape = new int[other._shape.Length - 2];
        Array.Copy(other._shape, 0, otherBatchShape, 0, other._shape.Length - 2);
        int[] broadcastShape;
        Tensor selfBroadcasted;
        Tensor otherBroadcasted;
        
        if (!Equals(batchShape, otherBatchShape)) {
            broadcastShape = BroadcastShape(batchShape, otherBatchShape);
            var selfBroadcastShape = Concat(broadcastShape, [m, k1]);
            var otherBroadcastShape = Concat(broadcastShape, [k2, n]);
            selfBroadcasted = this.BroadcastTo(selfBroadcastShape);
            otherBroadcasted = other.BroadcastTo(otherBroadcastShape);
        } else {
            broadcastShape = batchShape;
            selfBroadcasted = this;
            otherBroadcasted = other;
        }

        var resultShape = Concat(broadcastShape, [m, n]);
        var resultData = new List<double>();

        for (var i = 0; i < ComputeNumElements(resultShape); i++) {
            var indices = UnflattenIndex(i, ComputeStrides(resultShape));
            var batchIdx = new int[indices.Length - 2];
            Array.Copy(indices, batchIdx, indices.Length - 2);
            var row = indices[^2];
            var col = indices[^1];

            double sumResult = 0;
            for (var k = 0; k < k1; k++) {
                var aIdx = Concat(batchIdx, [row, k]);
                var bIdx = Concat(batchIdx, [k, col]);
                sumResult += selfBroadcasted.GetValue(aIdx) * otherBroadcasted.GetValue(bIdx);
            }

            resultData.Add(sumResult);
        }

        return new Tensor(resultData, resultShape);
    }

    /**
     * <summary>Helper method to concatenate two int arrays.</summary>
     */
    private int[] Concat(int[] a, int[] b) {
        var result = new int[a.Length + b.Length];
        Array.Copy(a, 0, result, 0, a.Length);
        Array.Copy(b, 0, result, a.Length, b.Length);
        return result;
    }

    /**
     * <summary>Extracts a sub-tensor from the given start indices to the end indices.</summary>
     *
     * <param name="startIndices"> Array specifying the start indices for each dimension.</param>
     * <param name="endIndices">   Array specifying the end indices (exclusive) for each dimension.</param>
     * <returns> A new Tensor containing the extracted sub-tensor.</returns>
     */
    public Tensor Partial(int[] startIndices, int[] endIndices) {
        if (startIndices.Length != _shape.Length || endIndices.Length != _shape.Length) {
            throw new ArgumentException("startIndices and endIndices must match the number of dimensions.");
        }
        
        // Compute the new shape of the extracted sub-tensor
        var newShape = new int[_shape.Length];
        for (var i = 0; i < _shape.Length; i++) {
            newShape[i] = endIndices[i] - startIndices[i];
            if (newShape[i] < 0) {
                throw new ArgumentException("End index must be greater than or equal to start index for dimension " + i);
            }
        }
        
        // Extract data from the original tensor
        var subData = new List<double>();
        var numElements = ComputeNumElements(newShape);
        var newStrides = ComputeStrides(newShape);

        for (var i = 0; i < numElements; i++) {
            var subIndices = UnflattenIndex(i, newStrides);
            var originalIndices = new int[_shape.Length];
            for (int j = 0; j < _shape.Length; j++) {
                originalIndices[j] = startIndices[j] + subIndices[j];
            }
            subData.Add(GetValue(originalIndices));
        }
        return new Tensor(subData, newShape);
    }

    /**
     * <summary>Returns a string representation of the tensor.</summary>
     *
     * <returns> string representing the tensor.</returns>
     */
    public string Tostring() {
        var formattedData = FormatTensor(_data, _shape);
        return "Tensor(data=" + formattedData + ")";
    }
    
    private object FormatTensor(List<double> data, int[] shape) {
        if (shape.Length == 1) {
            return data;
        }
        var range = new int[shape.Length - 1];
        Array.Copy(shape, 1, range, 0, shape.Length - 1);
        var stride = ComputeNumElements(range);
        var formattedList = new List<object>();
        for (var i = 0; i < shape[0]; i++) {
            formattedList.Add(FormatTensor(data.GetRange(i * stride, stride), range));
        }
        return formattedList;
    }

    public int[] GetShape() {
        return _shape;
    }

    public List<double> GetData() {
        return _data;
    }
}