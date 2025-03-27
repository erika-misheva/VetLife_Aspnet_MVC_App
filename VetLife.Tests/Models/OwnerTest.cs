using Microsoft.VisualStudio.TestTools.UnitTesting;
using VetLife.Models;

namespace VetLife.Tests.Models
{
    [TestClass]
    public sealed class OwnerTest
    {
        [TestMethod]
        public void GetFullName_ShouldReturnFullName_WhenNameAndSurnameAreProvided()
        {
            var owner = new Owner
            {
                Name = "John",
                Surname = "Doe"
            };

            var fullName = owner.GetFullName();

            Assert.AreEqual("John Doe", fullName);
        }

        [TestMethod]
        public void GetFullName_ShouldReturnEmpty_WhenNameAndSurnameAreEmpty()
        {
            var owner = new Owner
            {
                Name = "",
                Surname = ""
            };

            var fullName = owner.GetFullName();

            Assert.AreEqual(" ", fullName);
        }

        [TestMethod]
        public void GetFullName_ShouldReturnFullName_WhenNameIsProvidedAndSurnameIsEmpty()
        {

            var owner = new Owner
            {
                Name = "John",
                Surname = ""
            };

            var fullName = owner.GetFullName();

            Assert.AreEqual("John ", fullName);
        }

        [TestMethod]
        public void GetFullName_ShouldReturnFullName_WhenSurnameIsProvidedAndNameIsEmpty()
        {

            var owner = new Owner
            {
                Name = "",
                Surname = "Doe"
            };


            var fullName = owner.GetFullName();


            Assert.AreEqual(" Doe", fullName);
        }
    }
}
