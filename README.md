# Math-CS

Detailed Description
============
+ [Vector](#vector)
+ [Matrix](#matrix)
+ [Distribution](#distribution)

## Vector

Bir vektör yaratmak için:

	a = Vector(ArrayList<Double> values)

Vektörler eklemek için

	void Add(Vector v)

Çıkarmak için

	void Subtract(Vector v)
	Vector Difference(Vector v)

İç çarpım için

	double DotProduct(Vector v)
	double DotProduct()

Bir vektörle cosinüs benzerliğini hesaplamak için

	double CosineSimilarity(Vector v)

Bir vektörle eleman eleman çarpmak için

	Vector ElementProduct(Vector v)

## Matrix

3'e 4'lük bir matris yaratmak için

	a = Matrix(3, 4)

Elemanları rasgele değerler alan bir matris yaratmak için

	Matrix(int row, int col, double min, double max)

Örneğin, 

	a = Matrix(3, 4, 1, 5)
 
3'e 4'lük elemanları 1 ve 5 arasında değerler alan bir matris yaratır.

Birim matris yaratmak için

	Matrix(int size)

Örneğin,

	a = Matrix(4)

4'e 4'lük köşegeni 1, diğer elemanları 0 olan bir matris yaratır.

Matrisin i. satır, j. sütun elemanını getirmek için 

	double GetValue(int rowNo, int colNo)

Örneğin,

	a.GetValue(3, 4)

3. satır, 4. sütundaki değeri getirir.

Matrisin i. satır, j. sütunundaki elemanı değiştirmek için

	void SetValue(int rowNo, int colNo, double value)

Örneğin,

	a.SetValue(3, 4, 5)

3. satır, 4.sütundaki elemanın değerini 5 yapar.

Matrisleri toplamak için

	void Add(Matrix m)

Çıkarmak için 

	void Subtract(Matrix m)

Çarpmak için 

	Matrix Multiply(Matrix m)

Elaman eleman matrisleri çarpmak için

	Matrix RlementProduct(Matrix m)

Matrisin transpozunu almak için

	Matrix Transpose()

Matrisin simetrik olup olmadığı belirlemek için

	boolean IsSymmetric()

Determinantını almak için

	double Determinant()

Tersini almak için

	void Inverse()

Matrisin eigenvektör ve eigendeğerlerini bulmak için

	ArrayList<Eigenvector> Characteristics()

Bu metodla bulunan eigenvektörler eigendeğerlerine göre büyükten küçüğe doğru 
sıralı olarak döndürülür.

## Distribution

Verilen bir değerin normal dağılımdaki olasılığını döndürmek için

	static double ZNormal(double z)

Verilen bir olasılığın normal dağılımdaki değerini döndürmek için

	static double ZInverse(double p)

Verilen bir değerin chi kare dağılımdaki olasılığını döndürmek için

	static double ChiSquare(double x, int freedom)

Verilen bir olasılığın chi kare dağılımdaki değerini döndürmek için

	static double ChiSquareInverse(double p, int freedom)

Verilen bir değerin F dağılımdaki olasılığını döndürmek için

	static double FDistribution(double F, int freedom1, int freedom2)

Verilen bir olasılığın F dağılımdaki değerini döndürmek için

	static double FDistributionInverse(double p, int freedom1, int freedom2)

Verilen bir değerin t dağılımdaki olasılığını döndürmek için

	static double TDistribution(double T, int freedom)

Verilen bir olasılığın t dağılımdaki değerini döndürmek için

	static double TDistributionInverse(double p, int freedom)

