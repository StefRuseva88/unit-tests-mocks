using NUnit.Framework;
using Moq;
using ItemManagementApp.Services;
using ItemManagementLib.Repositories;
using ItemManagementLib.Models;
using System.Collections.Generic;
using System.Linq;

namespace ItemManagement.Tests
{
    [TestFixture]
    public class ItemServiceTests
    {

        private ItemService _itemService;
        private Mock<IItemRepository> _mockItemRepository;

        [SetUp]
        public void Setup()
        {
            _mockItemRepository = new Mock<IItemRepository>();
            
            _itemService = new ItemService(_mockItemRepository.Object);
            
        }

        [Test]
        public void AddItem_ShouldCallAddItemOnRepository()
        {
            // Arrange
            var item = new Item { Name = "test Item" };
            _mockItemRepository.Setup(x => x.AddItem(It.IsAny<Item>()));

            // Act
            _itemService.AddItem(item.Name);

            // Assert
            _mockItemRepository.Verify(x => x.AddItem(It.IsAny<Item>()), Times.Once);

        }


        [Test]
        public void AddItem_ShouldThrowAnError_IfNameIsInvalid()
        {
            // Arrange
            string invalidName = "";
            _mockItemRepository
                .Setup(x => x.AddItem(It.IsAny<Item>()))
                .Throws<ArgumentException>();

            // Act&Assert
            Assert.Throws<ArgumentException>(() => _itemService.AddItem(invalidName));
            _mockItemRepository.Verify(x => x.AddItem(It.IsAny<Item>()), Times.Once);

        }

        [Test]
        public void GetAllItems_ShouldReturnAllItems()
        {
            // Arrange
            var items = new List<Item>
            {
               new Item { Id = 1, Name = "Item 1" },
               new Item { Id = 2, Name = "Item 2" },
               new Item { Id = 3, Name = "Item 3" }
            };

            _mockItemRepository.Setup(x => x.GetAllItems()).Returns(items);

            // Act
            var result = _itemService.GetAllItems();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(items.Count));
            foreach (var item in items)
            {
                Assert.IsTrue(result.Any(x => x.Id == item.Id && x.Name == item.Name));
            }
        }

        [Test]
        public void UpdateItem_ShouldCallUpdateItemOnRepository()
        {
            // Arrange
            int existingItemId = 1;
            string newName = "New Name";
            var existingItem = new Item { Id = existingItemId, Name = "Old Name" };
            _mockItemRepository.Setup(x => x.GetItemById(existingItemId)).Returns(existingItem);
            _mockItemRepository.Setup(x => x.UpdateItem(It.IsAny<Item>()));

            // Act
            _itemService.UpdateItem(existingItemId, newName);

            // Assert
            _mockItemRepository.Verify(x => x.GetItemById(existingItemId), Times.Once);
            _mockItemRepository.Verify(x => x.UpdateItem(It.IsAny<Item>()), Times.Once);
        }

        [Test]
        public void DeleteItem_ShouldCallDeleteItemOnRepository()
        {
            // Arrange
            int itemIdToDelete = 1;
            _mockItemRepository.Setup(x => x.DeleteItem(It.IsAny<int>()));

            // Act
            _itemService.DeleteItem(itemIdToDelete);

            // Assert
            _mockItemRepository.Verify(x => x.DeleteItem(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ValidateItemName_WhenNameIsValid_ShouldReturnTrue()
        {
            // Arrange
            string validName = "Valid Name";

            // Act
            bool isValid = _itemService.ValidateItemName(validName);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void ValidateItemName_WhenNameIsTooLong_ShouldReturnFalse()
        {
            // Arrange
            string longName = "ThisIsAVeryLongItemNameThatExceedsTheMaximumAllowedLength";

            // Act
            bool isValid = _itemService.ValidateItemName(longName);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void ValidateItemName_WhenNameIsEmpty_ShouldReturnFalse()
        {
            // Arrange
            string emptyName = "";

            // Act
            bool isValid = _itemService.ValidateItemName(emptyName);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void UpdateItem_ShouldNotCallUpdateItem_IfTheItemIsNonExisting()
        {
            // Arrange
            var nonExistingId = 1;
            _mockItemRepository.Setup(x => x.GetItemById(nonExistingId)).Returns<Item>(null);
            _mockItemRepository.Setup(x => x.UpdateItem(It.IsAny<Item>()));

            // Act

            _itemService.UpdateItem(nonExistingId, "New Name");
            // Assert
            _mockItemRepository.Verify(x => x.GetItemById(nonExistingId), Times.Once());
            _mockItemRepository.Verify(x => x.UpdateItem(It.IsAny<Item>()), Times.Never());
        }
    }
}