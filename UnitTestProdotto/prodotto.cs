using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProdotto
{
    public class Prodotto
    {
        public int Id { get; set; }
        public string Descrizione { get; set; }
        public decimal Prezzo { get; set; }
        public int Quantita { get; set; }
    }

    public class Metodo
    {
        private List<Prodotto> eleprodotti = new List<Prodotto>();
        private int nextId = 1;

        public Prodotto Create(Prodotto product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            product.Id = nextId++;
            eleprodotti.Add(product);
            return product;
        }

        public Prodotto GetById(int id)
        {
            return eleprodotti.FirstOrDefault(p => p.Id == id);
        }

        public List<Prodotto> GetAll()
        {
            return eleprodotti;
        }

        public Prodotto Update(Prodotto product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var existingProdotto = eleprodotti.FirstOrDefault(p => p.Id == product.Id);
            if (existingProdotto == null)
            {
                throw new InvalidOperationException($"Prodotto with ID {product.Id} not found");
            }

            existingProdotto.Descrizione = product.Descrizione;
            existingProdotto.Prezzo = product.Prezzo;
            existingProdotto.Quantita = product.Quantita;
            return existingProdotto;
        }

        public void Delete(int id)
        {
            var productToDelete = eleprodotti.FirstOrDefault(p => p.Id == id);
            if (productToDelete != null)
            {
                eleprodotti.Remove(productToDelete);
            }
        }
    }

}
