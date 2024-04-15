using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Services;

namespace Test
{
    [TestClass]
    public class PasswordHasherTest
    {
        private readonly PasswordHasher _passwordHasher;

        public PasswordHasherTest()
        {
            _passwordHasher = new PasswordHasher();
        }

        [TestMethod]
        public void HashPassword_ValidPassword_ReturnsNonEmptyString()
        {
            // Arrange
            string password = "password";

            // Act
            string hashedPassword = _passwordHasher.HashPassword(password);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(hashedPassword));
        }

        [TestMethod]
        public void VerifyPassword_CorrectPassword_ReturnsTrue()
        {
            // Arrange
            string password = "password";
            string hashedPassword = _passwordHasher.HashPassword(password);

            // Act
            bool isPasswordValid = _passwordHasher.VerifyPassword(password, hashedPassword);

            // Assert
            Assert.IsTrue(isPasswordValid);
        }

        [TestMethod]
        public void VerifyPassword_IncorrectPassword_ReturnsFalse()
        {
            // Arrange
            string password = "password";
            string incorrectPassword = "incorrect";
            string hashedPassword = _passwordHasher.HashPassword(password);

            // Act
            bool isPasswordValid = _passwordHasher.VerifyPassword(incorrectPassword, hashedPassword);

            // Assert
            Assert.IsFalse(isPasswordValid);
        }
    }
}