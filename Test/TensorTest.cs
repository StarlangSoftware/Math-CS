using System.Collections.Generic;
using Math;
using NUnit.Framework;

namespace Test;

public class TensorTest
{
    private static readonly int[] EXPECTED = [2, 2];

    [Test]
    public void TestConstructorWithInferredShape() {
        var data = new List<List<double>>([[1.0, 2.0], [3.0, 4.0]]);
        var tensor = new Tensor(data);
        Assert.AreEqual(EXPECTED, tensor.GetShape());
        Assert.AreEqual(new List<double>([1.0, 2.0, 3.0, 4.0]), tensor.GetData());
    }

    [Test]
    public void TestConstructorWithExplicitShape() {
        var tensor = new Tensor([1.0, 2.0, 3.0, 4.0], [2, 2]);
        Assert.AreEqual(EXPECTED, tensor.GetShape());
        Assert.AreEqual(new List<double>([1.0, 2.0, 3.0, 4.0]), tensor.GetData());
    }

    [Test]
    public void TestGetShape() {
        var tensor = new Tensor([1.0, 2.0, 3.0], [3]);
        Assert.AreEqual(new List<int>([3]), tensor.GetShape());
    }

    [Test]
    public void TestGetValidIndices() {
        var tensor = new Tensor([1.0, 2.0, 3.0, 4.0], [2, 2]);
        Assert.AreEqual(1.0, tensor.GetValue([0, 0]));
        Assert.AreEqual(4.0, tensor.GetValue([1, 1]));
    }

    [Test]
    public void TestSetValidIndices() {
        var tensor = new Tensor([1.0, 2.0, 3.0, 4.0], [2, 2]);
        tensor.SetValue([0, 0], 5);
        Assert.AreEqual(5.0, tensor.GetValue([0, 0]));
    }
    
    [Test]
    public void TestReshape() {
        var tensor = new Tensor([1.0, 2.0, 3.0, 4.0], [2, 2]);
        var reshaped = tensor.Reshape([4]);
        Assert.AreEqual(new int[]{4}, reshaped.GetShape());
        Assert.AreEqual(new List<double>([1.0, 2.0, 3.0, 4.0]), reshaped.GetData());
    }
    
    [Test]
    public void TestTransposeNoAxes() {
        var data = new List<List<double>>([[1.0, 2.0], [3.0, 4.0]]);
        var tensor = new Tensor(data);
        var transposed = tensor.Transpose(null);
        Assert.AreEqual(EXPECTED, transposed.GetShape());
        Assert.AreEqual(new List<double>([1.0, 3.0, 2.0, 4.0]), transposed.GetData());
    }
    
    [Test]
    public void TestTransposeWithAxes() {
        var data = new List<List<double>>([[1.0, 2.0, 3.0], [4.0, 5.0, 6.0]]);
        var tensor = new Tensor(data);
        var transposed = tensor.Transpose([1, 0]);
        Assert.AreEqual(new int[]{3, 2}, transposed.GetShape());
        Assert.AreEqual(new List<double>([1.0, 4.0, 2.0, 5.0, 3.0, 6.0]), transposed.GetData());
    }

    [Test]
    public void TestBroadcastToValid() {
        var data = new List<double>([1.0, 2.0]);
        var tensor = new Tensor(data, [1, 2]);
        var broadcasted = tensor.BroadcastTo([2, 2]);
        Assert.AreEqual(new int[]{2, 2}, broadcasted.GetShape());
        Assert.AreEqual(new List<double>([1.0, 2.0, 1.0, 2.0]), broadcasted.GetData());
    }

    [Test]
    public void TestAddSameShape() {
        var data1 = new List<double>([1.0, 2.0, 3.0, 4.0]);
        var tensor1 = new Tensor(data1, [2, 2]);
        var data2 = new List<double>([5.0, 6.0, 7.0, 8.0]);
        var tensor2 = new Tensor(data2, [2, 2]);
        var sum = tensor1.Add(tensor2);
        Assert.AreEqual(new int[]{2, 2}, sum.GetShape());
        Assert.AreEqual(new List<double>([6.0, 8.0, 10.0, 12.0]), sum.GetData());
    }

    [Test]
    public void TestAddWithBroadCasting() {
        var data1 = new List<double>([1.0, 2.0]);
        var tensor1 = new Tensor(data1, [1, 2]);
        var data2 = new List<double>([3.0, 4.0]);
        var tensor2 = new Tensor(data2, [2, 1]);
        var sum = tensor1.Add(tensor2);
        Assert.AreEqual(new int[]{2, 2}, sum.GetShape());
        Assert.AreEqual(new List<double>([4.0, 5.0, 5.0, 6.0]), sum.GetData());
    }

