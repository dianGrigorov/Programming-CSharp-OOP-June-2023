namespace Database.Tests;

using NUnit.Framework;
using System;

[TestFixture]
public class DatabaseTests
{
    private Database database;

    [SetUp]
    public void SetUp()
    {
        database = new Database(1, 2);
    }

    [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 })]
    [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 })]
    public void CheckStoringCapacityIsMoreThen16(int[] data)
    {
        InvalidOperationException exeption = Assert.Throws<InvalidOperationException>(
            () => database = new Database(data));
        Assert.AreEqual("Array's capacity must be exactly 16 integers!", exeption.Message);
    }

    [Test]
    public void CreatingDatabaseShouldBeCorrect()
    {
        int actualResult = database.Count;
        int expectedResult = 2;

        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestCase(new int[] { 1, 2, 3, 4, 5, 6})]
    [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 })]
    public void CreatingDataBaceShouldAddElementsCorectly(int[] data)
    {
        database = new Database(data);
        int[] actualResult = database.Fetch();

        Assert.AreEqual(data, actualResult);
    }

    [TestCase(-1)]
    [TestCase(5)]
    public void DatabaseAddMethodShouldIncreaseCount(int number)
    {
        int expectedResult = 3;
        database.Add(number);
        Assert.AreEqual(expectedResult, database.Count);
    }

    [TestCase(new int[] {1, 2, 3, 4})]
    public void DatabaseAddMethodShouldAddelementCorectly(int[] data)
    {
        database = new Database();

        for (int i = 0; i < data.Length; i++)
        {
            database.Add(data[i]);
        }

        int[] actualResult = database.Fetch();

        Assert.AreEqual(data, actualResult);
    }

    [Test]
    public void AddMethodShouldThrowExeptionWhenCountIsMoreThen16()
    {
        for (int i = 0; i < 14; i++)
        {
            database.Add(i);
        }
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(
            () => database.Add(0));

        Assert.AreEqual("Array's capacity must be exactly 16 integers!", exception.Message);
    }

    [Test]
    public void DatabaseRemoveMethodShouldDecreaseCount()
    {
        int expectedResult = 1;
        database.Remove();

        Assert.AreEqual(expectedResult, database.Count);
    }

    [Test]
    public void DatabaseRemoveMethodShouldThrowExeptionWhenDataBaseIsEmpty()
    {
        database.Remove();
        database.Remove();

        InvalidOperationException exeption = Assert.Throws<InvalidOperationException>(() => database.Remove());

        Assert.AreEqual("The collection is empty!", exeption.Message);
    }

    [TestCase(new int[] {1, 2, 3})]
    public void FetchMethodShouldReturnCorectData(int[] data)
    {
        database = new Database(data);
        int[] actualResult = database.Fetch();

        Assert.AreEqual(data, actualResult);
    }
}
