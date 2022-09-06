using Cobros.API.Entities;
using FluentAssertions;

namespace Cobros.Test.Entities
{
    public class EntityTests
    {
        [Fact]
        public void IsDeleted_IsFalse_WhenDeletedAtIsNull()
        {
            // Arrange
            var entity = new Entity();

            // Act
            var sut = entity.IsDeleted;

            // Assert
            sut.Should().BeFalse();
        }        
        
        [Fact]
        public void IsDeleted_IsTrue_WhenDeletedAtIsADate()
        {
            // Arrange
            var entity = new Entity();
            entity.DeletedAt = DateTime.UtcNow;

            // Act
            var sut = entity.IsDeleted;

            // Assert
            sut.Should().BeTrue();
        }
    }
}