    [Test]
    public void TestSubtractSameShape() {
        var data1 = new List<double>([5.0, 6.0, 7.0, 8.0]);
        var tensor1 = new Tensor(data1, [2, 2]);
        var data2 = new List<double>([1.0, 2.0, 3.0, 4.0]);
        var tensor2 = new Tensor(data2, [2, 2]);
        var diff = tensor1.Subtract(tensor2);
        Assert.AreEqual(new int[]{2, 2}, diff.GetShape());
        Assert.AreEqual(new List<double>([4.0, 4.0, 4.0, 4.0]), diff.GetData());
    }

    [Test]
    public void TestSubtractWithBroadCasting() {
        var data1 = new List<double>([5.0, 5.0]);
        var tensor1 = new Tensor(data1, [2, 1]);
        var data2 = new List<double>([1.0, 2.0]);
        var tensor2 = new Tensor(data2, [1, 2]);
        var diff = tensor1.Subtract(tensor2);
        Assert.AreEqual(new int[]{2, 2}, diff.GetShape());
        Assert.AreEqual(new List<double>([4.0, 3.0, 4.0, 3.0]), diff.GetData());
    }

    [Test]
    public void TestHadamardProductSameShape() {
        var data1 = new List<double>([1.0, 2.0, 3.0, 4.0]);
        var tensor1 = new Tensor(data1, [2, 2]);
        var data2 = new List<double>([5.0, 6.0, 7.0, 8.0]);
        var tensor2 = new Tensor(data2, [2, 2]);
        var product = tensor1.HadamardProduct(tensor2);
        Assert.AreEqual(new int[]{2, 2}, product.GetShape());
        Assert.AreEqual(new List<double>([5.0, 12.0, 21.0, 32.0]), product.GetData());
    }

    [Test]
    public void TestHadamardProductWithBroadCasting() {
        var data1 = new List<double>([1.0, 2.0]);
        var tensor1 = new Tensor(data1, [1, 2]);
        var data2 = new List<double>([3.0, 4.0]);
        var tensor2 = new Tensor(data2, [2, 1]);
        var product = tensor1.HadamardProduct(tensor2);
        Assert.AreEqual(new int[]{2, 2}, product.GetShape());
        Assert.AreEqual(new List<double>([3.0, 6.0, 4.0, 8.0]), product.GetData());
    }
    
    [Test]
    public void TestMultiply2D() {
        var data1 = new List<List<double>>([[1.0, 2.0], [3.0, 4.0]]);
        var tensor1 = new Tensor(data1);
        var data2 = new List<List<double>>([[5.0, 6.0], [7.0, 8.0]]);
        var tensor2 = new Tensor(data2);
        var result = tensor1.Multiply(tensor2);
        Assert.AreEqual(new int[]{2, 2}, result.GetShape());
        Assert.AreEqual(new List<double>([19.0, 22.0, 43.0, 50.0]), result.GetData());
    }

    [Test]
    public void TestMultiply3D() {
        var data1 = new List<List<List<double>>>([[[1.0, 2.0], [3.0, 4.0]], [[9.0, 10.0], [11.0, 12.0]]]);
        var tensor1 = new Tensor(data1);
        var data2 = new List<List<List<double>>>([[[5.0, 6.0], [7.0, 8.0]], [[13.0, 14.0], [15.0, 16.0]]]);
        var tensor2 = new Tensor(data2);
        var result = tensor1.Multiply(tensor2);
        Assert.AreEqual(new int[]{2, 2, 2}, result.GetShape());
        Assert.AreEqual(new List<double>([19.0, 22.0, 43.0, 50.0, 267.0, 286.0, 323.0, 346.0]), result.GetData());
    }

    [Test]
    public void TestPartialValid() {
        var data = new List<List<double>>([[1.0, 2.0, 3.0], [4.0, 5.0, 6.0], [7.0, 8.0, 9.0]]);
        var tensor = new Tensor(data);
        var partial = tensor.Partial([0, 1], [2, 3]);
        Assert.AreEqual(new int[]{2, 2}, partial.GetShape());
        Assert.AreEqual(new List<double>([2.0, 3.0, 5.0, 6.0]), partial.GetData());
    }
    
}