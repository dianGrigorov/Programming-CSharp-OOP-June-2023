namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database database;
        [SetUp]
        public void SetUp()
        {
            Person[] persons =
            {
                 new Person(1, "Dido"),
                 new Person(2, "Pesho"),
                 new Person(3, "Gosho"),
                 new Person(4, "Tosho"),
                 new Person(5, "Sasho"),
                 new Person(6, "Mitko"),
                 new Person(7, "Pavel"),
                 new Person(8, "Rado"),
                 new Person(9, "Asen"),
                 new Person(10, "Teodor"),
                 new Person(11, "Svetlio")
            };

            database = new Database(persons);
        }

        [Test]
        public void CreatingDataBaseCountShouldBeCorect()
        {
            int expectedResult = 11;

            Assert.AreEqual(expectedResult, database.Count);
        }

        [Test]
        public void DatabaseCapacityShouldBeNotMoreThen16()
        {
            Person[] persons =
            {
                 new Person(1, "Dido"),
                 new Person(2, "Pesho"),
                 new Person(3, "Gosho"),
                 new Person(4, "Tosho"),
                 new Person(5, "Sasho"),
                 new Person(6, "Mitko"),
                 new Person(7, "Pavel"),
                 new Person(8, "Rado"),
                 new Person(9, "Asen"),
                 new Person(10, "Teodor"),
                 new Person(11, "Jon"),
                 new Person(12, "Jim"),
                 new Person(13, "Jo"),
                 new Person(14, "Ace"),
                 new Person(15, "Jin"),
                 new Person(16, "Jinx"),
                 new Person(17, "Axe"),
            };
            ArgumentException exeption = Assert.Throws<ArgumentException>(
                () => database = new Database(persons));

            Assert.AreEqual("Provided data length should be in range [0..16]!", exeption.Message);
        }

        [Test]
        public void AddMethodShouldIncreaceCount()
        {
            Person person = new Person(12, "Stosho");
            database.Add(person);
            int expectedResult = 12;
            int actualResult = database.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void AddMethodShouldWorkCorrectly()
        {
            Person person = new Person(12, "Stosho");
            database.Add(person);
            int expectedResult = 12;
            int actualResult = database.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void AddMethodhouldThrowExceptionIfCountIsBigerThen16()
        {
            Person person1 = new Person(12, "Jim");
            Person person2 = new Person(13, "Jo");
            Person person3 = new Person(14, "Ace");
            Person person4 = new Person(15, "Jin");
            Person person5 = new Person(16, "Jinx");

            database.Add(person1);
            database.Add(person2);
            database.Add(person3);
            database.Add(person4);
            database.Add(person5);

            InvalidOperationException exeption = Assert.Throws<InvalidOperationException>(() => database.Add(new Person(22, "Axe")));

            Assert.AreEqual("Array's capacity must be exactly 16 integers!", exeption.Message);
        }

        [Test]
        public void DatabaseShouldThrowExceptionIfPersonWhithSameNameIsAdded()
        {
            Person person = new(1, "Dido");

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => database.Add(person));

            Assert.AreEqual("There is already user with this username!", ex.Message);
        }
        [Test]
        public void DatabaseShouldThrowExceptionIfPersonWhithSameIDIsAdded()
        {
            Person person = new(1, "axe");

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => database.Add(person));

            Assert.AreEqual("There is already user with this Id!", ex.Message);
        }
        [Test]
        public void RemoveMethodShouldWorkProperly()
        {
            int expectedResult = 10;
            database.Remove();
            int actualResult = database.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void RemoveMethodShouldThrowExceptionIfDatabaseIsEmpty()
        {
            Database database = new Database();

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void DataBaseFindByUsernameShouldWorkProperly()
        {
            string expectedResult = "Dido";
            string actualResult = database.FindByUsername("Dido").UserName;

            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestCase("")]
        [TestCase(null)]
        public void DatabaseFindByUsernameThrowExceprionIfUsernameIsNullOrWithespace(string name)
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => database.FindByUsername(name));

            Assert.AreEqual("Username parameter is null!", ex.ParamName);
        }
        [TestCase("Ivan")]
        [TestCase("Ivar")]
        public void DatabaseFindByUsernameMethodShouldThrowExceptionIfUsernameIsNotFound(string username)
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
            => database.FindByUsername(username));

            Assert.AreEqual("No user is present by this username!", ex.Message);
        }
        [Test]
        public void DataBaseFindByIdShouldWorkProperly()
        {
            string expectedResult = "Dido";
            string actualResult = database.FindById(1).UserName;

            Assert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void DatabaseFindByIdMethodShouldThrowExceptionIfIdIsNegative()
        {
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(-1));

            Assert.AreEqual("Id should be a positive number!", ex.ParamName);
        }
        [Test]
        public void DatabaseFindByIdShouldThrowExceptionIfIdIsNotFound()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(
                () => database.FindById(100));

            Assert.AreEqual("No user is present by this ID!", ex.Message);
        }
    }
}