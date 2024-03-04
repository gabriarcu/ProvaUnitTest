using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProdotto
{
    [TestClass] // contiene metodi di test.
    public class ProductRepositoryTests
    {
        private Metodo productRepository;

        [TestInitialize] // è un metodo di inizializzazione che viene eseguito prima di ogni metodo di
                         // test.
                         // Qui, stiamo istanziando una nuova istanza di ProductRepository in modo che
                         // ogni test abbia una nuova istanza pulita da testare.
        public void Setup()
        {
            productRepository = new Metodo();
        }

        [TestMethod] // contiene i metodi da eseguire
        public void Create_AddsProductToList()
        {
            // Arrange - prepariamo le condizioni per il test, come la creazione di un prodotto
            var product = new Prodotto { Descrizione = "Laptop", Prezzo = 999.99m, Quantita = 10 };

            // Act - chiamiamo i metodi del ProductRepository che vogliamo testare
            var createdProduct = productRepository.Create(product);

            // Assert verifichiamo che il risultato ottenuto dal metodo corrisponda al risultato atteso
            Assert.IsNotNull(createdProduct);
            Assert.AreEqual(product.Descrizione, createdProduct.Descrizione);
            Assert.AreEqual(product.Prezzo, createdProduct.Prezzo);
            Assert.AreEqual(product.Quantita, createdProduct.Quantita);
            Assert.IsTrue(createdProduct.Id > 0);
        }

        [TestMethod]
        public void GetById_ReturnsProduct()
        {
            // Arrange
            var product = new Prodotto { Descrizione = "Smartphone", Prezzo = 499.99m, Quantita = 20 };
            var createdProduct = productRepository.Create(product);

            // Act
            var retrievedProduct = productRepository.GetById(createdProduct.Id);

            // Assert
            Assert.IsNotNull(retrievedProduct);
            Assert.AreEqual(createdProduct.Descrizione, retrievedProduct.Descrizione);
            Assert.AreEqual(createdProduct.Prezzo, retrievedProduct.Prezzo);
            Assert.AreEqual(createdProduct.Quantita, retrievedProduct.Quantita);
        }

        [TestMethod]
        public void GetAll_ReturnsAllProducts()
        {
            // Arrange
            var product1 = new Prodotto { Descrizione = "Keyboard", Prezzo = 49.99m, Quantita = 30 };
            var product2 = new Prodotto { Descrizione = "Mouse", Prezzo = 19.99m, Quantita = 50 };
            productRepository.Create(product1);
            productRepository.Create(product2);

            // Act
            var allProducts = productRepository.GetAll();

            // Assert
            Assert.IsNotNull(allProducts);
            Assert.AreEqual(2, allProducts.Count);
            CollectionAssert.Contains(allProducts, product1);
            CollectionAssert.Contains(allProducts, product2);
        }

        [TestMethod]
        public void Update_ModifiesProduct()
        {
            // Arrange
            var product = new Prodotto { Descrizione = "Headphones", Prezzo = 79.99m, Quantita = 15 };
            var createdProduct = productRepository.Create(product);
            var updatedProduct = new Prodotto { Id = createdProduct.Id, Descrizione = "Wireless Headphones", Prezzo = 99.99m, Quantita = 20 };

            // Act
            var modifiedProduct = productRepository.Update(updatedProduct);

            // Assert
            Assert.IsNotNull(modifiedProduct);
            Assert.AreEqual(updatedProduct.Descrizione, modifiedProduct.Descrizione);
            Assert.AreEqual(updatedProduct.Prezzo, modifiedProduct.Prezzo);
            Assert.AreEqual(updatedProduct.Quantita, modifiedProduct.Quantita);
        }

        [TestMethod]
        public void Delete_RemovesProduct()
        {
            // Arrange
            var product = new Prodotto { Descrizione = "Monitor", Prezzo = 199.99m, Quantita = 5 };
            var createdProduct = productRepository.Create(product);

            // Act
            productRepository.Delete(createdProduct.Id);
            var deletedProduct = productRepository.GetById(createdProduct.Id);

            // Assert
            Assert.IsNull(deletedProduct);
        }

        [TestMethod]
        public void Delete_NonExistingProduct_DoesNothing()
        {
            // Arrange
            var product = new Prodotto { Descrizione = "Speaker", Prezzo = 39.99m, Quantita = 10 };
            var createdProduct = productRepository.Create(product);

            // Act
            productRepository.Delete(createdProduct.Id + 1); // Deleting a non-existing product ID

            // Assert
            var retrievedProduct = productRepository.GetById(createdProduct.Id);
            Assert.IsNotNull(retrievedProduct);
        }
    }
}
