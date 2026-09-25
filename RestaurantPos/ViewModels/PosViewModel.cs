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

            IncreaseQuantityCommand = new RelayCommand(
                parameter =>
                {
                    CartItem item = (CartItem)parameter!;
                    IncreaseQuantity(item);
                });

            DecreaseQuantityCommand = new RelayCommand(
                parameter =>
                {
                    CartItem item = (CartItem)parameter!;
                    DecreaseQuantity(item);
                });

            RemoveFromCartCommand = new RelayCommand(
                parameter =>
                {
                    CartItem item = (CartItem)parameter!;
                    RemoveFromCart(item);
                });
            CheckoutCommand = new RelayCommand(
                parameter =>
                {
                    Checkout();
                },
                parameter =>
                {
                    return CartItems.Count > 0;
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

            CheckoutCommand.RaiseCanExecuteChanged();
        }

        public void IncreaseQuantity(CartItem item)
        {
            item.Quantity++;

            OnPropertyChanged(nameof(Total));
        }

        public void DecreaseQuantity(CartItem item)
        {
            if (item.Quantity > 1)
            {
                item.Quantity--;

                OnPropertyChanged(nameof(Total));
            }
        }

        public void RemoveFromCart(CartItem item)
        {
            CartItems.Remove(item);

            OnPropertyChanged(nameof(Total));

            CheckoutCommand.RaiseCanExecuteChanged();
        }

        public void Checkout()
        {
            // Temporary test
        }

        public RelayCommand AddToCartCommand { get; }

        public RelayCommand IncreaseQuantityCommand { get; }

        public RelayCommand DecreaseQuantityCommand { get; }

        public RelayCommand RemoveFromCartCommand { get; }

        public RelayCommand CheckoutCommand { get; }
    }
}
