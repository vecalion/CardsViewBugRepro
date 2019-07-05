using System;
using System.Collections.Generic;
using MvvmHelpers;
using Xamarin.Forms;

namespace cardsbug.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public List<Card> Cards { get; set; }

        int _position;
        public int Position
        {
            get => _position;
            set
            {
                SetProperty(ref _position, value);
                Cards[Position].Messages.Clear();
            }
        }

        Command _nextCommand;
        public Command NextCommand => _nextCommand = _nextCommand ?? new Command(DoNext);

        Command _addCommand;
        public Command AddCommand => _addCommand = _addCommand ?? new Command(DoAdd);

        public MainViewModel()
        {
            Cards = new List<Card>
            {
                new Card(1, CardTypes.Type1),
                new Card(2, CardTypes.Type2),
                new Card(3, CardTypes.Type3),
                new Card(4, CardTypes.Type1),
                new Card(5, CardTypes.Type2),
                new Card(6, CardTypes.Type3),
                new Card(7, CardTypes.Type1),
                new Card(8, CardTypes.Type2),
                new Card(9, CardTypes.Type3),
                new Card(10, CardTypes.Type1),
            };
        }

        void DoNext()
        {
            Position = (Position + 1) % 10;
        }

        void DoAdd()
        {
            Cards[Position].Messages.Add(new Item(Cards[Position]) { Message = "Test " + (Position + 1) });
        }
    }

    public class Card : ObservableObject
    {
        public ObservableRangeCollection<Item> Messages { get; set; }

        public Card(int seq, CardTypes cardType)
        {
            Messages = new ObservableRangeCollection<Item>()
            {
                new Item(this) { Message = $"{seq} - 1"},
                new Item(this) { Message = $"{seq} - 2"},
                new Item(this) { Message = $"{seq} - 3"},
                new Item(this) { Message = $"{seq} - 4"},
            };

            CardType = cardType;
        }

        public void DeleteItem(Item item)
        {
            Messages.Remove(item);
        }

        public CardTypes CardType { get; set; }
    }

    public enum CardTypes
    {
        Type1, Type2, Type3
    }


    public class Item
    {
        private Command _deleteCommand;
        private readonly Card card;

        public Item(Card card)
        {
            this.card = card;
        }

        public string Message { get; set; }

        public Command DeleteCommand => _deleteCommand = _deleteCommand ?? new Command(DoDelete);

        void DoDelete()
        {
            card.DeleteItem(this);
        }
    }
}
