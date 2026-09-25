using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RestaurantPos.Models
{
    public class CartItem : INotifyPropertyChanged
    {
        public Product Product { get; set; } = new Product();

        private int _quantity;

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;

                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(Subtotal));
            }
        }

        public decimal Subtotal
        {
            get
            {
                return Product.Price * Quantity;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
