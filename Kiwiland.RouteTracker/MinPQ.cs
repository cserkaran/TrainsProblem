using System;
using System.Collections.Generic;

namespace Kiwiland.RouteTracker
{

    /// <summary>
    /// Array based Min Priority Queue implementation based on min heap.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MinPQ<T> where T : IComparable<T>
    {
        List<T> _items = new List<T>();

        public void Insert(T key)
        {
            _items.Add(key);
            SiftUp(_items.Count - 1);
        }

        public T DelMin()
        {
            var top = _items[0];

            _items[0] = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);

            MinHeapify(_items, 0, _items.Count - 1);

            return top;
        }

        public bool IsEmpty()
        {
            return Size() == 0;
        }

        public T Min()
        {
            return _items[0];
        }

        public bool Contains(T key)
        {
            return _items.Contains(key);
        }

        public int Size()
        {
            return _items.Count;
        }

        public void ChangeKey(T oldKey,T newKey)
        {
            if (_items.Contains(oldKey))
            {
                var index = _items.IndexOf(oldKey);
                _items[index] = newKey;
                MinHeapify(_items, 0, _items.Count - 1);
            }
        }

        private void SiftUp(int index)
        {
            if (index > 0)
            {
                if (_items[index / 2].CompareTo(this._items[index]) == 1)
                {
                    var temp = this._items[index / 2];
                    _items[index / 2] = this._items[index];
                    _items[index] = temp;
                    SiftUp(index / 2);
                }
            }

        }

        private void MinHeapify(List<T> arr, int index, int size)
        {
            var left = 2 * index;
            var right = 2 * index + 1;

            if (left <= size && arr[index].CompareTo(arr[left]) == 1)
            {
                var temp = arr[index];
                arr[index] = arr[left];
                arr[left] = temp;
                MinHeapify(arr, left, size);
            }

            if (right <= size && arr[index].CompareTo(arr[right]) == 1)
            {
                var temp = arr[index];
                arr[index] = arr[right];
                arr[right] = temp;
                MinHeapify(arr, right, size);
            }
        }

    }
}
