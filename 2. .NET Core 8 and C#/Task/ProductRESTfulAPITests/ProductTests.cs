using ProductRESTfulAPI.Models;

namespace ProductRESTfulAPITests
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void Create_WithValidParameters_ShouldSucceed()
        {
            // Arrange
            string name = "product1";
            decimal price = 100;

            // Act
            var result = Product.Create(name, price);

            // Assert
            var product = result.Value;
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.True);
                Assert.That(product.Name, Is.EqualTo(name));
                Assert.That(product.Price, Is.EqualTo(price));
            });
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCaseSource(nameof(TooLongNameCase))]
        public void Create_WithInvalidName_ShouldFail(string? invalidName)
        {
            // Arrange
            decimal price = 100;

            // Act
            var result = Product.Create(invalidName, price);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
        }

        [TestCase(-999)]
        [TestCase(0)]
        public void Create_WithInvalidPrice_ShouldFail(decimal invalidValue)
        {
            // Arrange
            string name = "product1";

            // Act
            var result = Product.Create(name, invalidValue);

            // Assert
            Assert.That(result.IsSuccess, Is.False);
        }

 
        private static IEnumerable<TestCaseData> TooLongNameCase()
        {
            yield return new TestCaseData(new string('A', Product.MaxNameLength + 1)).SetName(
                "Create_WithNameTooLong_ShouldFail"
            );
        }
    }
}