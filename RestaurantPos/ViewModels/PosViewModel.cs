using RestaurantPos.Commands;
using RestaurantPos.Commands.RestaurantPos.Commands;
using RestaurantPos.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace RestaurantPos.ViewModels
{
    public class PosViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Product> Products { get; set; }

        public ObservableCollection<CartItem> CartItems { get; set; }

        public decimal Total
        {
            get
            {
                return CartItems.Sum(item => item.Subtotal);
            }
        }

        public PosViewModel()
        {
            Products = new ObservableCollection<Product>();

            CartItems = new ObservableCollection<CartItem>();

            AddToCartCommand = new RelayCommand(parameter =>
        {
            Product product = (Product)parameter!;
            AddToCart(product);
        });

            Products.Add(new Product
            {
                Name = "Nasi Lemak",
                Price = 8.00m
            });

            Products.Add(new Product
            {
                Name = "Mee Goreng",
                Price = 7.00m
            });

            Products.Add(new Product
            {
                Name = "Teh Tarik",
                Price = 3.00m
            });

            Products.Add(new Product
            {
                Name = "Coffee",
                Price = 4.00m
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        public void AddToCart(Product product)
        {
            CartItem? existingItem = CartItems.FirstOrDefault(
                item => item.Product == product);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                CartItems.Add(new CartItem
                {
                    Product = product,
                    Quantity = 1
                });
            }

            OnPropertyChanged(nameof(Total));
        }

        public RelayCommand AddToCartCommand { get; }
    }
}
